#region LGPL Header
// Copyright (C) 2009, Jackie Ng
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
//
// See license.txt for more/additional licensing information
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using FdoToolbox.Core.ETL.Operations;
using FdoToolbox.Core.Feature;
using System.Collections.Specialized;
using OSGeo.FDO.Schema;
using FdoToolbox.Core.Utility;
using System.Collections.ObjectModel;

namespace FdoToolbox.Core.ETL.Specialized
{
    /// <summary>
    /// A specialized ETL process that copies data from one features class to another
    /// </summary>
    public class FdoClassToClassCopyProcess : FdoSpecializedEtlProcess
    {
        private FdoClassCopyOptions _options;

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public FdoClassCopyOptions Options
        {
            get { return _options; }
            private set { _options = value; }
        }

        private int _ReportFrequency;

        /// <summary>
        /// Gets or sets the report frequency.
        /// </summary>
        /// <value>The report frequency.</value>
        public int ReportFrequency
        {
            get { return _ReportFrequency; }
            set { _ReportFrequency = value; }
        }
	

        /// <summary>
        /// Initializes a new instance of the <see cref="FdoClassToClassCopyProcess"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FdoClassToClassCopyProcess(FdoClassCopyOptions options)
        {
            this.Options = options;
        }

        /// <summary>
        /// Gets the name of this instance
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get
            {
                return string.IsNullOrEmpty(this.Options.Name) ?
                    string.Format("Copy features from {0} to {1}", this.Options.SourceClassName, this.Options.TargetClassName) :
                    this.Options.Name;
            }
        }

        class PreClassCopyModifyOperation : FdoOperationBase
        {
            private FdoConnection _source;
            private FdoConnection _target;
            private FdoClassCopyOptions _opts;

            public PreClassCopyModifyOperation(FdoClassCopyOptions opts, FdoConnection source, FdoConnection target)
            {
                if (opts.PreCopyTargetModifier == null)
                    throw new ArgumentException("No pre-copy modifier specified");

                _source = source;
                _target = target;
                _opts = opts;
            }

            private int _counter = 0;

