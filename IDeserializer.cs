using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
   interface IDeserializer
    {
        public void DeserializeHandler(FileStream fileStream);
        public void InitListRand();
        public string GetStringFile(FileStream fileStream);
        public void Check();
        public void CreateNodes( int countNodes);
    }


  
}
