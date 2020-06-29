using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLGUI.EventsLib
{
    public class FilterChangeEventArgs : EventArgs
    {
        private object selectedItem;

        public string Param { get; internal set; }
        public string Enum { get; internal set; }

        public FilterChangeEventArgs(string paramValue)
        {
            Param = paramValue;
        }

        public FilterChangeEventArgs(string paramValue, string enumValue)
        {
            Param = paramValue;
            Enum = enumValue;
        }
    }
}
