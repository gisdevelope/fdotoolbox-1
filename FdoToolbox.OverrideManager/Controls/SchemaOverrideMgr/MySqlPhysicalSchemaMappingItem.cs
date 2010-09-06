﻿#region LGPL Header
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
using MySql = OSGeo.FDO.Providers.Rdbms.Override.MySQL;

namespace FdoToolbox.OverrideManager.Controls.SchemaOverrideMgr
{
    public class MySqlPhysicalSchemaMappingItem : RdbmsPhysicalSchemaMappingItem<MySql.OvPhysicalSchemaMapping>
    {
        public MySqlPhysicalSchemaMappingItem(MySql.OvPhysicalSchemaMapping value)
            : base(value)
        {
        }

        protected override object[] GetClasses()
        {
            List<MySqlClassDefinitionItem> classes = new List<MySqlClassDefinitionItem>();
            foreach (MySql.OvClassDefinition cls in this.InternalValue.Classes)
            {
                classes.Add(new MySqlClassDefinitionItem(cls));
            }
            return classes.ToArray();
        }

        public string Database
        {
            get { return this.InternalValue.Database; }
            set { this.InternalValue.Database = value; }
        }

        public string DataDirectory
        {
            get { return this.InternalValue.DataDirectory; }
            set { this.InternalValue.DataDirectory = value; }
        }

        public string IndexDirectory
        {
            get { return this.InternalValue.IndexDirectory; }
            set { this.InternalValue.IndexDirectory = value; }
        }

        public string Provider
        {
            get { return this.InternalValue.Provider; }
        }

        public MySql.OvStorageEngineType StorageEngine
        {
            get { return this.InternalValue.StorageEngine; }
            set { this.InternalValue.StorageEngine = value; }
        }
    }
}