using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal abstract class NewSerializer : ISerializer
    {
        protected Dictionary<ListNode, int> _dictNodes;
        protected ListRand _list;
        protected NewSerializer(ListRand list)
        {
            _list = list;
            _dictNodes = new Dictionary<ListNode, int>();
        }
        public void ConvertListBlockToDict(List<ListNode> list)
        {
            for (int i = 0; i < list.Count; i++)
                _dictNodes.Add(list[i], i);
        }

        public List<ListNode> GetAllNode()
        {
            var res = new List<ListNode>();
            var node = _list.Head;
            while (true)
            {
                if (node != null)
                    res.Add(node);
                else break;

                node = node.Next;
            }
            return res;
        }

        public string GetTestString(ListNode node)
            =>  node == null ? "null" : _dictNodes[node].ToString();

        public void SerializeHandler(FileStream fileStream)
        {
            var nodes = GetAllNode();
            ConvertListBlockToDict(nodes);

            fileStream.Write(CreateData(nodes));
        }
        public abstract byte[] CreateData(List<ListNode> nodes);
    }
}
