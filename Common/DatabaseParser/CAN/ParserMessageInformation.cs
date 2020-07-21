using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsw_generation.DatabaseParser.CAN
{
    class ParserMessageInformation
    {
        private string message_node = "";
        private string message_name = "";
        private UInt32 message_length = 0;       
        private UInt32 message_cycle_time = 0;//
        private UInt32 message_repetiton_cycle_time = 0;
        private UInt32 message_repetiton_number = 0;
        private UInt32 message_delay_time = 0;
        private string message_send_mode = "";       
        private string message_direction = "";
        private UInt32 message_id = 0;
        private UInt32 message_start_offset_delay = 0;
        private string message_com_support = "";
        private byte messageTpIndex = 0;

        public string MessageNode
        {
            get { return message_node; }
            set { message_node = value; }
        }

        public string MessageName
        {
            get { return message_name; }
            set { message_name = value;}
        }

        public UInt32 MessageLength
        {
            get { return message_length; }
            set { message_length = value; }
        }
      
        public UInt32 MessageCycleTime
        {
            get { return message_cycle_time; }
            set { message_cycle_time = value; }
        }

        public UInt32 MessageRepetitonCycleTime
        {
            get { return message_repetiton_cycle_time; }
            set { message_repetiton_cycle_time = value; }
        }

        public UInt32 MessageRepetitonNumber
        {
            get { return message_repetiton_number; }
            set { message_repetiton_number = value; }
        }

        public UInt32 MessageDelayTime
        {
            get { return message_delay_time; }
            set { message_delay_time = value; }
        }

        public string MessageSendMode
        {
            get { return message_send_mode; }
            set { message_send_mode = value; }
        }

        public string MessageDirection
        {
            get { return message_direction; }
            set { message_direction = value; }
        }

        public UInt32 MessageID
        {
            get { return message_id; }
            set { message_id = value; }
        }

        public UInt32 MessageStartOffsetDelay
        {
            get { return message_start_offset_delay; }
            set { message_start_offset_delay = value; }
        }
        public string MessageComSupport
        {
            get { return message_com_support; }
            set { message_com_support = value; }
        }

        public byte MessageTpIndex
        {
            get { return messageTpIndex; }
            set { messageTpIndex = value; }
        }
        
        
    }
}
