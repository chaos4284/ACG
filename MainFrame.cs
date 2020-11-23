using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bsw_generation.LayerTab.TPTab;
using bsw_generation.LayerTab.ComTab;
using bsw_generation.CodeGeneration;
using bsw_generation.MenuFrame.ConfigurationMenu;
namespace bsw_generation
{
    public partial class MainFrame : Form
    {
        /*Interaction Layer */
        ComManagement COMManagement = new ComManagement();
        TPLayerManagement TPManagement = new TPLayerManagement();

        /*Menu Item Start*/
        ConfigureationPathDialog filePathDialog = new ConfigureationPathDialog();
        bsw_generation.MenuFrame.FileMenu.FileMenuOperation fileMenuOperation = new bsw_generation.MenuFrame.FileMenu.FileMenuOperation();
        /*Menu Item End*/
        bsw_generation.DatabaseParser.CAN.DatabaseParserClass dbParser = new bsw_generation.DatabaseParser.CAN.DatabaseParserClass();

        private string generationPath;
        private string databasePath;
        private string selectNode;
        
        public string GenerationPathSet
        {
            get { return generationPath; }
            set { generationPath = value; }
        }

        public string DatabasePathSet
        {
            get { return databasePath; }
            set { databasePath = value; }
        }

        public MainFrame()
        {
            InitializeComponent();
        }

        private void initLayerModule()
        {
            dbParser.parser_msg_info.Clear();
            dbParser.parser_sig_info.Clear();
            dbParser.result_parser_msg_info.Clear();
            dbParser.result_parser_sig_info.Clear();
            COMManagement.InitialLLayer();
            TPManagement.InitialTPLayer();
           
        }

        /*TreeView를 초기상태로 변경한다. */
        private void restartLayerModule()
        {
            COMManagement.restartlLLayer();
            TPManagement.restartTPLayer();
        }

        private void pathUpdate()
        {
            //각종 PATH 업데이트
            databasePath = filePathDialog.DataBaseFilePath;// DB파일 경로 업데이트
            generationPath = filePathDialog.GenerationFilePath; // 생성파일 경로 업데이트
            selectNode = filePathDialog.SelectNodeName; //선택노드 업데이트
        }


        
        /*코드 생성전 조건을 체크 한다. */
        private Boolean checkGenerateCondition()
        {
            Boolean ret = true;

            if (COMManagement.checkCOMGenerationCondition() == false)
            {
                ret = false;
            }

            return ret;
        }

        /*Load the initial tree node. */
        private void InitialLayerModuleParameter(object sender, System.EventArgs e)
        {
            /*COM Module Init */
            //COMManagement.

            COMManagement.ComDataGrid = dataGridView1;
            COMManagement.MainTree = ComTab; // ComTab: Tree of COM TAB in Desiner 
            COMManagement.IlProperty = COMParameterProperty;
            COMManagement.IlProperty.PropertySort = PropertySort.Categorized;
            COMManagement.InitialLLayer();

            COMManagement.GeneralTree = ComTab.Nodes.Add("General");//ComTab.Nodes.Add("General"); Generate General Tree
            COMManagement.MessageObjectTree = ComTab.Nodes.Add("Message Object");
            COMManagement.SendMessageTree  = COMManagement.MessageObjectTree.Nodes.Add("SendMessage");
            COMManagement.ReceiveMessageTree = COMManagement.MessageObjectTree.Nodes.Add("ReceiveMessage");


            /*TP Module Init */
            TPManagement.MainTree = TpTab;
            TPManagement.TPProperty = TPParameterProperty;
            TPManagement.TPProperty.PropertySort = PropertySort.Categorized;

            TPManagement.CommonTree = TpTab.Nodes.Add("Common");
            TPManagement.ConnectionTree = TpTab.Nodes.Add("TPConnectionList");
            /*NM Module Init */

            /*General Path Init */
            generationPath = filePathDialog.GenerationFilePath;
            databasePath = "";
            selectNode = "";
        }


        /*COM Mouse and Keyboard 클릭시 정보 출력하는 루틴 */
        private void COMTreeViewInfoAfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((e.Action == TreeViewAction.ByMouse) || (e.Action == TreeViewAction.ByKeyboard))
            {
                COMManagement.COMPropertyDisplay(e.Node.Text);       
            }
        }

