namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using Microsoft.Win32;
    #endregion
    
    /// <summary>
    /// Standard About dialog for a Windows application.
    /// </summary>
    internal class About : System.Windows.Forms.Form
    {
        private string sysInfoPath;

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSysInfo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDisclaimer;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public About()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            #region Custom Constructor Logic
            RegistryKey root = Registry.LocalMachine;

            RegistryKey file = root.OpenSubKey( @"SOFTWARE\Microsoft\Shared Tools\MSINFO" );

            // Try to get system info program path\name from registry...
            if( file != null )
                sysInfoPath = file.GetValue("Path").ToString();
            else			
            {
                // Try to get system info program path only from registry...
                RegistryKey path = root.OpenSubKey( @"SOFTWARE\Microsoft\Shared Tools Location" );

                sysInfoPath = path.GetValue( "MsInfo" ).ToString() + "\\MSINFO32.EXE";
            }

            // Does the utility exist?
            if( File.Exists( sysInfoPath ) )
                btnSysInfo.Enabled = true;
            #endregion
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

		#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSysInfo = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picIcon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(9, 19);
            this.picIcon.Name = "picIcon";
            this.picIcon.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.picIcon.Size = new System.Drawing.Size(64, 64);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 2;
            this.picIcon.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(361, 217);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 0;
            this.btnOK.Tag = "OK";
            this.btnOK.Text = "OK";
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnSysInfo.Enabled = false;
            this.btnSysInfo.Location = new System.Drawing.Point(361, 246);
            this.btnSysInfo.Name = "btnSysInfo";
            this.btnSysInfo.TabIndex = 1;
            this.btnSysInfo.Tag = "&System Info...";
            this.btnSysInfo.Text = "&System Info...";
            this.btnSysInfo.Click += new System.EventHandler(this.btnSysInfo_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.SystemColors.Control;
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(82, 87);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDescription.Size = new System.Drawing.Size(319, 90);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Tag = "App Description";
            this.lblDescription.Text = @"This application is designed as a front-end for working with XML documents and schemas. It has the ability to validate a document against a schema (provided the document contains a schemaLocation attribute), validate an XML schema, and generate an empty XML document from a schema.";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(82, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size(319, 34);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Tag = "Application Title";
            this.lblTitle.Text = "XML Wizard";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point(82, 60);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(319, 18);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Tag = "Version";
            this.lblVersion.Text = "Version 1.0.0";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.BackColor = System.Drawing.SystemColors.Control;
            this.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Black;
            this.lblDisclaimer.Location = new System.Drawing.Point(20, 202);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDisclaimer.Size = new System.Drawing.Size(329, 64);
            this.lblDisclaimer.TabIndex = 3;
            this.lblDisclaimer.Tag = "Warning: ...";
            this.lblDisclaimer.Text = "Copyright © 2003 Craig Wagner. All Rights Reserved.";
            // 
            // About
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(456, 279);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSysInfo);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblDisclaimer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About XML Wizard";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);

        }
		#endregion

        #region Event Handlers
        private void About_Load(object sender, System.EventArgs e)
        {
            AssemblyName aName = Assembly.GetExecutingAssembly().GetName();
            lblVersion.Text = aName.Version.ToString();
        }

        private void btnSysInfo_Click(object sender, System.EventArgs e)
        {
            Process.Start( sysInfoPath );
        }
        #endregion
    }
}
