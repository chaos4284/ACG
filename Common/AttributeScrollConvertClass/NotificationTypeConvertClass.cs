﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bsw_generation.Common.AttributeScrollConvertClass
{
    class NotificationTypeConvertClass : BoolConvertClass
    {
        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[] { "NOTIFCATION_INDICATE", "NOTIFCATION_CALLBACK", "ALL" });
        }
    }
}
