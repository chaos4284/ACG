using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace bsw_generation.DatabaseParser.CAN
{
    class DatabaseParserClass
    {
        private const int NON_MODE = 0x0;
        private const int MODE_CYCLE = 0x1;
        private const int MODE_EVENT = 0x2;
        /*Message Com Supporty Type*/
        private const string NOT_COM_SUPPORT_TYPE = "0";
        private const string COM_SUPPORT_TYPE = "1";
        /*Message Send Type */
        private const string CYCLE_MSG_SEND_TYPE = "0";
        private const string NON_MSG_SEND_TYPE = "8";

        /*Signal Send Type*/
        private const string CYCLE_SIG_SEND_TYPE = "0";
        private const string WRITE_SIG_SEND_TYPE = "1";
        private const string WRITE_REPTITION_SIG_SEND_TYPE = "2";
        private const string CHANGE_SIG_SEND_TYPE = "3";
        private const string CHANGE_REPTITION_SIG_SEND_TYPE = "4";
        private const string IF_ACTIVE_SIG_SEND_TYPE = "5";
        private const string IF_ACTIVE_REPTITION_SEND_TYPE = "6";
        private const string NON_SIG_SEND_TYPE = "7";


        string[] database_command = { "SG_", "BA_", "BU_", "BO_", "BA_DEF_DEF_", "BA_REL_" };

        public LinkedList<string> database_node = new LinkedList<string>();
        public LinkedList<string> message_send_type = new LinkedList<string>();
        public LinkedList<string> signal_send_type = new LinkedList<string>();
        public LinkedList<string> il_support_type = new LinkedList<string>();

        public LinkedList<string> signal_node = new LinkedList<string>();
        public LinkedList<ParserMessageInformation> parser_msg_info= new LinkedList<ParserMessageInformation>();
        public LinkedList<ParserSignalInformation> parser_sig_info = new LinkedList<ParserSignalInformation>();
        public LinkedList<ParserMessageInformation> result_parser_msg_info = new LinkedList<ParserMessageInformation>();
        public LinkedList<ParserSignalInformation> result_parser_sig_info = new LinkedList<ParserSignalInformation>();
        
        ParserMessageInformation select_msg_object;
        ParserSignalInformation select_sig_object;

        private string decide_msg_com_support_type(ParserMessageInformation decide_msg_object)
        {
            string ret = "";
            //System.Console.WriteLine(string.Format("decide_msg_object = {0}", decide_msg_object.MessageComSupport));
          //          private const string  = "0";
        //private const string COM_SUPPORT_TYPE = "1";
            if (decide_msg_object.MessageComSupport == NOT_COM_SUPPORT_TYPE)
            {
                ret = "FALSE";
            }
            else if (decide_msg_object.MessageComSupport == COM_SUPPORT_TYPE)
            {
                ret = "TRUE";
            }
            return ret;
        }

        private string decide_msg_send_type(ParserMessageInformation decide_msg_object)
        {
            string ret = "";
            byte result_msg_send_type = 0;

            //메시지에 해당하는 시그널들에 모든 전송타입을 체크하여, 최종 시그널 전송타입을 설정한다.
            foreach (var sig_object in parser_sig_info)
            {
                if (decide_msg_object.MessageID == sig_object.SignalParentID)
                {   //DTM, PTM, MTM
                    // 시그널 전송모드가 Cyclic일경우
                    if (sig_object.SignalSendProperty == CYCLE_SIG_SEND_TYPE)
                    {
                        result_msg_send_type |= MODE_CYCLE;
                    }
                    // 시그널 전송모드가 없을 경우
                    else if (sig_object.SignalSendProperty == NON_SIG_SEND_TYPE)
                    {
                        result_msg_send_type |= NON_MODE;
                    }
                    // 시그널 전송모드가 전송모드 Event 또는 Repetition 이벤트 일경우
                    else
                    {
                        result_msg_send_type |= MODE_EVENT;
                    }
                }
            }
           
            //시그널이 아무런 전송타입이 없을경우
            if (result_msg_send_type == NON_MODE)
            {
                // 메시지가 Cycle 메시지일경우
                if (decide_msg_object.MessageSendMode == CYCLE_MSG_SEND_TYPE)
                {
                    ret = "PTM";
                }
                // 메시지가 Cycle 메시지가 아닐경우
                else
                {
                    ret = "NON_SEND_MODE";
                }
            }
            //시그널 전송타입이 Cycle일경우
            else if (result_msg_send_type == MODE_CYCLE)
            {
                ret = "PTM";
            }
            //시그널 전송타입이 Event메시지 일경우
            else if (result_msg_send_type == MODE_EVENT)
            {
                if (decide_msg_object.MessageSendMode == CYCLE_MSG_SEND_TYPE)//메시지 전송타입이 Cycle전송일경우
                {
                    ret = "MTM";//혼합전송모드
                }
                else if (decide_msg_object.MessageSendMode == NON_MSG_SEND_TYPE)
                {
                    ret = "DTM";// 이벤트 모드
                }
            }
            else if (result_msg_send_type == (MODE_CYCLE | MODE_EVENT))
            {
                ret = "MTM";
            }

            return ret;
        }

        private Boolean check_command(string[] command )
        {
            Boolean ret = false;

            foreach (string command_object in command)
            {
                foreach (string database_command_object in database_command)
                {
                    if (database_command_object == command_object)
                    {
                        ret = true;
                        break;
                    }
                    else { }

                    if (ret == true)
                    {
                        break;
                    }
                    else { }
                   
                }
            }

            return ret;
        }
        //ID에 해당하는 메시지에 각각 DB Attribute 속성을 설정한다.
        private void set_message_attribute_by_id(UInt32 id, string message_attribute_name,string value)
        {
            foreach (ParserMessageInformation parser_msg_object in parser_msg_info)
            {
                if (parser_msg_object.MessageID == id)
                {
                    switch (message_attribute_name)
                    {
                        case "GenMsgCycleTime":
                            parser_msg_object.MessageCycleTime = Convert.ToUInt32(value);
                            break;

                        case "GenMsgCycleTimeFast":
                            parser_msg_object.MessageRepetitonCycleTime = Convert.ToUInt32(value);
                            break;

                        case "GenMsgNrOfRepetition":
                            parser_msg_object.MessageRepetitonNumber = Convert.ToUInt32(value);
                            break;

                        case "GenMsgDelayTime":
                            parser_msg_object.MessageDelayTime = Convert.ToUInt32(value);
                            break;

                        case "GenMsgStartDelayTime":
                            parser_msg_object.MessageStartOffsetDelay = Convert.ToUInt32(value);
                            break;

                        case "GenMsgSendType":
                            parser_msg_object.MessageSendMode = value;
                            break;

                        case "GenMsgILSupport":
                            parser_msg_object.MessageComSupport = value;
                            break;

                        case "TpTxIndex":
                            parser_msg_object.MessageTpIndex = Convert.ToByte(value);
                            break;
                        default:
                            break;

                    }
                }
            }
        }

        //ID에 해당하는 메시지에 속한 시그널들의 속성값들을 설정한다.
        private void set_signal_attribute_by_id(UInt32 id, string sig_attribute_name, string signal_name,string value)
        {
            
            foreach (ParserSignalInformation parser_sig_object in parser_sig_info)
            {
                if (parser_sig_object.SignalParentID == id)
                {
                    if (parser_sig_object.SignalName == signal_name)
                    {
                        switch (sig_attribute_name)
                        {
                            case "GenSigSendType":
                                parser_sig_object.SignalSendProperty = value;
                                break;

                            case "GenSigStartValue":
                                parser_sig_object.SignalStartValue = Convert.ToUInt32(value);
                                break;

                            case "GenSigTimeoutValue":
                                parser_sig_object.SignalTimeoutValue = Convert.ToUInt32(value);
                                break;
                            default:
                                break;
                        }
                    }

                }

            }
        }


        /*Command에 대한 database 파싱*/
        private void process_database_command(string[] process_command, int process_command_length, string select_node_name)
        {   
            string command = process_command[0];
            //시그널 타입뒤에 모든 노드 저장 필요
            switch (command)
            {
                case "BA_":
                    if (process_command_length > 1)
                    {
                        switch (process_command[1])
                        {
                            case "GenMsgCycleTime":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenMsgCycleTimeFast":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenMsgNrOfRepetition":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenMsgDelayTime":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenMsgStartDelayTime":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenMsgSendType":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;

                            case "GenSigSendType":
                                set_signal_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4], process_command[5]);
                                break;

                            case "GenSigStartValue":
                                set_signal_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4], process_command[5]);
                                break;

                            case "GenSigTimeoutValue":
                                set_signal_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4], process_command[5]);
                                break;

                            case "GenMsgILSupport":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;


                            case "TpTxIndex":
                                set_message_attribute_by_id(Convert.ToUInt32(process_command[3]), process_command[1], process_command[4]);
                                break;
                            default:
                                break;

                        }
                    }
                    break;

                case "BU_":
                    int command_index = 0;

                    for (command_index = 1; command_index < process_command_length; command_index++)
                    {
                        database_node.AddLast(process_command[command_index]);
                    }

                    break;

                case "BO_":
                    if (select_msg_object != null)
                    {
                        if (select_msg_object.MessageDirection == "")
                        {
                            parser_msg_info.Remove(select_msg_object);
                        }
                        else
                        {
                            select_msg_object = null;
                        }
                    }
                    else
                    {
                        select_msg_object = null;
                    }

                    select_msg_object = new ParserMessageInformation();
                    select_msg_object.MessageID = Convert.ToUInt32(Convert.ToUInt32(process_command[1]));                    
                    select_msg_object.MessageName = process_command[2];
                    select_msg_object.MessageLength = Convert.ToUInt32(process_command[3]);
                    select_msg_object.MessageNode = process_command[4];
                    if (select_msg_object.MessageNode == select_node_name)
                    {
                        select_msg_object.MessageDirection = "SEND";
                    }
                    else
                    {
                        ;
                    }

                    parser_msg_info.AddLast(select_msg_object);
                 
                    break;

                case "BA_DEF_":

                    if (process_command[2] == "GenMsgSendType")
                    {
                        for (int index = 4; index < process_command_length; index++)
                        {
                            message_send_type.AddLast(process_command[index]);
                        }
                    }
                    else if (process_command[2] == "GenSigSendType")
                    {
                        for (int index = 4; index < process_command_length; index++)
                        {
                            signal_send_type.AddLast(process_command[index]);
                        }
                    }
                    else if (process_command[2] == "GenMsgILSupport")
                    {
                        for (int index = 4; index < process_command_length; index++)
                        {
                            il_support_type.AddLast(process_command[index]);
                        }
                    }
                    
                    break;

                case "BA_DEF_DEF_":
                      int attribute_index = 0;
                      if (process_command_length > 1)
                      { 
                          switch (process_command[1])
                          {
                              case "GenMsgCycleTime":
                                  foreach(var parser_msg_object in parser_msg_info)
                                  {
                                      parser_msg_object.MessageCycleTime = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  
                                  break;

                              case "GenMsgCycleTimeFast":
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      parser_msg_object.MessageRepetitonCycleTime = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  break;

                              case "GenMsgNrOfRepetition":
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      parser_msg_object.MessageRepetitonNumber = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  break;

                              case "GenMsgDelayTime":
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      parser_msg_object.MessageDelayTime = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  break;

                              case "GenMsgStartDelayTime":
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      parser_msg_object.MessageStartOffsetDelay = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  break;

                              case "GenMsgSendType":
                                  attribute_index = 0;
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      foreach (string message_send_type_object in message_send_type)
                                      {
                                          if (process_command[process_command_length - 1] == message_send_type_object)
                                          {
                                              set_message_attribute_by_id(parser_msg_object.MessageID, process_command[1], string.Format("{0}", attribute_index));
                                              attribute_index = 0;
                                              break;
                                          }
                                          else
                                          {
                                              attribute_index++;
                                          }
                                      }

                                  }
                                  break;

                              case "GenSigSendType":
                                  attribute_index = 0;
                                  foreach (var parser_sig_object in parser_sig_info)
                                  {
                                     // parser_sig_object.SignalSendProperty = process_command[process_command_length - 1];
                                      foreach (string signal_send_type_object in signal_send_type)
                                      {
                                          if (process_command[process_command_length - 1] == signal_send_type_object)
                                          {
                                              /*전송 타입이 ENUM이므로 전송 타입에 해당하는  attribute_index를 저장한다 */
                                              //set_signal_attribute_by_id(parser_sig_object.SignalParentID, process_command[1], string.Format("{0}", attribute_index));
                                              parser_sig_object.SignalSendProperty = string.Format("{0}", attribute_index);
                                              attribute_index = 0;
                                              break;
                                          }
                                          else
                                          {
                                              attribute_index++;
                                          }
                                      }
                                  }
                                  break;

                              case "GenSigStartValue":
                                  foreach (var parser_sig_object in parser_sig_info)
                                  {
                                      parser_sig_object.SignalStartValue = Convert.ToUInt32(process_command[process_command_length - 1]);
                                  }
                                  break;

                              case "GenSigTimeoutValue":
                                  foreach (var parser_sig_object in parser_sig_info)
                                  {
                                      parser_sig_object.SignalTimeoutValue = Convert.ToUInt32(process_command[process_command_length - 1]);

                                  }
                                  break;

                              case "GenMsgILSupport":
                                  attribute_index = 0;
                                  foreach (var parser_msg_object in parser_msg_info)
                                  {
                                      foreach (string il_support_type_object in il_support_type)
                                      {
                                          if (process_command[process_command_length - 1] == il_support_type_object)
                                          {
                                              set_message_attribute_by_id(parser_msg_object.MessageID, process_command[1], string.Format("{0}", attribute_index));
                                              attribute_index = 0;
                                              break;
                                          }
                                          else
                                          {
                                              attribute_index++;
                                          }
                                      }

                                  }
                                  break;

                              default:
                                  break;

                          }
                      }
                    break;

                case "BA_REL_":

                    break;

                case "BA_SG_REL_":

                    break;

                case "SG_":
                    select_sig_object = new ParserSignalInformation();
                    select_sig_object.SignalName = process_command[1];

                    select_sig_object.SignalStartOffsetBit = Convert.ToUInt32(process_command[2]);
                    select_sig_object.SignalBitLength = Convert.ToUInt32(process_command[3]);
                    if(process_command[4] == "0+")
                    {
                        select_sig_object.SignalByteOrder = "BYTE_ORDER_BIG_ENDIAN";
                    }
                    else
                    {
                        select_sig_object.SignalByteOrder = "BYTE_ORDER_LITTLE_ENDIAN";
                    }
                    /*수신노드 처리 */
                    if(select_msg_object.MessageNode == select_node_name) // 전송
                    {
                       select_sig_object.SignalParentID = select_msg_object.MessageID;
                       parser_sig_info.AddLast(select_sig_object);
                    }
                    else // 수신
                    {
                        for(int index = 9; index < process_command_length;index++)
                        {
                            if (process_command[index] == select_node_name)
                            {
                                select_msg_object.MessageDirection = "RECEIVE";
                                select_sig_object.SignalParentID = select_msg_object.MessageID;
                                parser_sig_info.AddLast(select_sig_object);                       
                                break;
                            }
                            else
                            {
                                ;
                            }
                        }        
                    }
         
                    
                    break;
                case "CM_":

                    break;
            }

        }
       //DBC 파싱을 진행한다.
        public void process_database_parser(string database_path, string select_node_name)
        {
            //Node 
              /*
            CycleTime "GenMsgCycleTime"
            repetition cycle time "GenMsgCycleTimeFast"
            repetition number "GenMsgNrOfRepetition"
            MsgDelayTime "GenMsgDelayTime"
            StartOffsetDelay "GenMsgStartDelayTime"
            SendMode "GenMsgSendType"
            signal
            start value GenSigStartValue
            timeoutvalue TimeoutValue
            */

            /* 시그널 속성 */
            //Signal Name,Start offset bit,Bit Lengt, Byte Order(@1:little endian, 0: big endian)
            //Parent ID
            //Send Property, Filter Algorithm
            //전송 속성받아서 나눠서 가져옴
            //timeout value :GenSigTimeoutTime
            
            string line_string = "";
            char[] filter= {' '};
            Boolean exist_command = false;
            if (File.Exists(database_path))
            {
                FileStream database_file = new FileStream (database_path,FileMode.Open,FileAccess.Read);
                StreamReader databack_read = new StreamReader(database_file);
                while (databack_read.EndOfStream == false)
                {
                    line_string = databack_read.ReadLine();
                    line_string = line_string.Replace(":", " ");
                    line_string = line_string.Replace("|", " ");
                    line_string = line_string.Replace("@", " ");
                    line_string = line_string.Replace(";", " ");
                    line_string = line_string.Replace(",", " ");
                    line_string = line_string.Replace("\"", "");
                    line_string = line_string.Trim();
                    
                    string[] result = line_string.Split(filter, StringSplitOptions.RemoveEmptyEntries);
                    
                    exist_command = check_command(result);
                    if (exist_command == true)
                    {
                        process_database_command(result, result.Length, select_node_name);
                    }

                }

                /*메시지 전송모드 설정 */
                foreach (var msg_object in parser_msg_info)
                {
                    msg_object.MessageSendMode = decide_msg_send_type(msg_object);
                    msg_object.MessageComSupport = decide_msg_com_support_type(msg_object);
                }

                /*시그널 전송속성 / 필터 알고리즘 설정*/
                foreach (var sig_object in parser_sig_info)
                {
                    switch (sig_object.SignalSendProperty)
                    {
                        case CYCLE_SIG_SEND_TYPE:
                        case NON_SIG_SEND_TYPE:

                            sig_object.SignalSendProperty = "PTP";
                            sig_object.SignalFilterAlgorithm = "F_ALWAYS";
                            break;

                        case WRITE_SIG_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTPWR";
                            sig_object.SignalFilterAlgorithm = "F_ALWAYS";
                            break;

                        case WRITE_REPTITION_SIG_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTP";
                            sig_object.SignalFilterAlgorithm = "F_ALWAYS";
                            break;

                        case CHANGE_SIG_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTPWR";
                            sig_object.SignalFilterAlgorithm = "F_NEWISDIFFRENT";
 
                            break;

                        case CHANGE_REPTITION_SIG_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTP";
                            sig_object.SignalFilterAlgorithm = "F_NEWISDIFFRENT";
                            break;

                        case IF_ACTIVE_SIG_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTPWR";
                            sig_object.SignalFilterAlgorithm = "F_MASKEDNEWDIFFERSX";
                            break;

                        case IF_ACTIVE_REPTITION_SEND_TYPE:
                            sig_object.SignalSendProperty = "TTP";
                            sig_object.SignalFilterAlgorithm = "F_MASKEDNEWDIFFERSX";
                            break;
                    }
                }

                var sort_msg_object = parser_msg_info.OrderBy(msg_list => msg_list.MessageName);
                var sort_sig_object = parser_sig_info.OrderBy(msg_list => msg_list.SignalName);
                
                foreach (var msg_object in sort_msg_object)
                {
                    result_parser_msg_info.AddLast(msg_object);
                }

                foreach (var sig_object in sort_sig_object)
                {

                    result_parser_sig_info.AddLast(sig_object);
                }
  
            }


        }
    }

}

