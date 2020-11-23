using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using bsw_generation.DatabaseParser;
using System.Windows.Forms;
using bsw_generation.LayerTab.ComTab;
using bsw_generation.LayerTab.TPTab;
namespace bsw_generation.MenuFrame.FileMenu
{
    class FileMenuOperation
    {
        public FileMenuOperation()
        {

        }

        public void savePathInfo(string selectPath, string generationPath, string dbNode, string databasePath)
        {
            XmlDocument saveConfigPathInformation = new XmlDocument();
            //saveConfigPathInformation.Load(selectPath);
            XmlNode root = saveConfigPathInformation.CreateElement("GenerationInformation");

            /*========================Configuration OPTION XML ========================*/
            XmlNode configuration = saveConfigPathInformation.CreateElement("Configuration"); /* root */

            XmlNode generation_path = saveConfigPathInformation.CreateElement("GeneralPath");
            generation_path.InnerText = generationPath;
            configuration.AppendChild(generation_path);


            XmlNode select_node = saveConfigPathInformation.CreateElement("SelectNode");
            select_node.InnerText = dbNode;
            configuration.AppendChild(select_node);


            XmlNode database_path = saveConfigPathInformation.CreateElement("DatabasePath");
            database_path.InnerText = databasePath;
            configuration.AppendChild(database_path);

            root.AppendChild(configuration);
            /*========================Configuration OPTION XML ========================*/
            saveConfigPathInformation.AppendChild(root);
            saveConfigPathInformation.Save(selectPath);
        }

