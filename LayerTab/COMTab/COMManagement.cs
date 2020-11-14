using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using bsw_generation.DatabaseParser.CAN;

namespace bsw_generation.LayerTab.ComTab
{
    class ComManagement
    {
        const Byte GENERAL_INFO_INDEX = 1;
        const Byte MESSAGE_OBJECT_INFO_INDEX = 2;
        const Byte SEND_MESSAGE_INFO_INDEX = 3;
        const Byte RECEIVE_MESSAGE_INFO_INDEX = 4;
              
        const Int32 MSG_NAME_INDEX = 0;
        const Int32 LENGTH_INDEX = 1;
        const Int32 CYCLE_TIME_INDEX = 2;
        const Int32 REPETITION_CYCLE_INDEX = 3;
        const Int32 REPETITION_NUMBER_INDEX = 4;
        const Int32 MESSAGE_DELAY_TIME_INDEX = 5;
        const Int32 SEND_MODE_INDEX = 6;
        const Int32 MSG_ID_INDEX = 7;
        const Int32 START_OFFSET_DELAY_INDEX = 8;
        const Int32 MSG_COM_SUPPORT_INDEX = 9;
        const Int32 DEADLINE_MONITORING_OPTION_INDEX = 10;
        const Int32 DEADLINE_MONITORING_TIMEOUT = 11;
        const Int32 NOTIFICATION_OPTION_INDEX = 12;
        const Int32 NOTIFICATION_TYPE_INDEX = 13;
        const Int32 NOTIFCATION_CALLBACK_NAME_INDEX = 14;
        
        private DataGridView comDataGrid;
        private UInt32 send_msg_handle_index = 0; // 현재 전송 메시지 핸들 Index
        private UInt32 receive_msg_handle_index = 0; // 현재 수신 메시지 핸들 Index
        private UInt32 send_sig_handle_index = 0; // 현재 전송 시그널 핸들 Index
        private UInt32 receive_sig_handle_index = 0; //현재 수신 시그널 핸들 Index
        private Byte currentSelectTreeIndex = 0;
        
        private COMGeneral generalInfo = new COMGeneral();
        private LinkedList<ComMessageAttributesInformation> allMessageInfo = new LinkedList<ComMessageAttributesInformation>();
        private LinkedList<ComSignalAttributesInformation> allSignalInfo = new LinkedList<ComSignalAttributesInformation>();
        private LinkedList<ComMessageAttributesInformation> sendMessageInfo = new LinkedList<ComMessageAttributesInformation>();
        private LinkedList<ComMessageAttributesInformation> receiveMessageInfo = new LinkedList<ComMessageAttributesInformation>();
        private LinkedList<ComSignalAttributesInformation> sendSingalInfo = new LinkedList<ComSignalAttributesInformation>();
        private LinkedList<ComSignalAttributesInformation> receiveSingalInfo = new LinkedList<ComSignalAttributesInformation>();

        private TreeView mainTree;
        private TreeNode messageObjectTree;
        private TreeNode generalTree;
        private TreeNode sendMessageTree;
        private TreeNode receiveMessageTree;
        private TreeNode internalSignalInMessageTree;

        private PropertyGrid comProperty;
        //public LinkedList<ParserMessageInformation> dbMessageInfo= new LinkedList<ParserMessageInformation>();
        //public LinkedList<ParserSignalInformation> dbSignalInfo= new LinkedList<ParserSignalInformation>();

        private UInt32 readSendMsgHandleById(UInt32 message_id)
        {
            UInt32 ret = 0;

            foreach (var msg_object in sendMessageInfo)
            {
                if (msg_object.MsgID == message_id)
                {
                    ret = msg_object.Handle;
                    break;
                }
            }

            return ret;
        }

        private UInt32 readReceiveMsgHandleById(UInt32 message_id)
        {
            UInt32 ret = 0;

            foreach (var msg_object in receiveMessageInfo)
            {
                if (msg_object.MsgID == message_id)
                {
                    ret = msg_object.Handle;
                    break;
                }
            }

            return ret;
        }

