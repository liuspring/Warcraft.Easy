using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EnumDisplayNameAttribute : Attribute
    {
        private readonly string _displayName;
        public EnumDisplayNameAttribute(string displayName)
        {
            _displayName = displayName;
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
        }
    }
}