        private void TPTreeViewInfoAfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((e.Action == TreeViewAction.ByMouse) || (e.Action == TreeViewAction.ByKeyboard))
            {
                TPManagement.TPPropertyDisplay(e.Node.Text);
            }
        }

        /*Add button clock,Message and Signal */
        private void object_add_Click(object sender, EventArgs e)
        {
/*
            switch(bswItem.SelectedNode.Text)
            {
                case "General":
                    MessageBox.Show("Not met addition condition");
                    break;

                case "Message Object":
                    MessageBox.Show("Not met addition condition");
                    break;

                case "SendMessage":
                    COMMessageAttributesInformation send_message_object = new COMMessageAttributesInformation();
                    send_message_object.Handle = send_msg_handle_index;
                    send_message_object.MsgName = String.Format("SendMessage{0}", send_msg_handle_index);
                    totalMessageInfo.AddLast(send_message_object);
                    COMManagement.SendMessageTree .Nodes.Add(send_message_object.MsgName);
                    send_msg_handle_index++;
                    break;

                case "ReceiveMessage":
                    COMMessageAttributesInformation receive_message_object = new COMMessageAttributesInformation();
                    receive_message_object.Handle = receive_msg_handle_index;
                    receive_message_object.MsgName = String.Format("ReceiveMessage{0}", receive_msg_handle_index);
                    receive_message_object.Direction = "RECEIVE";
                    totalMessageInfo.AddLast(receive_message_object);
                    COMManagement.ReceiveMessageTree.Nodes.Add(receive_message_object.MsgName);
                    receive_msg_handle_index++;

                    break;

                default:
                    if (bswItem.SelectedNode.Parent.Text == "SendMessage")
                    {
                        SignalAttributesInformation send_signal_object = new SignalAttributesInformation();
                        send_signal_object.Handle = send_sig_handle_index;
                        send_signal_object.SignalName = String.Format("SendSignal{0}", send_sig_handle_index);
                        totalSignalInfo.AddLast(send_signal_object);
                        bswItem.SelectedNode.Nodes.Add(send_signal_object.SignalName);
                        send_sig_handle_index++;
                    }
                    else if (bswItem.SelectedNode.Parent.Text == "ReceiveMessage")
                    {
                        SignalAttributesInformation receive_signal_object = new SignalAttributesInformation();
                        receive_signal_object.Handle = receive_sig_handle_index;
                        receive_signal_object.SignalName = String.Format("ReceiveSignal{0}", receive_sig_handle_index);
                        totalSignalInfo.AddLast(receive_signal_object);
                        bswItem.SelectedNode.Nodes.Add(receive_signal_object.SignalName);
                        receive_sig_handle_index++;
                    }
                    break;
            }
 */
        }
        /*delete button click, Message and signal */
        private void object_delete_Click(object sender, EventArgs e)
        {
/*            
            if (check_deletion_condition_tree_node(bswItem.SelectedNode.Text) == true)
            {
                bswItem.SelectedNode.Nodes.Remove(bswItem.SelectedNode);
                if ((bswItem.SelectedNode.Parent.Text == "SendMessage") || (bswItem.SelectedNode.Parent.Text == "ReceiveMessage"))
                {
                    var messageNode = totalMessageInfo.First;
                    while (messageNode != null)
                    {
                        if (messageNode.Value.MsgName == current_message_selected_node)
                        {
                            totalMessageInfo.Remove(messageNode);
                            break;
                        }
                        else
                        {
                            messageNode = messageNode.Next;
                        }
                    }
                }
                else
                {

                }

                MessageBox.Show("The node deletion was successful.");
            }
            else
            {
                MessageBox.Show("Parent node cannot not be delete");
            }
 */
        }

        /*메시지 혹은 시그널 속성 변경시  */
        private void COMParameterPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            COMManagement.comPropertyChangedValue(e.ChangedItem.Label, e.ChangedItem.Value, e.OldValue);
        }

        
