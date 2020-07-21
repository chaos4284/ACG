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

namespace bsw_generation.CodeGeneration
{
    class AllCodeGeneration
    {
        private StreamWriter comConfigurationHandle;
        private StreamWriter comDefineHandle;
        private StreamWriter comParameterHandle;
        private StreamWriter comNotificationHandle;

        private StreamWriter tpParameterHandle;
        private StreamWriter tpDefineHandle;
       

        private UInt32 SendMessageErrorIndicationHandleIndex = 0;
        private UInt32 SendMessageErrorIndicationHandleOffset = 0;
        private UInt32 ReceiveMessageErrorIndicationHandleIndex = 0;
        private UInt32 ReceiveMessageErrorIndicationHandleOffset = 0;
        private UInt32 SendSignalNotificationIndicationHandleIndex = 0;
        private UInt32 SendSignalNotificationIndicationHandleOffset = 0;
        private UInt32 ReceiveSignalNotificationIndicationHandleIndex = 0;
        private UInt32 ReceiveSignalNotificationIndicationHandleOffset = 0;

        private UInt32 tpConnectionCount = 0;
        private UInt32 comSendMessageCount = 0;
        private UInt32 comSendSignalCount = 0;
        private UInt32 comReceiveMessageCount = 0;
        private UInt32 comReceiveSignalCount = 0;
        private UInt32 comSendMessageErrorIndicationCount = 0;
        private UInt32 comReceiveMessageErrorIndicationCount = 0;
        private UInt32 comSendSignalNotificationIndicationCount = 0;
        private UInt32 comReceiveSignalNotificationIndicationCount = 0;
        private string generationFolder;
        private string userFolder;


        private LinkedList<string> test = new LinkedList<string>();
        LinkedList<ComMessageAttributesInformation> relocateSendMessageInfo = new LinkedList<ComMessageAttributesInformation>();
        LinkedList<ComMessageAttributesInformation> relocateReceiveMessageInfo = new LinkedList<ComMessageAttributesInformation>();
        LinkedList<ComSignalAttributesInformation> relocateSendSignalInfo = new LinkedList<ComSignalAttributesInformation>();
        LinkedList<ComSignalAttributesInformation> relocateReceiveSignalInfo = new LinkedList<ComSignalAttributesInformation>();

        LinkedList<TPMessageAttributesInformation> relocateTPConnectionInfo = new LinkedList<TPMessageAttributesInformation>();
        
        public Boolean relocateMessageAndSignalInfo(LinkedList<ComMessageAttributesInformation> getMessageInfo, LinkedList<ComSignalAttributesInformation> getSignalInfo)
        {
            Boolean ret = true;

            if ((getMessageInfo.Count == 0) && (getSignalInfo.Count == 0))
            {
                ret = false;
            }
            else
            {
                foreach (var getMsgObject in getMessageInfo)
                {
                    if (getMsgObject.Direction == "SEND")
                    {
                        if (getMsgObject.NotificationOption == "NOTIFICATION_ACTIVE")
                        {
                            if ((getMsgObject.NotificationType == "ALL") || (getMsgObject.NotificationType == "NOTIFCATION_INDICATE"))
                            {

                                getMsgObject.HandleNotificationIndication = SendMessageErrorIndicationHandleIndex;
                                getMsgObject.HandleNotificationIndicationOffset = SendMessageErrorIndicationHandleOffset;

                                if (SendMessageErrorIndicationHandleOffset == 7)
                                {
                                    SendMessageErrorIndicationHandleOffset = 0;
                                    SendMessageErrorIndicationHandleIndex++;
                                }
                                else
                                {
                                    SendMessageErrorIndicationHandleOffset++;
                                }

                                comSendMessageErrorIndicationCount++;

                            }
                            else
                            {
                                ;
                            }
                        }
                        else
                        {
                            ;
                        }
                        //getMsgObject.Handle = sendMessageHandleIndex;
                        relocateSendMessageInfo.AddLast(getMsgObject);
                        comSendMessageCount++;
                    }
                    else if (getMsgObject.Direction == "RECEIVE")
                    {
                        if (getMsgObject.NotificationOption == "NOTIFICATION_ACTIVE")
                        {
                            if ((getMsgObject.NotificationType == "ALL") || (getMsgObject.NotificationType == "NOTIFCATION_INDICATE"))
                            {
                                getMsgObject.HandleNotificationIndication = ReceiveMessageErrorIndicationHandleIndex;
                                getMsgObject.HandleNotificationIndicationOffset = ReceiveMessageErrorIndicationHandleOffset;

                                if (ReceiveMessageErrorIndicationHandleOffset == 7)
                                {
                                    ReceiveMessageErrorIndicationHandleOffset = 0;
                                    ReceiveMessageErrorIndicationHandleIndex++;
                                }
                                else
                                {
                                    ReceiveMessageErrorIndicationHandleOffset++;
                                }
                                comReceiveMessageErrorIndicationCount++;
                            }
                            else
                            {
                                ;
                            }
                        }
                        else
                        {
                            ;
                        }
                        //getMsgObject.Handle = receiveMessageHandleIndex;
                        relocateReceiveMessageInfo.AddLast(getMsgObject);
                        comReceiveMessageCount++;
                    }
                    else
                    {
                        MessageBox.Show("Not Set Direction Attribute");
                    }

                }

                foreach (var getSigObject in getSignalInfo)
                {
                    if (CheckDirectionByMessageId(getSigObject.ParentMsgId, getMessageInfo) == "SEND")
                    {
                        if (getSigObject.NotificationOption == "NOTIFICATION_ACTIVE")
                        {
                            if ((getSigObject.NotificationType == "ALL") || (getSigObject.NotificationType == "NOTIFCATION_INDICATE"))
                            {
                                getSigObject.HandleNotificationIndication = SendSignalNotificationIndicationHandleIndex;
                                getSigObject.HandleNotificationIndicationOffset = SendSignalNotificationIndicationHandleOffset;

                                if (SendSignalNotificationIndicationHandleOffset == 7)
                                {
                                    SendSignalNotificationIndicationHandleOffset = 0;
                                    SendSignalNotificationIndicationHandleIndex++;
                                }
                                else
                                {
                                    SendSignalNotificationIndicationHandleOffset++;
                                }

                                comSendSignalNotificationIndicationCount++;
                            }
                            else
                            {
                                ;
                            }
                        }
                        relocateSendSignalInfo.AddLast(getSigObject);
                        comSendSignalCount++;
                    }
                    else if (CheckDirectionByMessageId(getSigObject.ParentMsgId, getMessageInfo) == "RECEIVE")
                    {
                        if (getSigObject.NotificationOption == "NOTIFICATION_ACTIVE")
                        {
                            if ((getSigObject.NotificationType == "ALL") || (getSigObject.NotificationType == "NOTIFCATION_INDICATE"))
                            {
                                getSigObject.HandleNotificationIndication = ReceiveSignalNotificationIndicationHandleIndex;
                                getSigObject.HandleNotificationIndicationOffset = ReceiveSignalNotificationIndicationHandleOffset;

                                if (ReceiveSignalNotificationIndicationHandleOffset == 7)
                                {
                                    ReceiveSignalNotificationIndicationHandleOffset = 0;
                                    ReceiveSignalNotificationIndicationHandleIndex++;
                                }
                                else
                                {
                                    ReceiveSignalNotificationIndicationHandleOffset++;
                                }
                                comReceiveSignalNotificationIndicationCount++;
                            }
                            else
                            {
                                ;
                            }
                        }
                        //getSigObject.Handle = ReceiveSignalHandleIndex;
                        relocateReceiveSignalInfo.AddLast(getSigObject);
                        comReceiveSignalCount++;
                    }
                    else
                    {
                        /*Not Supported Com */
                    }

                }
            }



            return ret;
        }

        public Boolean relocateTPConnectionList(LinkedList<TPMessageAttributesInformation> getTpConnection)
        {
            Boolean ret = true;

            if (getTpConnection.Count == 0)
            {
                ret = false;
            }
            else
            {
                foreach (var getTpConnectionObject in getTpConnection)
                {
                    relocateTPConnectionInfo.AddLast(getTpConnectionObject);
                    tpConnectionCount++;
                }
            }
            return ret;
        }

