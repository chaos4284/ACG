using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace bsw_generation.LayerTab.ComTab
{
    class COMGeneral
    {
        private string start_extension_state = "TRUE";
        private UInt16 taskTime = 5;
        private byte app_mode_number = 0;
        private string app_mode_name = "";
        //private string error_hook_state = "TRUE";

        [Browsable(true)]
        [Category("Category name")]
        [Description("Enable the StartCOMExtension option.")]
        [TypeConverter(typeof(bsw_generation.Common.AttributeScrollConvertClass.BoolConvertClass))]
        public string StartCOMExtensionOption
        {
            get { return start_extension_state; }
            set { start_extension_state = value; }
        }

        [CategoryAttribute("Category name")]
        [Description("Set the task call timing.[ms]")]
        public UInt16 ComTaskTime
        {
            get { return taskTime; }
            set { taskTime = value; }

        }

/*
        [Browsable(true)]
        [Category("Category name")]
        [Description("Enable the ErrorHook option.")]
        [TypeConverter(typeof(bsw_generation.ConvertClass.BoolConvertClass))]
        public string ErrorHookOption
        {
            get { return error_hook_state; }
            set { error_hook_state = value; }
        }
*/

        [Browsable(true)]
        [Category("Category name")]
        [Description("Set app mode number.")]
        public byte AppModeNumber
        {
            get { return app_mode_number; }
            set { app_mode_number = value; }
        }

        [Browsable(true)]
        [Category("Category name")]
        [Description("Set app mode name.")]
        public string AppModeName
        {
            get { return app_mode_name; }
            set { app_mode_name = value; }
        }
    }
}
