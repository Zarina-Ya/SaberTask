using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal abstract class NewDeserializer : IDeserializer
    {
        protected readonly string NULL = "null";
        protected Dictionary<int, ListNode> _dict;
        private ListRand _list;


        protected NewDeserializer(ListRand list)
        {
            _list = list; 
            _dict = new Dictionary<int, ListNode>();

        }
        public void Check()
        {
            var newDict = new Dictionary<ListNode, int>();
            foreach (var key in _dict.Keys)
            {
                var node = _dict[key];
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

        public void CreateNodes( int countNodes)
        {
            for (int i = 0; i < countNodes; i++)
                _dict.Add(i, new ListNode());
        }


        public string GetStringFile(FileStream fileStream)
        {
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            return Encoding.Default.GetString(buffer);
        }

        public void InitListRand()
        {
            _list.Count = _dict.Count;
            _list.Head = _dict[0];
            _list.Tail = _dict[_dict.Count - 1];
        }

      
        public abstract void DeserializeHandler(FileStream fileStream);
    }
}