            public override IEnumerable<FdoRow> Execute(IEnumerable<FdoRow> rows)
            {
                if (_counter < 1) //Shouldn't be reentrant, but just play it safe.
                {
                    /*
                     * Check and apply the following rules for all geometry properties to be created
                     * 
                     * Target supports multiple spatial contexts:
                     * -------------------------------------------
                     * If there is no spatial contexts of the specified (source) name. Create a copy of the source spatial context.
                     * If there is a spatial context of the same name, using the same WKT. Do nothing
                     * If there is a spatial context of the same name, but using a different WKT. Create a new spatial context using the source WKT, but use a different name.
                     * 
                     * Target only supports one spatial context:
                     * If there is no spatial context already. Create a copy of the source spatial context.
                     * If there is a spatial context of the same WKT. Change the association to match the name of this spatial context.
                     * If there is a spatial context not using the source WKT. Change the association to match the name of this spatial context. This may not be ideal, but there is no other option at this point.
                     * 
                     * The regular schema compatibility fixes will handle the other properties
                     */
                    bool targetSupportsMultipleSpatialContexts = _target.Capability.GetBooleanCapability(CapabilityType.FdoCapabilityType_SupportsMultipleSpatialContexts);
                    List<SpatialContextInfo> targetSpatialContexts = null;
                    List<SpatialContextInfo> sourceSpatialContexts = null;
                    
                    if (typeof(CreateTargetClassFromSource).IsAssignableFrom(_opts.PreCopyTargetModifier.GetType()))
                    {
                        using (var tsvc = _target.CreateFeatureService())
                        using (var ssvc = _source.CreateFeatureService())
                        {
                            targetSpatialContexts = new List<SpatialContextInfo>(tsvc.GetSpatialContexts());
                            sourceSpatialContexts = new List<SpatialContextInfo>(ssvc.GetSpatialContexts());
                            var ct = (CreateTargetClassFromSource)_opts.PreCopyTargetModifier;

                            Info("Getting current schema from target");
                            var schema = tsvc.GetSchemaByName(_opts.TargetSchema);
                            if (schema.Classes.IndexOf(ct.Name) >= 0)
                            {
                                Info("Class " + _opts.TargetSchema + ":" + ct.Name + " already exists. Nothing to do here");
                            }
                            else
                            {
                                List<SpatialContextInfo> createScs = new List<SpatialContextInfo>();

                                var cls = ssvc.GetClassByName(ct.Schema, ct.Name);
                                Info("Creating a cloned copy of source class " + ct.Schema + ":" + ct.Name);

                                var cloned = FdoSchemaUtil.CloneClass(cls);
                                var propList = new List<string>(_opts.CheckSourceProperties);
                                var removeList = new List<string>();
                                foreach (PropertyDefinition prop in cloned.Properties)
                                {
                                    string propName = prop.Name;
                                    if (!propList.Contains(propName))
                                    {
                                        removeList.Add(propName);
                                    }
                                }

                                if (removeList.Count > 0)
                                {
                                    Info("Removing " + removeList.Count + " unused properties from cloned class");
                                    var props = cloned.Properties;
                                    var ids = cloned.IdentityProperties;
                                    foreach (var name in removeList)
                                    {
                                        if (ids.Contains(name))
                                            ids.RemoveAt(ids.IndexOf(name));

                                        if (props.Contains(name))
                                            props.RemoveAt(props.IndexOf(name));
                                    }
                                    Info(removeList.Count + " unused properties removed");
                                }

                                foreach (var prop in ct.PropertiesToCreate)
                                {
                                    Info("Adding property to cloned class: " + prop.Name);
                                    PropertyDefinition clonedProp = FdoSchemaUtil.CloneProperty(prop);
                                    cloned.Properties.Add(clonedProp);
                                }

                                //Add an auto-generated identity property if none exist
                                if (cloned.IdentityProperties.Count == 0)
                                {
                                    var id = new DataPropertyDefinition("FID", "Auto-Generated Feature Id");
                                    id.IsAutoGenerated = true;
                                    id.Nullable = false;
                                    //This may not be valid for target connection, but FdoFeatureService
                                    //will fix this for us.
                                    id.DataType = DataType.DataType_Int32; 

                                    cloned.Properties.Add(id);
                                    cloned.IdentityProperties.Add(id);

                                    Info("Adding an auto-generated id (FID) to this cloned class");
                                }

                                Info("Checking this class for incompatibilities");
                                IncompatibleClass ic;
                                if (!tsvc.CanApplyClass(cloned, out ic))
                                {
                                    Info("Altering this class to become compatible with target connection");
                                    cloned = tsvc.AlterClassDefinition(cloned, ic);
                                    Info("Class successfully altered");
                                }

                                Info("Checking if any spatial contexts need to be created and/or references modified");
                                foreach (PropertyDefinition pd in cloned.Properties)
                                {
                                    if (pd.PropertyType == PropertyType.PropertyType_GeometricProperty)
                                        AddSpatialContextsToCreate(targetSupportsMultipleSpatialContexts, targetSpatialContexts, sourceSpatialContexts, createScs, (GeometricPropertyDefinition)pd);
                                }

                                //We have to create spatial contexts first before applying the schema
                                if (createScs.Count > 0)
                                {
                                    //The ones we create should be unique so no overwriting needed
                                    ExpressUtility.CopyAllSpatialContexts(createScs, _target, false);

                                    foreach (var sc in createScs)
                                    {
                                        Info("Created spatial context: " + sc.Name);
                                    }
                                }

                                Info("Adding cloned class to target schema");
                                schema.Classes.Add(cloned);
                                Info("Applying schema back to target connection");
                                tsvc.ApplySchema(schema);
                                Info("Updated schema applied to target connection");
                            }
                        }
                    }
                    else if (typeof(UpdateTargetClass).IsAssignableFrom(_opts.PreCopyTargetModifier.GetType()))
                    {
                        var ut = (UpdateTargetClass)_opts.PreCopyTargetModifier;
                        using (var tsvc = _target.CreateFeatureService())
                        using (var ssvc = _source.CreateFeatureService())
                        {
                            targetSpatialContexts = new List<SpatialContextInfo>(tsvc.GetSpatialContexts());
                            sourceSpatialContexts = new List<SpatialContextInfo>(ssvc.GetSpatialContexts());
                            var schema = tsvc.GetSchemaByName(_opts.TargetSchema);
                            var cidx = schema.Classes.IndexOf(ut.Name);
                            if (cidx < 0)
                            {
                                throw new InvalidOperationException("Target class to be updated " + _opts.TargetSchema + ":" + ut.Name + " not found");
                            }
                            else
                            {
                                List<SpatialContextInfo> createScs = new List<SpatialContextInfo>();

                                var cls = schema.Classes[cidx];
                                foreach (var prop in ut.PropertiesToCreate)
                                {
                                    if (cls.Properties.IndexOf(prop.Name) < 0)
                                    {
                                        Info("Adding property to class: " + prop.Name);
                                        var clonedProp = FdoSchemaUtil.CloneProperty(prop);
                                        if (clonedProp.PropertyType == PropertyType.PropertyType_GeometricProperty)
                                        {
                                            AddSpatialContextsToCreate(targetSupportsMultipleSpatialContexts, targetSpatialContexts, sourceSpatialContexts, createScs, (GeometricPropertyDefinition)clonedProp);
                                        }
                                        cls.Properties.Add(clonedProp);
                                    }
                                    else 
                                    {
                                        Info("Skipping property " + prop.Name + " because it already exists");
                                    }
                                }

                                //We have to create spatial contexts first before applying the schema
                                if (createScs.Count > 0)
                                {
                                    //The ones we create should be unique so no overwriting needed
                                    ExpressUtility.CopyAllSpatialContexts(createScs, _target, false);

                                    foreach (var sc in createScs)
                                    {
                                        Info("Created spatial context: " + sc.Name);
                                    }
                                }

                                Info("Applying modified schema " + schema.Name + " to target connection");
                                tsvc.ApplySchema(schema);
                                Info("Modified schema " + schema.Name + " applied to target connection");
                            }
                        }
                    }

                    _counter++;
                }
                return rows;
            }

