using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.Common.AttributeScrollConvertClass
{
    internal class BoolConvertClass : StringConverter
    {
        public override bool
            GetStandardValuesSupported(
            ITypeDescriptorContext context)
        {
            //True - means show a Combobox
            //and False for show a Modal 
            return true;
        }

        public override bool
            GetStandardValuesExclusive(
            ITypeDescriptorContext context)
        {
            //False - a option to edit values 
            //and True - set values to state readonly
            return true;
        }

        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[] {"TRUE","FALSE"});
        }
    }
}
