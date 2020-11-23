namespace bsw_generation.MenuFrame.ConfigurationMenu
{
    partial class ConfigureationPathDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GenerationPathDialogLabel = new System.Windows.Forms.Label();
            this.GenerationFileBrowse = new System.Windows.Forms.Button();
            this.GenerationPathText = new System.Windows.Forms.TextBox();
            this.FilePathOK = new System.Windows.Forms.Button();
            this.FilePathCANCEL = new System.Windows.Forms.Button();
            this.DataBasePathText = new System.Windows.Forms.TextBox();
            this.DataBaseFileBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DatabaseNode_Combo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GenerationPathDialogLabel
            // 
            this.GenerationPathDialogLabel.AutoSize = true;
            this.GenerationPathDialogLabel.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GenerationPathDialogLabel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.GenerationPathDialogLabel.Location = new System.Drawing.Point(29, 11);
            this.GenerationPathDialogLabel.Name = "GenerationPathDialogLabel";
            this.GenerationPathDialogLabel.Size = new System.Drawing.Size(153, 19);
            this.GenerationPathDialogLabel.TabIndex = 0;
            this.GenerationPathDialogLabel.Text = "Generation Path";
            // 
            // GenerationFileBrowse
            // 
            this.GenerationFileBrowse.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GenerationFileBrowse.Location = new System.Drawing.Point(506, 39);
            this.GenerationFileBrowse.Name = "GenerationFileBrowse";
            this.GenerationFileBrowse.Size = new System.Drawing.Size(100, 30);
            this.GenerationFileBrowse.TabIndex = 1;
            this.GenerationFileBrowse.Text = "Browse";
            this.GenerationFileBrowse.UseVisualStyleBackColor = true;
            this.GenerationFileBrowse.Click += new System.EventHandler(this.GenerationFileBrowse_Click);
            // 
            // GenerationPathText
            // 
            this.GenerationPathText.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GenerationPathText.Location = new System.Drawing.Point(32, 40);
            this.GenerationPathText.Multiline = true;
            this.GenerationPathText.Name = "GenerationPathText";
            this.GenerationPathText.Size = new System.Drawing.Size(439, 28);
            this.GenerationPathText.TabIndex = 2;
            // 
            // FilePathOK
            // 
            this.FilePathOK.Location = new System.Drawing.Point(438, 223);
            this.FilePathOK.Name = "FilePathOK";
            this.FilePathOK.Size = new System.Drawing.Size(88, 34);
            this.FilePathOK.TabIndex = 3;
            this.FilePathOK.Text = "OK";
            this.FilePathOK.UseVisualStyleBackColor = true;
            this.FilePathOK.Click += new System.EventHandler(this.FilePathOK_Click);
            // 
            // FilePathCANCEL
            // 
            this.FilePathCANCEL.Location = new System.Drawing.Point(543, 223);
            this.FilePathCANCEL.Name = "FilePathCANCEL";
            this.FilePathCANCEL.Size = new System.Drawing.Size(88, 34);
            this.FilePathCANCEL.TabIndex = 4;
            this.FilePathCANCEL.Text = "CANCEL";
            this.FilePathCANCEL.UseVisualStyleBackColor = true;
            this.FilePathCANCEL.Click += new System.EventHandler(this.FilePathCANCEL_Click);
            // 
            // DataBasePathText
            // 
            this.DataBasePathText.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DataBasePathText.Location = new System.Drawing.Point(34, 135);
            this.DataBasePathText.Multiline = true;
            this.DataBasePathText.Name = "DataBasePathText";
            this.DataBasePathText.Size = new System.Drawing.Size(439, 28);
            this.DataBasePathText.TabIndex = 7;
            // 
            // DataBaseFileBrowse
            // 
            this.DataBaseFileBrowse.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DataBaseFileBrowse.Location = new System.Drawing.Point(508, 134);
            this.DataBaseFileBrowse.Name = "DataBaseFileBrowse";
            this.DataBaseFileBrowse.Size = new System.Drawing.Size(100, 30);
            this.DataBaseFileBrowse.TabIndex = 6;
            this.DataBaseFileBrowse.Text = "Browse";
            this.DataBaseFileBrowse.UseVisualStyleBackColor = true;
            this.DataBaseFileBrowse.Click += new System.EventHandler(this.DataBaseFileBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(31, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "DataBase Path";
            // 
            // DatabaseNode_Combo
            // 
            this.DatabaseNode_Combo.FormattingEnabled = true;
            this.DatabaseNode_Combo.Location = new System.Drawing.Point(33, 219);
            this.DatabaseNode_Combo.Name = "DatabaseNode_Combo";
            this.DatabaseNode_Combo.Size = new System.Drawing.Size(121, 23);
            this.DatabaseNode_Combo.TabIndex = 8;
            this.DatabaseNode_Combo.SelectedIndexChanged += new System.EventHandler(this.DBNodeSelect_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(31, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Database Node";
            // 
            // ConfigureationPathDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(653, 269);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DatabaseNode_Combo);
            this.Controls.Add(this.DataBasePathText);
            this.Controls.Add(this.DataBaseFileBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilePathCANCEL);
            this.Controls.Add(this.FilePathOK);
            this.Controls.Add(this.GenerationPathText);
            this.Controls.Add(this.GenerationFileBrowse);
            this.Controls.Add(this.GenerationPathDialogLabel);
            this.Name = "ConfigureationPathDialog";
            this.Text = "Generation_path_dialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GenerationPathDialogLabel;
        private System.Windows.Forms.Button GenerationFileBrowse;
        private System.Windows.Forms.TextBox GenerationPathText;
        private System.Windows.Forms.Button FilePathOK;
        private System.Windows.Forms.Button FilePathCANCEL;
        private System.Windows.Forms.TextBox DataBasePathText;
        private System.Windows.Forms.Button DataBaseFileBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DatabaseNode_Combo;
        private System.Windows.Forms.Label label2;
    }
}