        public void ILSaveConfigurationFile(string selectPath, LinkedList<ComMessageAttributesInformation> ilMessageInfo, LinkedList<ComSignalAttributesInformation> ilSignalInfo, COMGeneral ilGeneralInfo)
        {
            LinkedList<XmlNode> message_list = new LinkedList<XmlNode>();
            LinkedList<XmlNode> signal_list = new LinkedList<XmlNode>();

            XmlDocument saveConfigObjectInformation = new XmlDocument();

            saveConfigObjectInformation.Load(selectPath);

            /*Root Option*/
            XmlNode root = saveConfigObjectInformation.SelectSingleNode("GenerationInformation");


            /*========================General OPTION XML ========================*/
            XmlNode general = saveConfigObjectInformation.CreateElement("General"); /* root */

            XmlNode startCOMExtensionOption = saveConfigObjectInformation.CreateElement("StartCOMExtension");
            startCOMExtensionOption.InnerText = ilGeneralInfo.StartCOMExtensionOption;
            general.AppendChild(startCOMExtensionOption);

            XmlNode CallTaskTime = saveConfigObjectInformation.CreateElement("ComCallTaskTime");
            CallTaskTime.InnerText = string.Format("{0}", ilGeneralInfo.ComTaskTime);
            general.AppendChild(CallTaskTime);

            XmlNode AppModeNumber = saveConfigObjectInformation.CreateElement("AppModeNumber");
            AppModeNumber.InnerText = string.Format("{0}", ilGeneralInfo.AppModeNumber);
            general.AppendChild(AppModeNumber);

            XmlNode AppModeName = saveConfigObjectInformation.CreateElement("AppModeName");
            if (ilGeneralInfo.AppModeName == "")
            {
                AppModeName.InnerText = "DEFAULT_APP_MODE";
            }
            else
            {
                AppModeName.InnerText = ilGeneralInfo.AppModeName;
            }
            general.AppendChild(AppModeName);
            root.AppendChild(general);
            /*========================General OPTION XML ========================*/

            /*========================MessageObject OPTION XML ========================*/
            XmlNode message_object = saveConfigObjectInformation.CreateElement("MessageObject"); /* root */
            //XmlNode message_object = saveConfigObjectInformation.CreateElement("SendMessageInformation");
            //XmlNode receive_message_object = saveConfigObjectInformation.CreateElement("ReceiveMessageInformation");
            foreach (var msg_object in ilMessageInfo)
            {

                XmlNode message = saveConfigObjectInformation.CreateElement("Message");

                XmlAttribute Handle = saveConfigObjectInformation.CreateAttribute("Handle");
                Handle.Value = string.Format("{0}", msg_object.Handle);
                message.Attributes.Append(Handle);

                XmlAttribute Name = saveConfigObjectInformation.CreateAttribute("Name");
                Name.Value = msg_object.MsgName;
                message.Attributes.Append(Name);

                XmlAttribute Length = saveConfigObjectInformation.CreateAttribute("Length");
                Length.Value = string.Format("{0}", msg_object.Length);
                message.Attributes.Append(Length);

                XmlAttribute CycleTime = saveConfigObjectInformation.CreateAttribute("CycleTime");
                CycleTime.Value = string.Format("{0}", msg_object.CycleTime);
                message.Attributes.Append(CycleTime);

                XmlAttribute RepetitionCycleTime = saveConfigObjectInformation.CreateAttribute("RepetitionCycleTime");
                RepetitionCycleTime.Value = string.Format("{0}", msg_object.RepetitionCycleTime);
                message.Attributes.Append(RepetitionCycleTime);

                XmlAttribute RepetitionNumber = saveConfigObjectInformation.CreateAttribute("RepetitionNumber");
                RepetitionNumber.Value = string.Format("{0}", msg_object.RepetitionNumber);
                message.Attributes.Append(RepetitionNumber);

                XmlAttribute MessageDelayTime = saveConfigObjectInformation.CreateAttribute("MessageDelayTime");
                MessageDelayTime.Value = string.Format("{0}", msg_object.MessageDelayTime);
                message.Attributes.Append(MessageDelayTime);

                XmlAttribute SendMode = saveConfigObjectInformation.CreateAttribute("SendMode");
                SendMode.Value = string.Format("{0}", msg_object.SendMode);
                message.Attributes.Append(SendMode);

                XmlAttribute Direction = saveConfigObjectInformation.CreateAttribute("Direction");
                Direction.Value = string.Format("{0}", msg_object.Direction);
                message.Attributes.Append(Direction);

                XmlAttribute MsgID = saveConfigObjectInformation.CreateAttribute("MsgID");
                MsgID.Value = string.Format("{0}", msg_object.MsgID);
                message.Attributes.Append(MsgID);

                XmlAttribute StartOffsetDelay = saveConfigObjectInformation.CreateAttribute("StartOffsetDelay");
                StartOffsetDelay.Value = string.Format("{0}", msg_object.StartOffsetDelay);
                message.Attributes.Append(StartOffsetDelay);

                XmlAttribute MessagComSupport = saveConfigObjectInformation.CreateAttribute("MessagComSupport");
                MessagComSupport.Value = string.Format("{0}", msg_object.MessageComSupport);
                message.Attributes.Append(MessagComSupport);

                XmlAttribute DeadLineMonitoringOption = saveConfigObjectInformation.CreateAttribute("DeadLineMonitoringOption");
                DeadLineMonitoringOption.Value = string.Format("{0}", msg_object.DeadLineMonitoringOption);
                message.Attributes.Append(DeadLineMonitoringOption);

                XmlAttribute DeadLineMonitoringTimeout = saveConfigObjectInformation.CreateAttribute("DeadLineMonitoringTimeout");
                DeadLineMonitoringTimeout.Value = string.Format("{0}", msg_object.DeadLineMonitoringTimeout);
                message.Attributes.Append(DeadLineMonitoringTimeout);

                XmlAttribute NotificationOption = saveConfigObjectInformation.CreateAttribute("NotificationOption");
                NotificationOption.Value = string.Format("{0}", msg_object.NotificationOption);
                message.Attributes.Append(NotificationOption);

                XmlAttribute NotificationType = saveConfigObjectInformation.CreateAttribute("NotificationType");
                NotificationType.Value = string.Format("{0}", msg_object.NotificationType);
                message.Attributes.Append(NotificationType);

                XmlAttribute NotificationCallback = saveConfigObjectInformation.CreateAttribute("NotificationCallback");
                NotificationCallback.Value = string.Format("{0}", msg_object.NotificationCallbackName);
                message.Attributes.Append(NotificationCallback);


                message_list.AddLast(message);
            }

            foreach (var list in message_list)
            {
                message_object.AppendChild(list);
            }

            root.AppendChild(message_object);
            /*========================MessageObject OPTION XML ========================*/

            /*========================SignalObject OPTION XML =========================*/
            XmlNode signal_object = saveConfigObjectInformation.CreateElement("SignalObject"); /* root */

            foreach (var sig_object in ilSignalInfo)
            {
                XmlNode signal = saveConfigObjectInformation.CreateElement("Signal");

                XmlAttribute Handle = saveConfigObjectInformation.CreateAttribute("Handle");
                Handle.Value = string.Format("{0}", sig_object.Handle);
                signal.Attributes.Append(Handle);

                XmlAttribute Name = saveConfigObjectInformation.CreateAttribute("Name");
                Name.Value = sig_object.SignalName;
                signal.Attributes.Append(Name);

                XmlAttribute Length = saveConfigObjectInformation.CreateAttribute("Length");
                Length.Value = string.Format("{0}", sig_object.BitLength);
                signal.Attributes.Append(Length);

                XmlAttribute ParentMsgID = saveConfigObjectInformation.CreateAttribute("ParentMsgID");
                ParentMsgID.Value = string.Format("{0}", sig_object.ParentMsgId);
                signal.Attributes.Append(ParentMsgID);

                XmlAttribute ParentMsgHandle = saveConfigObjectInformation.CreateAttribute("ParentMsgHandle");
                ParentMsgHandle.Value = string.Format("{0}", sig_object.ParentMsgHandle);
                signal.Attributes.Append(ParentMsgHandle);

                XmlAttribute SendProperty = saveConfigObjectInformation.CreateAttribute("SendProperty");
                SendProperty.Value = string.Format("{0}", sig_object.SendProperty);
                signal.Attributes.Append(SendProperty);

                XmlAttribute FilterAlgorithm = saveConfigObjectInformation.CreateAttribute("FilterAlgorithm");
                FilterAlgorithm.Value = string.Format("{0}", sig_object.FilterAlogirithm);
                signal.Attributes.Append(FilterAlgorithm);

                XmlAttribute StartOffsetBit = saveConfigObjectInformation.CreateAttribute("StartOffsetBit");
                StartOffsetBit.Value = string.Format("{0}", sig_object.StartOffsetBit);
                signal.Attributes.Append(StartOffsetBit);

                XmlAttribute ByteOrder = saveConfigObjectInformation.CreateAttribute("ByteOrder");
                ByteOrder.Value = string.Format("{0}", sig_object.ByteOrder);
                signal.Attributes.Append(ByteOrder);

                XmlAttribute StartValue = saveConfigObjectInformation.CreateAttribute("StartValue");
                StartValue.Value = string.Format("{0}", sig_object.StartValue);
                signal.Attributes.Append(StartValue);

                XmlAttribute TimeoutValue = saveConfigObjectInformation.CreateAttribute("TimeoutValue");
                TimeoutValue.Value = string.Format("{0}", sig_object.TimeoutValue);
                signal.Attributes.Append(TimeoutValue);

                XmlAttribute NotificationOption = saveConfigObjectInformation.CreateAttribute("NotificationOption");
                NotificationOption.Value = string.Format("{0}", sig_object.NotificationOption);
                signal.Attributes.Append(NotificationOption);

                XmlAttribute NotificationType = saveConfigObjectInformation.CreateAttribute("NotificationType");
                NotificationType.Value = string.Format("{0}", sig_object.NotificationType);
                signal.Attributes.Append(NotificationType);

                XmlAttribute NotificationCallback = saveConfigObjectInformation.CreateAttribute("NotificationCallback");
                NotificationCallback.Value = string.Format("{0}", sig_object.NotificationCallbackName);
                signal.Attributes.Append(NotificationCallback);


                signal_list.AddLast(signal);
            }

            foreach (var list in signal_list)
            {
                signal_object.AppendChild(list);
            }

            root.AppendChild(signal_object);
            /*========================SignalObject OPTION XML ========================*/



            saveConfigObjectInformation.AppendChild(root);
            //root.AppendChild(general);
            //root.AppendChild(message_object);
            saveConfigObjectInformation.Save(selectPath);


            //saveConfigObjectInformation.Save("a.xml");
            /*MessageObject XML*/
            /*- SendMessage */
            /*- SendSignal*/
            /*- RecevieMessage*/
            /*- ReceiveSignal*/


            //config_file_fp = File.CreateText(selectPath);
            //config_file_fp.Close();       

        }
        /*Save TP Inforamation to xml. */
        public void TPSaveConfigurationFile(string selectPath, TPCommon tpCommonInfo, LinkedList<TPMessageAttributesInformation> tpMessageInfo)
        {
            /*Root Option*/
            XmlDocument saveConfigObjectInformation = new XmlDocument();
            LinkedList<XmlNode> tpConnectionList = new LinkedList<XmlNode>();
            saveConfigObjectInformation.Load(selectPath);
            /*Root Option*/
            XmlNode root = saveConfigObjectInformation.SelectSingleNode("GenerationInformation");


            /*========================TP OPTION COMMON XML ========================*/
            XmlNode common = saveConfigObjectInformation.CreateElement("Common"); /* root */

            XmlNode receveBufferMax = saveConfigObjectInformation.CreateElement("ReceiveBufferMax");
            receveBufferMax.InnerText = string.Format("{0}", tpCommonInfo.ReceiveMaxSize);
            common.AppendChild(receveBufferMax);

            XmlNode CallTaskTime = saveConfigObjectInformation.CreateElement("TPCallTaskTime");
            CallTaskTime.InnerText = string.Format("{0}", tpCommonInfo.TPTaskCallTime);
            common.AppendChild(CallTaskTime);

            root.AppendChild(common);

            /*========================TP OPTION COMMON XML ========================*/

            /*========================TP OPTION TP INFO XML ========================*/
            XmlNode tpMessageObject = saveConfigObjectInformation.CreateElement("TPInformation"); /* root */


            foreach (var tpObject in tpMessageInfo)
            {
                XmlNode tpConnection = saveConfigObjectInformation.CreateElement("TPConnection");

                XmlAttribute tpHandle = saveConfigObjectInformation.CreateAttribute("TPHandle");
                tpHandle.Value = string.Format("{0}", tpObject.TPHandle);
                tpConnection.Attributes.Append(tpHandle);

                XmlAttribute addressMode = saveConfigObjectInformation.CreateAttribute("AddressMode");
                addressMode.Value = tpObject.AddressMode;
                tpConnection.Attributes.Append(addressMode);

                XmlAttribute useFlowControl = saveConfigObjectInformation.CreateAttribute("UseFlowControl");
                useFlowControl.Value = tpObject.UseFlowControl;
                tpConnection.Attributes.Append(useFlowControl);

                XmlAttribute useBlockSize = saveConfigObjectInformation.CreateAttribute("UseBlockSize");
                useBlockSize.Value = tpObject.UseBlockSize;
                tpConnection.Attributes.Append(useBlockSize);

                XmlAttribute useSTmin = saveConfigObjectInformation.CreateAttribute("UseSTmin");
                useSTmin.Value = tpObject.UseSTmin;
                tpConnection.Attributes.Append(useSTmin);

                XmlAttribute blockSize = saveConfigObjectInformation.CreateAttribute("BlockSize");
                blockSize.Value = string.Format("{0}", tpObject.BlockSize);
                tpConnection.Attributes.Append(blockSize);

                XmlAttribute STmin = saveConfigObjectInformation.CreateAttribute("STmin");
                STmin.Value = string.Format("{0}", tpObject.STmin);
                tpConnection.Attributes.Append(STmin);

                XmlAttribute firstSequenceNumber = saveConfigObjectInformation.CreateAttribute("FirstSequenceNumber");
                firstSequenceNumber.Value = string.Format("{0}", tpObject.FirstSN);
                tpConnection.Attributes.Append(firstSequenceNumber);

                XmlAttribute tpSendID = saveConfigObjectInformation.CreateAttribute("TPSendID");
                tpSendID.Value = string.Format("{0}", tpObject.SendMsgID);
                tpConnection.Attributes.Append(tpSendID);

                XmlAttribute tpReceiveID = saveConfigObjectInformation.CreateAttribute("TPReceiveID");
                tpReceiveID.Value = string.Format("{0}", tpObject.ReceiveMsgID);
                tpConnection.Attributes.Append(tpReceiveID);

                XmlAttribute tpSendMsgName = saveConfigObjectInformation.CreateAttribute("TPSendMsgName");
                tpSendMsgName.Value = tpObject.SendMsgName;
                tpConnection.Attributes.Append(tpSendMsgName);

                XmlAttribute tpReceiveMsgName = saveConfigObjectInformation.CreateAttribute("TPRecevieMsgName");
                tpReceiveMsgName.Value = tpObject.ReceiveMsgName;
                tpConnection.Attributes.Append(tpReceiveMsgName);

                XmlAttribute nASTimeout = saveConfigObjectInformation.CreateAttribute("ASTimeout");
                nASTimeout.Value = string.Format("{0}", tpObject.N_As);
                tpConnection.Attributes.Append(nASTimeout);

                XmlAttribute nBSTimeout = saveConfigObjectInformation.CreateAttribute("BSTimeout");
                nBSTimeout.Value = string.Format("{0}", tpObject.N_Bs);
                tpConnection.Attributes.Append(nBSTimeout);

                XmlAttribute nARTimeout = saveConfigObjectInformation.CreateAttribute("ARTimeout");
                nARTimeout.Value = string.Format("{0}", tpObject.N_Ar);
                tpConnection.Attributes.Append(nARTimeout);

                XmlAttribute nCRTimeout = saveConfigObjectInformation.CreateAttribute("CRTimeout");
                nCRTimeout.Value = string.Format("{0}", tpObject.N_Cr);
                tpConnection.Attributes.Append(nCRTimeout);

                XmlAttribute waitingMode = saveConfigObjectInformation.CreateAttribute("WaitingMode");
                waitingMode.Value = tpObject.WaitMode;
                tpConnection.Attributes.Append(waitingMode);

                XmlAttribute wftMax = saveConfigObjectInformation.CreateAttribute("WftMax");
                wftMax.Value = string.Format("{0}", tpObject.WftMax);
                tpConnection.Attributes.Append(wftMax);

                XmlAttribute wftMaxTime = saveConfigObjectInformation.CreateAttribute("WftMaxTime");
                wftMaxTime.Value = string.Format("{0}", tpObject.WftMaxTime);
                tpConnection.Attributes.Append(wftMaxTime);

                XmlAttribute pad = saveConfigObjectInformation.CreateAttribute("PAD");
                pad.Value = string.Format("{0}", tpObject.Pad);
                tpConnection.Attributes.Append(pad);
                tpConnectionList.AddLast(tpConnection);

            }

            foreach (var list in tpConnectionList)
            {
                tpMessageObject.AppendChild(list);
            }

            root.AppendChild(tpMessageObject);
            /*========================TP OPTION TP INFO XML ========================*/

            root.AppendChild(tpMessageObject);
            saveConfigObjectInformation.AppendChild(root);

            saveConfigObjectInformation.Save(selectPath);

        }
        public void loadPathInfo(string selectPath, ref string dbNode, ref string generationPath, ref string databasePath)
        {
            XmlReader loadConfigInformation;
            XmlReaderSettings loadConfigSetting = new XmlReaderSettings();

            loadConfigSetting.IgnoreComments = true;
            loadConfigSetting.IgnoreWhitespace = true;

            loadConfigInformation = XmlReader.Create(selectPath, loadConfigSetting);

            while (loadConfigInformation.Read())
            {
                if (loadConfigInformation.Name.CompareTo("Configuration") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    generationPath = loadConfigInformation.ReadElementContentAsString("GeneralPath", "");
                    dbNode = loadConfigInformation.ReadElementContentAsString("SelectNode", "");
                    databasePath = loadConfigInformation.ReadElementContentAsString("DatabasePath", "");
                }
            }
            loadConfigInformation.Close();
        }

