#region LGPL Header
// Copyright (C) 2008, Jackie Ng
// http://code.google.com/p/fdotoolbox, jumpinjackie@gmail.com
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FdoToolbox.Core;
using OSGeo.FDO.Connections;
using OSGeo.FDO.Schema;
using System.IO;
using OSGeo.FDO.Commands.Schema;
using FdoToolbox.Core.ClientServices;
using OSGeo.FDO.Common;
using OSGeo.FDO.Commands.Feature;
using OSGeo.FDO.Commands;
using OSGeo.FDO.Expression;
using OSGeo.FDO.Geometry;

namespace FdoToolbox.Tests
{
    // Tests features of the FDO API that may not be covered or adequately covered
    // by the FDO test suite. Consider this a testbed for features
    // of FDO that we want to use in FdoToolbox.
    //
    // Do not expect these tests to pass. If any do pass, those APIs being tested 
    // are suitable for consumption, and the affected tests will be moved to the
    // FeatureService test fixture.
    //
    // Otherwise they simply expose issues of the underlying FDO API that need to be
    // addressed asap.

    [Category("RawFdo")]
    [TestFixture]
    [Explicit]
    public class FdoExtendedTests : BaseTest
    {
        [Test]
        public void TestPropertyDeleteRaw()
        {
            string file = "Test1.sdf";
            IConnection conn = null;
            try
            {
                bool result = ExpressUtility.CreateSDF(file);
                Assert.IsTrue(result);

                conn = ExpressUtility.CreateSDFConnection(file, false);
                conn.Open();
                Assert.IsTrue(conn.ConnectionState == ConnectionState.ConnectionState_Open, "Could not open SDF connection");

                AppConsole.WriteLine("Describing");

                IDescribeSchema describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                FeatureSchemaCollection schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }
                Assert.IsTrue(schemas != null, "Should be zero-size, not a null object");
                Assert.IsTrue(schemas.Count == 0, "Expected empty SDF file");

                AppConsole.WriteLine("Creating class: Class1");

                //Create one
                FeatureSchema schema = new FeatureSchema("Test", "Test Schema");
                FeatureClass c1 = new FeatureClass("Class1", "");
                DataPropertyDefinition idP1 = new DataPropertyDefinition("ID", "");
                idP1.DataType = DataType.DataType_Int32;
                idP1.IsAutoGenerated = true;
                c1.Properties.Add(idP1);
                c1.IdentityProperties.Add(idP1);

                DataPropertyDefinition fooP = new DataPropertyDefinition("Foo", "");
                fooP.DataType = DataType.DataType_String;
                fooP.Length = 20;
                fooP.Nullable = true;
                c1.Properties.Add(fooP);

                GeometricPropertyDefinition geomP1 = new GeometricPropertyDefinition("Geometry", "");
                geomP1.GeometryTypes = (int)GeometryType.GeometryType_Polygon;

                c1.Properties.Add(geomP1);
                c1.GeometryProperty = geomP1;

                schema.Classes.Add(c1);

                AppConsole.WriteLine("Applying schema");

                //Apply new class
                try
                {
                    using (IApplySchema apply = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_ApplySchema) as IApplySchema)
                    {
                        apply.FeatureSchema = schema;
                        apply.Execute();
                        conn.Flush();
                    }
                }
                catch
                {
                    Assert.Fail("Could not apply schema");
                }

                AppConsole.WriteLine("Re-describe");

                //re-describe
                describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }
                Assert.IsTrue(schemas != null, "Expected non-null object");
                Assert.IsTrue(schemas.Count == 1, "Expected 1 schema");
                Assert.IsTrue(schemas[0].Classes.Count == 1, "Expected 2 classes in schema");
                Assert.IsTrue(schemas[0].Classes[0].Properties.Count == 3, "Expected 3 properties");

                //Now remove the property "Foo"
                AppConsole.WriteLine("Removing Property: Foo");
                ClassDefinition cdef = schemas[0].Classes[0];
                int idx = cdef.Properties.IndexOf("Foo");
                Assert.IsTrue(idx >= 0, "Expected property Foo to be there for us to delete");

                cdef.Properties.RemoveAt(idx);

