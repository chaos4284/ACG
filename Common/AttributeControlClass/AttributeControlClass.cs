using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace bsw_generation.AttributeControlClass
{
    class AttributeControlClass
    {
        public Boolean control_readonly_attribute(PropertyDescriptor descriptor, Boolean flag)
        {
            Boolean ret;
            ReadOnlyAttribute attribute_descriptor = descriptor.Attributes[typeof(ReadOnlyAttribute)] as ReadOnlyAttribute;
            FieldInfo isReadOnly = attribute_descriptor.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
            if (isReadOnly == null)
            {
                ret = false;
            }
            else
            {
                isReadOnly.SetValue(attribute_descriptor, flag);
                ret = true;
            }

            return ret;
        }

        public Boolean control_browsable_attribute(PropertyDescriptor descriptor, Boolean flag)
        {
            Boolean ret;
            BrowsableAttribute attribute_descriptor = descriptor.Attributes[typeof(BrowsableAttribute)] as BrowsableAttribute;
            FieldInfo isBrowsable = attribute_descriptor.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
            if (isBrowsable == null)
            {
                ret = false;
            }
            else
            {
                isBrowsable.SetValue(attribute_descriptor, flag);
                ret = true;
            }

            return ret;
        }
    }
}