        private string ReadMessageNameById(UInt32 id, LinkedList<ComMessageAttributesInformation> messageInfo)
        {
            string ret = "";

            foreach (var messageObject in messageInfo)
            {
                if (id == messageObject.MsgID)
                {
                    ret = messageObject.MsgName;
                    break;
                }
            }

            return ret;

        }

        private string CheckDirectionByMessageId(UInt32 id, LinkedList<ComMessageAttributesInformation> messageInfo)
        {
            string ret = "";
            foreach (ComMessageAttributesInformation messageObject in messageInfo)
            {
                if (messageObject.MessageComSupport == "TRUE")
                {
                    if (messageObject.MsgID == id)
                    {
                        ret = messageObject.Direction;
                        break;
                    }
                    else
                    {
                        //System.Console.WriteLine("name === {0}", id);
                    }
                }
            }

            return ret;

        }
        public void CreateGenerationFolder(string generationFilePath)
        {
            generationFolder = generationFilePath + @"\gen";
            userFolder = generationFilePath + @"\user";

            if (Directory.Exists(generationFolder) == false)
            {
                Directory.CreateDirectory(generationFolder);
            }

            if (Directory.Exists(userFolder) == false)
            {
                Directory.CreateDirectory(userFolder);
            }
  
        }
        public byte CreateComConfigurationFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\com_configuration.h";
            comConfigurationHandle = File.CreateText(filePath);

            return ret;
        }
        public void GenerateUserHook(COMGeneral generalInfo)
        {
            StreamWriter hookHandle;
            string filePath = userFolder + @"\com_hook.c";

            hookHandle = File.CreateText(filePath);
            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * com_hook.c");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");

            hookHandle.WriteLine("#include \"osek_com.h\"");
            hookHandle.WriteLine("#include \"com_hook.h\"");
            hookHandle.WriteLine("#include \"stdint.h\"");

            hookHandle.WriteLine("");

            hookHandle.WriteLine("extern error_service_management_info_t ersm;");

            if (generalInfo.StartCOMExtensionOption == "TRUE")
            {
                hookHandle.WriteLine("StatusType StartCOMExtension(void)");
                hookHandle.WriteLine("{");
                hookHandle.WriteLine("    int ret = 0;");
                hookHandle.WriteLine("    ret = StartPeriodic();");
                hookHandle.WriteLine("    return ret;");
                hookHandle.WriteLine("}");
            }
            hookHandle.WriteLine("void COMErrorHook(StatusType error)");
            hookHandle.WriteLine("{");
            hookHandle.WriteLine("	uint16_t errorType = 0;");
            hookHandle.WriteLine("	errorType = COMErrorGetServiceId;");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("    while(1)");
            hookHandle.WriteLine("    {");
            hookHandle.WriteLine("        ;");
            hookHandle.WriteLine("    }");
            hookHandle.WriteLine("}");

            hookHandle.Close();

            //END com_hook.c//////////////////
            filePath = userFolder + @"\com_hook.h";
            hookHandle = File.CreateText(filePath);
            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * com_hook.h");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("#ifndef COM_HOOK_H_");
            hookHandle.WriteLine("#define COM_HOOK_H_");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("#include \"internal_osek_com_parameter.h\"");
            hookHandle.WriteLine("");

            if (generalInfo.StartCOMExtensionOption == "TRUE")
            {
                hookHandle.WriteLine("StatusType StartCOMExtension(void);");
            }
            hookHandle.WriteLine("void COMErrorHook (StatusType error);");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("extern void COMErrorHook (StatusType error);");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("#endif /* COM_HOOK_H_ */");


            hookHandle.Close();
            //END com_hook.h//////////////////
#if FALSE
            filePath = userFolder + @"\interaction_layer_callback.c";

            hookHandle = File.CreateText(filePath);
            
            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * interaction_layer_callback.c");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");

            hookHandle.Close();
            //END interaction_layer_callback.c//////////////////
