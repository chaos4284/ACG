using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using bsw_generation.DatabaseParser.CAN;

namespace bsw_generation.LayerTab.TPTab
{
    class TPLayerManagement
    {
        private UInt16 tpHandleIndex;
        private PropertyGrid tpProperty;
        private TPCommon tpCommonInfo = new TPCommon();

        private TreeView mainTree;
        private TreeNode commonTree;
        private TreeNode connectionTree;

        private DataGridView tpDataGrid;

        Byte currentSelectTreeIndex = 0;

        const Byte COMMON_INDEX = 1;
        const Byte TP_CONNECTION_LIST_INDEX = 2;
        
        const Byte ADDRESS_MODE_INDEX = 0;
        const Byte USE_FLOW_CONTROL_INDEX = 1;
        const Byte USE_BLOCK_SIZE_INDEX = 2;
        const Byte USE_STMIN_INDEX = 3;
        const Byte BLOCK_SIZE_INDEX = 4;
        const Byte STMIN_INDEX = 5;
        const Byte FIRST_SN_INDEX = 6;
        const Byte SEND_MSG_ID_INDEX = 7;
        const Byte RECEIVE_MSG_ID_INDEX = 8;
        const Byte SEND_MSG_NAME_INDEX = 9;
        const Byte RECEIVE_MSG_NAME_INDEX = 10;
        const Byte N_AS_TIMEOUT_INDEX = 11;
        const Byte N_AR_TIMEOUT_INDEX = 12;
        const Byte N_BS_TIMEOUT_INDEX = 13;
        const Byte N_CR_TIMEOUT_INDEX = 14;
        const Byte WAIT_MODE_INDEX = 15;
        const Byte WFTMAX_INDEX = 16;
        const Byte WFTMAX_TIME_INDEX = 17;
        const Byte PAD_DATA_INDEX = 18;

        private LinkedList<TPMessageAttributesInformation> tpMessageInfo = new LinkedList<TPMessageAttributesInformation>();
        
