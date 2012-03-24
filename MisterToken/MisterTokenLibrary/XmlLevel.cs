using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class XmlLevel {
        public string name;
        public string help;
        public string tokens;
        public string colors;
        public string pattern;
        public bool wrap;
    }

    public class XmlWorld {
        public string name;
        public XmlLevel[] level;
    }
}
