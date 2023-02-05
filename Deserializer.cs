using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal static class Deserializer
    {
        private static readonly string NULL = "null";
        public static void DeserializeHandler(this ListRand list,FileStream fileStream)
        {
            TestJson(fileStream);

            //var text = GetStringFile(fileStream);
            //string[] words = text.Split(ConstInformation.SeparatorStream);
            //var countNodes = words.Length / ConstInformation.CountSeparatorStream;
            //Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();
            //dict.CreateNodes(countNodes);
            //dict.SetConnectionNode(words);
            //list.InitListRand(dict);
            //dict.Check();
        }



        private static List<byte> SkiipChar(byte[] buffer)
        {
            var res = new List<byte>();
            bool isData = false;
            
            for (int i = 0; i < buffer.Length; i++)
            {

                if (buffer[i] == '"')
                    isData = !isData;


                if ((buffer[i] == ' ' || buffer[i] == '\n' || buffer[i] == '\r' || buffer[i] == '\t') && isData)
                    res.Add(buffer[i]);

                if (!(buffer[i] == ' ' || buffer[i] == '\n' || buffer[i] == '\r' || buffer[i] == '\t'))
                    res.Add(buffer[i]);
        
            }
            return res;
        }
        private static void TestJson(FileStream fileStream)
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

                if(isNode)
                    tmp.Append(c);
            }

            Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();
            dict.CreateNodes(strNode.Count);
            for(int i = 0; i < strNode.Count; i++)
            {
                if (strNode[i] == String.Empty)
                    continue;
                SplitInformation(strNode[i], dict, i);
            }

            Console.WriteLine("-----------------------JSONDes--------------------------------");
            dict.Check();
            
        }


     
        private static void SplitInformation(string str, Dictionary<int, ListNode> dict, int index)
        {
            var words = str.Split(',');
         
            var node = dict[index];
            
            for (int i = 0; i < words.Length; i++)
            {
                
                var found = words[i].IndexOf(":");
                var pair = words[i].Substring(found + 1);
        

                switch (i % ConstInformation.CountSeparatorStream)
                {
                    case 0:
                        node.Data = pair;
                        break;
                    case 1:
                        node.Prev = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;
                    case 2:
                        node.Next = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;
                    case 3:
                        node.Rand = pair == NULL ? null : dict[Convert.ToInt32(pair)];
                        break;

                }
            }
           

        }
        private static void InitListRand(this ListRand list, Dictionary<int, ListNode> dict)
        {
            list.Count = dict.Count;
            list.Head = dict[0];
            list.Tail = dict[dict.Count - 1];
        }

        private static string GetStringFile(FileStream fileStream)
        {
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            return Encoding.Default.GetString(buffer);
        }
        private static void Check(this Dictionary<int, ListNode> dict)
        {
            var newDict = new Dictionary<ListNode, int>();
            foreach(var key in dict.Keys)
            {
               var node = dict[key];
               newDict.Add(node, key);
            }
            foreach (var node in newDict.Keys)
            {

                var tmp = $"Data: {node.Data} " +
                                  $"- PrevIndex: { (node.Prev == null ? "null" : newDict[node.Prev])}" +
                                  $" - NextIndex: {(node.Next == null ? "null" : newDict[node.Next])}" +
                                  $" - RandIndex: {(node.Rand == null ? "null" : newDict[node.Rand])}";
                Console.WriteLine(tmp);
            }
        }

        private static void CreateNodes(this Dictionary<int, ListNode> dict, int countNodes)
        {
            for (int i = 0; i < countNodes; i++)
                dict.Add(i, new ListNode());
        }

        private static void SetConnectionNode(this Dictionary<int, ListNode> dict, string[] words)
        {
            var count = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (i % ConstInformation.CountSeparatorStream == 0)
                    count++;

                if (words[i] == String.Empty)
                    continue;

                var node = dict[count - 1];

                switch (i % ConstInformation.CountSeparatorStream)
                {
                    case 0:
                        node.Data = words[i];
                        break;
                    case 1:
                        node.Prev = words[i] == NULL ? null : dict[Convert.ToInt32(words[i])];
                        break;
                    case 2:
                        node.Next = words[i] == NULL ? null : dict[Convert.ToInt32(words[i])];
                        break;
                    case 3:
                        node.Rand = words[i] == NULL ? null : dict[Convert.ToInt32(words[i])];
                        break;

                }
            }

        }
    }


    
}
