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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FdoToolbox.Base.Services;
using FdoToolbox.Core.Feature;
using FdoToolbox.Base;

namespace FdoToolbox.Tasks.Controls.BulkCopy
{
    public partial class NewTaskDialog : Form
    {
        private string[] _availNames;

        public NewTaskDialog(string [] availableConnectionNames)
        {
            InitializeComponent();
            _availNames = availableConnectionNames;
        }

        private FdoConnectionManager _connMgr;

        private void CheckButtonStates()
        {
            btnOK.Enabled = (!string.IsNullOrEmpty(txtName.Text)
                && (this.CreateIfNotExist || cmbDstClass.SelectedIndex >= 0)
                && cmbDstConnection.SelectedIndex >= 0
                && cmbDstSchema.SelectedIndex >= 0
                && (this.CreateIfNotExist || cmbSrcClass.SelectedIndex >= 0)
                && cmbSrcConnection.SelectedIndex >= 0
                && cmbSrcSchema.SelectedIndex >= 0);

            cmbDstClass.Enabled = !this.CreateIfNotExist;
        }

        protected override void OnLoad(EventArgs e)
        {
            _connMgr = ServiceManager.Instance.GetService<FdoConnectionManager>();
            List<string> srcNames = new List<string>(_availNames);
            List<string> dstNames = new List<string>(_availNames);

            cmbSrcConnection.DataSource = srcNames;
            cmbDstConnection.DataSource = dstNames;

            cmbSrcConnection.SelectedIndex = 0;
            cmbDstConnection.SelectedIndex = 0;

            cmbSrcConnection_SelectionChangeCommitted(this, EventArgs.Empty);
            cmbDstConnection_SelectionChangeCommitted(this, EventArgs.Empty);

            base.OnLoad(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private FdoFeatureService CreateSourceService()
        {
            FdoConnection conn = _connMgr.GetConnection(cmbSrcConnection.SelectedItem.ToString());
            return conn.CreateFeatureService();
        }

        private FdoFeatureService CreateTargetService()
        {
            FdoConnection conn = _connMgr.GetConnection(cmbDstConnection.SelectedItem.ToString());
            return conn.CreateFeatureService();
        }

        private void cmbDstConnection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateTargetSchemas();
            CheckButtonStates();
        }

        private void UpdateTargetSchemas()
        {
            using (FdoFeatureService svc = CreateTargetService())
            {
                cmbDstSchema.DataSource = svc.GetSchemaNames();
                cmbDstSchema.SelectedIndex = 0;
                UpdateTargetClasses();
            }
        }

        private void cmbSrcConnection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateSourceSchemas();
            CheckButtonStates();
        }

        private void UpdateSourceSchemas()
        {
            using (FdoFeatureService svc = CreateSourceService())
            {
                cmbSrcSchema.DataSource = svc.GetSchemaNames();
                cmbSrcSchema.SelectedIndex = 0;
                UpdateSourceClasses();
            }
        }

        private void cmbSrcSchema_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateSourceClasses();
            CheckButtonStates();
        }

        private void UpdateSourceClasses()
        {
            using (FdoFeatureService svc = CreateSourceService())
            {
                string schema = cmbSrcSchema.SelectedItem.ToString();
                var classes = svc.GetClassNames(schema);
                cmbSrcClass.DataSource = classes;
                if (classes.Count > 0)
                    cmbSrcClass.SelectedIndex = 0;
                CheckEmptyName();
            }
        }

        private void cmbDstSchema_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateTargetClasses();
            CheckButtonStates();
        }

        private void UpdateTargetClasses()
        {
            using (FdoFeatureService svc = CreateTargetService())
            {
                string schema = cmbDstSchema.SelectedItem.ToString();
                string srcClass = this.SourceClass;
                var classes = svc.GetClassNames(schema);
                if (!string.IsNullOrEmpty(srcClass))
                {
                    //Activate this option only if the source class name doesn't exist in the target's selected schema
                    chkCreate.Enabled = !classes.Contains(srcClass);
                }
                cmbDstClass.DataSource = classes;
                if (classes.Count > 0)
                    cmbDstClass.SelectedIndex = 0;
                CheckEmptyName();
            }
        }

        private void cmbSrcClass_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CheckEmptyName();
            CheckButtonStates();
            chkCreate.Enabled = cmbDstClass.FindString(this.SourceClass) < 0;
        }

        private void CheckEmptyName()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                if (!string.IsNullOrEmpty(this.SourceClass) && !string.IsNullOrEmpty(this.TargetClass))
                {
                    txtName.Text = "Copy " + this.SourceClass + " to " + this.TargetClass;
                }
            }
        }

        private void cmbDstClass_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CheckEmptyName();
            CheckButtonStates();
        }

        public string SourceConnectionName
        {
            get { return cmbSrcConnection.SelectedItem != null ? cmbSrcConnection.SelectedItem.ToString() : string.Empty; }
        }

        public string TargetConnectionName
        {
            get { return cmbDstConnection.SelectedItem != null ? cmbDstConnection.SelectedItem.ToString() : string.Empty; }
        }

        public string SourceSchema
        {
            get { return cmbSrcSchema.SelectedItem != null ? cmbSrcSchema.SelectedItem.ToString() : string.Empty; }
        }

        public string TargetSchema
        {
            get { return cmbDstSchema.SelectedItem != null ? cmbDstSchema.SelectedItem.ToString() : string.Empty; }
        }

        public string SourceClass
        {
            get { return cmbSrcClass.SelectedItem != null ? cmbSrcClass.SelectedItem.ToString() : string.Empty; }
        }

        public string TargetClass
        {
            get 
            {
                if (this.CreateIfNotExist)
                    return this.SourceClass;
                else
                    return cmbDstClass.SelectedItem != null ? cmbDstClass.SelectedItem.ToString() : string.Empty; 
            }
        }

        public string TaskName
        {
            get { return txtName.Text; }
        }

        public bool CreateIfNotExist
        {
            get { return chkCreate.Enabled && chkCreate.Checked; }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            CheckButtonStates();
        }

        private void chkCreate_CheckedChanged(object sender, EventArgs e)
        {
            CheckButtonStates();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            CheckButtonStates();
        }
    }
}