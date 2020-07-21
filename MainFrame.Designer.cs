namespace bsw_generation
{
    partial class MainFrame
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitCtrlXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfigurationPathSet = new System.Windows.Forms.ToolStripMenuItem();
            this.setPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generationGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComTab = new System.Windows.Forms.TreeView();
            this.COMParameterProperty = new System.Windows.Forms.PropertyGrid();
            this.ObjectDel = new System.Windows.Forms.Button();
            this.ObjectAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TPParameterProperty = new System.Windows.Forms.PropertyGrid();
            this.TpTab = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.menuConfigurationPathSet,
            this.generationGToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1254, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitCtrlXToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.fileFToolStripMenuItem.Text = "File(F)";
            this.fileFToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitCtrlXToolStripMenuItem
            // 
            this.exitCtrlXToolStripMenuItem.Name = "exitCtrlXToolStripMenuItem";
            this.exitCtrlXToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.exitCtrlXToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitCtrlXToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.exitCtrlXToolStripMenuItem.Text = "Exit";
            // 
            // menuConfigurationPathSet
            // 
            this.menuConfigurationPathSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPathToolStripMenuItem});
            this.menuConfigurationPathSet.Name = "menuConfigurationPathSet";
            this.menuConfigurationPathSet.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.menuConfigurationPathSet.Size = new System.Drawing.Size(136, 24);
            this.menuConfigurationPathSet.Text = "Configuration(C)";
            // 
            // setPathToolStripMenuItem
            // 
            this.setPathToolStripMenuItem.Name = "setPathToolStripMenuItem";
            this.setPathToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.setPathToolStripMenuItem.Text = "Path Set...";
            this.setPathToolStripMenuItem.Click += new System.EventHandler(this.menuConfigurationPathSet_Click);
            // 
            // generationGToolStripMenuItem
            // 
            this.generationGToolStripMenuItem.Name = "generationGToolStripMenuItem";
            this.generationGToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.generationGToolStripMenuItem.Text = "Generation";
            this.generationGToolStripMenuItem.Click += new System.EventHandler(this.generationGToolStripMenuItem_Click);
            // 
            // ComTab
            // 
            this.ComTab.BackColor = System.Drawing.Color.White;
            this.ComTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComTab.Location = new System.Drawing.Point(3, 3);
            this.ComTab.Name = "ComTab";
            this.ComTab.Size = new System.Drawing.Size(240, 611);
            this.ComTab.TabIndex = 1;
            this.ComTab.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.COMTreeViewInfoAfterSelect);
            // 
            // COMParameterProperty
            // 
            this.COMParameterProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.COMParameterProperty.Location = new System.Drawing.Point(249, 3);
            this.COMParameterProperty.Name = "COMParameterProperty";
            this.COMParameterProperty.Size = new System.Drawing.Size(571, 611);
            this.COMParameterProperty.TabIndex = 2;
            this.COMParameterProperty.ToolbarVisible = false;
            this.COMParameterProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.COMParameterPropertyValueChanged);
            // 
            // ObjectDel
            // 
            this.ObjectDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ObjectDel.Location = new System.Drawing.Point(160, 27);
            this.ObjectDel.Name = "ObjectDel";
            this.ObjectDel.Size = new System.Drawing.Size(120, 49);
            this.ObjectDel.TabIndex = 4;
            this.ObjectDel.Text = "DEL";
            this.ObjectDel.UseVisualStyleBackColor = true;
            this.ObjectDel.Click += new System.EventHandler(this.object_add_Click);
            // 
            // ObjectAdd
            // 
            this.ObjectAdd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ObjectAdd.Location = new System.Drawing.Point(15, 27);
            this.ObjectAdd.Name = "ObjectAdd";
            this.ObjectAdd.Size = new System.Drawing.Size(120, 49);
            this.ObjectAdd.TabIndex = 5;
            this.ObjectAdd.Text = "ADD";
            this.ObjectAdd.UseVisualStyleBackColor = true;
            this.ObjectAdd.Click += new System.EventHandler(this.object_add_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ObjectAdd);
            this.groupBox1.Controls.Add(this.ObjectDel);
            this.groupBox1.Location = new System.Drawing.Point(891, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message / Signal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1254, 646);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ComTab);
            this.tabPage1.Controls.Add(this.COMParameterProperty);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1246, 617);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "COM";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TPParameterProperty);
            this.tabPage2.Controls.Add(this.TpTab);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1246, 617);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TPParameterProperty
            // 
            this.TPParameterProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.TPParameterProperty.Location = new System.Drawing.Point(249, 3);
            this.TPParameterProperty.Name = "TPParameterProperty";
            this.TPParameterProperty.Size = new System.Drawing.Size(571, 611);
            this.TPParameterProperty.TabIndex = 3;
            this.TPParameterProperty.ToolbarVisible = false;
            // 
            // TpTab
            // 
            this.TpTab.BackColor = System.Drawing.Color.White;
            this.TpTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.TpTab.Location = new System.Drawing.Point(3, 3);
            this.TpTab.Name = "TpTab";
            this.TpTab.Size = new System.Drawing.Size(240, 611);
            this.TpTab.TabIndex = 2;
            this.TpTab.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TPTreeViewInfoAfterSelect);
            // 
            // MainFrame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1254, 674);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrame";
            this.Text = "MainFrame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InitialLayerModuleParameter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitCtrlXToolStripMenuItem;
        private System.Windows.Forms.TreeView ComTab;
        private System.Windows.Forms.PropertyGrid COMParameterProperty;
        private System.Windows.Forms.Button ObjectDel;
        private System.Windows.Forms.Button ObjectAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem menuConfigurationPathSet;
        private System.Windows.Forms.ToolStripMenuItem setPathToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem generationGToolStripMenuItem;
        private System.Windows.Forms.TreeView TpTab;
        private System.Windows.Forms.PropertyGrid TPParameterProperty;
    }
}