        private Boolean checkExistedSignalInMessage()
        {
            Boolean ret = false;
            foreach (ComMessageAttributesInformation select_message_info in allMessageInfo)
            {
                foreach (ComSignalAttributesInformation sig_object in allSignalInfo)
                {
                    if (sig_object.ParentMsgId == select_message_info.MsgID)
                    {
                        ret = true;
                        break;
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }

            return ret;

        }

        public void InitialLLayer()
        {

            mainTree.Nodes.Clear();
            allMessageInfo.Clear();
            allSignalInfo.Clear();
            sendMessageInfo.Clear();
            receiveMessageInfo.Clear();
            sendSingalInfo.Clear();
            receiveSingalInfo.Clear();

#if false           
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            comboBoxColumn.HeaderText = "DeadLineMonitoringOptiong";
            comboBoxColumn.Name = "DeadLineMonitoringOptiong";
            comboBoxColumn.Items.AddRange("DEACTIVE_DEADLINE_MONITORING", "ACTIVE_DEADLINE_MONITORING");

            new string[] { "NOTIFICATION_DEACTIVE", "NOTIFICATION_ACTIVE" });
#endif
            generalInfo.setDefaultComGeneralData();
            comDataGrid.Hide();
            comProperty.Hide();

#if false
            comDataGrid.Columns.Add("col0", "MsgName");
            comDataGrid.Columns.Add("col1", "Length");
#endif
            //comDataGrid.ColumnCount = 0;
            comDataGrid.AllowUserToResizeColumns = true;
 
            comDataGrid.ColumnCount = 15;

            comDataGrid.Columns[MSG_NAME_INDEX].Name = "MsgName";
            comDataGrid.Columns[MSG_NAME_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[MSG_NAME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[LENGTH_INDEX].Name = "Length";
            comDataGrid.Columns[LENGTH_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[LENGTH_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[CYCLE_TIME_INDEX].Name = "CycleTime";
            comDataGrid.Columns[CYCLE_TIME_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[CYCLE_TIME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[REPETITION_CYCLE_INDEX].Name = "RepetitionCycleTime";
            comDataGrid.Columns[REPETITION_CYCLE_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[REPETITION_CYCLE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[REPETITION_NUMBER_INDEX].Name = "RepetitionNumber";
            comDataGrid.Columns[REPETITION_NUMBER_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[REPETITION_NUMBER_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[MESSAGE_DELAY_TIME_INDEX].Name = "MessageDelayTime";
            comDataGrid.Columns[MESSAGE_DELAY_TIME_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[MESSAGE_DELAY_TIME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[SEND_MODE_INDEX].Name = "SendMode";
            comDataGrid.Columns[SEND_MODE_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[SEND_MODE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[MSG_ID_INDEX].Name = "MsgID";
            comDataGrid.Columns[MSG_ID_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[MSG_ID_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            comDataGrid.Columns[START_OFFSET_DELAY_INDEX].Name = "StartOffsetDelay";
            comDataGrid.Columns[START_OFFSET_DELAY_INDEX].ValueType = typeof(uint);
            comDataGrid.Columns[START_OFFSET_DELAY_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[MSG_COM_SUPPORT_INDEX].Name = "MessageComSupport";
            comDataGrid.Columns[MSG_COM_SUPPORT_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[MSG_COM_SUPPORT_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[DEADLINE_MONITORING_OPTION_INDEX].Name = "DeadLineMonitoringOption";
            comDataGrid.Columns[DEADLINE_MONITORING_OPTION_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[DEADLINE_MONITORING_OPTION_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[DEADLINE_MONITORING_TIMEOUT].Name = "DeadLineMonitoringTimeout";
            comDataGrid.Columns[DEADLINE_MONITORING_TIMEOUT].ValueType = typeof(uint);
            comDataGrid.Columns[DEADLINE_MONITORING_TIMEOUT].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[NOTIFICATION_OPTION_INDEX].Name = "NotificationOption";
            comDataGrid.Columns[NOTIFICATION_OPTION_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[NOTIFICATION_OPTION_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            comDataGrid.Columns[NOTIFICATION_TYPE_INDEX].Name = "NotificationType";
            comDataGrid.Columns[NOTIFICATION_TYPE_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[NOTIFICATION_TYPE_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            comDataGrid.Columns[NOTIFCATION_CALLBACK_NAME_INDEX].Name = "NotificationCallbackName";
            comDataGrid.Columns[NOTIFCATION_CALLBACK_NAME_INDEX].ValueType = typeof(string);
            comDataGrid.Columns[NOTIFCATION_CALLBACK_NAME_INDEX].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
             }

        public void restartlLLayer()
        {
            generalTree = mainTree.Nodes.Add("General");
            messageObjectTree = mainTree.Nodes.Add("Message Object");
            sendMessageTree = messageObjectTree.Nodes.Add("SendMessage");
            receiveMessageTree = messageObjectTree.Nodes.Add("ReceiveMessage");
                      

            send_msg_handle_index = 0;
            receive_msg_handle_index = 0;
            send_sig_handle_index = 0;
            receive_sig_handle_index = 0;

            switch(currentSelectTreeIndex)
            {
                case GENERAL_INFO_INDEX:
                    comProperty.SelectedObject = generalInfo;
                    break;

                case MESSAGE_OBJECT_INFO_INDEX:
                    comProperty.SelectedObject = null;
                    break;

                case SEND_MESSAGE_INFO_INDEX:
                    comProperty.SelectedObject = null;
                    break;

                case RECEIVE_MESSAGE_INFO_INDEX:
                    comProperty.SelectedObject = null;
                    break;
            }
            
        }

        public Boolean checkCOMGenerationCondition()
        {
            Boolean ret = true;

            if (checkNumberOfDbData() == false)
            {
                ret = false;
            }
            else if (checkDuplicatedInformation() == false)
            {
                ret = false;
            }
            else if (checkExistedSignalInMessage() == false)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }

            return ret;

        }

        private Boolean checkDuplicatedInformation()
        {
            UInt32 same_id_count = 0;
            UInt32 same_handle_count = 0;
            UInt32 same_name_count = 0;
            Boolean ret = true;

            foreach (ComMessageAttributesInformation select_message_info in allMessageInfo)
            {
                foreach (ComMessageAttributesInformation msg_object in allMessageInfo)
                {
                    if (select_message_info.MsgID == msg_object.MsgID)
                    {
                        same_id_count++;
                    }

                    if ((select_message_info.Handle == msg_object.MsgID) && (select_message_info.Direction == msg_object.Direction))
                    {
                        same_handle_count++;
                    }

                    if (select_message_info.MsgName == msg_object.MsgName)
                    {
                        same_name_count++;
                    }
                }
           

                //ID중복을 검출한다.
                if (same_id_count > 1)
                {
                    ret = false;
                    
                    MessageBox.Show(string.Format(" Occur Duplicated id = {0}", select_message_info.MsgID));
                    break;
                }
                else
                {
                    ;
                }
                //Handle값 중복을 검출한다.
                if (same_handle_count > 1)
                {
                    ret = false;
                    MessageBox.Show(string.Format("Occur Duplicated {0} message handle = {1}", select_message_info.Direction, select_message_info.Handle));
                    break;
                }
                else
                {
                    ;
                }
                
                //메시지 이름 중복을 검출한다.
                if (same_name_count > 1)
                {
                    ret = false;
                    MessageBox.Show(string.Format("Occur Duplicated message name = {0}", select_message_info.MsgName));
                    break;
                }
                else
                {
                    ;
                }
                same_id_count = 0;
                same_handle_count = 0;
                same_name_count = 0;
            }
            return ret;
        }

        private Boolean checkNumberOfDbData()
        {
            Boolean ret = true;

            if ((allMessageInfo.Count == 0) || (allSignalInfo.Count == 0))
            {
                ret = false;
                MessageBox.Show("Not Defined Message or Signal");
            }

            return ret;
        }
        public void ilXmlToDataUpdate(LinkedList<ComMessageAttributesInformation> xmlMessageObject, LinkedList<ComSignalAttributesInformation> xmlSignalObject, COMGeneral xmlGeneralInfo)
        {
            generalInfo.StartCOMExtensionOption = xmlGeneralInfo.StartCOMExtensionOption;
            generalInfo.ComTaskTime = xmlGeneralInfo.ComTaskTime;
            generalInfo.AppModeNumber = xmlGeneralInfo.AppModeNumber;
            generalInfo.AppModeName = xmlGeneralInfo.AppModeName;

            foreach (var msg_object in xmlMessageObject)
            {
                //파싱해온 메시지가 전송메시지일 경우
                if (msg_object.Direction == "SEND")
                {
                    ComMessageAttributesInformation sendMessageObject = new ComMessageAttributesInformation();
                    sendMessageObject.Handle = msg_object.Handle;
                    sendMessageObject.MsgName = msg_object.MsgName;
                    sendMessageObject.Length = msg_object.Length;
                    sendMessageObject.CycleTime = msg_object.CycleTime;
                    sendMessageObject.RepetitionCycleTime = msg_object.RepetitionCycleTime;
                    sendMessageObject.RepetitionNumber = msg_object.RepetitionNumber;
                    sendMessageObject.MessageDelayTime = msg_object.MessageDelayTime;
                    sendMessageObject.SendMode = msg_object.SendMode;
                    sendMessageObject.Direction = msg_object.Direction;
                    sendMessageObject.MsgID = msg_object.MsgID;
                    sendMessageObject.StartOffsetDelay = msg_object.StartOffsetDelay;
                    sendMessageObject.MessageComSupport = msg_object.MessageComSupport;
                    sendMessageInfo.AddLast(sendMessageObject);
                    allMessageInfo.AddLast(sendMessageObject);
                    internalSignalInMessageTree = sendMessageTree.Nodes.Add(sendMessageObject.MsgName);
                    send_msg_handle_index++;

                }
                else //파싱해온 메시지가 수신메시지일 경우
                {
                    ComMessageAttributesInformation receiveMessageObject = new ComMessageAttributesInformation();
                    receiveMessageObject.Handle = receive_msg_handle_index;
                    receiveMessageObject.MsgName = msg_object.MsgName;
                    receiveMessageObject.Length = msg_object.Length;
                    receiveMessageObject.CycleTime = msg_object.CycleTime;
                    receiveMessageObject.RepetitionCycleTime = msg_object.RepetitionCycleTime;
                    receiveMessageObject.RepetitionNumber = msg_object.RepetitionNumber;
                    receiveMessageObject.MessageDelayTime = msg_object.MessageDelayTime;
                    receiveMessageObject.SendMode = msg_object.SendMode;
                    receiveMessageObject.Direction = msg_object.Direction;
                    receiveMessageObject.MsgID = msg_object.MsgID;
                    receiveMessageObject.StartOffsetDelay = msg_object.StartOffsetDelay;
                    receiveMessageObject.MessageComSupport = msg_object.MessageComSupport;
                    receiveMessageInfo.AddLast(receiveMessageObject);
                    allMessageInfo.AddLast(receiveMessageObject);
                    internalSignalInMessageTree = receiveMessageTree.Nodes.Add(receiveMessageObject.MsgName);
                    receive_msg_handle_index++;

                }

                foreach (var sig_object in xmlSignalObject)
                {
                    //파싱해온 시그널에 해당하는 메시지정보를 가져온다
                    if (msg_object.MsgID == sig_object.ParentMsgId)
                    {
                        //전송 메시지에 시그널 일경우
                        if (msg_object.Direction == "SEND")
                        {
                            ComSignalAttributesInformation sendSignalObject = new ComSignalAttributesInformation();
                            sendSignalObject.Handle = sig_object.Handle;
                            sendSignalObject.SignalName = sig_object.SignalName;
                            sendSignalObject.BitLength = sig_object.BitLength;
                            sendSignalObject.ParentMsgId = sig_object.ParentMsgId;
                            sendSignalObject.ParentMsgHandle = msg_object.Handle;
                            sendSignalObject.SendProperty = sig_object.SendProperty;
                            sendSignalObject.FilterAlogirithm = sig_object.FilterAlogirithm;
                            sendSignalObject.StartOffsetBit = sig_object.StartOffsetBit;
                            sendSignalObject.ByteOrder = sig_object.ByteOrder;
                            sendSignalObject.StartValue = sig_object.StartValue;
                            sendSignalObject.TimeoutValue = sig_object.TimeoutValue;
                            sendSingalInfo.AddLast(sendSignalObject);
                            allSignalInfo.AddLast(sendSignalObject);
                            internalSignalInMessageTree.Nodes.Add(sendSignalObject.SignalName);
                            send_sig_handle_index++;
   }
                        //수신 메시지에 시그널 일경우
                        else if (msg_object.Direction == "RECEIVE")
                        {
                            ComSignalAttributesInformation receiveSignalObject = new ComSignalAttributesInformation();
                            receiveSignalObject.Handle = receive_sig_handle_index;
                            receiveSignalObject.SignalName = sig_object.SignalName;
                            receiveSignalObject.BitLength = sig_object.BitLength;
                            receiveSignalObject.ParentMsgId = sig_object.ParentMsgId;
                            receiveSignalObject.ParentMsgHandle = msg_object.Handle;
                            receiveSignalObject.SendProperty = sig_object.SendProperty;
                            receiveSignalObject.FilterAlogirithm = sig_object.FilterAlogirithm;
                            receiveSignalObject.StartOffsetBit = sig_object.StartOffsetBit;
                            receiveSignalObject.ByteOrder = sig_object.ByteOrder;
                            receiveSignalObject.StartValue = sig_object.StartValue;
                            receiveSignalObject.TimeoutValue = sig_object.TimeoutValue;
                            receiveSingalInfo.AddLast(receiveSignalObject);
                            allSignalInfo.AddLast(receiveSignalObject);
                            internalSignalInMessageTree.Nodes.Add(receiveSignalObject.SignalName);
                            receive_sig_handle_index++;

}
                        else
                        {

                        }

                    }
                    else
                    {

                    }
                }

            }
        }

        public void UpdateCOMDbToData(LinkedList<ParserMessageInformation> dbMessageObject,LinkedList<ParserSignalInformation> dbSignalObject)
        {

            foreach (var msg_object in dbMessageObject)
            {
                //파싱해온 메시지가 전송메시지일 경우
                if (msg_object.MessageComSupport == "TRUE")
                {
                    if (msg_object.MessageDirection == "SEND")
                    {
                        ComMessageAttributesInformation sendMessageObject = new ComMessageAttributesInformation();
                        sendMessageObject.Handle = send_msg_handle_index;
                        sendMessageObject.MsgName = msg_object.MessageName;
                        sendMessageObject.Length = msg_object.MessageLength;
                        sendMessageObject.CycleTime = msg_object.MessageCycleTime;
                        sendMessageObject.RepetitionCycleTime = msg_object.MessageRepetitonCycleTime;
                        sendMessageObject.RepetitionNumber = msg_object.MessageRepetitonNumber;
                        sendMessageObject.MessageDelayTime = msg_object.MessageDelayTime;
                        sendMessageObject.SendMode = msg_object.MessageSendMode;
                        sendMessageObject.Direction = msg_object.MessageDirection;
                        sendMessageObject.MsgID = msg_object.MessageID;
                        sendMessageObject.StartOffsetDelay = msg_object.MessageStartOffsetDelay;
                        sendMessageObject.MessageComSupport = msg_object.MessageComSupport;
                        sendMessageInfo.AddLast(sendMessageObject);
                        allMessageInfo.AddLast(sendMessageObject);
                        internalSignalInMessageTree = sendMessageTree.Nodes.Add(sendMessageObject.MsgName);
                        send_msg_handle_index++;
                    }
                    else //파싱해온 메시지가 수신메시지일 경우
                    {
                        ComMessageAttributesInformation receiveMessageObject = new ComMessageAttributesInformation();
                        receiveMessageObject.Handle = receive_msg_handle_index;
                        receiveMessageObject.MsgName = msg_object.MessageName;
                        receiveMessageObject.Length = msg_object.MessageLength;
                        receiveMessageObject.CycleTime = msg_object.MessageCycleTime;
                        receiveMessageObject.RepetitionCycleTime = msg_object.MessageRepetitonCycleTime;
                        receiveMessageObject.RepetitionNumber = msg_object.MessageRepetitonNumber;
                        receiveMessageObject.MessageDelayTime = msg_object.MessageDelayTime;
                        receiveMessageObject.SendMode = msg_object.MessageSendMode;
                        receiveMessageObject.Direction = msg_object.MessageDirection;
                        receiveMessageObject.MsgID = msg_object.MessageID;
                        receiveMessageObject.StartOffsetDelay = msg_object.MessageStartOffsetDelay;
                        receiveMessageObject.MessageComSupport = msg_object.MessageComSupport;
                        receiveMessageInfo.AddLast(receiveMessageObject);
                        allMessageInfo.AddLast(receiveMessageObject);
                        internalSignalInMessageTree = receiveMessageTree.Nodes.Add(receiveMessageObject.MsgName);
                        receive_msg_handle_index++;
                    }

                    foreach (var sig_object in dbSignalObject)
                    {
                        //파싱해온 시그널에 해당하는 메시지정보를 가져온다
                        if (msg_object.MessageID == sig_object.SignalParentID)
                        {
                            //전송 메시지에 시그널 일경우
                            if (msg_object.MessageDirection == "SEND")
                            {
                                ComSignalAttributesInformation sendSignalObject = new ComSignalAttributesInformation();
                                sendSignalObject.Handle = send_sig_handle_index;
                                sendSignalObject.SignalName = sig_object.SignalName;
                                sendSignalObject.BitLength = sig_object.SignalBitLength;
                                sendSignalObject.ParentMsgId = sig_object.SignalParentID;
                                sendSignalObject.ParentMsgHandle = readSendMsgHandleById(sig_object.SignalParentID);

                                sendSignalObject.SendProperty = sig_object.SignalSendProperty;
                                sendSignalObject.FilterAlogirithm = sig_object.SignalFilterAlgorithm;
                                sendSignalObject.StartOffsetBit = sig_object.SignalStartOffsetBit;
                                sendSignalObject.ByteOrder = sig_object.SignalByteOrder;
                                sendSignalObject.StartValue = sig_object.SignalStartValue;
                                sendSignalObject.TimeoutValue = sig_object.SignalTimeoutValue;
                                sendSingalInfo.AddLast(sendSignalObject);
                                allSignalInfo.AddLast(sendSignalObject);
                                internalSignalInMessageTree.Nodes.Add(sendSignalObject.SignalName);
                                send_sig_handle_index++;
                                //System.Console.WriteLine("send sig msg handle = {0}", sendSignalObject.ParentMsgHandle);
                            }
                            //수신 메시지에 시그널 일경우
                            else if (msg_object.MessageDirection == "RECEIVE")
                            {
                                ComSignalAttributesInformation receiveSignalObject = new ComSignalAttributesInformation();
                                receiveSignalObject.Handle = receive_sig_handle_index;
                                receiveSignalObject.SignalName = sig_object.SignalName;
                                receiveSignalObject.BitLength = sig_object.SignalBitLength;
                                receiveSignalObject.ParentMsgId = sig_object.SignalParentID;
                                receiveSignalObject.ParentMsgHandle = readReceiveMsgHandleById(sig_object.SignalParentID);
                                receiveSignalObject.SendProperty = sig_object.SignalSendProperty;
                                receiveSignalObject.FilterAlogirithm = sig_object.SignalFilterAlgorithm;
                                receiveSignalObject.StartOffsetBit = sig_object.SignalStartOffsetBit;
                                receiveSignalObject.ByteOrder = sig_object.SignalByteOrder;
                                receiveSignalObject.StartValue = sig_object.SignalStartValue;
                                receiveSignalObject.TimeoutValue = sig_object.SignalTimeoutValue;
                                receiveSingalInfo.AddLast(receiveSignalObject);
                                allSignalInfo.AddLast(receiveSignalObject);
                                internalSignalInMessageTree.Nodes.Add(receiveSignalObject.SignalName);
                                receive_sig_handle_index++;
                                //System.Console.WriteLine("receive sig msg handle = {0}", receiveSignalObject.ParentMsgHandle);
                            }
                            else
                            {

                            }

                        }
                        else
                        {

                        }
                    }
                }

            }

        }

        /*Check if the conidtion is deleted. */
        private Boolean checkDeletionConditionTreeNode(string selectNodeName)
        {
            Boolean ret = false;

            switch (selectNodeName)
            {
                case "General":
                    ret = false;
                    break;

                case "Message Object":
                    ret = false;
                    break;

                case "SendMessage":
                    ret = false;
                    break;

                case "ReceiveMessage":
                    ret = false;
                    break;

                default:
                    ret = true;
                    break;

            }

            return ret;
        }
        public void comPropertyChangedValue(string propertyLabel, object changedValue, object oldValue  )
        {
            object value;
            //int index;

            if (propertyLabel == "MsgName") //메시지이름이 변경되었을 경우
            {
                value = changedValue;
                //정해진 노드이름(MessageObject혹은 SendMessage등의 이름으로 메시지이름 변경을 시도할경우를 비교
                if (checkDeletionConditionTreeNode(value.ToString()) == true)
                {
                    //   current_treeview_event.Node.Text = value.ToString();
                    mainTree.SelectedNode.Text = value.ToString(); // Node Tree에 변경메시지이름을 설정한다.
                }
                else  
                {
                    mainTree.SelectedNode.Text = oldValue.ToString();// Node Tree에 변경이전 메시지이름으로 복구한다.

                    //부모 노드트리로 이름으로 메시지 이름이 설정된것을 리스트에서 찾아 이전 설정된값으로 복구한다.
                    foreach (ComMessageAttributesInformation msg_object in allMessageInfo)
                    {
                        if (value.ToString() == msg_object.MsgName)
                        {
                            msg_object.MsgName = oldValue.ToString();
                            MessageBox.Show("message name cannot be the same as the parent node name");
                            break;
                        }
                    }
 
                }

            }
            else if (propertyLabel == "SignalName")
            {
                value = changedValue;
                if (checkDeletionConditionTreeNode(value.ToString()) == true)
                {
                    mainTree.SelectedNode.Text = value.ToString();
                }
                else
                {
                    mainTree.SelectedNode.Text = oldValue.ToString();
                    foreach (ComSignalAttributesInformation sig_object in allSignalInfo)
                    {
                        if (value.ToString() == sig_object.SignalName)
                        {
                            sig_object.SignalName = oldValue.ToString();
                            MessageBox.Show("message name cannot be the same as the parent node name");
                            break;
                        }
                    }

                }
            }
        }

        public void comDataGridChangedValue(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(String.Format("current Index = {0}", currentSelectTreeIndex));
            if (currentSelectTreeIndex == SEND_MESSAGE_INFO_INDEX)
            {

                switch (e.ColumnIndex)
                {
                    case MSG_NAME_INDEX:
                        string oldMsgName;
                        string newMsgName;
                        oldMsgName = sendMessageInfo.ElementAt(e.RowIndex).MsgName;
                        newMsgName = comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        sendMessageInfo.ElementAt(e.RowIndex).MsgName = newMsgName;

                        foreach (TreeNode searchNode in sendMessageTree.Nodes)
                        {
                            if (searchNode.Text == oldMsgName)
                            { 
                                searchNode.Text = newMsgName;
                            }
                        }
                        break;

                    case LENGTH_INDEX:
                        //MessageBox.Show(String.Format("data = {0}", comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                        sendMessageInfo.ElementAt(e.RowIndex).Length = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case CYCLE_TIME_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).CycleTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case REPETITION_CYCLE_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).RepetitionCycleTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case REPETITION_NUMBER_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).RepetitionNumber = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MESSAGE_DELAY_TIME_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).MessageDelayTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case SEND_MODE_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).SendMode = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MSG_ID_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).MsgID = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case START_OFFSET_DELAY_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).StartOffsetDelay = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MSG_COM_SUPPORT_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).MessageComSupport = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case DEADLINE_MONITORING_OPTION_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).DeadLineMonitoringOption = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case DEADLINE_MONITORING_TIMEOUT:
                        sendMessageInfo.ElementAt(e.RowIndex).DeadLineMonitoringTimeout = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFICATION_OPTION_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).NotificationOption = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFICATION_TYPE_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).NotificationType = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFCATION_CALLBACK_NAME_INDEX:
                        sendMessageInfo.ElementAt(e.RowIndex).NotificationCallbackName = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    default:
                        MessageBox.Show("Unknow Attribute");
                        break;
                }

            }
            else if (currentSelectTreeIndex == RECEIVE_MESSAGE_INFO_INDEX)
            {
               
                switch (e.ColumnIndex)
                {
                    case MSG_NAME_INDEX:
                        string oldMsgName;
                        string newMsgName;
                        oldMsgName = receiveMessageInfo.ElementAt(e.RowIndex).MsgName;
                        newMsgName = comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        receiveMessageInfo.ElementAt(e.RowIndex).MsgName = newMsgName;

                        foreach (TreeNode searchNode in receiveMessageTree.Nodes)
                        {
                            if (searchNode.Text == oldMsgName)
                            {
                                searchNode.Text = newMsgName;
                            }
                        }
                        break;

                    case LENGTH_INDEX:
                        //MessageBox.Show(String.Format("data = {0}", comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                        receiveMessageInfo.ElementAt(e.RowIndex).Length = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case CYCLE_TIME_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).CycleTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case REPETITION_CYCLE_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).RepetitionCycleTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case REPETITION_NUMBER_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).RepetitionNumber = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MESSAGE_DELAY_TIME_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).MessageDelayTime = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case SEND_MODE_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).SendMode = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MSG_ID_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).MsgID = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case START_OFFSET_DELAY_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).StartOffsetDelay = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case MSG_COM_SUPPORT_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).MessageComSupport = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case DEADLINE_MONITORING_OPTION_INDEX:
                        MessageBox.Show("deadline");
                        receiveMessageInfo.ElementAt(e.RowIndex).DeadLineMonitoringOption = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case DEADLINE_MONITORING_TIMEOUT:
                        receiveMessageInfo.ElementAt(e.RowIndex).DeadLineMonitoringTimeout = (uint)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFICATION_OPTION_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).NotificationOption = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFICATION_TYPE_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).NotificationType = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    case NOTIFCATION_CALLBACK_NAME_INDEX:
                        receiveMessageInfo.ElementAt(e.RowIndex).NotificationCallbackName = (string)(comDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        break;

                    default:
                        MessageBox.Show("Unknow Attribute");
                        break;
                }
            }
            else
            {
                ;
            }

        }

        public void COMPropertyDisplay(string objName)
        {
            int rowIndex = 0;
            switch (objName)
            {
                case "General": /* Display Property */
                    comProperty.Show();
                    comDataGrid.Hide();
                    currentSelectTreeIndex = GENERAL_INFO_INDEX;
                    break;

                case "Message Object": /* Display All Off */
                    //current_treeview_event = e;
                    comDataGrid.Hide();
                    comProperty.Hide();
                    comProperty.SelectedObject = null;
                    currentSelectTreeIndex = MESSAGE_OBJECT_INFO_INDEX;
                    break;

                case "SendMessage":  /* Display DataGridView*/
                    //current_treeview_event = e;
                    comDataGrid.Show();
                    comProperty.Hide();
                    comProperty.SelectedObject = null; // dataGrid 변환
                    currentSelectTreeIndex = SEND_MESSAGE_INFO_INDEX;

                    /*Display all attribute of SendMessage */
                    comDataGrid.Rows.Clear();
                    foreach (ComMessageAttributesInformation msg_object in sendMessageInfo) 
                    {
                        //comDataGrid.Rows.Add.cellrowindex
#if true

                        //, comsupport
                        DataGridViewComboBoxCell sendModecomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell comSupportcomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell deadLineOptioncomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell notificationOptioncomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell notificationTypecomboBoxColumn = new DataGridViewComboBoxCell();

                        
                        sendModecomboBoxColumn.Items.AddRange("NON_SEND_MODE", "DTM", "PTM", "MTM");
                        comSupportcomboBoxColumn.Items.AddRange("TRUE","FALSE");
                        deadLineOptioncomboBoxColumn.Items.AddRange("DEACTIVE_DEADLINE_MONITORING", "ACTIVE_DEADLINE_MONITORING");
                        notificationOptioncomboBoxColumn.Items.AddRange("NOTIFICATION_DEACTIVE", "NOTIFICATION_ACTIVE");
                        notificationTypecomboBoxColumn.Items.AddRange("NOTIFCATION_INDICATE", "NOTIFCATION_CALLBACK", "ALL");
#endif
                        rowIndex = comDataGrid.Rows.Add(msg_object.MsgName, msg_object.Length, msg_object.CycleTime, msg_object.RepetitionCycleTime, msg_object.RepetitionNumber,
                                             msg_object.MessageDelayTime, null/*sendmode*/, msg_object.MsgID, msg_object.StartOffsetDelay, null/*message com support*/, null/*deadline option */,
                                             msg_object.DeadLineMonitoringTimeout, null/*notification option*/, null/*notification type*/, msg_object.NotificationCallbackName);
              
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


                        /*

                        (msg_object.MsgName)
                        {
                            comProperty.SelectedObject = msg_object;
                            break;
                        }
                        else
                        {
                            ;
                        }
                        */
                    }


                    break;

                case "ReceiveMessage": /* Display DataGridView*/
                    //current_treeview_event = e;
                    comDataGrid.Show();
                    comProperty.Hide();
                    comProperty.SelectedObject = null; // dataGrid 변환
                    currentSelectTreeIndex = RECEIVE_MESSAGE_INFO_INDEX;

                    /*Display all attribute of SendMessage */
                    comDataGrid.Rows.Clear();
                    foreach (ComMessageAttributesInformation msg_object in receiveMessageInfo)
                    {
                        //comDataGrid.Rows.Add.cellrowindex
#if true

                        //, comsupport
                        DataGridViewComboBoxCell sendModecomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell comSupportcomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell deadLineOptioncomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell notificationOptioncomboBoxColumn = new DataGridViewComboBoxCell();
                        DataGridViewComboBoxCell notificationTypecomboBoxColumn = new DataGridViewComboBoxCell();


                        sendModecomboBoxColumn.Items.AddRange("NON_SEND_MODE", "DTM", "PTM", "MTM");
                        comSupportcomboBoxColumn.Items.AddRange("TRUE", "FALSE");
                        deadLineOptioncomboBoxColumn.Items.AddRange("DEACTIVE_DEADLINE_MONITORING", "ACTIVE_DEADLINE_MONITORING");
                        notificationOptioncomboBoxColumn.Items.AddRange("NOTIFICATION_DEACTIVE", "NOTIFICATION_ACTIVE");
                        notificationTypecomboBoxColumn.Items.AddRange("NOTIFCATION_INDICATE", "NOTIFCATION_CALLBACK", "ALL");
#endif
                        rowIndex = comDataGrid.Rows.Add(msg_object.MsgName, msg_object.Length, msg_object.CycleTime, msg_object.RepetitionCycleTime, msg_object.RepetitionNumber,
                                             msg_object.MessageDelayTime, null/*sendmode*/, msg_object.MsgID, msg_object.StartOffsetDelay, null/*message com support*/, null/*deadline option */,
                                             msg_object.DeadLineMonitoringTimeout, null/*notification option*/, null/*notification type*/, msg_object.NotificationCallbackName);

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
                    }
                    break;

                default:
                    comDataGrid.Hide();
                    comProperty.Show();
                    if (mainTree.SelectedNode.Parent.Text == "SendMessage")
                    {
                        foreach (ComMessageAttributesInformation msg_object in sendMessageInfo)
                        {
                            if (objName == msg_object.MsgName)
                            {
                                comProperty.SelectedObject = msg_object;
                                break;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }
                    else if (mainTree.SelectedNode.Parent.Text == "ReceiveMessage")
                    {
                        foreach (ComMessageAttributesInformation msg_object in receiveMessageInfo)
                        {
                            if (objName == msg_object.MsgName)
                            {
                                comProperty.SelectedObject = msg_object;
                                //MessageBox.Show("Click");

                                break;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }

                    else if (mainTree.SelectedNode.Parent.Parent.Text == "SendMessage")
                    {
                        foreach (ComMessageAttributesInformation msg_object in sendMessageInfo)
                        {
                            foreach (ComSignalAttributesInformation signal_object in sendSingalInfo)
                            {
                                if (objName == signal_object.SignalName)
                                {
                                    comProperty.SelectedObject = signal_object;

                                    break;
                                }
                            }
                        }
                    }
                    else if (mainTree.SelectedNode.Parent.Parent.Text == "ReceiveMessage")
                    {
                        foreach (ComMessageAttributesInformation msg_object in receiveMessageInfo)
                        {
                            foreach (ComSignalAttributesInformation signal_object in receiveSingalInfo)
                            {
                                if (objName == signal_object.SignalName)
                                {
                                    comProperty.SelectedObject = signal_object;
                                    
                                    break;
                                }
                            }
                        }
                    }

                    break;

            }
        }
        /*
        public void ilTreeViewUpdate()
        {
            foreach (var parser_msg_object in allMessageInfo)
            {
   
                foreach (var parser_sig_object in allSignalInfo)
                {
                    if (parser_msg_object.MsgID == parser_sig_object.ParentMsgId)
                    {
                        if (parser_msg_object.Direction == "SEND")
                        {
                            internalSignalInMessageTree.Nodes.Add(parser_sig_object.SignalName);
                        }
                        else if (parser_msg_object.Direction == "RECEIVE")
                        {
                            internalSignalInMessageTree.Nodes.Add(parser_sig_object.SignalName);
                        }
                        else
                        {
    
                        }
    
                    }
                    else
                    {
    
                    }
                }
    
            }
            MessageBox.Show("Load Complete");


        }
        */

        public DataGridView ComDataGrid
        {
            get { return comDataGrid; }
            set { comDataGrid = value; }
        }
        public TreeView MainTree
        {
            get { return mainTree; }
            set { mainTree = value; }
        }
        public TreeNode MessageObjectTree
        {
            get { return messageObjectTree; }
            set { messageObjectTree = value; }
        }

        public TreeNode GeneralTree
        {
            get { return generalTree; }
            set { generalTree = value; }
        }

        public TreeNode SendMessageTree
        {
            get { return sendMessageTree; }
            set { sendMessageTree = value; }
        }

        public TreeNode ReceiveMessageTree
        {
            get { return receiveMessageTree; }
            set { receiveMessageTree = value; }
        }


        public COMGeneral GeneralInfo
        {
            get { return generalInfo; }
            set { generalInfo = value; }
        }

        public PropertyGrid IlProperty
        {
            get { return comProperty; }
            set { comProperty = value; }
        }


        public LinkedList<ComMessageAttributesInformation> AllMessageInfo
        {
            get { return allMessageInfo; }
            set { allMessageInfo = value; }
        }

        public LinkedList<ComSignalAttributesInformation> AllSingalInfo
        {
            get { return allSignalInfo; }
            set { allSignalInfo = value; }
        }

        public LinkedList<ComMessageAttributesInformation> SendMessageInfo
        {
            get { return sendMessageInfo; }
            set { sendMessageInfo = value; }
        }

        public LinkedList<ComMessageAttributesInformation> ReceiveMessageInfo
        {
            get { return receiveMessageInfo; }
            set { receiveMessageInfo = value; }
        }

        public LinkedList<ComSignalAttributesInformation> SendSingalInfo
        {
            get { return sendSingalInfo; }
            set { sendSingalInfo = value; }
        }

        public LinkedList<ComSignalAttributesInformation> ReceiveSingalInfo
        {
            get { return receiveSingalInfo; }
            set { receiveSingalInfo = value; }
        }
    }
}
