using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.Common.AttributeScrollConvertClass
{
    class FilterAlgorithmConvertClass : BoolConvertClass
    {
        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[] { "F_NEVER","F_ALWAYS", "F_NEWISDIFFRENT","F_MASKEDNEWDIFFERSX" });
        }
    }
}
