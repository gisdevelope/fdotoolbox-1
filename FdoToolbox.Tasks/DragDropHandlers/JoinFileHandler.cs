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
using FdoToolbox.Base.Services.DragDropHandlers;
using FdoToolbox.Core.ETL;
using FdoToolbox.Tasks.Services;
using FdoToolbox.Base.Services;
using FdoToolbox.Core.ETL.Specialized;

namespace FdoToolbox.Tasks.DragDropHandlers
{
    public class JoinFileHandler : IDragDropHandler
    {
        /// <summary>
        /// Gets a description of the action this handler will take
        /// </summary>
        /// <value></value>
        public string HandlerAction
        {
            get { throw new NotImplementedException(); }
        }

        string[] extensions = { TaskDefinitionHelper.JOINDEFINITION };

        /// <summary>
        /// Gets the file extensions this handler can handle
        /// </summary>
        /// <value></value>
        public string[] FileExtensions
        {
            get { return extensions; }
        }

        /// <summary>
        /// Handles the file drop
        /// </summary>
        /// <param name="file">The file being dropped</param>
        public void HandleDrop(string file)
        {
            TaskManager mgr = ServiceManager.Instance.GetService<TaskManager>();
            TaskLoader ldr = new TaskLoader();
            string prefix = string.Empty;
            FdoJoinOptions opt = ldr.JoinFromXml(file, ref prefix, false);
            FdoJoin join = new FdoJoin(opt);
            
            string name = prefix;
            int counter = 0;
            while (mgr.NameExists(name))
            {
                counter++;
                name = prefix + counter;
            }
            mgr.AddTask(name, join);
        }
    }
}
