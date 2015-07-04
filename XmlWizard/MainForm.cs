namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.IO;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Collections;
    using System.ComponentModel;
    using System.Net;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Schema;
    using Microsoft.Win32;
    using Wagner.Xml.Schema;
    #endregion

    public class MainForm : System.Windows.Forms.Form
	{
        #region Constants
        private const int initialCapacity = 1024 * 5;
        private const string regKey = @"Software\XMLWizard";
        #endregion

        private System.Windows.Forms.TextBox txtResults;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.Label lblSourceFilePath;
        private System.Windows.Forms.OpenFileDialog dlgFileSelect;
        private System.Windows.Forms.ComboBox cboSourceFilePath;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.StatusBarPanel panel1;
        private System.Windows.Forms.CheckBox chkSampleData;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemFilePrint;
        private System.Windows.Forms.MenuItem menuItemFileExit;
        private System.Windows.Forms.MenuItem menuItemAction;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemHelpAbout;
        private System.Windows.Forms.MenuItem menuItemActionValidate;
        private System.Windows.Forms.MenuItem menuItemActionGenerateEmptyDocument;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolBarButton btnExit;
        private System.Windows.Forms.ToolBarButton btnValidate;
        private System.Windows.Forms.ToolBarButton btnSep1;
        private System.Windows.Forms.ToolBarButton btnGenerate;
        private System.Windows.Forms.CheckBox chkChoiceComments;
        private System.Windows.Forms.MenuItem menuItemFileSep2;
        private System.Windows.Forms.MenuItem menuItemFileSave;
        private System.Windows.Forms.MenuItem menuItemFileSep1;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.ToolBarButton btnSave;
        private System.Windows.Forms.ToolBarButton btnSep2;
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.TextBox txtSchemaFile;
        private System.Windows.Forms.Button btnSchemaBrowse;
        private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
            this.txtResults = new System.Windows.Forms.TextBox();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.btnFileBrowse = new System.Windows.Forms.Button();
            this.lblSourceFilePath = new System.Windows.Forms.Label();
            this.dlgFileSelect = new System.Windows.Forms.OpenFileDialog();
            this.cboSourceFilePath = new System.Windows.Forms.ComboBox();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.StatusBarPanel();
            this.chkSampleData = new System.Windows.Forms.CheckBox();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemFileSave = new System.Windows.Forms.MenuItem();
            this.menuItemFileSep1 = new System.Windows.Forms.MenuItem();
            this.menuItemFilePrint = new System.Windows.Forms.MenuItem();
            this.menuItemFileSep2 = new System.Windows.Forms.MenuItem();
            this.menuItemFileExit = new System.Windows.Forms.MenuItem();
            this.menuItemAction = new System.Windows.Forms.MenuItem();
            this.menuItemActionValidate = new System.Windows.Forms.MenuItem();
            this.menuItemActionGenerateEmptyDocument = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.MenuItem();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.btnSave = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnValidate = new System.Windows.Forms.ToolBarButton();
            this.btnGenerate = new System.Windows.Forms.ToolBarButton();
            this.btnSep2 = new System.Windows.Forms.ToolBarButton();
            this.btnExit = new System.Windows.Forms.ToolBarButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.chkChoiceComments = new System.Windows.Forms.CheckBox();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.lblSchema = new System.Windows.Forms.Label();
            this.txtSchemaFile = new System.Windows.Forms.TextBox();
            this.btnSchemaBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Location = new System.Drawing.Point(8, 108);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(583, 248);
            this.txtResults.TabIndex = 5;
            this.txtResults.TabStop = false;
            this.txtResults.Text = "";
            this.txtResults.TextChanged += new System.EventHandler(this.txtResults_TextChanged);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileBrowse.Location = new System.Drawing.Point(512, 32);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(22, 23);
            this.btnFileBrowse.TabIndex = 2;
            this.btnFileBrowse.TabStop = false;
            this.btnFileBrowse.Text = "...";
            this.btnFileBrowse.Click += new System.EventHandler(this.btnFileBrowse_Click);
            // 
            // lblSourceFilePath
            // 
            this.lblSourceFilePath.Location = new System.Drawing.Point(5, 32);
            this.lblSourceFilePath.Name = "lblSourceFilePath";
            this.lblSourceFilePath.Size = new System.Drawing.Size(97, 23);
            this.lblSourceFilePath.TabIndex = 0;
            this.lblSourceFilePath.Text = "Source File Path:";
            this.lblSourceFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSourceFilePath
            // 
            this.cboSourceFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSourceFilePath.Location = new System.Drawing.Point(105, 33);
            this.cboSourceFilePath.Name = "cboSourceFilePath";
            this.cboSourceFilePath.Size = new System.Drawing.Size(404, 21);
            this.cboSourceFilePath.Sorted = true;
            this.cboSourceFilePath.TabIndex = 1;
            this.cboSourceFilePath.TextChanged += new System.EventHandler(this.cboSourceFilePath_TextChanged);
            this.cboSourceFilePath.SelectedIndexChanged += new System.EventHandler(this.cboSourceFilePath_SelectedIndexChanged);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 365);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                         this.panel1});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(597, 22);
            this.statusBar.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.panel1.Text = "  Ready";
            this.panel1.Width = 581;
            // 
            // chkSampleData
            // 
            this.chkSampleData.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSampleData.Location = new System.Drawing.Point(7, 83);
            this.chkSampleData.Name = "chkSampleData";
            this.chkSampleData.Size = new System.Drawing.Size(141, 24);
            this.chkSampleData.TabIndex = 3;
            this.chkSampleData.Text = "Generate Sample Data";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemFile,
                                                                                     this.menuItemAction,
                                                                                     this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemFileSave,
                                                                                         this.menuItemFileSep1,
                                                                                         this.menuItemFilePrint,
                                                                                         this.menuItemFileSep2,
                                                                                         this.menuItemFileExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemFileSave
            // 
            this.menuItemFileSave.Enabled = false;
            this.menuItemFileSave.Index = 0;
            this.menuItemFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItemFileSave.Text = "&Save";
            this.menuItemFileSave.Click += new System.EventHandler(this.menuItemFileSave_Click);
            // 
            // menuItemFileSep1
            // 
            this.menuItemFileSep1.Index = 1;
            this.menuItemFileSep1.Text = "-";
            // 
            // menuItemFilePrint
            // 
            this.menuItemFilePrint.Enabled = false;
            this.menuItemFilePrint.Index = 2;
            this.menuItemFilePrint.Text = "&Print";
            this.menuItemFilePrint.Click += new System.EventHandler(this.menuItemFilePrint_Click);
            // 
            // menuItemFileSep2
            // 
            this.menuItemFileSep2.Index = 3;
            this.menuItemFileSep2.Text = "-";
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Index = 4;
            this.menuItemFileExit.Text = "E&xit";
            this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
            // 
            // menuItemAction
            // 
            this.menuItemAction.Index = 1;
            this.menuItemAction.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.menuItemActionValidate,
                                                                                           this.menuItemActionGenerateEmptyDocument});
            this.menuItemAction.Text = "&Action";
            // 
            // menuItemActionValidate
            // 
            this.menuItemActionValidate.Enabled = false;
            this.menuItemActionValidate.Index = 0;
            this.menuItemActionValidate.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItemActionValidate.Text = "&Validate";
            this.menuItemActionValidate.Click += new System.EventHandler(this.menuItemActionValidate_Click);
            // 
            // menuItemActionGenerateEmptyDocument
            // 
            this.menuItemActionGenerateEmptyDocument.Enabled = false;
            this.menuItemActionGenerateEmptyDocument.Index = 1;
            this.menuItemActionGenerateEmptyDocument.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.menuItemActionGenerateEmptyDocument.Text = "&Generate Empty Document";
            this.menuItemActionGenerateEmptyDocument.Click += new System.EventHandler(this.menuItemActionGenerateEmptyDocument_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 2;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemHelpAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Index = 0;
            this.menuItemHelpAbout.Text = "&About";
            this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                       this.btnSave,
                                                                                       this.btnSep1,
                                                                                       this.btnValidate,
                                                                                       this.btnGenerate,
                                                                                       this.btnSep2,
                                                                                       this.btnExit});
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(597, 29);
            this.toolBar.TabIndex = 7;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ImageIndex = 3;
            this.btnSave.ToolTipText = "Save";
            // 
            // btnSep1
            // 
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnValidate
            // 
            this.btnValidate.Enabled = false;
            this.btnValidate.ImageIndex = 1;
            this.btnValidate.ToolTipText = "Validate (F5)";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.ImageIndex = 2;
            this.btnGenerate.ToolTipText = "Generate Empty Document (CTRL-G)";
            // 
            // btnSep2
            // 
            this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 0;
            this.btnExit.ToolTipText = "Exit";
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // chkChoiceComments
            // 
            this.chkChoiceComments.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkChoiceComments.Checked = true;
            this.chkChoiceComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChoiceComments.Location = new System.Drawing.Point(161, 83);
            this.chkChoiceComments.Name = "chkChoiceComments";
            this.chkChoiceComments.Size = new System.Drawing.Size(223, 24);
            this.chkChoiceComments.TabIndex = 4;
            this.chkChoiceComments.Text = "Include Comments for Choice Elements";
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.Filter = "XML Files|*.xml|Text Files|*.txt";
            this.dlgFileSave.Title = "Save Results";
            // 
            // lblSchema
            // 
            this.lblSchema.Location = new System.Drawing.Point(5, 59);
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(97, 23);
            this.lblSchema.TabIndex = 8;
            this.lblSchema.Text = "Schema File Path:";
            this.lblSchema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSchemaFile
            // 
            this.txtSchemaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSchemaFile.Location = new System.Drawing.Point(105, 60);
            this.txtSchemaFile.Name = "txtSchemaFile";
            this.txtSchemaFile.ReadOnly = true;
            this.txtSchemaFile.Size = new System.Drawing.Size(404, 20);
            this.txtSchemaFile.TabIndex = 10;
            this.txtSchemaFile.Text = "";
            // 
            // btnSchemaBrowse
            // 
            this.btnSchemaBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSchemaBrowse.Enabled = false;
            this.btnSchemaBrowse.Location = new System.Drawing.Point(512, 59);
            this.btnSchemaBrowse.Name = "btnSchemaBrowse";
            this.btnSchemaBrowse.Size = new System.Drawing.Size(22, 23);
            this.btnSchemaBrowse.TabIndex = 11;
            this.btnSchemaBrowse.TabStop = false;
            this.btnSchemaBrowse.Text = "...";
            this.btnSchemaBrowse.Click += new System.EventHandler(this.btnSchemaBrowse_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(597, 387);
            this.Controls.Add(this.btnSchemaBrowse);
            this.Controls.Add(this.lblSchema);
            this.Controls.Add(this.chkChoiceComments);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.chkSampleData);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.btnFileBrowse);
            this.Controls.Add(this.lblSourceFilePath);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.cboSourceFilePath);
            this.Controls.Add(this.txtSchemaFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(500, 360);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "XML Wizard";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        #region Event Handlers
        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            int count = 0;
            int bigCount = 0;
            int leftMargin = e.MarginBounds.Left;
            float yPos = e.MarginBounds.Top;
            string line = null;
            Font printFont = new Font( "Arial", 10 );
            float fontHeight = 0;

            fontHeight = printFont.GetHeight( e.Graphics );

            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height / fontHeight;

            // Print each line of the file.
            while( count < Math.Round( linesPerPage, 0 ) && bigCount < txtResults.Lines.Length )
            {
                line = txtResults.Lines[bigCount];

                float rectHeight = (float)(Math.Ceiling( e.Graphics.MeasureString( line, printFont ).Width / 800 ) * fontHeight);

                e.Graphics.DrawString( line, printFont, Brushes.Black, new RectangleF( leftMargin, yPos, 800, rectHeight ), new StringFormat() );

                yPos += rectHeight;
                count++;
                bigCount++;
            }

            // add a pagebreak until the bigcounter reaches the upper limit of the textbox-line-count
            if( bigCount < txtResults.Lines.Length )
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void btnFileBrowse_Click(object sender, System.EventArgs e)
        {
            txtSchemaFile.Clear();

            dlgFileSelect.Filter = "XML & XSD Files|*.xml;*.xsd";
            dlgFileSelect.Title = "Select XML Schema or Document";
            dlgFileSelect.FileName = String.Empty;

            dlgFileSelect.ShowDialog( this );
            if( dlgFileSelect.FileName.Length > 0 )
            {
                cboSourceFilePath.Text = dlgFileSelect.FileName;
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            try 
            {
                RegistryKey hkcu = Registry.CurrentUser;

                RegistryKey appkey = hkcu.OpenSubKey( regKey );

                if( appkey != null )
                {
                    int left = (int)appkey.GetValue( "Left", this.Left );
                    int top = (int)appkey.GetValue( "Top", this.Top );
                    int width = (int)appkey.GetValue( "Width", this.Width );
                    int height = (int)appkey.GetValue( "Height", this.Height );

                    this.Location = new Point( left, top );
                    this.Size = new Size( width, height );

                    string [] keys = appkey.GetValueNames();

                    foreach( string key in keys )
                    {
                        if( key.StartsWith( "mru" ) )
                            cboSourceFilePath.Items.Add( appkey.GetValue( key ).ToString() );
                    }
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( "There was a problem getting your saved settings from the registry.\n\r\n\r" + ex.ToString(), this.Text );
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try 
            {
                RegistryKey hkcu = Registry.CurrentUser;

                RegistryKey appkey;

                appkey = hkcu.CreateSubKey( regKey );

                if( this.WindowState == FormWindowState.Normal )
                {
                    appkey.SetValue( "Left", this.Left );
                    appkey.SetValue( "Top", this.Top );
                    appkey.SetValue( "Width", this.Width );
                    appkey.SetValue( "Height", this.Height );
                }

                string [] keys = appkey.GetValueNames();

                foreach( string key in keys )
                {
                    if( key.StartsWith( "mru" ) )
                        appkey.DeleteValue( key );
                }

                for( int i = 0; i < cboSourceFilePath.Items.Count; i++ )
                    appkey.SetValue( String.Format( "mru{0:00}", i ), cboSourceFilePath.Items[i] );
            }
            catch( Exception ex )
            {
                MessageBox.Show( "There was a problem saving your settings to the registry.\n\r\n\r" + ex.ToString(), this.Text );
            }
        }

        private void menuItemFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void menuItemFilePrint_Click(object sender, System.EventArgs e)
        {
            pd.DefaultPageSettings.Margins = new Margins( 25, 25, 25, 25 );
            pd.Print();
            this.BringToFront();
            this.Focus();
        }

        private void menuItemActionValidate_Click(object sender, System.EventArgs e)
        {
            ValidateFile();
        }

        private void menuItemActionGenerateEmptyDocument_Click(object sender, System.EventArgs e)
        {
            GenerateDocument();
        }

        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if( e.Button == btnExit )
                this.Close();
            else if( e.Button == btnValidate )
                ValidateFile();
            else if( e.Button == btnGenerate )
                GenerateDocument();
            else if( e.Button == btnSave )
                SaveResults();
        }

        private void menuItemFileSave_Click(object sender, System.EventArgs e)
        {
            SaveResults();
        }
        
        private void txtResults_TextChanged(object sender, System.EventArgs e)
        {
            bool isEnabled = false;

            if( ((TextBox)sender).TextLength > 0 )
                isEnabled = true;

            menuItemFileSave.Enabled = isEnabled;
            menuItemFilePrint.Enabled = isEnabled;
            
            btnSave.Enabled = isEnabled;
        }

        private void btnSchemaBrowse_Click(object sender, System.EventArgs e)
        {
            SelectSchemaFile();
        }
        
        private void cboSourceFilePath_TextChanged(object sender, System.EventArgs e)
        {
            SetUIEnableState();
        }
        
        private void menuItemHelpAbout_Click(object sender, System.EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void cboSourceFilePath_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtSchemaFile.Clear();

            if( cboSourceFilePath.SelectedIndex > -1 )
            {
                if( Path.GetExtension( cboSourceFilePath.Text ).ToLower() == ".xml" )
                {
                    if( BrowseForSchema() )
                        SelectSchemaFile();
                }
            }
        }
        #endregion

        #region Private Methods
        private bool BrowseForSchema()
        {
            DialogResult result = MessageBox.Show(
                "Select XML schema file for XML document?",
                "Select Schema", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2 );

            return result == DialogResult.Yes;
        }

        private void HandleWebException( WebException ex )
        {
            const string msg1 = "The server returned a response of '{0}'.";
            const string msg2 = "Do you want to remove the file from the list?";
            string err = String.Empty;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1;

            HttpWebResponse response = (HttpWebResponse)ex.Response;

            if( response == null )
                err = String.Format( msg1, ex.Status );
            else
                err = String.Format( msg1, response.StatusDescription );

            if( IsItemInList( cboSourceFilePath.Text ) )
            {
                err = err + " " + msg2;
                buttons = MessageBoxButtons.YesNo;
                defaultButton = MessageBoxDefaultButton.Button2;
            }

            DialogResult msgResult = MessageBox.Show( 
                err, 
                "Resource Not Found", 
                buttons, 
                MessageBoxIcon.Question, 
                defaultButton );

            if( msgResult == DialogResult.Yes )
            {
                cboSourceFilePath.Items.Remove( cboSourceFilePath.Text );
                cboSourceFilePath.SelectedIndex = -1;

                SetUIEnableState();
            }
        }

        private void HandleFileNotFoundException()
        {
            string msg1 = "The file {0} cannot be found.";
            const string msg2 = "It will be removed from the list.";

            if( IsItemInList( cboSourceFilePath.Text ) )
                msg1 = msg1 + " " + msg2;

            MessageBox.Show( String.Format( msg1, cboSourceFilePath.Text ), "File Not Found" );

            cboSourceFilePath.Items.Remove( cboSourceFilePath.Text );
            cboSourceFilePath.SelectedIndex = -1;

            SetUIEnableState();
        }

        private bool IsItemInList( object item )
        {
            bool found = false;

            string itemText = item.ToString();

            foreach( object listItem in cboSourceFilePath.Items )
            {
                if( itemText == listItem.ToString() )
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        private void AddItemToList()
        {
            bool found = false;

            if( cboSourceFilePath.Text.Length > 0 )
            {
                found = IsItemInList( cboSourceFilePath.Text );

                if( !found )
                    cboSourceFilePath.Items.Add( cboSourceFilePath.Text );
            }
        }

        private void ValidateFile()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                if( Path.GetExtension( cboSourceFilePath.Text ).ToLower() == ".xsd" )
                {
                    ValidateSchema();
                    txtResults.Text = "Schema " + cboSourceFilePath.Text + " validated successfully.";
                }
                else if( Path.GetExtension( cboSourceFilePath.Text ).ToLower() == ".xml" )
                {
                    ValidateDocument();
                    txtResults.Text = "Document " + cboSourceFilePath.Text + " validated successfully.";
                }
                else
                {
                    MessageBox.Show( "Selected file is not an XML document (.xml) or schema (.xsd)", "Invalid File Specified" );
                }

                AddItemToList();
            }
            catch( WebException ex )
            {
                HandleWebException( ex );
            }
            catch( FileNotFoundException )
            {
                HandleFileNotFoundException();
            }
            catch( Exception ex )
            {
                txtResults.Text = ex.ToString();
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void SetUIEnableState()
        {
            string extension = Path.GetExtension( cboSourceFilePath.Text ).ToLower();

            bool isXml = extension == ".xml";
            bool isXsd = extension == ".xsd";

            menuItemActionValidate.Enabled = isXml || isXsd;
            menuItemActionGenerateEmptyDocument.Enabled = isXsd;

            btnValidate.Enabled = isXml || isXsd;
            btnGenerate.Enabled = isXsd;
            btnSchemaBrowse.Enabled = isXml;
        }

        private void GenerateDocument()
        {
            Validator validator = null;
            string rootNodeName = null;
            Stream schemaStream = null;

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                if( cboSourceFilePath.Text.EndsWith( ".xsd" ) )
                {
                    statusBar.Panels[0].Text = "Validating schema...";

                    ValidateSchema();

                    statusBar.Panels[0].Text = "Loading schema...";

                    if( cboSourceFilePath.Text.ToLower().StartsWith( "http://" ) || cboSourceFilePath.Text.ToLower().StartsWith( "file://" ) )
                        validator = new ValidatorUri();
                    else
                        validator = new ValidatorFilepath();

                    schemaStream = validator.GetSchemaStream( cboSourceFilePath.Text );
                    XmlSchema schema = XmlSchema.Read( schemaStream, null );

                    XmlSchemaUtility util = new XmlSchemaUtility();

                    statusBar.Panels[0].Text = "Loading schema root node names...";

                    string [] elementNames = util.GetRootNodeNames( schema );

                    statusBar.Panels[0].Text = "Ready";

                    if( elementNames.Length > 0 )
                    {
                        if( elementNames.Length > 1 )
                        {
                            ChooseRootNode chooser = new ChooseRootNode( elementNames );
                            DialogResult result = chooser.ShowDialog( this );

                            if( result == DialogResult.OK )
                            {
                                rootNodeName = chooser.RootNodeName;
                                chooser.Dispose();
                            }
                        }
                        else
                        {
                            rootNodeName = elementNames[0];
                        }

                        statusBar.Panels[0].Text = "Generating XML document...";

                        txtResults.Text = util.GetXmlString( schema, rootNodeName, chkSampleData.Checked, chkChoiceComments.Checked );

                        AddItemToList();
                    }
                    else
                    {
                        MessageBox.Show( "Schema has no root nodes.", "Invalid Schema" );
                    }
                }
                else
                {
                    MessageBox.Show( "Selected file is not an XML schema (.xsd)", "Invalid File Specified" );
                }
            }
            catch( FileNotFoundException )
            {
                HandleFileNotFoundException();
            }
            catch( Exception ex )
            {
                txtResults.Text = ex.ToString();
            }
            finally
            {
                if( schemaStream != null )
                    schemaStream.Close();

                statusBar.Panels[0].Text = "Ready";
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void SaveResults()
        {
            StreamWriter writer = null;

            try
            {
                DialogResult result = dlgFileSave.ShowDialog();

                if( result == DialogResult.OK )
                {
                    writer = new StreamWriter( dlgFileSave.FileName );
                    writer.Write( txtResults.Text );
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString() );
            }
            finally
            {
                if( writer != null )
                    writer.Close();
            }
        }

        private void ValidateDocument()
        {
            if( txtSchemaFile.TextLength > 0 )
                ValidateDocumentWithExternalSchema();
            else
                ValidateDocumentNoExternalSchema();
        }

        private void ValidateDocumentWithExternalSchema()
        {
            string xml = String.Empty;
            string xsd = String.Empty;

            StreamReader xmlReader = null;
            StreamReader xsdReader = null;

            try
            {
                xmlReader = new StreamReader( cboSourceFilePath.Text );
                xml = xmlReader.ReadToEnd();

                xsdReader = new StreamReader( txtSchemaFile.Text );
                xsd = xsdReader.ReadToEnd();
            }
            finally
            {
                if( xmlReader != null )
                    xmlReader.Close();

                if( xsdReader != null )
                    xsdReader.Close();
            }

            XmlSchemaUtility util = new XmlSchemaUtility();
            util.ValidateXml( xml, xsd );
        }

        private void ValidateDocumentNoExternalSchema()
        {
            XmlDocument xmlDocument;
            bool schemaLocationFound = false;

            // Load the incoming XML document
            xmlDocument = new XmlDocument();
            xmlDocument.Load( cboSourceFilePath.Text );

            // Look for the schemaLocation attribute on the root node
            // of the document. The assumption is that every document
            // will supply this attribute and will use a URI to reference
            // the schema, for example:
            //  file:///D://Development/XML/plandata.xsd 
            //  or
            //  file:////somemachine/someshare/plandata.xsd 
            //  or
            //  http://wagner.com/schemas/plandata.xsd
            XmlAttributeCollection attributes = xmlDocument.DocumentElement.Attributes;

            foreach( XmlAttribute attribute in attributes )
            {
                if( attribute.LocalName == "schemaLocation" && attribute.NamespaceURI == "http://www.w3.org/2001/XMLSchema-instance" ||
                    attribute.LocalName == "noNamespaceSchemaLocation" )
                {
                    schemaLocationFound = true;
                    break;
                }
            }

            if( schemaLocationFound )
            {
                XmlSchemaUtility util = new XmlSchemaUtility();

                util.ValidateXmlFile( cboSourceFilePath.Text );
            }
            else
            {
                MessageBox.Show( "Selected document does not contain a schemaLocation attribute on the root node so it cannot be validated.", "schemaLocation Missing" );
            }
        }

        private void ValidateSchema()
        {
            Validator validator = null;

            if( cboSourceFilePath.Text.ToLower().StartsWith( "http://" ) || cboSourceFilePath.Text.ToLower().StartsWith( "file://" ) )
                validator = new ValidatorUri();
            else
                validator = new ValidatorFilepath();

            validator.ValidateSchema( cboSourceFilePath.Text );
        }

        private void SelectSchemaFile()
        {
            dlgFileSelect.Filter = "XSD Files|*.xsd";
            dlgFileSelect.Title = "Select XML Schema";
            dlgFileSelect.FileName = String.Empty;

            dlgFileSelect.ShowDialog( this );
            if( dlgFileSelect.FileName.Length > 0 )
            {
                txtSchemaFile.Text = dlgFileSelect.FileName;
            }
        }
        #endregion
    }
}
