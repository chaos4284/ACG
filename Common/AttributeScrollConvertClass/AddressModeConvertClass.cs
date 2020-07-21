using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.Common.AttributeScrollConvertClass
{
    class AddressModeConvertClass : BoolConvertClass
    {
        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[] { "NORMAL_ADDRESS", "EXTENDED_ADDRESS", "NORMAL_FIXED_ADDRESS", "MIXED_11BIT_ADDRESS", "MIXED_29BIT_ADDRESS" });
        }
    }
}