            private void AddSpatialContextsToCreate(bool targetSupportsMultipleSpatialContexts, List<SpatialContextInfo> targetSpatialContexts, List<SpatialContextInfo> sourceSpatialContexts, List<SpatialContextInfo> createScs, GeometricPropertyDefinition geom)
            {
                if (targetSupportsMultipleSpatialContexts)
                {
                    // NOTE:
                    //
                    // There's a defect in the PostgreSQL provider regarding spatial context creation.
                    //  - WKT is disregarded
                    //  - Extents are disregarded when creating
                    //
                    // What this means is that when the PostgreSQL provider is the target connection, multiple
                    // copies of the same spatial context will be created because the WKT comparison check will fail
                    // because of the above point.

                    if (!string.IsNullOrEmpty(geom.SpatialContextAssociation))
                    {
                        //See if the source spatial context has a matching target (by name)
                        SpatialContextInfo matchingTargetSc = FindMatchingSpatialContextByName(targetSpatialContexts, geom.SpatialContextAssociation);
                        if (matchingTargetSc == null)
                        {
                            //The source spatial context name doesn't exist in target
                            //We are free to create a copy of the source one as target supports multiple SCs
                            SpatialContextInfo sourceSc = FindMatchingSpatialContextByName(sourceSpatialContexts, geom.SpatialContextAssociation);
                            if (sourceSc != null)
                            {
                                Info("Adding source spatial context (" + sourceSc.Name + ") to list to be copied to target");
                                var sc = sourceSc.Clone();
                                //Add to list of ones to create
                                createScs.Add(sc);
                                //So that subsequent travels along this code path take into account
                                //spatial context we will create as well
                                targetSpatialContexts.Add(sc);
                            }
                            else
                            {
                                /* UNCOMMON CODE PATH. WE'RE WORKING WITH SOME MESSED UP DATA IF WE GET HERE */

                                //We have a source geom property with an invalid spatial context association reference
                                //So which SC should we assign?

                                SpatialContextInfo sc = null;
                                if (targetSpatialContexts.Count > 0)
                                {
                                    //Try first active SC on target
                                    sc = FindFirstActiveSpatialContext(targetSpatialContexts);
                                    //Otherwise first one on target
                                    if (sc == null)
                                        sc = targetSpatialContexts[0];

                                    if (sc != null)
                                    {
                                        //Update reference only. No need to create 
                                        geom.SpatialContextAssociation = sc.Name;
                                        return;
                                    }
                                }
                                if (sourceSpatialContexts.Count > 0)
                                {
                                    //Try first active SC on source
                                    sc = FindFirstActiveSpatialContext(sourceSpatialContexts);
                                    //Otherwise first one on target
                                    if (sc == null)
                                        sc = sourceSpatialContexts[0];

                                    //Still nothing????
                                    if (sc == null)
                                        throw new Exception("Could not find a suitable replacement spatial context for geometry property" + geom.Name);

                                    sc = sc.Clone();
                                    //
                                    string prefix = "SC" + geom.Name;
                                    string name = prefix;
                                    while (SpatialContextExistsByName(targetSpatialContexts, name))
                                    {
                                        _counter++;
                                        name = prefix + _counter;
                                    }
                                    sc.Name = name;
                                    //Add to list of ones to create
                                    createScs.Add(sc);
                                    //So that subsequent travels along this code path take into account
                                    //spatial context we will create as well
                                    targetSpatialContexts.Add(sc);
                                    //Update reference to point to this sc we will create
                                    geom.SpatialContextAssociation = sc.Name;
                                }
                            }
                        }
                        else //There is a matching target spatial context of the same name
                        {
                            //Compare target spatial context with source spatial context
                            SpatialContextInfo sourceSc = FindMatchingSpatialContextByName(sourceSpatialContexts, geom.SpatialContextAssociation);
                            if (sourceSc != null)
                            {
                                //Compare WKTs
                                if (!sourceSc.CoordinateSystemWkt.Equals(matchingTargetSc.CoordinateSystemWkt))
                                {
                                    //WKTs do not match. Create a clone of the source but with a different name
                                    var sc = sourceSc.Clone();
                                    //
                                    string prefix = "SC" + geom.SpatialContextAssociation;
                                    string name = prefix;
                                    while (SpatialContextExistsByName(targetSpatialContexts, name))
                                    {
                                        _counter++;
                                        name = prefix + _counter;
                                    }
                                    sc.Name = name;
                                    //Add to list of ones to create
                                    createScs.Add(sc);
                                    //So that subsequent travels along this code path take into account
                                    //spatial context we will create as well
                                    targetSpatialContexts.Add(sc);
                                    //Update reference to point to this sc we will create
                                    geom.SpatialContextAssociation = sc.Name;
                                } 
                                //Otherwise matches both in name and WKT. Nothing needs to be done
                            }
                            else //No source spatial context to compare with
                            {
                                /* UNCOMMON CODE PATH. WE'RE WORKING WITH SOME MESSED UP DATA IF WE GET HERE */

                                //We have a source geom property with an invalid spatial context association reference
                                //So which SC should we assign?

                                geom.SpatialContextAssociation = matchingTargetSc.Name;
                            }
                        }
                    }
                    else
                    {
                        /* UNCOMMON CODE PATH. WE'RE WORKING WITH SOME MESSED UP DATA IF WE GET HERE */

                        //No spatial context association found on the cloned geometry property we created
                        //Which SC should we assign?

                        SpatialContextInfo sc = null;
                        if (targetSpatialContexts.Count > 0)
                        {
                            //Try first active SC on target
                            sc = FindFirstActiveSpatialContext(targetSpatialContexts);
                            //Otherwise first one on target
                            if (sc == null)
                                sc = targetSpatialContexts[0];

                            //Still nothing????
                            if (sc != null)
                            {
                                //Update reference only. No need to create 
                                geom.SpatialContextAssociation = sc.Name;
                                return;
                            }
                        }
                        
                        if (sourceSpatialContexts.Count > 0)
                        {
                            //Try first active SC on source
                            sc = FindFirstActiveSpatialContext(sourceSpatialContexts);
                            //Otherwise first one on target
                            if (sc == null)
                                sc = sourceSpatialContexts[0];

                            //Still nothing????
                            if (sc == null)
                                throw new Exception("Could not find a suitable replacement spatial context for geometry property" + geom.Name);

                            sc = sc.Clone();
                            //
                            string prefix = "SC" + geom.Name;
                            string name = prefix;
                            int scc = 0;
                            while (SpatialContextExistsByName(targetSpatialContexts, name))
                            {
                                _counter++;
                                name = prefix + scc;
                            }
                            sc.Name = name;
                            //Add to list of ones to create
                            createScs.Add(sc);
                            //So that subsequent travels along this code path take into account
                            //spatial context we will create as well
                            targetSpatialContexts.Add(sc);
                            //Update reference to point to this sc we will create
                            geom.SpatialContextAssociation = sc.Name;
                        }
                    }
                }
                else //Only supports one spatial context
                {
                    System.Diagnostics.Debug.Assert(targetSpatialContexts.Count <= 1);
                    if (targetSpatialContexts.Count == 1)
                    {
                        //Coerce to the target spatial context. We can't do anything else
                        geom.SpatialContextAssociation = targetSpatialContexts[0].Name;
                    }
                    else
                    {
                        //We can create one, but only one!
                        SpatialContextInfo sourceSc = FindMatchingSpatialContextByName(sourceSpatialContexts, geom.SpatialContextAssociation);
                        if (sourceSc != null)
                        {
                            //You're it!
                            var sc = sourceSc.Clone();
                            createScs.Add(sc);
                            geom.SpatialContextAssociation = sc.Name;
                        }
                        else
                        {
                            /* UNCOMMON CODE PATH. WE'RE WORKING WITH SOME MESSED UP DATA IF WE GET HERE */

                            //No spatial contexts on target and the source reference points to 
                            //nowhere! Now what?
                            if (sourceSpatialContexts.Count == 0)
                                throw new Exception("Could not find source spatial context with name " + geom.SpatialContextAssociation + " and target has no spatial context");

                            //Try first active source spatial context
                            sourceSc = FindFirstActiveSpatialContext(sourceSpatialContexts);

                            //Last resort. First known source spatial context
                            if (sourceSc == null)
                                sourceSc = sourceSpatialContexts[0];

                            var sc = sourceSc.Clone();
                            createScs.Add(sc);
                            geom.SpatialContextAssociation = sc.Name;
                        }
                    }
                }
            }

