namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    #endregion

    /// <summary>
	/// Dialog that allows the user to select which node they want to generate
	/// from the schema.
	/// </summary>
	public class ChooseRootNode : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ComboBox cboRootNodeNames;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblChooseNode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public string RootNodeName
        {
            get { return cboRootNodeNames.SelectedItem.ToString(); }
        }

		private ChooseRootNode()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

        public ChooseRootNode( string [] rootNodeNames ) : this()
        {
            cboRootNodeNames.Items.AddRange( rootNodeNames );
            cboRootNodeNames.SelectedIndex = 0;
            cboRootNodeNames.MaxDropDownItems = rootNodeNames.Length > 10 ? 10 : rootNodeNames.Length;
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
            this.cboRootNodeNames = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblChooseNode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboRootNodeNames
            // 
            this.cboRootNodeNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRootNodeNames.Location = new System.Drawing.Point(149, 13);
            this.cboRootNodeNames.Name = "cboRootNodeNames";
            this.cboRootNodeNames.Size = new System.Drawing.Size(251, 21);
            this.cboRootNodeNames.Sorted = true;
            this.cboRootNodeNames.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(125, 47);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(211, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // lblChooseNode
            // 
            this.lblChooseNode.Location = new System.Drawing.Point(11, 12);
            this.lblChooseNode.Name = "lblChooseNode";
            this.lblChooseNode.Size = new System.Drawing.Size(145, 23);
            this.lblChooseNode.TabIndex = 0;
            this.lblChooseNode.Text = "Choose Node to Generate:";
            this.lblChooseNode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChooseRootNode
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(411, 84);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.btnCancel,
                                                                          this.btnOK,
                                                                          this.cboRootNodeNames,
                                                                          this.lblChooseNode});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChooseRootNode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Root Node";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
