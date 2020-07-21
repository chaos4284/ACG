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

        private LinkedList<TPMessageAttributesInformation> tpMessageInfo = new LinkedList<TPMessageAttributesInformation>();
        
        public void InitialTPLayer()
        {
            mainTree.Nodes.Clear();
            tpMessageInfo.Clear();
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
                                    TPMessageAttributesInformation TPMessageObject = new TPMessageAttributesInformation();
                                    connectionName = string.Format("connection{0}", tpHandleIndex);

                                    TPMessageObject.TPHandle = tpHandleIndex;
                                    TPMessageObject.SendMsgName = sendMsgObject.MessageName;
                                    TPMessageObject.SendMsgID = sendMsgObject.MessageID;
                                    TPMessageObject.ReceiveMsgName = receiveMessageObject.MessageName;
                                    TPMessageObject.ReceiveMsgID = receiveMessageObject.MessageID;
                                    TPMessageObject.TPTxIndex = sendMsgObject.MessageTpIndex;
                                    TPMessageObject.TpConnectionName = connectionName;

                                    tpMessageInfo.AddLast(TPMessageObject);
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
                    tpProperty.SelectedObject = tpCommonInfo;
                    break;

                case "TPConnectionList":
                    tpProperty.SelectedObject = null;
                    break;

                default:
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

        public TPCommon CommonInfo
        {
            get { return tpCommonInfo; }
            set { tpCommonInfo = value; }
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