            private static SpatialContextInfo FindFirstActiveSpatialContext(List<SpatialContextInfo> spatialContexts)
            {
                if (spatialContexts == null)
                    return null;

                foreach (var sc in spatialContexts)
                {
                    if (sc.IsActive)
                        return sc;
                }
                return null;
            }

            private static bool SpatialContextExistsByName(ICollection<SpatialContextInfo> spatialContexts, string name)
            {
                return FindMatchingSpatialContextByName(spatialContexts, name) != null;
            }

            private static SpatialContextInfo FindMatchingSpatialContextByName(ICollection<SpatialContextInfo> spatialContexts, string name)
            {
                if (spatialContexts == null || string.IsNullOrEmpty(name))
                    return null;

                foreach (var sc in spatialContexts)
                {
                    if (sc.Name.Equals(name))
                        return sc;
                }
                return null;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            FdoConnection srcConn = Options.Parent.GetConnection(Options.SourceConnectionName);
            FdoConnection dstConn = Options.Parent.GetConnection(Options.TargetConnectionName);

            //Register delete operation first if delete target specified
            if (Options.DeleteTarget)
            {
                using (var svc = dstConn.CreateFeatureService())
                {
                    var cls = svc.GetClassByName(Options.TargetSchema, Options.TargetClassName);
                    //We can't delete if the class in question doesn't exist
                    if (cls != null)
                    {
                        FdoDeleteOperation op = new FdoDeleteOperation(dstConn, Options.TargetClassName);
                        //There's info here worth bubbling up
                        op.OnInfo += (sender, e) =>
                        {
                            SendMessageFormatted("[{0}:{1}] {2}", this.Name, "Delete", e.Message);
                        };
                        Register(op);
                    }
                }
            }

            if (Options.PreCopyTargetModifier != null)
            {
                var op = new PreClassCopyModifyOperation(Options, srcConn, dstConn);
                //There's info here worth bubbling up
                op.OnInfo += (sender, e) =>
                {
                    SendMessageFormatted("[{0}:{1}] {2}", this.Name, "PreCopy", e.Message);
                };
                Register(op);
            }

            IFdoOperation input = new FdoInputOperation(srcConn, CreateSourceQuery());
            IFdoOperation output = null;
            IFdoOperation convert = null;
            IFdoOperation reproject = null;

            NameValueCollection propertyMappings = new NameValueCollection();
            string[] srcProps = this.Options.SourcePropertyNames;
            string[] srcAliases = this.Options.SourceAliases;
            if (srcProps.Length > 0)
            {
                foreach (string srcProp in srcProps)
                {
                    propertyMappings.Add(srcProp, Options.GetTargetProperty(srcProp));
                }
            }
            if (srcAliases.Length > 0)
            {
                foreach (string srcAlias in srcAliases)
                {
                    propertyMappings.Add(srcAlias, Options.GetTargetPropertyForAlias(srcAlias));
                }
            }
            if (propertyMappings.Count > 0)
            {
                if (Options.BatchSize > 0)
                {
                    FdoBatchedOutputOperation bat = new FdoBatchedOutputOperation(dstConn, Options.TargetClassName, propertyMappings, Options.BatchSize);
                    bat.BatchInserted += delegate(object sender, BatchInsertEventArgs e)
                    {
                        SendMessageFormatted("[Bulk Copy => {0}] {1} feature batch written", Options.TargetClassName, e.BatchSize);
                    };
                    bat.OnInfo += (sender, e) =>
                    {
                        SendMessageFormatted("[{0}:{1}] {2}", this.Name, "BatchOutput", e.Message);
                    };
                    output = bat;
                }
                else
                {
                    var outop = new FdoOutputOperation(dstConn, Options.TargetClassName, propertyMappings);
                    outop.OnInfo += (sender, e) =>
                    {
                        SendMessageFormatted("[{0}:{1}] {2}", this.Name, "Output", e.Message);
                    };
                    output = outop;
                }
            }
            else
            {
                if (Options.BatchSize > 0)
                {
                    FdoBatchedOutputOperation bat = new FdoBatchedOutputOperation(dstConn, Options.TargetClassName, Options.BatchSize);
                    bat.BatchInserted += delegate(object sender, BatchInsertEventArgs e)
                    {
                        SendMessageFormatted("[Bulk Copy => {0}] {1} feature batch written", Options.TargetClassName, e.BatchSize);
                    };
                    bat.OnInfo += (sender, e) =>
                    {
                        SendMessageFormatted("[{0}:{1}] {2}", this.Name, "BatchOutput", e.Message);
                    };
                    output = bat;
                }
                else
                {
                    var outop = new FdoOutputOperation(dstConn, Options.TargetClassName);
                    outop.OnInfo += (sender, e) =>
                    {
                        SendMessageFormatted("[{0}:{1}] {2}", this.Name, "Output", e.Message);
                    };
                    output = outop;
                }
            }

            if (Options.ConversionRules.Count > 0)
            {
                FdoDataValueConversionOperation op = new FdoDataValueConversionOperation(Options.ConversionRules);
                op.OnInfo += (sender, e) =>
                {
                    SendMessageFormatted("[{0}:{1}] {2}", this.Name, "DataValueConvert", e.Message);
                };
                convert = op;
            }

            //TODO:
            //
            //Compare the WKTs of the source and target spatial contexts by their association (Pre-copy modifiers should've
            //created and/or assigned the correct contexts). If they are different, set up a re-projection operation using
            //the source and target WKTs, which will do a vertex by vertex transformation of all the geometries that pass
            //through it.
            //
            //I found this solution in a dream I had. (I am *NOT* kidding!)
            //
            //I N C E P T I O N?

            Register(input);
            if(convert != null)
                Register(convert);
            if (Options.FlattenGeometries)
                Register(new FdoFlattenGeometryOperation());
            if (Options.ForceWkb)
                Register(new FdoForceWkbOperation());
            if (reproject != null) //Will always be null atm
                Register(reproject);
            Register(output);
            
            //This is to dispose of any FDO objects stored in the FdoRows being sent through
            Register(new FdoCleanupOperation());
        }