        public void ILLoadConfigurationFile(string selectPath, LinkedList<ComMessageAttributesInformation> ilMessageInfo, LinkedList<ComSignalAttributesInformation> ilSignalInfo, COMGeneral ilGeneralInfo)
        {
            XmlReader loadConfigInformation;
            string message_load_config_content = "";
            string siganl_load_config_content = "";
            XmlReaderSettings loadConfigSetting = new XmlReaderSettings();

            loadConfigSetting.IgnoreComments = true;
            loadConfigSetting.IgnoreWhitespace = true;

            loadConfigInformation = XmlReader.Create(selectPath, loadConfigSetting);
            //loadConfigInformation.Load(selectPath);

            while (loadConfigInformation.Read())
            {
                if (loadConfigInformation.Name.CompareTo("StartCOMExtension") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    ilGeneralInfo.StartCOMExtensionOption = loadConfigInformation.Value;
                }

                else if (loadConfigInformation.Name.CompareTo("ComCallTaskTime") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    ilGeneralInfo.ComTaskTime = Convert.ToUInt16(loadConfigInformation.Value);
                }
                else if (loadConfigInformation.Name.CompareTo("AppModeNumber") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    ilGeneralInfo.AppModeNumber = Convert.ToByte(loadConfigInformation.Value);
                }
                else if (loadConfigInformation.Name.CompareTo("AppModeName") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    ilGeneralInfo.AppModeName = loadConfigInformation.Value;
                }

                else if (loadConfigInformation.Name.CompareTo("Message") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    if (loadConfigInformation.MoveToFirstAttribute())    // 첫 번째 속성이 있다면..
                    {
                        ComMessageAttributesInformation message_information = new ComMessageAttributesInformation();
                        do
                        {
                            message_load_config_content = loadConfigInformation.Name;

                            switch (message_load_config_content)
                            {
                                case "Handle":
                                    message_information.Handle = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "Name":
                                    message_information.MsgName = loadConfigInformation.Value;
                                    break;

                                case "Length":
                                    message_information.Length = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "CycleTime":
                                    message_information.CycleTime = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "RepetitionCycleTime":
                                    message_information.RepetitionCycleTime = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "RepetitionNumber":
                                    message_information.RepetitionNumber = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "MessageDelayTime":
                                    message_information.MessageDelayTime = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "SendMode":
                                    message_information.SendMode = loadConfigInformation.Value;
                                    break;

                                case "Direction":
                                    message_information.Direction = loadConfigInformation.Value;
                                    break;

                                case "MsgID":
                                    message_information.MsgID = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "StartOffsetDelay":
                                    message_information.StartOffsetDelay = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "MessagComSupport":
                                    message_information.MessageComSupport = loadConfigInformation.Value;
                                    break;

                                case "DeadLineMonitoringOption":
                                    message_information.DeadLineMonitoringOption = loadConfigInformation.Value;
                                    break;

                                case "DeadLineMonitoringTimeout":
                                    message_information.DeadLineMonitoringTimeout = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "NotificationOption":
                                    message_information.NotificationOption = loadConfigInformation.Value;
                                    break;

                                case "NotificationType":
                                    message_information.NotificationType = loadConfigInformation.Value;
                                    break;

                                case "NotificationCallback":
                                    message_information.NotificationCallbackName = loadConfigInformation.Value;
                                    break;

                                default:
                                    break;
                            }
                        } while (loadConfigInformation.MoveToNextAttribute());
                        ilMessageInfo.AddLast(message_information);
                    }

                }
                else if (loadConfigInformation.Name.CompareTo("Signal") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    ComSignalAttributesInformation signal_information = new ComSignalAttributesInformation();
                    do
                    {
                        siganl_load_config_content = loadConfigInformation.Name;
                        switch (siganl_load_config_content)
                        {
                            case "Handle":
                                signal_information.Handle = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "Name":
                                signal_information.SignalName = loadConfigInformation.Value;
                                break;

                            case "Length":
                                signal_information.BitLength = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "ParentMsgID":
                                signal_information.ParentMsgId = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "ParentMsgHandle":
                                signal_information.ParentMsgHandle = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "SendProperty":
                                signal_information.SendProperty = loadConfigInformation.Value;
                                break;

                            case "FilterAlgorithm":
                                signal_information.FilterAlogirithm = loadConfigInformation.Value;
                                break;

                            case "StartOffsetBit":
                                signal_information.StartOffsetBit = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "ByteOrder":
                                signal_information.ByteOrder = loadConfigInformation.Value;
                                break;

                            case "StartValue":
                                signal_information.StartValue = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "TimeoutValue":
                                signal_information.TimeoutValue = Convert.ToUInt32(loadConfigInformation.Value);
                                break;

                            case "NotificationOption":
                                signal_information.NotificationOption = loadConfigInformation.Value;
                                break;

                            case "NotificationType":
                                signal_information.NotificationType = loadConfigInformation.Value;
                                break;

                            case "NotificationCallback":
                                signal_information.NotificationCallbackName = loadConfigInformation.Value;
                                break;


                            default:
                                break;
                        }

                    } while (loadConfigInformation.MoveToNextAttribute());
                    ilSignalInfo.AddLast(signal_information);
                }
            }
            loadConfigInformation.Close();

        }

