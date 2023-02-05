using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{

    internal static class Serializer
    {
      
        public static void SerializeHandler(this ListRand list, FileStream fileStream)
        {
            var nodes = list.GetAllNode();
            var dictNodes = nodes.ConvertListBlockToDict();
            //STREAM
            //fileStream.Write(StreamData(nodes, dictNodes));

            //JSON
            fileStream.Write(JSONData(nodes, dictNodes));
        }


    
        private static string GetTestString(this ListNode node, Dictionary<ListNode, int> dictNodes)
        {
            return node == null ? "null" : dictNodes[node].ToString();
        }

        private static byte[] StreamData(List<ListNode> nodes, Dictionary<ListNode, int> dictNodes)
        {
            var strRes = new StringBuilder();
            foreach (var node in nodes)
            {
                strRes.Append(node.Data);
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(node.Prev.GetTestString(dictNodes));
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(node.Next.GetTestString(dictNodes));
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(node.Rand.GetTestString(dictNodes));
                strRes.Append(ConstInformation.SeparatorStream);
            }
            return UnicodeEncoding.UTF8.GetBytes(strRes.ToString());
        }

        private static byte[] JSONData(List<ListNode> nodes, Dictionary<ListNode, int> dictNodes)
        {
            JsonArray jsonArray = new JsonArray();
        
            foreach (var node in nodes)
            {
                var jsonNode = new JsonNode(node.Data,
                    node.Prev == null ? null : dictNodes[node.Prev],
                    node.Next == null ? null : dictNodes[node.Next],
                    node.Rand == null ? null : dictNodes[node.Rand]);
             
                jsonArray.AddElement(jsonNode);
            }

            Console.WriteLine(jsonArray.ToString());
            return UnicodeEncoding.UTF8.GetBytes(jsonArray.ToString());
        }

        private static Dictionary<ListNode, int> ConvertListBlockToDict(this List<ListNode> list)
        {
            var res = new Dictionary<ListNode, int>();
            for(int i = 0;  i < list.Count; i++)
                res.Add(list[i], i);
            return res;
        }

        private static List<ListNode> GetAllNode(this ListRand list)
        {
            var res = new List<ListNode>();
            var node = list.Head;
            while (true)
            {
                if (node != null)
                    res.Add(node);
                else break;

                node = node.Next;
            }
            return res;
        }
    }
}
