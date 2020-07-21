using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace bsw_generation.Common.AttributeScrollConvertClass
{
    class SendModeCovertClass : BoolConvertClass
    {
        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[] { "NON_SEND_MODE","DTM","PTM","MTM" });
        }
    }
}