                //Apply changes
                try
                {
                    using (IApplySchema apply = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_ApplySchema) as IApplySchema)
                    {
                        apply.FeatureSchema = cdef.FeatureSchema;
                        apply.Execute();
                    }
                }
                catch
                {
                    Assert.Fail("Could not apply schema");
                }

                AppConsole.WriteLine("Can flush: {0}", conn.ConnectionCapabilities.SupportsFlush());
                conn.Flush();

                AppConsole.WriteLine("Re-describing");
                //re-describe
                describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }
                Assert.IsTrue(schemas != null, "Expected non-null object");
                Assert.IsTrue(schemas.Count == 1, "Expected 1 schema");
                Assert.IsTrue(schemas[0].Classes.Count == 1, "Expected 2 classes in schema");

                Assert.IsTrue(schemas[0].Classes[0].Properties.Count == 2, "Expected 2 properties");
                cdef = schemas[0].Classes[0];
                idx = cdef.Properties.IndexOf("Foo");
                Assert.IsTrue(idx < 0, "Property Foo was not deleted");
            }
            finally
            {
                AppConsole.WriteLine("Cleaning up");
                if (conn != null)
                {
                    if (conn.ConnectionState == ConnectionState.ConnectionState_Open)
                        conn.Close();
                    conn.Dispose();
                }
                if (File.Exists(file))
                    File.Delete(file);
            }
        }

        [Test]
        public void TestClassDeleteRaw()
        {
            string file = "Test2.sdf";
            IConnection conn = null;
            try
            {
                bool result = ExpressUtility.CreateSDF(file);
                Assert.IsTrue(result);

                conn = ExpressUtility.CreateSDFConnection(file, false);
                conn.Open();
                Assert.IsTrue(conn.ConnectionState == ConnectionState.ConnectionState_Open, "Could not open SDF connection");

                AppConsole.WriteLine("Describing");
                IDescribeSchema describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                FeatureSchemaCollection schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }
                Assert.IsTrue(schemas != null, "Should be zero-size, not a null object");
                Assert.IsTrue(schemas.Count == 0, "Expected empty SDF file");

                //Create one
                AppConsole.WriteLine("Creating class: Class1");
                FeatureSchema schema = new FeatureSchema("Test", "Test Schema");
                FeatureClass c1 = new FeatureClass("Class1", "");
                DataPropertyDefinition idP1 = new DataPropertyDefinition("ID", "");
                idP1.DataType = DataType.DataType_Int32;
                idP1.IsAutoGenerated = true;
                c1.Properties.Add(idP1);
                c1.IdentityProperties.Add(idP1);

                GeometricPropertyDefinition geomP1 = new GeometricPropertyDefinition("Geometry", "");
                geomP1.GeometryTypes = (int)GeometryType.GeometryType_Polygon;

                c1.Properties.Add(geomP1);
                c1.GeometryProperty = geomP1;

                schema.Classes.Add(c1);
                AppConsole.WriteLine("Creating class: Class2");
                FeatureClass c2 = new FeatureClass("Class2", "");
                DataPropertyDefinition idP2 = new DataPropertyDefinition("ID", "");
                idP2.DataType = DataType.DataType_Int32;
                idP2.IsAutoGenerated = true;
                c2.Properties.Add(idP2);
                c2.IdentityProperties.Add(idP2);

                GeometricPropertyDefinition geomP2 = new GeometricPropertyDefinition("Geometry", "");
                geomP2.GeometryTypes = (int)GeometryType.GeometryType_Polygon;

                c2.Properties.Add(geomP2);
                c2.GeometryProperty = geomP2;

                schema.Classes.Add(c2);

                AppConsole.WriteLine("Applying schema");
                try
                {
                    using (IApplySchema apply = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_ApplySchema) as IApplySchema)
                    {
                        apply.FeatureSchema = schema;
                        apply.Execute();
                    }
                }
                catch
                {
                    Assert.Fail("Could not apply schema");
                }

                AppConsole.WriteLine("Re-describing");
                describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }
                Assert.IsTrue(schemas != null, "Expected non-null object");
                Assert.IsTrue(schemas.Count == 1, "Expected 1 schema");
                Assert.IsTrue(schemas[0].Classes.Count == 2, "Expected 2 classes in schema");

                //Now remove Class2
                AppConsole.WriteLine("Removing class: Class2");
                int idx = schemas[0].Classes.IndexOf("Class2");
                Assert.IsTrue(idx >= 0, "Could not find Class2 in schema Test");
                ClassDefinition cdef = schemas[0].Classes[idx];

                schema = cdef.FeatureSchema;
                schema.Classes.Remove(cdef);

                Assert.IsTrue(cdef.ElementState == SchemaElementState.SchemaElementState_Detached, "Class2 is not detached");
                AppConsole.WriteLine("Applying schema");
                try
                {
                    using (IApplySchema apply = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_ApplySchema) as IApplySchema)
                    {
                        apply.FeatureSchema = schema;
                        apply.Execute();
                        conn.Flush();
                    }
                }
                catch
                {
                    Assert.Fail("Could not apply modified schema");
                }

                Assert.IsTrue(schemas != null, "Expected non-null object");
                Assert.IsTrue(schemas.Count == 1, "Expected 1 schema");

                idx = schema.Classes.IndexOf("Class2");
                Assert.IsTrue(idx < 0, "Class2 was not deleted");

                //re-describe
                AppConsole.WriteLine("Re-describe");
                describe = conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_DescribeSchema) as IDescribeSchema;
                schemas = null;
                using (describe)
                {
                    schemas = describe.Execute();
                }

                Assert.IsTrue(schemas != null, "Expected non-null object");
                Assert.IsTrue(schemas.Count == 1, "Expected 1 schema");
                Assert.IsTrue(schemas[0].Classes.Count == 1, "Expected 1 class (Class1) in schema");
                idx = schemas[0].Classes.IndexOf("Class2");
                Assert.IsTrue(idx < 0, "Class2 was not deleted");
            }
            finally
            {
                AppConsole.WriteLine("Cleaning up");
                if (conn != null)
                {
                    if (conn.ConnectionState == ConnectionState.ConnectionState_Open)
                        conn.Close();
                    conn.Dispose();
                }
                if (File.Exists(file))
                    File.Delete(file);
            }
        }
    }
}
