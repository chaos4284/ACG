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
        UInt32 send_msg_handle_index = 0; // 현재 전송 메시지 핸들 Index
        UInt32 receive_msg_handle_index = 0; // 현재 수신 메시지 핸들 Index
        UInt32 send_sig_handle_index = 0; // 현재 전송 시그널 핸들 Index
        UInt32 receive_sig_handle_index = 0; //현재 수신 시그널 핸들 Index

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
            int index;
            if (propertyLabel == "MsgName")
            {
                value = changedValue;
                //정해진 노드이름(MessageObject혹은 SendMessage등의 이름으로 메시지이름 변경을 시도할경우를 비교
                if (checkDeletionConditionTreeNode(value.ToString()) == true)
                {
                    //   current_treeview_event.Node.Text = value.ToString();
                    mainTree.SelectedNode.Text = value.ToString();

                }
                else
                {
                    mainTree.SelectedNode.Text = oldValue.ToString();
                    for (index = 0; index < allMessageInfo.Count; index++)
                    {
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
                    for (index = 0; index < allSignalInfo.Count; index++)
                    {
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
        }

        public void COMPropertyDisplay(string objName)
        {
            switch (objName)
            {
                case "General":
                    comProperty.SelectedObject = generalInfo;
                    break;

                case "Message Object":
                    //current_treeview_event = e;
                    comProperty.SelectedObject = null;
                    break;

                case "SendMessage":
                    //current_treeview_event = e;
                    comProperty.SelectedObject = null;
                    break;

                case "ReceiveMessage":
                    //current_treeview_event = e;
                    comProperty.SelectedObject = null;
                    break;

                default:
                 
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