        public void InitialTPLayer()
        {
            tpProperty.Hide();
            tpDataGrid.Hide();
            mainTree.Nodes.Clear();
            tpMessageInfo.Clear();

            tpDataGrid.AllowUserToResizeColumns = true;

            tpDataGrid.ColumnCount = 19;

            tpDataGrid.Columns[ADDRESS_MODE_INDEX].Name = "AddressMode";
            tpDataGrid.Columns[ADDRESS_MODE_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[ADDRESS_MODE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[USE_FLOW_CONTROL_INDEX].Name = "UseFlowControl";
            tpDataGrid.Columns[USE_FLOW_CONTROL_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[USE_FLOW_CONTROL_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[USE_BLOCK_SIZE_INDEX].Name = "UseBlockSize";
            tpDataGrid.Columns[USE_BLOCK_SIZE_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[USE_BLOCK_SIZE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[USE_STMIN_INDEX].Name = "UseSTmin";
            tpDataGrid.Columns[USE_STMIN_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[USE_STMIN_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[BLOCK_SIZE_INDEX].Name = "BlockSize";
            tpDataGrid.Columns[BLOCK_SIZE_INDEX].ValueType = typeof(Byte);
            tpDataGrid.Columns[BLOCK_SIZE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[STMIN_INDEX].Name = "STmin";
            tpDataGrid.Columns[STMIN_INDEX].ValueType = typeof(Byte);
            tpDataGrid.Columns[STMIN_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[FIRST_SN_INDEX].Name = "FirstSN";
            tpDataGrid.Columns[FIRST_SN_INDEX].ValueType = typeof(Byte);
            tpDataGrid.Columns[FIRST_SN_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[SEND_MSG_ID_INDEX].Name = "SendMsgID";
            tpDataGrid.Columns[SEND_MSG_ID_INDEX].ValueType = typeof(uint);
            tpDataGrid.Columns[SEND_MSG_ID_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[RECEIVE_MSG_ID_INDEX].Name = "ReceiveMsgID";
            tpDataGrid.Columns[RECEIVE_MSG_ID_INDEX].ValueType = typeof(uint);
            tpDataGrid.Columns[RECEIVE_MSG_ID_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[SEND_MSG_NAME_INDEX].ReadOnly = true;
            tpDataGrid.Columns[SEND_MSG_NAME_INDEX].Name = "SendMsgName";
            tpDataGrid.Columns[SEND_MSG_NAME_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[SEND_MSG_NAME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[RECEIVE_MSG_NAME_INDEX].ReadOnly = true;
            tpDataGrid.Columns[RECEIVE_MSG_NAME_INDEX].Name = "ReceiveMsgName";
            tpDataGrid.Columns[RECEIVE_MSG_NAME_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[RECEIVE_MSG_NAME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[N_AS_TIMEOUT_INDEX].Name = "N_AS";
            tpDataGrid.Columns[N_AS_TIMEOUT_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[N_AS_TIMEOUT_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[N_BS_TIMEOUT_INDEX].Name = "N_BS";
            tpDataGrid.Columns[N_BS_TIMEOUT_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[N_BS_TIMEOUT_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[N_AR_TIMEOUT_INDEX].Name = "N_AR";
            tpDataGrid.Columns[N_AR_TIMEOUT_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[N_AR_TIMEOUT_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[N_CR_TIMEOUT_INDEX].Name = "N_CR";
            tpDataGrid.Columns[N_CR_TIMEOUT_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[N_CR_TIMEOUT_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[WAIT_MODE_INDEX].Name = "WaitMode";
            tpDataGrid.Columns[WAIT_MODE_INDEX].ValueType = typeof(string);
            tpDataGrid.Columns[WAIT_MODE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[WFTMAX_INDEX].Name = "WFTMAX_COUNT";
            tpDataGrid.Columns[WFTMAX_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[WFTMAX_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[WFTMAX_TIME_INDEX].Name = "WFTMAX_TIME";
            tpDataGrid.Columns[WFTMAX_TIME_INDEX].ValueType = typeof(ushort);
            tpDataGrid.Columns[WFTMAX_TIME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            tpDataGrid.Columns[PAD_DATA_INDEX].Name = "PAD";
            tpDataGrid.Columns[PAD_DATA_INDEX].ValueType = typeof(Byte);
            tpDataGrid.Columns[PAD_DATA_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;;

        }

        public void restartTPLayer()
        {
            commonTree = mainTree.Nodes.Add("Common");
            connectionTree = mainTree.Nodes.Add("TPConnectionList");
            tpHandleIndex = 0;

        }


        public void UpdateDbToTP(LinkedList<ParserMessageInformation> dbMessageObject, LinkedList<ParserSignalInformation> dbSignalObject)
        {

            foreach (var sendMsgObject in dbMessageObject)
            {
                //파싱해온 메시지가 전송메시지일 경우
                if (sendMsgObject.MessageTpIndex > 0)
                {
                    if (sendMsgObject.MessageDirection == "SEND")
                    {

                        //TPMessageObject.SendMsgName = msg_object.MessageName;
                        foreach (var receiveMessageObject in dbMessageObject)
                        {
                            if (receiveMessageObject.MessageDirection == "RECEIVE")
                            {
                                if (sendMsgObject.MessageTpIndex == receiveMessageObject.MessageTpIndex)
                                {
                                    string connectionName = "";
                                    TPMessageAttributesInformation tpMessageObject = new TPMessageAttributesInformation();
                                    connectionName = string.Format("connection{0}", tpHandleIndex);

                                    tpMessageObject.TPHandle = tpHandleIndex;
                                    tpMessageObject.SendMsgName = sendMsgObject.MessageName;
                                    tpMessageObject.SendMsgID = sendMsgObject.MessageID;
                                    tpMessageObject.ReceiveMsgName = receiveMessageObject.MessageName;
                                    tpMessageObject.ReceiveMsgID = receiveMessageObject.MessageID;
                                    tpMessageObject.TPTxIndex = sendMsgObject.MessageTpIndex;
                                    tpMessageObject.TpConnectionName = connectionName;

                                    tpMessageInfo.AddLast(tpMessageObject);
                                    connectionTree.Nodes.Add(connectionName);
                                    tpHandleIndex++;
                                }
                            }
                        }

                    }
                }
            }
        }
/*
        public void ChangedTPOption(string objName)
        {
            switch (objName)
            {

                case "option":
                    foreach(var tpObjectInfo in tpMessageInfo)
                    {
                        tpObjectInfo.OptionControl = tpCommonInfo.OptionControl;
                    }
                    break;

                default:
                    break;
            }

        }
*/
        public void TPPropertyDisplay(string objName)
        {
            switch (objName)
            {
                case "Common":
                    tpProperty.Show();
                    tpDataGrid.Hide();
                    tpProperty.SelectedObject = tpCommonInfo;
                    currentSelectTreeIndex = COMMON_INDEX;
                    break;

                case "TPConnectionList":
                    int rowIndex = 0;
                    tpProperty.Hide();
                    tpDataGrid.Show();
                    tpProperty.SelectedObject = null;
                    currentSelectTreeIndex = TP_CONNECTION_LIST_INDEX;
                    tpDataGrid.Rows.Clear();
                    
                    foreach (TPMessageAttributesInformation tpMsgObject in tpMessageInfo)
                    {
                        DataGridViewComboBoxCell addressModeComboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell useFlowControlComboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell useBlockSizeComboBoxColumn   = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell useSTminComboBoxColumn       = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell waitModeComboBoxColumn       = new DataGridViewComboBoxCell();

                        addressModeComboBoxColumn.Items.AddRange("NORMAL_ADDRESS");//, "EXTENDED_ADDRESS", "NORMAL_FIXED_ADDRESS", "MIXED_11BIT_ADDRESS", "MIXED_29BIT_ADDRESS");
                        useFlowControlComboBoxColumn.Items.AddRange("TRUE", "FALSE");
                        useBlockSizeComboBoxColumn.Items.AddRange("TRUE", "FALSE");
                        useSTminComboBoxColumn.Items.AddRange("TRUE", "FALSE");
                        waitModeComboBoxColumn.Items.AddRange("TRUE", "FALSE");

                        rowIndex = tpDataGrid.Rows.Add(null, null, null,null, tpMsgObject.BlockSize, tpMsgObject.STmin, tpMsgObject.FirstSN,
                                                       tpMsgObject.SendMsgID, tpMsgObject.ReceiveMsgID, tpMsgObject.SendMsgName, tpMsgObject.ReceiveMsgName,
                                                       tpMsgObject.N_As, tpMsgObject.N_Bs, tpMsgObject.N_Ar, tpMsgObject.N_Cr,
                                                       null, tpMsgObject.WftMax, tpMsgObject.WftMaxTime, tpMsgObject.Pad);

                        addressModeComboBoxColumn.Value    = tpMsgObject.AddressMode;
                        useFlowControlComboBoxColumn.Value = tpMsgObject.UseFlowControl;
                        useBlockSizeComboBoxColumn.Value = tpMsgObject.UseBlockSize;
                        useSTminComboBoxColumn.Value = tpMsgObject.UseSTmin;
                        waitModeComboBoxColumn.Value = tpMsgObject.WaitMode;

                        tpDataGrid.Rows[rowIndex].Cells[ADDRESS_MODE_INDEX] = addressModeComboBoxColumn;
                        tpDataGrid.Rows[rowIndex].Cells[USE_FLOW_CONTROL_INDEX] = useFlowControlComboBoxColumn;
                        tpDataGrid.Rows[rowIndex].Cells[USE_BLOCK_SIZE_INDEX] = useBlockSizeComboBoxColumn;
                        tpDataGrid.Rows[rowIndex].Cells[USE_STMIN_INDEX] = useSTminComboBoxColumn;
                        tpDataGrid.Rows[rowIndex].Cells[WAIT_MODE_INDEX] = waitModeComboBoxColumn;

                    }
#if false
                    foreach (TPMessageAttributesInformation tpMsgObject in tpMessageInfo)
                    {
                        sendModecomboBoxColumn.Value = msg_object.SendMode;
                        comDataGrid.Rows[rowIndex].Cells[SEND_MODE_INDEX] = sendModecomboBoxColumn;

                        comSupportcomboBoxColumn.Value = msg_object.MessageComSupport;
                        comDataGrid.Rows[rowIndex].Cells[MSG_COM_SUPPORT_INDEX] = comSupportcomboBoxColumn;

                        notificationOptioncomboBoxColumn.Value = msg_object.NotificationOption;
                        comDataGrid.Rows[rowIndex].Cells[NOTIFICATION_OPTION_INDEX] = notificationOptioncomboBoxColumn;

                        notificationTypecomboBoxColumn.Value = msg_object.NotificationType;
                        comDataGrid.Rows[rowIndex].Cells[NOTIFICATION_TYPE_INDEX] = notificationTypecomboBoxColumn;

                        deadLineOptioncomboBoxColumn.Value = msg_object.DeadLineMonitoringOption;
                        comDataGrid.Rows[rowIndex].Cells[DEADLINE_MONITORING_OPTION_INDEX] = deadLineOptioncomboBoxColumn;
#endif
                        break;

                default:
                    tpProperty.Show();
                    tpDataGrid.Hide();
                    if (mainTree.SelectedNode.Parent.Text == "TPConnectionList")
                    {
                        
                        foreach (TPMessageAttributesInformation tpMsgObject in tpMessageInfo)
                        {
                            if (objName == tpMsgObject.TpConnectionName)
                            {
                                tpProperty.SelectedObject = tpMsgObject;
                                break;
                            }
                             else
                            {
                                ;
                            }
                        }
                    }

                    break;

            }
        }
        public void tpDataGridChangedValue(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(String.Format("current Index = {0}", currentSelectTreeIndex));
            if (currentSelectTreeIndex == TP_CONNECTION_LIST_INDEX)
            {

                switch (e.ColumnIndex)
                {
                    case ADDRESS_MODE_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).AddressMode = (string)tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                          break;

                    case USE_FLOW_CONTROL_INDEX:
                        //MessageBox.Show(String.Format("data = {0}", comDataGrid.Rows[e.RowIndex].Celsls[e.ColumnIndex].Value.ToString()));
                        tpMessageInfo.ElementAt(e.RowIndex).UseFlowControl = (string)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case USE_BLOCK_SIZE_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).UseBlockSize = (string)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case USE_STMIN_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).UseSTmin = (string)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case BLOCK_SIZE_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).BlockSize = (Byte)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case STMIN_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).STmin = (Byte)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case FIRST_SN_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).FirstSN = (Byte)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case SEND_MSG_ID_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).SendMsgID = (uint)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case RECEIVE_MSG_ID_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).ReceiveMsgID = (uint)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case N_AS_TIMEOUT_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).N_As = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case N_BS_TIMEOUT_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).N_Bs = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case N_AR_TIMEOUT_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).N_Ar = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case N_CR_TIMEOUT_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).N_Cr = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case WAIT_MODE_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).WaitMode = (string)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case WFTMAX_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).WftMax = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case WFTMAX_TIME_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).WftMaxTime = (ushort)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case PAD_DATA_INDEX:
                        tpMessageInfo.ElementAt(e.RowIndex).Pad = (Byte)(tpDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;
                            
                    default:
                        MessageBox.Show("Unknow Attribute");
                        break;
                }

            }
        }
        public void TPXmlToDataUpdate(TPCommon xmlTPCommonInfo, LinkedList<TPMessageAttributesInformation> xmlTPConnectInfo)
        {
            //tpMessageInfo
            tpCommonInfo.ReceiveMaxSize = xmlTPCommonInfo.ReceiveMaxSize;
            tpCommonInfo.TPTaskCallTime = xmlTPCommonInfo.TPTaskCallTime;
            string connectionName;
            foreach (var connectObject in xmlTPConnectInfo)
            {
                TPMessageAttributesInformation tpConnectObject = new TPMessageAttributesInformation();
                
                connectionName = string.Format("connection{0}", connectObject.TPHandle);

                tpConnectObject.TPHandle = connectObject.TPHandle;
                
                tpConnectObject.AddressMode = connectObject.AddressMode;
                tpConnectObject.UseFlowControl = connectObject.UseFlowControl;
                tpConnectObject.UseBlockSize = connectObject.UseBlockSize;
                tpConnectObject.UseSTmin = connectObject.UseSTmin;
                tpConnectObject.BlockSize = connectObject.BlockSize;
                tpConnectObject.STmin = connectObject.STmin;
                tpConnectObject.FirstSN = connectObject.FirstSN;
                tpConnectObject.SendMsgID = connectObject.SendMsgID;
                tpConnectObject.ReceiveMsgID = connectObject.ReceiveMsgID;
                tpConnectObject.SendMsgName = connectObject.SendMsgName;
                tpConnectObject.ReceiveMsgName = connectObject.ReceiveMsgName;
                tpConnectObject.N_As = connectObject.N_As;
                tpConnectObject.N_Bs = connectObject.N_Bs;
                tpConnectObject.N_Ar = connectObject.N_Ar;
                tpConnectObject.N_Cr = connectObject.N_Cr;
                tpConnectObject.WaitMode = connectObject.WaitMode;
                tpConnectObject.WftMax = connectObject.WftMax;
                tpConnectObject.WftMaxTime = connectObject.WftMaxTime;
                tpConnectObject.Pad = connectObject.Pad;
                tpConnectObject.TpConnectionName = connectionName;
                connectionTree.Nodes.Add(connectionName);
                tpMessageInfo.AddLast(tpConnectObject);
                tpHandleIndex++;
            }

        }

        public TPCommon CommonInfo
        {
            get { return tpCommonInfo; }
            set { tpCommonInfo = value; }
        }
        
        public DataGridView TPDataGrid
        {
            get { return tpDataGrid; }
            set { tpDataGrid = value; }
        }
        public LinkedList<TPMessageAttributesInformation>  TPMessageInfo
        {
            get { return tpMessageInfo; }
            set { tpMessageInfo = value; }
        }

        public TreeView MainTree
        {
            get { return mainTree; }
            set { mainTree = value; }
        }

        public TreeNode CommonTree
        {
            get { return commonTree; }
            set { commonTree = value; }
        }

        public TreeNode ConnectionTree
        {
            get { return connectionTree; }
            set { connectionTree = value; }
        }
        public PropertyGrid TPProperty
        {
            get { return tpProperty; }
            set { tpProperty = value; }
        }

    }
}
