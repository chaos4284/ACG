using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.LayerTab.ComTab
{
    class ComSignalAttributesInformation : bsw_generation.AttributeControlClass.AttributeControlClass
    {
        private UInt32 handle = 0;
        private UInt32 handle_notification_indication = 0;
        private UInt32 handle_notification_indication_offset = 0;
        private string signal_name = "SignalA";
        private UInt32 signal_bit_length = 8;
        private UInt32 message_id = 0x100;
        private UInt32 message_handle = 0x0;
        private string send_property = "NON_SEND_PROPERTY";
        private string filter_alorigthm = "F_NEVER";
        private UInt32 start_offset_bit = 0;
        private string byte_order = "BYTE_ORDER_BIG_ENDIAN";
        private UInt32 signal_start_value = 0;
        private UInt32 timeout_value = 0;
        private string notification_option = "NOTIFICATION_DEACTIVE";
        private string notification_type = "NOTIFCATION_INDICATE";
        private string notification_callback = "";



        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the handle option.")]
        public UInt32 Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the handle option.")]
        public UInt32 HandleNotificationIndication
        {
            get { return handle_notification_indication; }
            set { handle_notification_indication = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the handle option.")]
        public UInt32 HandleNotificationIndicationOffset
        {
            get { return handle_notification_indication_offset; }
            set { handle_notification_indication_offset = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the signal_name option.")]
        public string SignalName
        {
            get { return signal_name; }
            set { signal_name = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the signal_bit_length option.")]
        public UInt32 BitLength
        {
            get { return signal_bit_length; }
            set { signal_bit_length = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_id option.")]
        public UInt32 ParentMsgId
        {
            get { return message_id; }
            set { message_id = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_id option.")]
        public UInt32 ParentMsgHandle
        {
            get { return message_handle; }
            set { message_handle = value; }
        }
/*
        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_id option.")]
        public UInt32 ParentMsgId
        {
            get { return message_id; }
            set { message_id = value; }
        }
*/
        [Browsable(true)]
        [Category("\tGeneral Information")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.SendPropertyConvertClass))]
        [Description("TTP : Triggered Transfer Property with Reptition\nPTP : Pending Transfer Property\nTTPWR : Triggered Transfer Property without Reptition")]
        public string SendProperty
        {   
            get { return send_property; }
            set { send_property = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.FilterAlgorithmConvertClass))]
        [Description("Select the filter_alorigthm option.\nF_ALWAYS : Pass all message\nF_NEWISDIFFRENT : Pass if the value is diffrent from previous value.")]
        public string FilterAlogirithm
        {
            get { return filter_alorigthm; }
            set { filter_alorigthm = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the start offset bit option.")]
        public UInt32 StartOffsetBit
        {
            get { return start_offset_bit; }
            set { start_offset_bit = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.ByteOrderConvertClass))]
        [Description("Enable the byte_order option.")]
        public string ByteOrder
        {
            get { return byte_order; }
            set { byte_order = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the signal_data option.")]
        public UInt32 StartValue
        {
            get { return signal_start_value; }
            set { signal_start_value = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the timeout_value option.")]
        public UInt32 TimeoutValue
        {
            get { return timeout_value; }
            set { timeout_value = value; }
        }

        [Browsable(true)]
        [Category("Notification information")]
        [Description("Enable the notification option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.NotificationOptionConvertClass))]
        [RefreshProperties(RefreshProperties.All)]
        public string NotificationOption
        {
            get 
            {
                PropertyDescriptor attribute_notification_callback_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationCallbackName"];
                PropertyDescriptor attribute_notification_type_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationType"];
                if (notification_option == "NOTIFICATION_DEACTIVE")
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, true);
                    control_readonly_attribute(attribute_notification_type_descriptor, true);

                }
                else
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, false);
                    control_readonly_attribute(attribute_notification_type_descriptor, false);
                }
                return notification_option; 
            }
            set 
            {
                notification_option = value;
                PropertyDescriptor attribute_notification_callback_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationCallbackName"];
                PropertyDescriptor attribute_notification_type_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationType"];
                if (notification_option == "NOTIFICATION_DEACTIVE")
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, true);
                    control_readonly_attribute(attribute_notification_type_descriptor, true);

                }
                else
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, false);
                    control_readonly_attribute(attribute_notification_type_descriptor, false);

                } 
            
            }
        }

        [Browsable(true)]
        [Category("Notification information")]
        [Description("Enable the notification_type option.")]
        [ReadOnlyAttribute(false)]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.NotificationTypeConvertClass))]
        
        public string NotificationType
        {
            get { return notification_type; }
            set { notification_type = value; }
        }

        [Browsable(true)]
        [Category("Notification information")]
        [Description("Enable the notification_callback option.")]
        [ReadOnlyAttribute(false)]
        
        public string NotificationCallbackName
        {
            get { return notification_callback; }
            set { notification_callback = value; }
        }


    }
}