////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////Menu Process /////////////////////////////////////
        /*Configure -> path set menu process */
        private void menuConfigurationPathSet_Click(object sender, EventArgs e)
        {
            DialogResult result = filePathDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (filePathDialog.DataBaseFilePath == "")
                {
                    MessageBox.Show("The database path is not set");
                }
                else if (filePathDialog.GenerationFilePath == "")
                {
                    MessageBox.Show("The generation path is not set");
                }
                else
                {
                    initLayerModule();
                    restartLayerModule();
                    pathUpdate();

                    /*DBC를 기준으로 모든 메시지 및 시그널 값을 파싱해서 가져온다. */
                    dbParser.process_database_parser(databasePath, filePathDialog.SelectNodeName);

                    /*전체 메시지 보관 */

                    /*모든 COM 구별없이 모든 메시지 업데이트 */
                    COMManagement.UpdateCOMDbToData(dbParser.result_parser_msg_info, dbParser.result_parser_sig_info);
                    /*모든 TP Connection정보 업데이트 */
                    TPManagement.UpdateDbToTP(dbParser.result_parser_msg_info, dbParser.result_parser_sig_info);
                     
                }

            } 
            else if (result == DialogResult.Cancel)
            {
                /*파일 경로를 이전 경로로 업데이트 한다. */
                filePathDialog.UpdateGenerationFilePath(generationPath, databasePath, selectNode);
            }
        }


        /*File Menu -> New */
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            DialogResult selectSaveConfigureation;
            SaveFileDialog saveConfigurationFilePathDialog;
            DialogResult saveConfigurationResult;

            string configurationFilePath = "";
            saveConfigurationFilePathDialog = new SaveFileDialog();
            saveConfigurationFilePathDialog.FileName = "test.cfg";
            saveConfigurationFilePathDialog.InitialDirectory = @"D:\";
            saveConfigurationFilePathDialog.Filter = "configuration files (*.cfg)|*.cfg";

            selectSaveConfigureation = MessageBox.Show("Save Configuration?", "New",MessageBoxButtons.OKCancel);

            if (selectSaveConfigureation == DialogResult.OK)
            {
                saveConfigurationResult = saveConfigurationFilePathDialog.ShowDialog();

                if (saveConfigurationResult == DialogResult.OK)
                {
                    configurationFilePath = saveConfigurationFilePathDialog.FileName;
                    fileMenuOperation.savePathInfo(configurationFilePath, filePathDialog.GenerationFilePath, filePathDialog.SelectNodeName, filePathDialog.DataBaseFilePath);

                    //COM 메시지 정보, 시그널정보 및 일반 옵션정보를 전달한다.
                    fileMenuOperation.ILSaveConfigurationFile(configurationFilePath, COMManagement.AllMessageInfo, COMManagement.AllSingalInfo, COMManagement.GeneralInfo);
                    initLayerModule();
                    restartLayerModule();
                    filePathDialog.ClearConfigurationPath();
                }
                else
                {

                }
            }
            else
            {
                initLayerModule();
                restartLayerModule();
                filePathDialog.ClearConfigurationPath();
            }

        }

        /*현재 설정된 메시지 혹은 시그널들의 정보를 저장한다. */
        /*File Menu -> Save */
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveConfigurationFilePathDialog;
            DialogResult saveConfigurationResult;
            string configurationFilePath = "";
            
            saveConfigurationFilePathDialog = new SaveFileDialog();
            saveConfigurationFilePathDialog.FileName = "test.cfg";
            saveConfigurationFilePathDialog.InitialDirectory = @"D:\";
            saveConfigurationFilePathDialog.Filter = "configuration files (*.cfg)|*.cfg";
            saveConfigurationResult = saveConfigurationFilePathDialog.ShowDialog();

            if (saveConfigurationResult == DialogResult.OK)
            {
                configurationFilePath = saveConfigurationFilePathDialog.FileName;
                fileMenuOperation.savePathInfo(configurationFilePath, filePathDialog.GenerationFilePath, filePathDialog.SelectNodeName, filePathDialog.DataBaseFilePath);

                //COM 메시지 정보, 시그널정보 및 일반 옵션정보를 전달한다.
                fileMenuOperation.ILSaveConfigurationFile(configurationFilePath, COMManagement.AllMessageInfo, COMManagement.AllSingalInfo, COMManagement.GeneralInfo);
                fileMenuOperation.TPSaveConfigurationFile(configurationFilePath, TPManagement.CommonInfo,TPManagement.TPMessageInfo);
            }
            else
            {

            }
        }

        /*File Menu -> Load */
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadConfigurationFilePathDialog = new OpenFileDialog();
            DialogResult loadConfigurationResult;

            LinkedList<ComMessageAttributesInformation> getMessageInfo = new LinkedList<ComMessageAttributesInformation>();
            LinkedList<ComSignalAttributesInformation> getSignalInfo = new LinkedList<ComSignalAttributesInformation>();
            COMGeneral getGeneralInfo = new COMGeneral();

            LinkedList<TPMessageAttributesInformation> getTPMessageInfo = new LinkedList<TPMessageAttributesInformation>();
            TPCommon getTPConmmonInfo = new TPCommon();
            string configurationFilePath = "";
            
            loadConfigurationFilePathDialog.FileName = "";
            loadConfigurationFilePathDialog.InitialDirectory = @"D:\";
            loadConfigurationFilePathDialog.Filter = "configuration files (*.cfg)|*.cfg";

            loadConfigurationResult = loadConfigurationFilePathDialog.ShowDialog();

            /*Configuration 파일 경로를 가져온다. */
            if (loadConfigurationResult == DialogResult.OK)
            {
                /*현재 트리뷰에 있는 메시지 및 시그널 정보를 초기화 한다. */
                initLayerModule();
                restartLayerModule();

                configurationFilePath = loadConfigurationFilePathDialog.FileName;

                /*Configuration파일내에 xml내용을 파싱해서 각종 정보를 가져온다. */
                fileMenuOperation.loadPathInfo(configurationFilePath, ref selectNode, ref generationPath, ref databasePath);
                fileMenuOperation.ILLoadConfigurationFile(configurationFilePath, getMessageInfo, getSignalInfo, getGeneralInfo);
                fileMenuOperation.TPLoadConfigurationFile(configurationFilePath, getTPConmmonInfo, getTPMessageInfo);
                
                COMManagement.ILXmlToDataUpdate(getMessageInfo, getSignalInfo, getGeneralInfo);
                TPManagement.TPXmlToDataUpdate(getTPConmmonInfo, getTPMessageInfo);

                /*파일 경로를 업데이트 한다. */
                filePathDialog.UpdateGenerationFilePath(generationPath, databasePath, selectNode);
                MessageBox.Show("Load Complete");               
            }
            else
            {
                MessageBox.Show("Load Cancel");
            }

        }
 
        /*Generation Menu */
        private void generationGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LinkedList<COMMessageAttributesInformation> totalMessageInfo = new LinkedList<COMMessageAttributesInformation>();
            //LinkedList<SignalAttributesInformation> totalSignalInfo = new LinkedList<SignalAttributesInformation>();
            if (checkGenerateCondition() == true)
            {
                AllCodeGeneration generationCode = new bsw_generation.CodeGeneration.AllCodeGeneration();
                //COM메시지만 가져온다.
                generationCode.relocateMessageAndSignalInfo(COMManagement.AllMessageInfo, COMManagement.AllSingalInfo);
                generationCode.relocateTPConnectionList(TPManagement.TPMessageInfo);
                generationCode.CreateGenerationFolder(generationPath); // Create folder of user and gen folder.
                generationCode.CreateComConfigurationFile(); // Create com_configuration.h file.
                generationCode.CreateComDefineFile(); // Create com_define.h 
                generationCode.CreateComParameterFile(); //Create com_parameter.c
                generationCode.CreateComNotificationFile(); // create com_notification.c
                
                generationCode.CreateIsoTPDefineFile(); // create tp_configuration.h 
                generationCode.CreateIsoTPParameterFile(); // create isotp_parameter.c

                generationCode.GenerateUserHook(COMManagement.GeneralInfo); // user/

                generationCode.TpDefineParser(TPManagement.CommonInfo);
                generationCode.TpParameterParser(TPManagement.CommonInfo);
                generationCode.ComConfigurationParser(COMManagement.GeneralInfo);
                generationCode.ComDefineParser(COMManagement.GeneralInfo, COMManagement.AllMessageInfo, COMManagement.AllSingalInfo);
                generationCode.ComParameterParser(COMManagement.GeneralInfo, COMManagement.AllMessageInfo, COMManagement.AllSingalInfo);
                generationCode.ComNotifcationParser();
                MessageBox.Show("Generation Complete");
            }
            else
            {
                MessageBox.Show("Generation Failed");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            COMManagement.comDataGridChangedValue(sender,e);
        }







        //////////////////////////////////////Menu Process /////////////////////////////////////

    }
}
