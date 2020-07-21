using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using bsw_generation.DatabaseParser.CAN;
namespace bsw_generation.LayerTab.TPTab
{
    class TPCommon:bsw_generation.AttributeControlClass.AttributeControlClass
    {
        //INDIVIDUAL
/*
        private string addressMode = "NORMAL_ADDRESS";
        private string addressType = "PHYSICAL";
        private UInt32 baseAddress = 0x400;
        private UInt16 rxMask = 1;
        private string acceptExtension = "FALSE";
        private string useFlowControl = "FALSE";
        private string useBlockSize = "FALSE";
        private string useSTmin = "FALSE";
        private byte blockSize = 0x4;
        private byte stmin = 0x14;
        private byte firstSN = 0x1;
        private string usingRxMask = "FALSE";
        

        private string optionControl = "ALL";
        
        private UInt16 asTimeout = 1000;
        private UInt16 bsTimeout = 1000;
        private UInt16 arTimeout = 1000;
        private UInt16 crTimeout = 1000;
        private UInt16 wftMax = 0;
        private string waitMode = "FALSE";
        private UInt16 pad = 0xFF;
*/
        private UInt16 taskTime = 5;
        private UInt16 protocolDLC = 8;
        private UInt32 receiveMaxSize = 4095;

        [Browsable(true)]
        [DisplayName("calling task time")]
        [Category("\tCommon Information")]
        [ReadOnlyAttribute(false)]
        [Description("Set the task call timing.[ms]")]
        public UInt16 TPTaskCallTime
        {
            get { return taskTime; }
            set { taskTime = value; }
        }

        [Browsable(true)]
        [DisplayName("communication protocol DLC")]
        [Category("\tCommon Information")]
        [ReadOnlyAttribute(false)]
        [Description("Set the communication protocol dlc")]
        public UInt16 ProtocolDLC
        {
            get { return protocolDLC; }
            set { protocolDLC = value; }
        }

        [Browsable(true)]
        [DisplayName("allowed received size")]
        [Category("\tCommon Information")]
        [ReadOnlyAttribute(false)]
        [Description("Set the allowed receive size")]
        public UInt32 ReceiveMaxSize
        {
            get { return receiveMaxSize; }
            set { receiveMaxSize = value; }
        }
#if FALSE
        [Browsable(true)]
        [DisplayName("address mode")]
        [Category("Common ISO TP Property ")]
        [Description("Select address mode.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.AddressModeConvertClass))]
        public string AddressMode
        {
            get { return addressMode; }
            set { addressMode = value; }
        }

        [Browsable(true)]
        [DisplayName("address type")]
        [Category("Common ISO TP Property ")]
        [Description("Select address mode.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.AddressTypeConvertClass))]
        public string AddressType
        {
            get { return addressType ; }
            set { addressType = value; }
        }

        [Browsable(true)]
        [DisplayName("base address")]
        [Category("Common ISO TP Property ")]
        [Description("Base address in extended address")]
        public UInt32 BaseAddress
        {
            get { return baseAddress; }
            set { baseAddress = value; }
        }

        [Browsable(true)]
        [DisplayName("All Address Extension")]
        [Category("Common ISO TP Property ")]
        [Description("Set to check of address extension.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string AcceptExtension
        {
            get { return acceptExtension; }
            set { acceptExtension = value; }
        }

        [Browsable(true)]
        [DisplayName("Use Flow Control")]
        [Category("Common ISO TP Property ")]
        [Description("Set to use of Flow Control")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string UseFlowControl
        {
            get { return useFlowControl; }
            set { useFlowControl = value; }
        }

        [Browsable(true)]
        [DisplayName("Use Block Size")]
        [Category("Common ISO TP Property ")]
        [Description("Set to BlockSize of BlockSize")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string UseBlockSize
        {
            get { return useBlockSize; }
            set { useBlockSize = value; }
        }

        [Browsable(true)]
        [DisplayName("Use STmin")]
        [Category("Common ISO TP Property ")]
        [Description("Set to use of STmin")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string UseSTmin
        {
            get { return useSTmin; }
            set { useSTmin = value; }
        }

        [Browsable(true)]
        [DisplayName("BS in Flow Control")]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of blocksize in Flow Control")]
        public byte BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }

        }

        [Browsable(true)]
        [DisplayName("STmin in Flow Control")]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of STmin in Flow Control")]
        public byte STmin
        {
            get { return stmin; }
            set { stmin = value; }

        }

        [Browsable(true)]
        [DisplayName("First Sequence Number")]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of first sequence number")]
        public byte FirstSN
        {
            get { return firstSN; }
            set { firstSN = value; }

        }

        [Browsable(true)]
        [DisplayName("rxmask value")]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of rxmask")]
        public UInt16 RxMask
        {
            get { return rxMask; }
            set { rxMask = value; }

        }

        [Browsable(true)]
        [DisplayName("Use rxmask")]
        [Category("Common ISO TP Property ")]
        [Description("Set to use of STmin")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string UsingRxMask
        {
            get { return usingRxMask; }
            set { usingRxMask = value; }
        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("N_As.[ms]")]
        public UInt16 N_As
        {
            get { return asTimeout; }
            set { asTimeout = value; }

        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("N_Bs.[ms]")]
        public UInt16 N_Bs
        {
            get { return bsTimeout; }
            set { bsTimeout = value; }

        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("N_Ar.[ms]")]
        public UInt16 N_Ar
        {
            get { return arTimeout; }
            set { arTimeout = value; }

        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("N_Cr.[ms]")]
        public UInt16 N_Cr
        {
            get { return crTimeout; }
            set { crTimeout = value; }

        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("Wait Count.")]
        public UInt16 WftMax
        {
            get { return wftMax; }
            set { wftMax = value; }

        }

        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("Select WT mode in FlowControl.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string WaitMode
        {
            get { return waitMode; }
            set { waitMode = value; }

        }

        
        [Browsable(true)]
        [Category("Common ISO TP Property ")]
        [Description("this value filled into unused byte SF and last CF")]
        public UInt16 Pad
        {
            get { return pad; }
            set { pad = value; }

        }


#endif
    }

}
