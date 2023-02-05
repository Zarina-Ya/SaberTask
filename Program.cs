
namespace SaberTask
{
    internal class Program
    {
        private static int _count = 1;
        private static readonly int _maxCount = 3;
        private static Random _random = new Random();
        private static string _nameFileJson = "test.json";
        private static string _nameFileStream = "test.txt";
        public static void Main(string[] args)
        {
            
            ListRand listRand = new ListRand();
            List<ListNode> listNodes = GenerateListNodes();
            InitRandomNode(listNodes);
            listRand.Head = listNodes.First();
            listRand.Tail = listNodes[^1];
            listRand.Count = listNodes.Count;

            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var needFileName = Path.Combine(dir, _nameFileJson);
            using (FileStream fs = new FileStream(needFileName, FileMode.OpenOrCreate))
            {
                listRand.Serialize(fs);
            }

            ListRand listRand2 = new ListRand();
            using (FileStream fs = new FileStream(needFileName, FileMode.OpenOrCreate))
            {
                listRand2.Deserialize(fs);
            }

        }

        private static void InitRandomNode(List<ListNode> listNodes)
        {
            foreach (var node in listNodes)
                node.Rand = listNodes[_random.Next(0, listNodes.Count)];
        }

        private static List<ListNode> GenerateListNodes()
        {
            List<ListNode> listNodes = new List<ListNode>();
            listNodes.Add(new ListNode() { Data = "Node 0", });

            while (_count < _maxCount)
            {

                listNodes.Add(new ListNode()
                {
                    Data = $"Node {_count}",
                    Prev = listNodes[^1],

                });
                listNodes[listNodes.Count - 2].Next = listNodes[^1];
                _count++;
            }

          

            return listNodes;

        }
    }
}