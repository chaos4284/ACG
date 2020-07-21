using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsw_generation.DatabaseParser.CAN
{
    class ParserSignalInformation
    {
        private string signal_name = "";
        private UInt32 singal_bit_length = 0;
        private UInt32 signal_parent_id = 0;
        private string signal_byte_order = "";
        private string signal_send_propery = "";
        private string signal_filter_algorithm = "";
        private UInt32 signal_start_offset_bit = 0;
        private UInt32 signal_start_value  = 0;
        private UInt32 signal_timeout_value = 0;

        public string SignalName
        {
            get { return signal_name; }
            set { signal_name = value; }
        }

        public UInt32 SignalBitLength
        {
            get { return singal_bit_length; }
            set { singal_bit_length = value; }
        }

        public UInt32 SignalParentID
        {
            get { return signal_parent_id; }
            set { signal_parent_id  = value; }
        }

        public string SignalByteOrder
        {
            get { return signal_byte_order; }
            set { signal_byte_order = value; }
        }
      
        public string SignalSendProperty
        {
            get { return signal_send_propery; }
            set { signal_send_propery = value; }
        }

        public string SignalFilterAlgorithm
        {
            get { return signal_filter_algorithm; }
            set { signal_filter_algorithm = value; }
        }

        public UInt32 SignalStartOffsetBit
        {
            get { return signal_start_offset_bit; }
            set { signal_start_offset_bit = value; }
        }

        public UInt32 SignalStartValue
        {
            get { return signal_start_value; }
            set { signal_start_value = value; }
        }

        public UInt32 SignalTimeoutValue
        {
            get { return signal_timeout_value; }
            set { signal_timeout_value = value; }
        }



    }
}
