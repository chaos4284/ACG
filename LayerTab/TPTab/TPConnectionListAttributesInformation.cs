using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace bsw_generation.LayerTab.TPTab
{
    class TPMessageAttributesInformation : bsw_generation.AttributeControlClass.AttributeControlClass
    {
        //Common ISO TP Property
        private string addressMode = "NORMAL_ADDRESS";
        private string addressType = "PHYSICAL";
        private UInt32 baseAddress = 0x700;
        private UInt16 rxMask = 1;
        private string acceptExtension = "FALSE";
        private string useFlowControl = "TRUE";
        private string useBlockSize = "TRUE";
        private string useSTmin = "TRUE";
        private byte blockSize = 0x4;
        private byte stmin = 0x14;
        private byte firstSN = 0x1;
        private string usingRxMask = "FALSE";
        //private string optionControl = "ALL";
        private UInt16 asTimeout = 1000;
        private UInt16 bsTimeout = 1000;
        private UInt16 arTimeout = 1000;
        private UInt16 crTimeout = 1000;
        private UInt16 wftMax = 0;
        private UInt16 wftMaxTime = 1000;
        private string waitMode = "FALSE";
        private UInt16 pad = 0xFF;

        //Message Information
        private string sendMsgName = "";
        private string receiveMsgName = "";
        private UInt32 sendMsgID = 0;
        private UInt32 receiveMsgID = 0;
        private byte targetAddress = 0xAA;
        private byte sourceAddress = 0xBB;
        private byte addressExtension = 0xFF;
        private byte priority = 0x1;
        private byte dataPage = 0x1;
        private byte protocolUnit = 0x1;
        private UInt16 messageCount = 4000;

        //Internal Data
        private UInt32 tpHandle = 0;
        private byte tpTxIndex = 0;
        private string tpConnectionName = "";


        //////////////////////////////////////Common ISO TP Property//////////////////////////////////////

        [Browsable(true)]
        [DisplayName("address mode")]
        [ReadOnlyAttribute(false)]
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
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Select address mode.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.AddressTypeConvertClass))]
        public string AddressType
        {
            get { return addressType; }
            set { addressType = value; }
        }

        [Browsable(true)]
        [DisplayName("base address")]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Base address in extended address")]
        public UInt32 BaseAddress
        {
            get { return baseAddress; }
            set { baseAddress = value; }
        }

        [Browsable(true)]
        [DisplayName("All Address Extension")]
        [ReadOnlyAttribute(false)]
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
        [ReadOnlyAttribute(false)]
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
        [ReadOnlyAttribute(false)]
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
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of blocksize in Flow Control")]
        public byte BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }

        }

        [Browsable(true)]
        [DisplayName("STmin in Flow Control")]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of STmin in Flow Control")]
        public byte STmin
        {
            get { return stmin; }
            set { stmin = value; }

        }

        [Browsable(true)]
        [DisplayName("First Sequence Number")]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of first sequence number")]
        public byte FirstSN
        {
            get { return firstSN; }
            set { firstSN = value; }

        }

        [Browsable(true)]
        [DisplayName("rxmask value")]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Set the value of rxmask")]
        public UInt16 RxMask
        {
            get { return rxMask; }
            set { rxMask = value; }

        }

        [Browsable(true)]
        [DisplayName("Use rxmask")]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Set to use of STmin")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string UsingRxMask
        {
            get { return usingRxMask; }
            set { usingRxMask = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("N_As.[ms]")]
        public UInt16 N_As
        {
            get { return asTimeout; }
            set { asTimeout = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("N_Bs.[ms]")]
        public UInt16 N_Bs
        {
            get { return bsTimeout; }
            set { bsTimeout = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("N_Ar.[ms]")]
        public UInt16 N_Ar
        {
            get { return arTimeout; }
            set { arTimeout = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("N_Cr.[ms]")]
        public UInt16 N_Cr
        {
            get { return crTimeout; }
            set { crTimeout = value; }

        }


        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Select WT mode in FlowControl.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string WaitMode
        {
            get { return waitMode; }
            set { waitMode = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Wait Count.")]
        public UInt16 WftMax
        {
            get { return wftMax; }
            set { wftMax = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Wait Status Cycle .")]
        public UInt16 WftMaxTime
        {
            get { return wftMaxTime; }
            set { wftMaxTime = value; }

        }



        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("this value filled into unused byte SF and last CF")]
        public UInt16 Pad
        {
            get { return pad; }
            set { pad = value; }
        }
        

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Common ISO TP Property ")]
        [Description("Specifies range of allowed reception IDs")]
        public UInt16 MessageCount
        {
            get { return messageCount; }
            set { messageCount = value; }

        }
        //////////////////////////////////////Common ISO TP Property//////////////////////////////////////

        //////////////////////////////////////Message Information/////////////////////////////////////////
        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set to the transmission ID")]
        public UInt32 SendMsgID
        {
            get { return sendMsgID; }
            set { sendMsgID = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the reception ID.")]
        public UInt32 ReceiveMsgID
        {
            get { return receiveMsgID; }
            set { receiveMsgID = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(true)]
        [Category("Message Information")]
        [Description("Show the name of transmission message")]
        public string SendMsgName
        {
            get { return sendMsgName; }
            set { sendMsgName = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(true)]
        [Category("Message Information")]
        [Description("Show the name of reception message")]
        public string ReceiveMsgName
        {
            get { return receiveMsgName; }
            set { receiveMsgName = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set to the Target Address")]
        public byte TargetAddress
        {
            get { return targetAddress; }
            set { targetAddress = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the Source Address.")]
        public byte SourceAddress
        {
            get { return sourceAddress; }
            set { sourceAddress = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the Address Extension.")]
        public byte AddressExtension
        {
            get { return addressExtension; }
            set { addressExtension = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the J1939 priority")]
        public byte Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the J1939 DP")]
        public byte DataPage
        {
            get { return dataPage; }
            set { dataPage = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Message Information")]
        [Description("Set the J1939 PDU")]
        public byte ProtocolUnit
        {
            get { return protocolUnit; }
            set { protocolUnit = value; }
        }
        //////////////////////////////////////Message Information/////////////////////////////////////////


        //////////////////////////////////////Internal Information/////////////////////////////////////////
        [Browsable(false)]
        [ReadOnlyAttribute(true)]
        public byte TPTxIndex
        {
            get { return tpTxIndex; }
            set { tpTxIndex = value; }
        }

        [Browsable(false)]
        [ReadOnlyAttribute(true)]
        public UInt32 TPHandle
        {
            get { return tpHandle; }
            set { tpHandle = value; }
        }

        [Browsable(false)]
        [ReadOnlyAttribute(true)]
        public string TpConnectionName
        {
            get { return tpConnectionName; }
            set { tpConnectionName = value; }
        }
#if FALSE
        [Browsable(true)]
        [ReadOnlyAttribute(true)]
        public string OptionControl
        {
            get
            {

                PropertyDescriptor attributeAddressModeDescriptor = TypeDescriptor.GetProperties(this.GetType())["AddressMode"];
                PropertyDescriptor attributeAddressTypeDescriptor = TypeDescriptor.GetProperties(this.GetType())["AddressType"];
                PropertyDescriptor attributeBaseAddressDescriptor = TypeDescriptor.GetProperties(this.GetType())["BaseAddress"];
                PropertyDescriptor attributeRxMaskTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["RxMask"];
                PropertyDescriptor attributeAcceptExtensionDescriptor = TypeDescriptor.GetProperties(this.GetType())["AcceptExtension"];
                PropertyDescriptor attributeuseFlowControlDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseFlowControl"];
                PropertyDescriptor attributeuseBlockSizeDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseBlockSize"];
                PropertyDescriptor attributeuseSTminDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseSTmin"];
                PropertyDescriptor attributeusingRxMaskDescriptor = TypeDescriptor.GetProperties(this.GetType())["UsingRxMask"];
                PropertyDescriptor attributeBlockSizeDescriptor = TypeDescriptor.GetProperties(this.GetType())["BlockSize"];
                PropertyDescriptor attributeSTminDescriptor = TypeDescriptor.GetProperties(this.GetType())["STmin"];
                PropertyDescriptor attributeFirstSNTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["FirstSN"];
                PropertyDescriptor attributeAsTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_As"];
                PropertyDescriptor attributeBsTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Bs"];
                PropertyDescriptor attributeCrTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Cr"];
                PropertyDescriptor attributeArTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Ar"];
                PropertyDescriptor attributeWftMaxDescriptor = TypeDescriptor.GetProperties(this.GetType())["WftMax"];
                PropertyDescriptor attributeWaitModeDescriptor = TypeDescriptor.GetProperties(this.GetType())["WaitMode"];
                PropertyDescriptor attributePadDescriptor = TypeDescriptor.GetProperties(this.GetType())["Pad"];

                if (optionControl == "ALL")
                {

                    control_readonly_attribute(attributeAddressModeDescriptor, true);
                    control_readonly_attribute(attributeAddressTypeDescriptor, true);
                    control_readonly_attribute(attributeBaseAddressDescriptor, true);
                    control_readonly_attribute(attributeRxMaskTimeoutDescriptor, true);
                    control_readonly_attribute(attributeAcceptExtensionDescriptor, true);
                    control_readonly_attribute(attributeuseFlowControlDescriptor, true);
                    control_readonly_attribute(attributeuseBlockSizeDescriptor, true);
                    control_readonly_attribute(attributeuseSTminDescriptor, true);
                    control_readonly_attribute(attributeusingRxMaskDescriptor, true);
                    control_readonly_attribute(attributeBlockSizeDescriptor, true);
                    control_readonly_attribute(attributeSTminDescriptor, true);
                    control_readonly_attribute(attributeFirstSNTimeoutDescriptor, true);
                    control_readonly_attribute(attributeAsTimeoutDescriptor, true);
                    control_readonly_attribute(attributeBsTimeoutDescriptor, true);
                    control_readonly_attribute(attributeCrTimeoutDescriptor, true);
                    control_readonly_attribute(attributeArTimeoutDescriptor, true);
                    control_readonly_attribute(attributeWftMaxDescriptor, true);
                    control_readonly_attribute(attributeWaitModeDescriptor, true);
                    control_readonly_attribute(attributePadDescriptor, true);


                }
                else
                {

                    control_readonly_attribute(attributeAddressModeDescriptor, false);
                    control_readonly_attribute(attributeAddressTypeDescriptor, false);
                    control_readonly_attribute(attributeBaseAddressDescriptor, false);
                    control_readonly_attribute(attributeRxMaskTimeoutDescriptor, false);
                    control_readonly_attribute(attributeAcceptExtensionDescriptor, false);
                    control_readonly_attribute(attributeuseFlowControlDescriptor, false);
                    control_readonly_attribute(attributeuseBlockSizeDescriptor, false);
                    control_readonly_attribute(attributeuseSTminDescriptor, false);
                    control_readonly_attribute(attributeusingRxMaskDescriptor, false);
                    control_readonly_attribute(attributeBlockSizeDescriptor, false);
                    control_readonly_attribute(attributeSTminDescriptor, false);
                    control_readonly_attribute(attributeFirstSNTimeoutDescriptor, false);
                    control_readonly_attribute(attributeAsTimeoutDescriptor, false);
                    control_readonly_attribute(attributeBsTimeoutDescriptor, false);
                    control_readonly_attribute(attributeCrTimeoutDescriptor, false);
                    control_readonly_attribute(attributeArTimeoutDescriptor, false);
                    control_readonly_attribute(attributeWftMaxDescriptor, false);
                    control_readonly_attribute(attributeWaitModeDescriptor, false);
                    control_readonly_attribute(attributePadDescriptor, false);

                }

                return optionControl;

            }

            set
            {
                optionControl = value;
                PropertyDescriptor attributeAddressModeDescriptor = TypeDescriptor.GetProperties(this.GetType())["AddressMode"];
                PropertyDescriptor attributeAddressTypeDescriptor = TypeDescriptor.GetProperties(this.GetType())["AddressType"];
                PropertyDescriptor attributeBaseAddressDescriptor = TypeDescriptor.GetProperties(this.GetType())["BaseAddress"];
                PropertyDescriptor attributeRxMaskTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["RxMask"];
                PropertyDescriptor attributeAcceptExtensionDescriptor = TypeDescriptor.GetProperties(this.GetType())["AcceptExtension"];
                PropertyDescriptor attributeuseFlowControlDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseFlowControl"];
                PropertyDescriptor attributeuseBlockSizeDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseBlockSize"];
                PropertyDescriptor attributeuseSTminDescriptor = TypeDescriptor.GetProperties(this.GetType())["UseSTmin"];
                PropertyDescriptor attributeusingRxMaskDescriptor = TypeDescriptor.GetProperties(this.GetType())["UsingRxMask"];
                PropertyDescriptor attributeBlockSizeDescriptor = TypeDescriptor.GetProperties(this.GetType())["BlockSize"];
                PropertyDescriptor attributeSTminDescriptor = TypeDescriptor.GetProperties(this.GetType())["STmin"];
                PropertyDescriptor attributeFirstSNTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["FirstSN"];
                PropertyDescriptor attributeAsTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_As"];
                PropertyDescriptor attributeBsTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Bs"];
                PropertyDescriptor attributeCrTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Cr"];
                PropertyDescriptor attributeArTimeoutDescriptor = TypeDescriptor.GetProperties(this.GetType())["N_Ar"];
                PropertyDescriptor attributeWftMaxDescriptor = TypeDescriptor.GetProperties(this.GetType())["WftMax"];
                PropertyDescriptor attributeWaitModeDescriptor = TypeDescriptor.GetProperties(this.GetType())["WaitMode"];
                PropertyDescriptor attributePadDescriptor = TypeDescriptor.GetProperties(this.GetType())["Pad"];

                if (optionControl == "ALL")
                {
                    control_readonly_attribute(attributeAddressModeDescriptor, true);
                    control_readonly_attribute(attributeAddressTypeDescriptor, true);
                    control_readonly_attribute(attributeBaseAddressDescriptor, true);
                    control_readonly_attribute(attributeRxMaskTimeoutDescriptor, true);
                    control_readonly_attribute(attributeAcceptExtensionDescriptor, true);
                    control_readonly_attribute(attributeuseFlowControlDescriptor, true);
                    control_readonly_attribute(attributeuseBlockSizeDescriptor, true);
                    control_readonly_attribute(attributeuseSTminDescriptor, true);
                    control_readonly_attribute(attributeusingRxMaskDescriptor, true);
                    control_readonly_attribute(attributeBlockSizeDescriptor, true);
                    control_readonly_attribute(attributeSTminDescriptor, true);
                    control_readonly_attribute(attributeFirstSNTimeoutDescriptor, true);
                    control_readonly_attribute(attributeAsTimeoutDescriptor, true);
                    control_readonly_attribute(attributeBsTimeoutDescriptor, true);
                    control_readonly_attribute(attributeCrTimeoutDescriptor, true);
                    control_readonly_attribute(attributeArTimeoutDescriptor, true);
                    control_readonly_attribute(attributeWftMaxDescriptor, true);
                    control_readonly_attribute(attributeWaitModeDescriptor, true);
                    control_readonly_attribute(attributePadDescriptor, true);
                }
                else
                {
                    control_readonly_attribute(attributeAddressModeDescriptor, false);
                    control_readonly_attribute(attributeAddressTypeDescriptor, false);
                    control_readonly_attribute(attributeBaseAddressDescriptor, false);
                    control_readonly_attribute(attributeRxMaskTimeoutDescriptor, false);
                    control_readonly_attribute(attributeAcceptExtensionDescriptor, false);
                    control_readonly_attribute(attributeuseFlowControlDescriptor, false);
                    control_readonly_attribute(attributeuseBlockSizeDescriptor, false);
                    control_readonly_attribute(attributeuseSTminDescriptor, false);
                    control_readonly_attribute(attributeusingRxMaskDescriptor, false);
                    control_readonly_attribute(attributeBlockSizeDescriptor, false);
                    control_readonly_attribute(attributeSTminDescriptor, false);
                    control_readonly_attribute(attributeFirstSNTimeoutDescriptor, false);
                    control_readonly_attribute(attributeAsTimeoutDescriptor, false);
                    control_readonly_attribute(attributeBsTimeoutDescriptor, false);
                    control_readonly_attribute(attributeCrTimeoutDescriptor, false);
                    control_readonly_attribute(attributeArTimeoutDescriptor, false);
                    control_readonly_attribute(attributeWftMaxDescriptor, false);
                    control_readonly_attribute(attributeWaitModeDescriptor, false);
                    control_readonly_attribute(attributePadDescriptor, false);

                }

            }
        }
#endif
        //////////////////////////////////////Internal Information/////////////////////////////////////////
    }


}
