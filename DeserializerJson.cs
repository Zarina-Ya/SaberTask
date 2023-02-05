using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal class DeserializerJson : NewDeserializer
    {
        public DeserializerJson(ListRand list) : base(list)
        {
        }

        public override void DeserializeHandler(FileStream fileStream)
        {
            var buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);

            List<string> strNode = new List<string>();
            StringBuilder tmp = new StringBuilder();
            bool isNode = false;
            foreach (char c in buffer)
            {
                if (c == '{')
                {
                    isNode = true;
                    continue;
                }

                else if (c == '}')
                {
                    isNode = false;
                    strNode.Add(tmp.ToString());
                    tmp.Clear();
                }

                if (isNode)
                    tmp.Append(c);
            }

            
            CreateNodes(strNode.Count);
            for (int i = 0; i < strNode.Count; i++)
            {
                if (strNode[i] == String.Empty)
                    continue;
                SplitInformation(strNode[i], _dict, i);
            }

            Console.WriteLine("-----------------------JSONDes--------------------------------");
            Check();

        }
        private void SplitInformation(string str, Dictionary<int, ListNode> dict, int index)
        {
            var words = str.Split(',');

            var node = dict[index];

            for (int i = 0; i < words.Length; i++)
            {

                var found = words[i].IndexOf(":");

                string pair = "";

                switch (i % ConstInformation.CountSeparatorStream)
                {
                    case 0:
                        node.Data = words[i].Substring(found + 2, words[i].Length - found - 3);
                        break;
                    case 1:
                        pair = words[i].Substring(found + 1);
                        node.Prev = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;
                    case 2:
                        pair = words[i].Substring(found + 1);
                        node.Next = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;
                    case 3:

                        pair = words[i].Substring(found + 1);
                        node.Rand = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;

                }
            }


        }

   
    }
}
