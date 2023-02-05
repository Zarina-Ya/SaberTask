using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal class SerializerStream : NewSerializer
    {
        public SerializerStream(ListRand list) : base(list)
        {
        }

        public override byte[] CreateData(List<ListNode> nodes)
        {
            Console.WriteLine("***********************************************");
            var strRes = new StringBuilder();
            foreach (var node in nodes)
            {
                strRes.Append(node.Data);
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(GetTestString(node.Prev));
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(GetTestString(node.Next));
                strRes.Append(ConstInformation.SeparatorStream);
                strRes.Append(GetTestString(node.Rand));
                strRes.Append(ConstInformation.SeparatorStream);

               

                Console.WriteLine($"Data: {node.Data} - Prev: {GetTestString(node.Prev)} - Next: {GetTestString(node.Next)} - Rand: {GetTestString(node.Rand)}");

            }
            Console.WriteLine("***********************************************");
            return UnicodeEncoding.UTF8.GetBytes(strRes.ToString());
        }

     
    }
}
