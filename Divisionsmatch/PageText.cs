using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divisionsmatch
{
    class PageText
    {
        public bool NySide { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        
        public PageText()
        {
            NySide = true;
            Header = "";
            Body = "";
        }

        public string AllText
        {
            get
            {
                return Header + Body;
            }
        }
    }
}
