using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.MenuFrame.ConfigurationMenu
{
    public partial class ConfigureationPathDialog : Form
    {
        private string generationFilePath;
        private string databaseFilePath;
        private FolderBrowserDialog generationFolderPathDialog = new FolderBrowserDialog();
        private OpenFileDialog databaseFilePathDialog = new OpenFileDialog();
        private string selectNodeName = "";

        public string GenerationFilePath
        {
            get { return generationFilePath; }
            set { generationFilePath = value; }
        }

        public string DataBaseFilePath
        {
            get { return databaseFilePath; }
            set { databaseFilePath = value; }
        }

        public string SelectNodeName
        {
            get { return selectNodeName; }
            set { selectNodeName = value; }
        }

        public ConfigureationPathDialog()
        {
            InitializeComponent();
            generationFilePath = @"C:\";
            databaseFilePath = "";
            this.GenerationPathText.Text = generationFilePath;
            this.DataBasePathText.Text = databaseFilePath;
            generationFolderPathDialog.SelectedPath = @"C:\";
            databaseFilePathDialog.InitialDirectory = @"C:\";
            databaseFilePathDialog.Filter = "dbc files (*.dbc)|*.dbc|All files (*.*)|*.*";
        }
        
        public void ClearConfigurationPath()
        {
            generationFilePath = @"C:\";
            databaseFilePath = "";
            selectNodeName = "";

            generationFolderPathDialog.SelectedPath = @"C:\";
            databaseFilePathDialog.InitialDirectory = @"C:\";
            
            this.GenerationPathText.Text = generationFilePath;
            this.DataBasePathText.Text = databaseFilePath;
            this.DatabaseNode_Combo.Text = selectNodeName;
            //this.DatabaseNode_Combo.TextUpdate = selectNodeName;
            MessageBox.Show(string.Format("Generaiton Path = {0}", generationFilePath));
            MessageBox.Show(string.Format("DataBasePathText Path = {0}", databaseFilePath));
            MessageBox.Show(string.Format("DatabaseNode_Combo Path = {0}", selectNodeName));
        }
        public void UpdateGenerationFilePath(string generationPath, string databasePath, string selectNode)
        {
            generationFilePath = generationPath;
            databaseFilePath = databasePath;
            selectNodeName = selectNode;

            if ((this.DatabaseNode_Combo.Items.Count == 0) && (databaseFilePath != ""))
            {
                if (File.Exists(databasePath))
                {
                    string line_string;
                    FileStream databaseFile = new FileStream(databaseFilePath, FileMode.Open, FileAccess.Read);
                    StreamReader databaseRead = new StreamReader(databaseFile);
                    char[] filter = { ' ' };
                    //Node Parsing
                    while (databaseRead.EndOfStream == false)
                    {
                        line_string = databaseRead.ReadLine();
                        line_string = line_string.Replace(":", " ");
                        string[] result = line_string.Split(filter, StringSplitOptions.RemoveEmptyEntries);
                        if (result.Length > 0)
                        {

                            if (result[0] == "BU_")
                            {
                                int command_index = 0;

                                for (command_index = 1; command_index < result.Length; command_index++)
                                {
                                    DatabaseNode_Combo.Items.Add(result[command_index]);
                                }

                                break;
                            }
                        }
                        else
                        {
                            ;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please check the database file path");
                }
            }
            this.GenerationPathText.Text = generationPath;
            this.DataBasePathText.Text = databasePath;
            this.DatabaseNode_Combo.Text = selectNodeName;
            this.DatabaseNode_Combo.Items.Clear();

        }

        private void GenerationFileBrowse_Click(object sender, EventArgs e)
        {
            DialogResult file_path_result = generationFolderPathDialog.ShowDialog();
            
            if (file_path_result == DialogResult.OK)
            {
                generationFilePath = generationFolderPathDialog.SelectedPath;
                this.GenerationPathText.Text = generationFilePath;
            }           
            
        }


        private void DataBaseFileBrowse_Click(object sender, EventArgs e)
        {
            DialogResult file_path_result = databaseFilePathDialog.ShowDialog();
            
            if (file_path_result == DialogResult.OK)
            {
                string line_string;
                FileStream databaseFile = new FileStream(databaseFilePathDialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader databaseRead = new StreamReader(databaseFile);
                databaseFilePath = databaseFilePathDialog.FileName;
                //System.Console.WriteLine(databaseFilePath);
                this.DataBasePathText.Text = databaseFilePath;
                char[] filter = {' '};
                //Node Parsing
                DatabaseNode_Combo.Items.Clear();
                while (databaseRead.EndOfStream == false)
                {
                    line_string = databaseRead.ReadLine();
                    line_string = line_string.Replace(":", " ");
                    string[] result = line_string.Split(filter, StringSplitOptions.RemoveEmptyEntries);
                    if (result.Length > 0)
                    {

                        if (result[0] == "BU_")
                        {
                            int command_index = 0;

                            for (command_index = 1; command_index < result.Length; command_index++)
                            {
                                DatabaseNode_Combo.Items.Add(result[command_index]);
                            }

                            break;
                        }
                    }
                    else
                    {
                        ;
                    }
                }
            }        
        }

        private void FilePathOK_Click(object sender, EventArgs e)
        {
            if (selectNodeName == "")
            {
                MessageBox.Show("Please Select a Node");
                   
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            
        }


        private void FilePathCANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DBNodeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectNodeName  = DatabaseNode_Combo.SelectedItem as string;

        }



    }

}
