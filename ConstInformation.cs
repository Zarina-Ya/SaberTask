using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
   
    public class ConstInformation
    {
        public static readonly char SeparatorStream = '\0';
        public static readonly char[] NodeSeparatorJSON = {'{', '}'};
        public static readonly char[] ArrayNodeSeparatorJSON = { '[', ']' };
        public static readonly char SeparatorJSON = ',';
        public static readonly int CountSeparatorStream = 4;
      
    }
}
