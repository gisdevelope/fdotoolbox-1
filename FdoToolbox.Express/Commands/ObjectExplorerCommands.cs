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
using ICSharpCode.Core;
using FdoToolbox.Base.Services;
using FdoToolbox.Core.Feature;
using System.Windows.Forms;
using FdoToolbox.Base;
using FdoToolbox.Core.Utility;

using Msg = ICSharpCode.Core.MessageService;
using Res = ICSharpCode.Core.ResourceService;
using FdoToolbox.Express.Controls;

namespace FdoToolbox.Express.Commands
{
    public class CopySpatialContextsCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench wb = Workbench.Instance;
            TreeNode connNode = wb.ObjectExplorer.GetSelectedNode();
            string srcConnName = connNode.Name;
            CopySpatialContextsCtl ctl = new CopySpatialContextsCtl(srcConnName);
            wb.ShowContent(ctl, ViewRegion.Dialog);
        }
    }

    public class DumpFeatureClassCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench wb = Workbench.Instance;
            TreeNode classNode = wb.ObjectExplorer.GetSelectedNode();
            string srcConnName = classNode.Parent.Parent.Name;
            string schemaName = classNode.Parent.Name;
            string className = classNode.Name;
            FdoConnectionManager mgr = ServiceManager.Instance.GetService<FdoConnectionManager>();
            FdoConnection source = mgr.GetConnection(srcConnName);
            var ctl = new DumpFeatureClassCtl(source, schemaName, className);
            wb.ShowContent(ctl, ViewRegion.Dialog);
        }
    }
}