#endif
            ///////////////////////////////////////////////Start Inteface_hook.h////////////////////////////////////////////////
            filePath = userFolder + @"\interface_hook.h";

            hookHandle = File.CreateText(filePath);

            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * interface_hook.h");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("#ifndef INTERFACE_HOOK_H_");
            hookHandle.WriteLine("#define INTERFACE_HOOK_H_");
            hookHandle.WriteLine("");

            hookHandle.WriteLine("extern void ProcessComReceive(uint32_t ipduID,uint8_t* data);");
            hookHandle.WriteLine("extern void ReceiveTpTask(uint32_t tpReceiveID, uint8_t *tpBuffer);");
            hookHandle.WriteLine("extern void UserSendConfirm(uint32_t messageID);");
            hookHandle.WriteLine("extern void SendTpConfirm(uint32_t tpSendID, uint8_t* tpData);");
            hookHandle.WriteLine("");

            hookHandle.WriteLine("#endif /* INTERFACE_HOOK_H_ */");
            hookHandle.Close();
            ///////////////////////////////////////////////End Inteface_hook.h////////////////////////////////////////////////

            ///////////////////////////////////////////////Start Inteface_hook.c//////////////////////////////////////////////
            filePath = userFolder + @"\interface_hook.c";

            hookHandle = File.CreateText(filePath);

            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * interface_hook.c");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");

            hookHandle.WriteLine("#include \"stdint.h\"");
            hookHandle.WriteLine("#include \"interface_hook.h\"");
            hookHandle.WriteLine("#include \"internal_osek_com_parameter.h\"");
            hookHandle.WriteLine("");
            //uint8_t ReceiveRawData(uint32_t msgID, uint8_t *protocolData) function 
            hookHandle.WriteLine("void ReceiveRawData (uint32_t msgID, uint8_t *protocolData)");
            hookHandle.WriteLine("{");
            //COM OPTION 
            hookHandle.WriteLine("    ProcessComReceive(msgID,protocolData);");
            //TP OPTION
            hookHandle.WriteLine("    ReceiveTpTask(msgID,protocolData);");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");

            //uint8_t SendRawData(uint32_t msgID, uint8_t protocolDLC, uint8_t *protocolData) function
            hookHandle.WriteLine("uint8_t SendRawData(uint32_t msgID, uint8_t protocolDLC, uint8_t *protocolData)");
            hookHandle.WriteLine("{");
            hookHandle.WriteLine("    uint8_t ret = 0;");
            hookHandle.WriteLine("    /*Implement Transmit Code(Return Success = 1, Fail = 0)*/");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("    return ret;");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");

            //void SendRawDataConfirm(uint32_t id, uint8_t *data) function 
            hookHandle.WriteLine("void SendRawDataConfirm(uint32_t msgID, uint8_t *protocolData)");
            hookHandle.WriteLine("{");
            //COM OPTION 
            hookHandle.WriteLine("    UserSendConfirm(msgID);");
            //TP OPTION
            hookHandle.WriteLine("    SendTpConfirm(msgID,protocolData);");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");
            hookHandle.Close();
            ///////////////////////////////////////////////End Inteface_hook.c//////////////////////////////////////////////

            ///////////////////////////////////////////////Start isotp_hook.c///////////////////////////////////////////////
            filePath = userFolder + @"\isotp_hook.c";

            hookHandle = File.CreateText(filePath);

            hookHandle.WriteLine(@"/*");
            hookHandle.WriteLine(" * isotp_hook.c");
            hookHandle.WriteLine(" *");
            hookHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            hookHandle.WriteLine("*/");
            hookHandle.WriteLine("");

            hookHandle.WriteLine("#include \"stdint.h\"");
            hookHandle.WriteLine("");
            //uint8_t* TpBufferHook(uint16_t handle) function 
            hookHandle.WriteLine("uint8_t* TpBufferHook(uint16_t tpHandle)");
            hookHandle.WriteLine("{");
            hookHandle.WriteLine("    /*Returns the receive buffer corresponding to the handle.*/");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");

            //void TpSendConfirmHook(uint32_t tpHandle, uint8_t *tpData, uint32_t tpSize)) function 
            hookHandle.WriteLine("void TpSendConfirmHook(uint32_t tpHandle, uint8_t *tpData, uint32_t tpSize)");
            hookHandle.WriteLine("{");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");

            //void TpReceiveConfirmHook(uint32_t tpHandle, uint8_t *tpData)
            hookHandle.WriteLine("void TpReceiveConfirmHook(uint32_t tpHandle, uint8_t *tpData)");
            hookHandle.WriteLine("{");
            hookHandle.WriteLine("");
            hookHandle.WriteLine("}");
            hookHandle.WriteLine("");
            hookHandle.Close();
            ///////////////////////////////////////////////End isotp_hook.c///////////////////////////////////////////////
        }

        public byte CreateComDefineFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\com_define.h";
            comDefineHandle = File.CreateText(filePath);
            return ret;
        }

        public byte CreateComParameterFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\com_paramter.c";
            comParameterHandle = File.CreateText(filePath);
            return ret;
        }

        public byte CreateComNotificationFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\com_notification.c";
            comNotificationHandle = File.CreateText(filePath);
            return ret;
        }

        public byte CreateIsoTPParameterFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\isotp_parameter.c";
            tpParameterHandle = File.CreateText(filePath);

            return ret;
        }

        public byte CreateIsoTPDefineFile()
        {
            byte ret = 0;
            string filePath = generationFolder + @"\isotp_define.h";
            tpDefineHandle = File.CreateText(filePath);

            return ret;
        }

        /*
                public byte create_test_paramter_file(string generationFilePath)
                {
                    byte ret = 0;
                    string filepath = generationFilePath + @"\test.txt";
                    com_test_fp = File.CreateText(filepath);
                    return ret;
                }


                public byte com_test_parser(General generalInfo, LinkedList<COMMessageAttributesInformation> messageInfo, LinkedList<COMSignalAttributesInformation> signalInfo)
                {
                    byte ret = 0;

                    com_test_fp.WriteLine("send Message");
                    foreach (COMMessageAttributesInformation messageObject in messageInfo)
                    {
                        if (messageObject.Direction == "SEND")
                        {
                            com_test_fp.WriteLine("send_msg name = {0}", messageObject.MsgName);
                        }
                    }
                    com_test_fp.WriteLine("");

                    com_test_fp.WriteLine("receive Message");
                    foreach (COMMessageAttributesInformation messageObject in messageInfo)
                    {
                        if (messageObject.Direction == "RECEIVE")
                        {
                            com_test_fp.WriteLine("receive_msg name = {0}", messageObject.MsgName);
                        }
                    }

                    com_test_fp.WriteLine("");

           

                    foreach (COMSignalAttributesInformation signalObject in signalInfo)
                    {
                        if (CheckDirectionByMessageId(signalObject.ParentMsgId, messageInfo) == "SEND")
                        {
                            com_test_fp.WriteLine("send_signal name = {0}", signalObject.SignalName);
                        }
                    }

                    com_test_fp.WriteLine("");



                    foreach (COMSignalAttributesInformation signalObject in signalInfo)
                    {
                        if (CheckDirectionByMessageId(signalObject.ParentMsgId, messageInfo) == "RECEIVE")
                        {
                            com_test_fp.WriteLine("receive_signal name = {0}", signalObject.SignalName);
                            test.AddLast(signalObject.SignalName);
                        }
                    }
                    com_test_fp.WriteLine("");
                    com_test_fp.WriteLine("");
                    com_test_fp.WriteLine("End");
                    com_test_fp.Close();  
                    return ret;

                }
        */
        public byte ComConfigurationParser(COMGeneral generalInfo)
        {
            comConfigurationHandle.WriteLine(@"/*");
            comConfigurationHandle.WriteLine(" * com_configuration.h");
            comConfigurationHandle.WriteLine(" *");
            comConfigurationHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            comConfigurationHandle.WriteLine("*/");
            comConfigurationHandle.WriteLine("");
            comConfigurationHandle.WriteLine("");
            comConfigurationHandle.WriteLine("#ifndef COM_CONFIGURATION_H_");
            comConfigurationHandle.WriteLine("#define COM_CONFIGURATION_H_");
            comConfigurationHandle.WriteLine("");

            if (generalInfo.StartCOMExtensionOption == "TRUE")
            {
                comConfigurationHandle.WriteLine("#define COM_ENABLE_START_COM_EXTENSION");
            }
            comConfigurationHandle.WriteLine("#endif /* COM_CONFIGURATION_H_ */");

            comConfigurationHandle.Close();

            return 1;
        }

        public byte ComDefineParser(COMGeneral generalInfo, LinkedList<ComMessageAttributesInformation> messageInfo, LinkedList<ComSignalAttributesInformation> signalInfo)
        {

            UInt32 comMessageMaxLength = 0;

            comDefineHandle.WriteLine(@"/*");
            comDefineHandle.WriteLine(" * com_define.h");
            comDefineHandle.WriteLine(" *");
            comDefineHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            comDefineHandle.WriteLine("*/");
            comDefineHandle.WriteLine("");
            comDefineHandle.WriteLine("");
            comDefineHandle.WriteLine("#ifndef COM_DEFINE_H_");
            comDefineHandle.WriteLine("#define COM_DEFINE_H_");
            comDefineHandle.WriteLine("");
            comDefineHandle.WriteLine("#include \"stdint.h\"");
            comDefineHandle.WriteLine(string.Format("#define CALL_TASK_TIME  {0}", generalInfo.ComTaskTime));

            if (generalInfo.AppModeName == "")
            {
                comDefineHandle.WriteLine(string.Format("#define {0}  {1}", "DEFAULT_APP_MODE", generalInfo.AppModeNumber));
            }
            else
            {
                comDefineHandle.WriteLine(string.Format("#define {0}  {1}", generalInfo.AppModeName, generalInfo.AppModeNumber));
            }
            comDefineHandle.WriteLine("");


            foreach (var sendMessageObject in relocateSendMessageInfo)
            {
                if (sendMessageObject.MessageComSupport == "TRUE")
                {
                    if (comMessageMaxLength < sendMessageObject.Length)
                    {
                        comMessageMaxLength = sendMessageObject.Length;
                    }
                    else
                    {
                        ;
                    }

                }
            }

            foreach (var receiveMessageObject in relocateReceiveMessageInfo)
            {
                if (receiveMessageObject.MessageComSupport == "TRUE")
                {
                    if (comMessageMaxLength < receiveMessageObject.Length)
                    {
                        comMessageMaxLength = receiveMessageObject.Length;
                    }
                    else
                    {
                        ;
                    }

                }
            }


            comDefineHandle.WriteLine(string.Format("#define COM_MESSAGE_MAX_LENGTH  {0}", comMessageMaxLength));
            comDefineHandle.WriteLine(string.Format("#define COM_SEND_MESSAGE_COUNT  {0}", comSendMessageCount));
            comDefineHandle.WriteLine(string.Format("#define COM_SEND_SIGNAL_COUNT  {0}", comSendSignalCount));
            comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_MESSAGE_COUNT  {0}", comReceiveMessageCount));
            comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_SIGNAL_COUNT  {0}", comReceiveSignalCount));


            if ((comSendMessageErrorIndicationCount % 8) == 0)
            {
                comSendMessageErrorIndicationCount = (comSendMessageErrorIndicationCount / 8);
                if (comSendMessageErrorIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_SEND_MESSAGE_ERROR_INDICATION_COUNT  {0}", comSendMessageErrorIndicationCount));
                }
                else
                {
                    ;
                }
            }
            else
            {
                comSendMessageErrorIndicationCount = (comSendMessageErrorIndicationCount / 8) + 1;
                if (comSendMessageErrorIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_SEND_MESSAGE_ERROR_INDICATION_COUNT  {0}", comSendMessageErrorIndicationCount));
                }
                else
                {
                    ;
                }
            }

            if ((comReceiveMessageErrorIndicationCount % 8) == 0)
            {
                comReceiveMessageErrorIndicationCount = (comReceiveMessageErrorIndicationCount / 8);
                if (comReceiveMessageErrorIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_MESSAGE_ERROR_INDICATION_COUNT  {0}", comReceiveMessageErrorIndicationCount));
                }
                else
                {
                    ;
                }
            }
            else
            {
                comReceiveMessageErrorIndicationCount = (comReceiveMessageErrorIndicationCount / 8) + 1;
                if (comReceiveMessageErrorIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_MESSAGE_ERROR_INDICATION_COUNT  {0}", comReceiveMessageErrorIndicationCount));
                }
                else
                {
                    ;
                }
            }

            if ((comSendSignalNotificationIndicationCount % 8) == 0)
            {
                comSendSignalNotificationIndicationCount = (comSendSignalNotificationIndicationCount / 8);
                if (comSendSignalNotificationIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_SEND_SIGNAL_NOTIFICATION_INDICATION_COUNT  {0}", comSendSignalNotificationIndicationCount));
                }
                else
                {
                    ;
                }
            }
            else
            {
                comSendSignalNotificationIndicationCount = (comSendSignalNotificationIndicationCount / 8) + 1;
                if (comSendSignalNotificationIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_SEND_SIGNAL_NOTIFICATION_INDICATION_COUNT  {0}", comSendSignalNotificationIndicationCount));
                }
                else
                {
                    ;
                }
            }

            if ((comReceiveSignalNotificationIndicationCount % 8) == 0)
            {
                comReceiveSignalNotificationIndicationCount = (comReceiveSignalNotificationIndicationCount / 8);
                if (comReceiveSignalNotificationIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_SIGNAL_NOTIFICATION_INDICATION_COUNT  {0}", comReceiveSignalNotificationIndicationCount));
                }
                else
                {
                    ;
                }
            }
            else
            {
                comReceiveSignalNotificationIndicationCount = (comReceiveSignalNotificationIndicationCount / 8) + 1;
                if (comReceiveSignalNotificationIndicationCount > 0)
                {
                    comDefineHandle.WriteLine(string.Format("#define COM_RECEIVE_SIGNAL_NOTIFICATION_INDICATION_COUNT  {0}", comReceiveSignalNotificationIndicationCount));
                }
                else
                {
                    ;
                }
            }

            comDefineHandle.WriteLine("");

            //extern////////////////
            comDefineHandle.WriteLine(@"/*Send Notification Flag Object */");
            if (comSendMessageErrorIndicationCount > 0)
            {
                comDefineHandle.WriteLine("extern uint8_t comSendMsgErrorIndicationFlag[COM_SEND_MESSAGE_ERROR_INDICATION_COUNT];");
            }
            else
            {
                ;
            }

            if (comSendSignalNotificationIndicationCount > 0)
            {
                comDefineHandle.WriteLine("extern uint8_t comSendSignalIndicationFlag[COM_SEND_SIGNAL_NOTIFICATION_INDICATION_COUNT];");
            }
            else
            {
                ;
            }

            comDefineHandle.WriteLine(@"/*Receive Notification Flag Object */");
            if (comReceiveMessageErrorIndicationCount > 0)
            {
                comDefineHandle.WriteLine("extern uint8_t comReceiveMessageErrorIndicationFlag[COM_RECEIVE_MESSAGE_ERROR_INDICATION_COUNT];");
            }
            else
            {
                ;
            }

            if (comReceiveSignalNotificationIndicationCount > 0)
            {
                comDefineHandle.WriteLine("extern uint8_t comReceiveSignalIndicationFlag[COM_RECEIVE_SIGNAL_NOTIFICATION_INDICATION_COUNT];");
            }
            else
            {
                ;
            }

            //////////////////////////////////////////////////////////
            comDefineHandle.WriteLine(@"/*Send Message Handle */");
            foreach (ComMessageAttributesInformation messageObject in relocateSendMessageInfo)
            {
                comDefineHandle.WriteLine(string.Format("#define COM_MSG_HANDLE_{0}  {1}", messageObject.MsgName, messageObject.Handle));
            }

            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/*Receive Message Handle */");

            foreach (ComMessageAttributesInformation messageObject in relocateReceiveMessageInfo)
            {
                comDefineHandle.WriteLine(string.Format("#define COM_MSG_HANDLE_{0}  {1}", messageObject.MsgName, messageObject.Handle));
            }
            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/*Send Signal Handle */");

            foreach (ComSignalAttributesInformation signalObject in relocateSendSignalInfo)
            {
                comDefineHandle.WriteLine(string.Format("#define COM_SIG_HANDLE_{0}_{1}  {2}", ReadMessageNameById(signalObject.ParentMsgId, messageInfo), signalObject.SignalName, signalObject.Handle));
            }
            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/*Receive Signal Handle */");

            foreach (ComSignalAttributesInformation signalObject in relocateReceiveSignalInfo)
            {
                comDefineHandle.WriteLine(string.Format("#define COM_SIG_HANDLE_{0}_{1}  {2}", ReadMessageNameById(signalObject.ParentMsgId, messageInfo), signalObject.SignalName, signalObject.Handle));
            }
            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/* Send Message Deadline Monitoring Notification Flag*/");
            foreach (ComMessageAttributesInformation messageObject in relocateSendMessageInfo)
            {
                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((messageObject.NotificationType == "ALL") || (messageObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comDefineHandle.WriteLine(string.Format("#define READ_FLAG_{0}()  ((comSendMsgErrorIndicationFlag[{1}] >> {2}) & 0x1) ", messageObject.MsgName, messageObject.HandleNotificationIndication, messageObject.HandleNotificationIndicationOffset));
                        comDefineHandle.WriteLine(string.Format("#define RESET_FLAG_{0}()  (comSendMsgErrorIndicationFlag[{1}] &= ~(1 << {2}))", messageObject.MsgName, messageObject.HandleNotificationIndication, messageObject.HandleNotificationIndicationOffset));
                    }
                    else
                    {
                        ;
                    }

                }
                else
                {
                    ;
                }
            }
            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/* Receive Message Deadline Monitoring Notification Flag*/");
            foreach (ComMessageAttributesInformation messageObject in relocateReceiveMessageInfo)
            {
                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((messageObject.NotificationType == "ALL") || (messageObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comDefineHandle.WriteLine(string.Format("#define READ_FLAG_{0}()  ((comReceiveMessageErrorIndicationFlag[{1}] >> {2}) & 0x1) ", messageObject.MsgName, messageObject.HandleNotificationIndication, messageObject.HandleNotificationIndicationOffset));
                        comDefineHandle.WriteLine(string.Format("#define RESET_FLAG_{0}()  (comReceiveMessageErrorIndicationFlag[{1}] &= ~(1 << {2}))", messageObject.MsgName, messageObject.HandleNotificationIndication, messageObject.HandleNotificationIndicationOffset));
                    }
                    else
                    {
                        ;
                    }

                }
                else
                {
                    ;
                }
            }

            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/* Send Signal Notification Flag*/");
            foreach (ComSignalAttributesInformation signalObject in relocateSendSignalInfo)
            {
                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((signalObject.NotificationType == "ALL") || (signalObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comDefineHandle.WriteLine(string.Format("#define READ_FLAG_{0}_{1}()  ((comSendSignalIndicationFlag[{2}] >> {3}) & 0x1)", ReadMessageNameById(signalObject.ParentMsgId, relocateSendMessageInfo), signalObject.SignalName, signalObject.HandleNotificationIndication, signalObject.HandleNotificationIndicationOffset));
                        comDefineHandle.WriteLine(string.Format("#define RESET_FLAG_{0}_{1}()  (comSendSignalIndicationFlag[{2}] &= ~(1 << {3}))", ReadMessageNameById(signalObject.ParentMsgId, relocateSendMessageInfo), signalObject.SignalName, signalObject.HandleNotificationIndication, signalObject.HandleNotificationIndicationOffset));
                    }
                    else
                    {
                        ;
                    }

                }
                else
                {
                    ;
                }
            }
            comDefineHandle.WriteLine("");

            comDefineHandle.WriteLine(@"/* Receive Signal Notification Flag*/");
            foreach (ComSignalAttributesInformation signalObject in relocateReceiveSignalInfo)
            {
                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((signalObject.NotificationType == "ALL") || (signalObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comDefineHandle.WriteLine(string.Format("#define READ_FLAG_{0}_{1}()  ((comReceiveSignalIndicationFlag[{2}] >> {3}) & 0x1)", ReadMessageNameById(signalObject.ParentMsgId, relocateReceiveMessageInfo), signalObject.SignalName, signalObject.HandleNotificationIndication, signalObject.HandleNotificationIndicationOffset));
                        comDefineHandle.WriteLine(string.Format("#define RESET_FLAG_{0}_{1}()  (comReceiveSignalIndicationFlag[{2}] &= ~(1 << {3}))", ReadMessageNameById(signalObject.ParentMsgId, relocateReceiveMessageInfo), signalObject.SignalName, signalObject.HandleNotificationIndication, signalObject.HandleNotificationIndicationOffset));
                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    ;
                }
            }

            comDefineHandle.WriteLine("");
            comDefineHandle.WriteLine("");
            comDefineHandle.WriteLine("#endif /* COM_DEFINE_H_ */");

            comDefineHandle.Close();
            return 1;
        }

        public byte ComParameterParser(COMGeneral generalInfo, LinkedList<ComMessageAttributesInformation> messageInfo, LinkedList<ComSignalAttributesInformation> signalInfo)
        {
            comParameterHandle.WriteLine(@"/*");
            comParameterHandle.WriteLine(" * com_parameter.c");
            comParameterHandle.WriteLine(" *");
            comParameterHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            comParameterHandle.WriteLine("*/");
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("#include \"internal_osek_com_parameter.h\"");
            comParameterHandle.WriteLine("#include \"com_define.h\"");
            comParameterHandle.WriteLine("#include \"stdint.h\"");

            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Send Message Notification Callback*/");
            foreach (ComMessageAttributesInformation messageObject in relocateSendMessageInfo)
            {
                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((messageObject.NotificationType == "ALL") || (messageObject.NotificationType == "NOTIFCATION_CALLBACK"))
                    {
                        if (messageObject.NotificationCallbackName == "")
                        {

                            messageObject.NotificationCallbackName = string.Format("{0}_TransmissionErrorCallback", messageObject.MsgName);
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", messageObject.NotificationCallbackName));

                        }
                        else
                        {
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", messageObject.NotificationCallbackName));
                        }

                    }
                }
            }
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Send Signal Notification Callback*/");
            foreach (ComSignalAttributesInformation signalObject in relocateSendSignalInfo)
            {
                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((signalObject.NotificationType == "ALL") || (signalObject.NotificationType == "NOTIFCATION_CALLBACK"))
                    {
                        if (signalObject.NotificationCallbackName == "")
                        {
                            signalObject.NotificationCallbackName = string.Format("{0}_{1}_TransmissionCompleteCallback", ReadMessageNameById(signalObject.ParentMsgId, relocateSendMessageInfo), signalObject.SignalName);
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", signalObject.NotificationCallbackName));
                        }
                        else
                        {
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", signalObject.NotificationCallbackName));
                        }

                    }
                }
            }
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Receive Message Notification Callback*/");
            foreach (ComMessageAttributesInformation messageObject in relocateReceiveMessageInfo)
            {
                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((messageObject.NotificationType == "ALL") || (messageObject.NotificationType == "NOTIFCATION_CALLBACK"))
                    {
                        if (messageObject.NotificationCallbackName == "")
                        {
                            messageObject.NotificationCallbackName = string.Format("{0}_ReceptionErrorCallback", messageObject.MsgName);
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", messageObject.NotificationCallbackName));
                        }
                        else
                        {
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", messageObject.NotificationCallbackName));
                        }

                    }
                }
            }
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Receive Signal Notification Callback*/");
            foreach (ComSignalAttributesInformation signalObject in relocateReceiveSignalInfo)
            {
                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((signalObject.NotificationType == "ALL") || (signalObject.NotificationType == "NOTIFCATION_CALLBACK"))
                    {
                        if (signalObject.NotificationCallbackName == "")
                        {
                            signalObject.NotificationCallbackName = string.Format("{0}_{1}_ReceptionCompleteCallback", ReadMessageNameById(signalObject.ParentMsgId, relocateReceiveMessageInfo), signalObject.SignalName);
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", signalObject.NotificationCallbackName));
                        }
                        else
                        {
                            comParameterHandle.WriteLine(string.Format("extern void {0}();", signalObject.NotificationCallbackName));
                        }

                    }
                }
            }
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Send Message Buffer*/");
            foreach (ComMessageAttributesInformation messageObject in relocateSendMessageInfo)
            {
                messageObject.BufferName = string.Format("{0}_MessageBuffer", messageObject.MsgName);
                comParameterHandle.WriteLine(string.Format("uint8_t {0}[{1}];", messageObject.BufferName, messageObject.Length));
            }

            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Receive Message Buffer*/");
            foreach (ComMessageAttributesInformation messageObject in relocateReceiveMessageInfo)
            {

                messageObject.BufferName = string.Format("{0}_MessageBuffer", messageObject.MsgName);
                comParameterHandle.WriteLine(string.Format("uint8_t {0}[{1}];", messageObject.BufferName, messageObject.Length));
            }

            comParameterHandle.WriteLine("");

            /*Notification이 적용된 메시지 혹은 시그널이 없을 경우 생성하지 않는다.*/
            comParameterHandle.WriteLine("/*Send Notification Flag Object */");
            if (comSendMessageErrorIndicationCount > 0)
            {
                comParameterHandle.WriteLine("uint8_t comSendMsgErrorIndication_flag[COM_SEND_MESSAGE_ERROR_INDICATION_COUNT];");

            }
            if (comSendSignalNotificationIndicationCount > 0)
            {
                comParameterHandle.WriteLine("uint8_t comSendSignalIndicationFlag[COM_SEND_SIGNAL_NOTIFICATION_INDICATION_COUNT];");
            }

            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/*Receive Notification Flag Object */");

            if (comReceiveMessageErrorIndicationCount > 0)
            {
                comParameterHandle.WriteLine("uint8_t comReceiveMessageErrorIndicationFlag[COM_RECEIVE_MESSAGE_ERROR_INDICATION_COUNT];");
            }

            if (comReceiveSignalNotificationIndicationCount > 0)
            {
                comParameterHandle.WriteLine("uint8_t comReceiveSignalIndicationFlag[COM_RECEIVE_SIGNAL_NOTIFICATION_INDICATION_COUNT];");
            }
            /*Notification이 적용된 메시지 혹은 시그널이 없을 경우 생성하지 않는다.*/


            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/* Send Message Property Information*/");

            comParameterHandle.WriteLine("message_property_info_t comSendMsgProperty[COM_SEND_MESSAGE_COUNT] =");
            comParameterHandle.WriteLine("{");

            foreach (ComMessageAttributesInformation messageObject in relocateSendMessageInfo)
            {
                comParameterHandle.WriteLine("    {");
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageHandle */", messageObject.Handle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageLength */", messageObject.Length));
                comParameterHandle.WriteLine(String.Format("     {0}, /* cycleTime */", messageObject.CycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadCycleTime */", messageObject.CycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* repetitionCycleTime */", messageObject.RepetitionCycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadRepetitionCycleTime */", messageObject.RepetitionCycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* repetitionNumber */", messageObject.RepetitionNumber));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadRepetitionNumber */", messageObject.RepetitionNumber));
                comParameterHandle.WriteLine(String.Format("     0, /* messageDelayTime */"));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadMessageDelayTime */", messageObject.MessageDelayTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* sendMode */", messageObject.SendMode));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageDirection */", messageObject.Direction));
                comParameterHandle.WriteLine(String.Format("     \"{0}\", /* messageName */", messageObject.MsgName));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageID */", messageObject.MsgID));
                comParameterHandle.WriteLine(String.Format("     {{{0},{1},{2}}}, /* deadlineMonitor */", messageObject.DeadLineMonitoringOption, messageObject.DeadLineMonitoringTimeout / generalInfo.ComTaskTime, messageObject.DeadLineMonitoringTimeout / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startOffsetDelay */", messageObject.StartOffsetDelay / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadStartOffsetDelay */", messageObject.StartOffsetDelay / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine("     START_OFFSET_DEACTIVE, /* startOffsetState */");
                comParameterHandle.WriteLine("     NOT_CHECK_SEND_CONFIRM, /* sendConfirm */");
                comParameterHandle.WriteLine("     SEND_INITIAL_TIME_INVALID_TYPE, /* sendInitialTimeType */");
                comParameterHandle.WriteLine("     NON_OCCUR_DIRECT_SEND_EVENT, /* directSendEvent */");
                //comParameterHandle.WriteLine("     NON_OCCUR_REPETITION_SEND_EVENT, /* repetitionSendEvent */");
                comParameterHandle.WriteLine("     NON_MESSGE_SEND_REQUEST, /* messageSendRequest */");
                comParameterHandle.WriteLine(String.Format("     {0}, /* *currentMessageObject */", messageObject.BufferName));

                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if (messageObject.NotificationType == "ALL")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionErrorNotification */", messageObject.NotificationOption, "NOTIFCATION_INDICATE | NOTIFCATION_CALLBACK", messageObject.NotificationCallbackName));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    }
                    else if (messageObject.NotificationType == "NOTIFCATION_CALLBACK")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionErrorNotification */", messageObject.NotificationOption, messageObject.NotificationType, messageObject.NotificationCallbackName));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    }
                    else /*Indicate */
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionErrorNotification */", messageObject.NotificationOption, messageObject.NotificationType, "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));

                    }
                }
                else
                {
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                }

                //  comParameterHandle.WriteLine(String.Format("     {0},",messageObject));
                comParameterHandle.WriteLine("    },");
                comParameterHandle.WriteLine("");

            }
            comParameterHandle.WriteLine("};");
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/* Receive Message Property Information*/");

            comParameterHandle.WriteLine("message_property_info_t comReceiveMsgProperty[COM_RECEIVE_MESSAGE_COUNT] =");
            comParameterHandle.WriteLine("{");

            foreach (ComMessageAttributesInformation messageObject in relocateReceiveMessageInfo)
            {
                comParameterHandle.WriteLine("    {");
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageHandle */", messageObject.Handle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageLength */", messageObject.Length));
                comParameterHandle.WriteLine(String.Format("     {0}, /* cycleTime */", messageObject.CycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadCycleTime */", messageObject.CycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* repetitionCycleTime */", messageObject.RepetitionCycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadRepetitionCycleTime */", messageObject.RepetitionCycleTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* repetitionNumber */", messageObject.RepetitionNumber));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadRepetitionNumber */", messageObject.RepetitionNumber));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageDelayTime */",0));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadMessageDelayTime */", messageObject.MessageDelayTime / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* sendMode */", messageObject.SendMode));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageDirection */", messageObject.Direction));
                comParameterHandle.WriteLine(String.Format("     \"{0}\", /* messageName */", messageObject.MsgName));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageID */", messageObject.MsgID));
                comParameterHandle.WriteLine(String.Format("     {{{0},{1},{2}}}, /* deadlineMonitor */", messageObject.DeadLineMonitoringOption, messageObject.DeadLineMonitoringTimeout / generalInfo.ComTaskTime, messageObject.DeadLineMonitoringTimeout / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startOffsetDelay */", messageObject.StartOffsetDelay / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine(String.Format("     {0}, /* reloadStartOffsetDelay */", messageObject.StartOffsetDelay / generalInfo.ComTaskTime));
                comParameterHandle.WriteLine("     START_OFFSET_DEACTIVE, /* startOffsetState */");
                comParameterHandle.WriteLine("     NOT_CHECK_SEND_CONFIRM, /* sendConfirm */");
                comParameterHandle.WriteLine("     SEND_INITIAL_TIME_INVALID_TYPE, /* sendInitialTimeType */");
                comParameterHandle.WriteLine("     NON_OCCUR_DIRECT_SEND_EVENT, /* directSendEvent */");
                //comParameterHandle.WriteLine("     NON_OCCUR_REPETITION_SEND_EVENT, /* repetitionSendEvent */");
                comParameterHandle.WriteLine("     NON_MESSGE_SEND_REQUEST, /* messageSendRequest */");
                comParameterHandle.WriteLine(String.Format("     {0}, /* *currentMessageObject */", messageObject.BufferName));

                if (messageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if (messageObject.NotificationType == "ALL")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionErrorNotification */", messageObject.NotificationOption, "NOTIFCATION_INDICATE | NOTIFCATION_CALLBACK", messageObject.NotificationCallbackName));
                    }
                    else if (messageObject.NotificationType == "NOTIFCATION_CALLBACK")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionErrorNotification */", messageObject.NotificationOption, messageObject.NotificationType, messageObject.NotificationCallbackName));
                    }
                    else /*Indicate */
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionErrorNotification */", messageObject.NotificationOption, messageObject.NotificationType, "NULL"));
                    }
                }
                else
                {
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionErrorNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                }

                comParameterHandle.WriteLine("    },");
                comParameterHandle.WriteLine("");

            }
            comParameterHandle.WriteLine("};");
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/* Set Send Singal Property Information*/");
            comParameterHandle.WriteLine("signal_property_info_t comSendSignalProperty[COM_SEND_SIGNAL_COUNT] =");
            comParameterHandle.WriteLine("{");

            foreach (ComSignalAttributesInformation signalObject in relocateSendSignalInfo)
            {
                comParameterHandle.WriteLine("    {");

                comParameterHandle.WriteLine(String.Format("     \"{0}\", /* signalName */", signalObject.SignalName));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalHandle */", signalObject.Handle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalLength */", signalObject.BitLength));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageID */", signalObject.ParentMsgId));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageHandle */", signalObject.ParentMsgHandle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* sendProperty */", signalObject.SendProperty));
                comParameterHandle.WriteLine(String.Format("     {0}, /* filterAlorigthm */", signalObject.FilterAlogirithm));
                comParameterHandle.WriteLine(String.Format("     {0}, /* byteOrder */", signalObject.ByteOrder));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startOffsetBit */", signalObject.StartOffsetBit));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startValue */", signalObject.StartValue));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalData */", signalObject.StartValue));
                comParameterHandle.WriteLine(String.Format("     {0}, /* timeoutValue */", signalObject.TimeoutValue));


                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    string direction = CheckDirectionByMessageId(signalObject.ParentMsgId, messageInfo);
                    if (signalObject.NotificationType == "ALL")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionNotification */", signalObject.NotificationOption, "NOTIFCATION_INDICATE | NOTIFCATION_CALLBACK", signalObject.NotificationCallbackName));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    }
                    else if (signalObject.NotificationType == "NOTIFCATION_CALLBACK")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionNotification */", signalObject.NotificationOption, signalObject.NotificationType, signalObject.NotificationCallbackName));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));

                    }
                    else /*Indicate */
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionNotification */", signalObject.NotificationOption, signalObject.NotificationType, "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    }
                }
                else
                {
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                }

                //  comParameterHandle.WriteLine(String.Format("     {0},",messageObject));
                comParameterHandle.WriteLine("    },");
                comParameterHandle.WriteLine("");

            }

            comParameterHandle.WriteLine("};");
            comParameterHandle.WriteLine("");

            comParameterHandle.WriteLine("/* Set Receive Singal Property Information*/");
            comParameterHandle.WriteLine("signal_property_info_t comReceiveSignalProperty[COM_RECEIVE_SIGNAL_COUNT] =");
            comParameterHandle.WriteLine("{");

            foreach (ComSignalAttributesInformation signalObject in relocateReceiveSignalInfo)
            {
                comParameterHandle.WriteLine("    {");

                comParameterHandle.WriteLine(String.Format("     \"{0}\", /* signalName */", signalObject.SignalName));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalHandle */", signalObject.Handle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalLength */", signalObject.BitLength));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageID */", signalObject.ParentMsgId));
                comParameterHandle.WriteLine(String.Format("     {0}, /* messageHandle */", signalObject.ParentMsgHandle));
                comParameterHandle.WriteLine(String.Format("     {0}, /* sendProperty */", signalObject.SendProperty));
                comParameterHandle.WriteLine(String.Format("     {0}, /* filterAlorigthm */", signalObject.FilterAlogirithm));
                comParameterHandle.WriteLine(String.Format("     {0}, /* byteOrder */", signalObject.ByteOrder));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startOffsetBit */", signalObject.StartOffsetBit));
                comParameterHandle.WriteLine(String.Format("     {0}, /* startValue */", signalObject.StartValue));
                comParameterHandle.WriteLine(String.Format("     {0}, /* signalData */", signalObject.StartValue));
                comParameterHandle.WriteLine(String.Format("     {0}, /* timeoutValue */", signalObject.TimeoutValue));

                if (signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    string direction = CheckDirectionByMessageId(signalObject.ParentMsgId, messageInfo);
                    if (signalObject.NotificationType == "ALL")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionNotification */", signalObject.NotificationOption, "NOTIFCATION_INDICATE | NOTIFCATION_CALLBACK", signalObject.NotificationCallbackName));
                    }
                    else if (signalObject.NotificationType == "NOTIFCATION_CALLBACK")
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* transmissionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}}, /* receptionNotification */", signalObject.NotificationOption, signalObject.NotificationType, signalObject.NotificationCallbackName));
                    }
                    else /*Indicate */
                    {
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                        comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionNotification */", signalObject.NotificationOption, signalObject.NotificationType, "NULL"));
                    }
                }
                else
                {
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* transmissionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                    comParameterHandle.WriteLine(string.Format("     {{{0},{1},{2}}},/* receptionNotification */", "NOTIFICATION_DEACTIVE", "NOTIFICATION_INVALID", "NULL"));
                }

                //  comParameterHandle.WriteLine(String.Format("     {0},",messageObject));
                comParameterHandle.WriteLine("    },");
                comParameterHandle.WriteLine("");

            }

            comParameterHandle.WriteLine("};");
            comParameterHandle.Close();
            return 1;
        }


        public byte ComNotifcationParser()
        {
            byte ret = 0;
            comNotificationHandle.WriteLine(@"/*");
            comNotificationHandle.WriteLine(" * com_notification.c");
            comNotificationHandle.WriteLine(" *");
            comNotificationHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            comNotificationHandle.WriteLine("*/");
            comNotificationHandle.WriteLine("");
            comNotificationHandle.WriteLine("#include \"com_define.h\"");
            comNotificationHandle.WriteLine("#include \"stdint.h\"");

            //            comNotificationHandle.WriteLine("/*Send Notification Flag Object Reference */");
            /*           if (comSendMessageErrorIndicationCount > 0)
                        {
                            comNotificationHandle.WriteLine("extern uint8_t comSendMsgErrorIndication_flag[comSendMessageErrorIndicationCount];");

                        }
                        if (comSendSignalNotificationIndicationCount > 0)
                        {
                            comNotificationHandle.WriteLine("extern uint8_t comSendSignalIndicationFlag[comSendSignalNotificationIndicationCount];");
                        }

                        comNotificationHandle.WriteLine("");
            */
            //            comNotificationHandle.WriteLine("/*Receive Notification Flag Object Reference */");

            /*           if (comReceiveMessageErrorIndicationCount > 0)
                        {
                            comNotificationHandle.WriteLine("extern uint8_t comReceiveMessageErrorIndicationFlag[comReceiveMessageErrorIndicationCount];");
                        }

                        if (comReceiveSignalNotificationIndicationCount > 0)
                        {
                            comNotificationHandle.WriteLine("extern uint8_t comReceiveSignalIndicationFlag[COM_RECEIVE_SIGNAL_NOTIFICATION_INDICATION_COUNT];");
                        }
                        comNotificationHandle.WriteLine("");
             */
            /******************************************************************************************************/
            /******************************Send Message Error Indication Function Generate Start*******************/
            /******************************************************************************************************/
            comNotificationHandle.WriteLine("uint8_t ComSetSendMessageErrorIndication(uint32_t messageHandle)");
            comNotificationHandle.WriteLine("{");
            comNotificationHandle.WriteLine("    uint8_t ret = 1;");
            comNotificationHandle.WriteLine("    switch(messageHandle)");
            comNotificationHandle.WriteLine("    {");

            foreach (ComMessageAttributesInformation send_message_object in relocateSendMessageInfo)
            {
                if (send_message_object.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((send_message_object.NotificationType == "ALL") || (send_message_object.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comNotificationHandle.WriteLine("        case COM_MSG_HANDLE_{0}:", send_message_object.MsgName);
                        comNotificationHandle.WriteLine("            comSendMsgErrorIndicationFlag[{0}] |= 1 << {1};", send_message_object.HandleNotificationIndication, send_message_object.HandleNotificationIndicationOffset);
                        comNotificationHandle.WriteLine("            break;");
                    }
                }
            }

            comNotificationHandle.WriteLine("        default:");
            comNotificationHandle.WriteLine("            ret = 0;");
            comNotificationHandle.WriteLine("        break;");
            comNotificationHandle.WriteLine("    }");
            comNotificationHandle.WriteLine("    return ret;");

            comNotificationHandle.WriteLine("}");
            comNotificationHandle.WriteLine("");
            /******************************************************************************************************/
            /******************************Send Message Error Indication Function Generate End*********************/
            /******************************************************************************************************/

            /******************************************************************************************************/
            /******************************Send Signal Indication Function Generate Start**************************/
            /******************************************************************************************************/

            comNotificationHandle.WriteLine("uint8_t ComSetSendSignalIndication(uint32_t signalHandle)");
            comNotificationHandle.WriteLine("{");
            comNotificationHandle.WriteLine("    uint8_t ret = 1;");
            comNotificationHandle.WriteLine("    switch(signalHandle)");
            comNotificationHandle.WriteLine("    {");

            foreach (var send_signalObject in relocateSendSignalInfo)
            {

                if (send_signalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((send_signalObject.NotificationType == "ALL") || (send_signalObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        //comDefineHandle.WriteLine(string.Format("#define COM_MSG_HANDLE_{0}  {1}", messageObject.MsgName, messageObject.Handle));
                        comNotificationHandle.WriteLine("        case COM_SIG_HANDLE_{0}_{1}:", ReadMessageNameById(send_signalObject.ParentMsgId, relocateSendMessageInfo), send_signalObject.SignalName);
                        comNotificationHandle.WriteLine("            comSendSignalIndicationFlag[{0}] |= 1 << {1};", send_signalObject.HandleNotificationIndication, send_signalObject.HandleNotificationIndicationOffset);
                        comNotificationHandle.WriteLine("            break;");
                    }
                }

            }

            comNotificationHandle.WriteLine("        default:");
            comNotificationHandle.WriteLine("            ret = 0;");
            comNotificationHandle.WriteLine("        break;");
            comNotificationHandle.WriteLine("    }");
            comNotificationHandle.WriteLine("    return ret;");

            comNotificationHandle.WriteLine("}");
            comNotificationHandle.WriteLine("");
            /******************************************************************************************************/
            /******************************Send Signal Indication Function Generate End****************************/
            /******************************************************************************************************/


            /******************************************************************************************************/
            /******************************Receive Message Error Indication Function Generate*************************/
            /******************************************************************************************************/
            comNotificationHandle.WriteLine("uint8_t ComSetReceiveMessageErrorIndication(uint32_t messageHandle)");
            comNotificationHandle.WriteLine("{");
            comNotificationHandle.WriteLine("    uint8_t ret = 1;");
            comNotificationHandle.WriteLine("    switch(messageHandle)");
            comNotificationHandle.WriteLine("    {");

            foreach (ComMessageAttributesInformation receiveMessageObject in relocateReceiveMessageInfo)
            {
                if (receiveMessageObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((receiveMessageObject.NotificationType == "ALL") || (receiveMessageObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        comNotificationHandle.WriteLine("        case COM_MSG_HANDLE_{0}:", receiveMessageObject.MsgName);
                        comNotificationHandle.WriteLine("            comReceiveMessageErrorIndicationFlag[{0}] |= 1 << {1};", receiveMessageObject.HandleNotificationIndication, receiveMessageObject.HandleNotificationIndicationOffset);
                        comNotificationHandle.WriteLine("            break;");
                    }
                }
            }

            comNotificationHandle.WriteLine("        default:");
            comNotificationHandle.WriteLine("            ret = 0;");
            comNotificationHandle.WriteLine("        break;");
            comNotificationHandle.WriteLine("    }");
            comNotificationHandle.WriteLine("    return ret;");

            comNotificationHandle.WriteLine("}");
            comNotificationHandle.WriteLine("");
            /******************************************************************************************************/
            /******************************Receive Message Error Indication Function Generate*************************/
            /******************************************************************************************************/

            /******************************************************************************************************/
            /******************************Receive Signal Indication Function Generate Start***********************/
            /******************************************************************************************************/
            comNotificationHandle.WriteLine("uint8_t ComSetReceiveSignalIndication(uint32_t signalHandle)");
            comNotificationHandle.WriteLine("{");
            comNotificationHandle.WriteLine("    uint8_t ret = 1;");
            comNotificationHandle.WriteLine("    switch(signalHandle)");
            comNotificationHandle.WriteLine("    {");

            foreach (var receiveSignalObject in relocateReceiveSignalInfo)
            {
                if (receiveSignalObject.NotificationOption == "NOTIFICATION_ACTIVE")
                {
                    if ((receiveSignalObject.NotificationType == "ALL") || (receiveSignalObject.NotificationType == "NOTIFCATION_INDICATE"))
                    {
                        //comDefineHandle.WriteLine(string.Format("#define COM_MSG_HANDLE_{0}  {1}", messageObject.MsgName, messageObject.Handle));
                        comNotificationHandle.WriteLine("        case COM_SIG_HANDLE_{0}_{1}:", ReadMessageNameById(receiveSignalObject.ParentMsgId, relocateReceiveMessageInfo), receiveSignalObject.SignalName);
                        comNotificationHandle.WriteLine("            comReceiveSignalIndicationFlag[{0}] |= 1 << {1};", receiveSignalObject.HandleNotificationIndication, receiveSignalObject.HandleNotificationIndicationOffset);
                        comNotificationHandle.WriteLine("            break;");
                    }
                }

            }
            comNotificationHandle.WriteLine("        default:");
            comNotificationHandle.WriteLine("            ret = 0;");
            comNotificationHandle.WriteLine("        break;");
            comNotificationHandle.WriteLine("    }");
            comNotificationHandle.WriteLine("    return ret;");

            comNotificationHandle.WriteLine("}");
            /******************************************************************************************************/
            /******************************Receive Signal Indication Function Generate End ************************/
            /******************************************************************************************************/


            comNotificationHandle.WriteLine("");
            comNotificationHandle.Close();

            return ret;
        }
        public void TpDefineParser(TPCommon getTPCommonInfo)
        {
            tpDefineHandle.WriteLine(@"/*");
            tpDefineHandle.WriteLine(" * isotp_parameter.c");
            tpDefineHandle.WriteLine(" *");
            tpDefineHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            tpDefineHandle.WriteLine("*/");
            tpDefineHandle.WriteLine("");
            tpDefineHandle.WriteLine("");
            tpDefineHandle.WriteLine("#ifndef ISOTP_DEFINE_H_");
            tpDefineHandle.WriteLine("#define ISOTP_DEFINE_H_");
            tpDefineHandle.WriteLine("");
            tpDefineHandle.WriteLine("");
            tpDefineHandle.WriteLine("#include \"iso15765_TP.h\"");
            tpDefineHandle.WriteLine("#include \"isotp_define.h\"");
            tpDefineHandle.WriteLine("");

            tpDefineHandle.WriteLine(string.Format("#define CAN_DL  {0}", getTPCommonInfo.ProtocolDLC));
            tpDefineHandle.WriteLine(string.Format("#define TP_TASK_TIME  {0}", getTPCommonInfo.TPTaskCallTime));
            tpDefineHandle.WriteLine(string.Format("#define TP_CONNECTION_COUNT  {0}", tpConnectionCount));
            tpDefineHandle.WriteLine(string.Format("#define TP_MAX_RECEIVE_BUFFER_SIZE  {0}", getTPCommonInfo.ReceiveMaxSize));
            tpDefineHandle.WriteLine("");

            //TP Handle 출력
            foreach(var tpConnectionObject in relocateTPConnectionInfo)
            {
                tpDefineHandle.WriteLine(string.Format("#define TP_HANDLE_{0}_TO_{1}  {2}", tpConnectionObject.SendMsgName, tpConnectionObject.ReceiveMsgName, tpConnectionObject.TPHandle));
            }
            tpDefineHandle.WriteLine("");
            tpDefineHandle.WriteLine("#endif /* ISOTP_DEFINE_H_ */");
            tpDefineHandle.Close();
        }
        public void TpParameterParser(TPCommon getTPCommonInfo)
        {
            tpParameterHandle.WriteLine(@"/*");
            tpParameterHandle.WriteLine(" * isotp_parameter.c");
            tpParameterHandle.WriteLine(" *");
            tpParameterHandle.WriteLine(string.Format(" * Generate Date: {0}  {1}", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:dd")));
            tpParameterHandle.WriteLine("*/");
            tpParameterHandle.WriteLine("");
            tpParameterHandle.WriteLine("");
            tpParameterHandle.WriteLine("#include \"iso15765_TP.h\"");
            tpParameterHandle.WriteLine("#include \"isotp_define.h\"");

            tpParameterHandle.WriteLine("/* Receive Message Property Information*/");

            tpParameterHandle.WriteLine("tpInfo_t tpConnectionInfo[TP_CONNECTION_COUNT] =");
            tpParameterHandle.WriteLine("{");

            foreach (var tpConnectionObject in relocateTPConnectionInfo)
            {
                tpParameterHandle.WriteLine("    {");
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendID */", tpConnectionObject.SendMsgID));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveID */", tpConnectionObject.ReceiveMsgID));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* baseAddress */", tpConnectionObject.BaseAddress));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* addressMode */", tpConnectionObject.AddressMode));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* addressType */", tpConnectionObject.AddressType));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sourceAddress */", tpConnectionObject.SourceAddress));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* targetAddress */", tpConnectionObject.TargetAddress));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveTargetAddress */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* allAddressExtension */", tpConnectionObject.AcceptExtension == "TRUE" ? 1 : 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* addressExtension */", tpConnectionObject.AddressExtension));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveAddressExtension */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* timeoutAs */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadTimeoutAs */", tpConnectionObject.N_As));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* timeoutAr */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadTimeoutAr */", tpConnectionObject.N_Ar));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* timeoutBs */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadTimeoutBs */", tpConnectionObject.N_Bs));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* timeoutCr */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadTimeoutCr */", tpConnectionObject.N_Cr));

                tpParameterHandle.WriteLine(String.Format("     {0}, /* wftMaxTime */", tpConnectionObject.WftMaxTime));
                tpParameterHandle.WriteLine(string.Format("     {{{0},{1}}}, /* sendSideInfo */", "INVALID_FRAME", "INIT_STATUS"));
                tpParameterHandle.WriteLine(String.Format("     {{{0},{1}}}, /* receiveSideInfo */", "INVALID_FRAME","INIT_STATUS"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendProtocolRequest */", "TRANSMIT_PROTOCOL_DATA"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendProtocolBuffer */", "{0,}"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveProtocolRequest */", "TRANSMIT_PROTOCOL_DATA"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveProtocolBuffer */", "{0,}"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* *sendFrameBuffer */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* *receiveFrameBuffer */",0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendFrameSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* totalSendFrameSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveFrameSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* totalReceiveFrameSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendFrameOffset */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveFrameOffset */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveBufferMaxSize */", "TP_MAX_RECEIVE_BUFFER_SIZE"));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* pad */", tpConnectionObject.Pad));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* priority */", tpConnectionObject.Priority));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* dataPage */", tpConnectionObject.DataPage));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reserved */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* protocolDataUnit */", tpConnectionObject.ProtocolUnit));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* waitingMode */", tpConnectionObject.WaitMode == "TRUE" ? 1 : 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* wftmax */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadWftmax */", tpConnectionObject.WftMax));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* useFlowControl */", tpConnectionObject.UseFlowControl == "TRUE" ? 1 : 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* useBlockSize */", tpConnectionObject.UseBlockSize == "TRUE" ? 1 : 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* useSTmin */", tpConnectionObject.UseSTmin == "TRUE" ? 1 : 0));

                tpParameterHandle.WriteLine(String.Format("     {0}, /* ownBlockSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadOwnBlockSize */", tpConnectionObject.BlockSize));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadOwnSTmin */", tpConnectionObject.STmin)); 
               
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveBlockSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadReceiveBlockSize */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveSTmin */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* reloadReceiveSTmin */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* firstSN */", tpConnectionObject.FirstSN));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendSN */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* receiveSN */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* useRxMask */", tpConnectionObject.UsingRxMask == "TRUE" ? 1 : 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* waitTime */", 0));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* rxMask */", tpConnectionObject.RxMask));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* messageCount */", tpConnectionObject.MessageCount));
                tpParameterHandle.WriteLine(String.Format("     {0}, /* sendConfirm */", 0));
                tpParameterHandle.WriteLine("    },");
            }

            tpParameterHandle.WriteLine("};");
            tpParameterHandle.WriteLine("");

            tpParameterHandle.Close();
        }
    }
}