        /// <summary>
        /// Called when a row is processed.
        /// </summary>
        /// <param name="op">The operation.</param>
        /// <param name="dictionary">The dictionary.</param>
        protected override void OnFeatureProcessed(FdoOperationBase op, FdoRow dictionary)
        {
            if (op.Statistics.OutputtedRows % this.ReportFrequency == 0)
            {
                if (op is FdoOutputOperation)
                {
                    string className = (op as FdoOutputOperation).ClassName;
                    SendMessageFormatted("[{0}]: {1} features processed", this.Name, op.Statistics.OutputtedRows);
                }
            }
        }

        /// <summary>
        /// Called when this process has finished processing.
        /// </summary>
        /// <param name="op">The op.</param>
        protected override void OnFinishedProcessing(FdoOperationBase op)
        {
            if (op is FdoBatchedOutputOperation)
            {
                FdoBatchedOutputOperation bop = op as FdoBatchedOutputOperation;
                string className = bop.ClassName;
                SendMessageFormatted("[{0}]: {1}", this.Name, op.Statistics.ToString());
            }
            else if (op is FdoOutputOperation)
            {
                string className = (op as FdoOutputOperation).ClassName;
                SendMessageFormatted("[{0}]: {1}", this.Name, op.Statistics.ToString());
            }
        }

        private FeatureQueryOptions CreateSourceQuery()
        {
            FeatureQueryOptions query = new FeatureQueryOptions(Options.SourceClassName);
            query.AddFeatureProperty(Options.SourcePropertyNames);


            foreach (string alias in Options.SourceAliases)
            {
                query.AddComputedProperty(alias, Options.GetExpression(alias));
            }

            if (!string.IsNullOrEmpty(Options.SourceFilter))
                query.Filter = Options.SourceFilter;

            return query;
        }

    }
}