        public void TPLoadConfigurationFile(string selectPath, TPCommon tpCommonInfo, LinkedList<TPMessageAttributesInformation> tpConnectInfo)
        {
            XmlReader loadConfigInformation;

            XmlReaderSettings loadConfigSetting = new XmlReaderSettings();

            string tpLoadConfigContent = "";

            loadConfigSetting.IgnoreComments = true;
            loadConfigSetting.IgnoreWhitespace = true;

            loadConfigInformation = XmlReader.Create(selectPath, loadConfigSetting);
            //TPConnection
            while (loadConfigInformation.Read())
            {
                if (loadConfigInformation.Name.CompareTo("ReceiveBufferMax") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    tpCommonInfo.ReceiveMaxSize = Convert.ToUInt32(loadConfigInformation.Value);
                }

                else if (loadConfigInformation.Name.CompareTo("TPCallTaskTime") == 0 &&
                    loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    loadConfigInformation.Read();
                    tpCommonInfo.TPTaskCallTime = Convert.ToUInt16(loadConfigInformation.Value);
                }

                else if (loadConfigInformation.Name.CompareTo("TPConnection") == 0 && loadConfigInformation.NodeType == XmlNodeType.Element)
                {
                    if (loadConfigInformation.MoveToFirstAttribute())    // 첫 번째 속성이 있다면..
                    {
                        TPMessageAttributesInformation tpInformation = new TPMessageAttributesInformation();
                        do
                        {
                            tpLoadConfigContent = loadConfigInformation.Name;
                            switch (tpLoadConfigContent)
                            {
                                case "TPHandle":
                                    tpInformation.TPHandle = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "AddressMode":
                                    tpInformation.AddressMode = loadConfigInformation.Value;
                                    break;

                                case "UseFlowControl":
                                    tpInformation.UseFlowControl = loadConfigInformation.Value;
                                    break;

                                case "UseBlockSize":
                                    tpInformation.UseBlockSize = loadConfigInformation.Value;
                                    break;

                                case "UseSTmin":
                                    tpInformation.UseSTmin = loadConfigInformation.Value;
                                    break;

                                case "BlockSize":
                                    tpInformation.BlockSize = Convert.ToByte(loadConfigInformation.Value);
                                    break;

                                case "STmin":
                                    tpInformation.STmin = Convert.ToByte(loadConfigInformation.Value);
                                    break;

                                case "FirstSequenceNumber":
                                    tpInformation.FirstSN = Convert.ToByte(loadConfigInformation.Value);
                                    break;

                                case "TPSendID":
                                    tpInformation.SendMsgID = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "TPReceiveID":
                                    tpInformation.ReceiveMsgID = Convert.ToUInt32(loadConfigInformation.Value);
                                    break;

                                case "TPSendMsgName":
                                    tpInformation.SendMsgName= loadConfigInformation.Value;
                                    break;

                                case "TPRecevieMsgName":
                                    tpInformation.ReceiveMsgName = loadConfigInformation.Value;
                                    break;

                                case "ASTimeout":
                                    tpInformation.N_As = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "BSTimeout":
                                    tpInformation.N_Bs = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "ARTimeout":
                                    tpInformation.N_Ar = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "CRTimeout":
                                    tpInformation.N_Cr = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "WaitingMode":
                                    tpInformation.WaitMode = loadConfigInformation.Value;
                                    break;

                                case "WftMax":
                                    tpInformation.WftMax = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "WftMaxTime":
                                    tpInformation.WftMaxTime = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                case "PAD":
                                    tpInformation.Pad = Convert.ToUInt16(loadConfigInformation.Value);
                                    break;

                                default:
                                    break;
                            }

                        } while (loadConfigInformation.MoveToNextAttribute());
                        tpConnectInfo.AddLast(tpInformation);
                    }

                }

            }
            loadConfigInformation.Close();
        }
    }
}
