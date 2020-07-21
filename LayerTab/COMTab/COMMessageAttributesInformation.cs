using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace bsw_generation.LayerTab.ComTab
{
    class ComMessageAttributesInformation : bsw_generation.AttributeControlClass.AttributeControlClass
    {
        private UInt32 handle = 0;
        private UInt32 handle_notification_indication = 0;
        private UInt32 handle_notification_indication_offset = 0;
        private UInt32 message_length = 8;
        private UInt32 cycle_time = 100;
        private UInt32 repetition_cycle_time = 40;
        private UInt32 repetition_number = 0;
        private UInt32 message_delay_time = 20;
        private string send_mode = "NON_SEND_MODE";
        private string message_direction = "SEND";
        private string message_name = "sMessageA";
        private UInt32 message_id = 0x100;
        private string deadline_monitoring_option = "DEACTIVE_DEADLINE_MONITORING";
        private UInt32 deadline_monitoring_timeout = 0;
        private UInt32 start_offset_delay = 0;
        private string start_offset_state = "START_OFFSET_DEACTIVE";
        private string direct_send_event = "NON_OCCUR_DIRECT_SEND_EVENT";
        private string message_send_request = "NON_MESSGE_SEND_REQUEST";
        private string message_buffer_name = "MessageA_buffer";
        private string notification_state = "NOTIFICATION_DEACTIVE";
        private string notification_type = "NOTIFCATION_INDICATE";
        private string notification_callback = "";
        private string message_com_suppot = "FALSE";


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
        [Description("Enable the message_name option.")]
        public string MsgName
        {
            get { return message_name; }
            set { message_name = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_length option.")]
        public UInt32 Length
        {
            get { return message_length; }
            set { message_length = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the cycle_time option.")]
        public UInt32 CycleTime
        {
            get { return cycle_time; }
            set { cycle_time = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the repetition_cycle_time option.")]
        public UInt32 RepetitionCycleTime
        {
            get { return repetition_cycle_time; }
            set { repetition_cycle_time = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the repetition_number option.")]
        public UInt32 RepetitionNumber
        {
            get { return repetition_number; }
            set { repetition_number = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_delay_time option.")]
        public UInt32 MessageDelayTime
        {
            get { return message_delay_time; }
            set { message_delay_time = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.SendModeCovertClass))]
        [Description("DTM : Direct Transmission Mode\nPTM : Periodic Transmission Mode\nMTM : Mixed Transmission Mode")]                                          
        public string SendMode
        {
            get { return send_mode; }
            set { send_mode = value; }
        }

        [Browsable(true)]
        [Category("\tGeneral Information")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.DirectionCovertClass))]
        [Description("Enable the message_direction option.")]
        public string Direction
        {
            get { return message_direction; }
            set { message_direction = value; }
        }


        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_id option.")]
        public UInt32 MsgID
        {
            get { return message_id; }
            set { message_id = value; }
        }



        [Browsable(true)]
        [Category("\tGeneral Information")]
        [Description("Enable the start_offset_delay option.")]
        public UInt32 StartOffsetDelay
        {
            get { return start_offset_delay; }
            set { start_offset_delay = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the start_offset_state option.")]
        public string StartOffsetState
        {
            get { return start_offset_state; }
            set { start_offset_state = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the direct_send_event option.")]
        public string DirectSendEvent
        {
            get { return direct_send_event; }
            set { direct_send_event = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_send_request option.")]
        public string MessageSendRequest
        {
            get { return message_send_request; }
            set { message_send_request = value; }
        }

        [Browsable(false)]
        [Category("\tGeneral Information")]
        [Description("Enable the message_buffer_name option.")]
        public string BufferName
        {
            get { return message_buffer_name; }
            set { message_buffer_name = value; }
        }

        [Browsable(true)]
        [Category("Deadline monitroing information")]
        [ReadOnlyAttribute(false)]
        [Description("Enable the deadline_monitoring_option option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.DeadlineOptionConvertClass))]
        [RefreshProperties(RefreshProperties.All)]
        public string DeadLineMonitoringOption
        {
            get 
            {
                PropertyDescriptor attribute_deadline_monitoring_timeout_descriptor = TypeDescriptor.GetProperties(this.GetType())["DeadLineMonitoringTimeout"];
                if (deadline_monitoring_option == "DEACTIVE_DEADLINE_MONITORING")
                {
                    control_readonly_attribute(attribute_deadline_monitoring_timeout_descriptor, true);
                }
                else
                {
                    control_readonly_attribute(attribute_deadline_monitoring_timeout_descriptor, false);
                }            
                return deadline_monitoring_option;
            }
            set 
            { 
                deadline_monitoring_option = value;
                PropertyDescriptor attribute_deadline_monitoring_timeout_descriptor = TypeDescriptor.GetProperties(this.GetType())["DeadLineMonitoringTimeout"];
                if (deadline_monitoring_option == "DEACTIVE_DEADLINE_MONITORING")
                {
                    control_readonly_attribute(attribute_deadline_monitoring_timeout_descriptor, true);
                }
                else
                {
                    control_readonly_attribute(attribute_deadline_monitoring_timeout_descriptor, false);
                }      
            }
        }

        [Browsable(true)]
        [Category("Deadline monitroing information")]
        [ReadOnlyAttribute(false)]
        [Description("Enable the deadline_monitoring_timeout option.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public UInt32 DeadLineMonitoringTimeout
        {
            get { return deadline_monitoring_timeout; }
            set { deadline_monitoring_timeout = value; }
        }

        [Browsable(true)]
        [Category("Notification information")]
        [Description("Enable the notification_state option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.NotificationOptionConvertClass))]
        [RefreshProperties(RefreshProperties.All)]
        public string NotificationOption
        {
            get 
            {
                PropertyDescriptor attribute_notification_callback_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationCallbackName"];
                PropertyDescriptor attribute_notification_type_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationType"];
                if (notification_state == "NOTIFICATION_DEACTIVE")
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, true);
                    control_readonly_attribute(attribute_notification_type_descriptor, true);

                }
                else
                {
                    control_readonly_attribute(attribute_notification_callback_descriptor, false);
                    control_readonly_attribute(attribute_notification_type_descriptor, false);
                }
                return notification_state;
                
            } 
                
            set
            {
                notification_state = value;
                PropertyDescriptor attribute_notification_callback_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationCallbackName"];
                PropertyDescriptor attribute_notification_type_descriptor = TypeDescriptor.GetProperties(this.GetType())["NotificationType"];
                if (notification_state == "FALSE")
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
        [ReadOnlyAttribute(false)]
        [Description("Enable the notification_type option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.NotificationTypeConvertClass))]
        public string NotificationType
        {
            get {return notification_type; }
            set { notification_type = value; }
        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Notification information")]
        [Description("Enable the notification_callback option.")]
        public string NotificationCallbackName
        {
            get{ return notification_callback; }
            set { notification_callback = value; }

        }

        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        [Category("Com Support")]
        [Description("Enable the notification_callback option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string MessageComSupport
        {
            get { return message_com_suppot; }
            set { message_com_suppot = value; }

        }
        
    }
}
