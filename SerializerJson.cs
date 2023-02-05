using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal class SerializerJson : NewSerializer
    {
        public SerializerJson(ListRand list) : base(list)
        {
        }

        public override byte[] CreateData(List<ListNode> nodes)
        {
            JsonArray jsonArray = new JsonArray();
            Console.WriteLine("***********************************************");
            foreach (var node in nodes)
            {
                var jsonNode = new JsonNode(node.Data,
                    node.Prev == null ? null : _dictNodes[node.Prev],
                    node.Next == null ? null : _dictNodes[node.Next],
                    node.Rand == null ? null : _dictNodes[node.Rand]);

                jsonArray.AddElement(jsonNode);

                Console.WriteLine($"Data: {node.Data} - Prev: {GetTestString(node.Prev)} - Next: {GetTestString(node.Next)} - Rand: {GetTestString(node.Rand)}");
            }
            Console.WriteLine("***********************************************");
            Console.WriteLine(jsonArray.ToString());
            return UnicodeEncoding.UTF8.GetBytes(jsonArray.ToString());
        }

   
    }
}
