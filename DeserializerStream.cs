using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal class DeserializerStream : NewDeserializer
    {
        public DeserializerStream(ListRand list) : base(list)
        {
        }

        public override void DeserializeHandler(FileStream fileStream)
        {
            var text = GetStringFile(fileStream);
            string[] words = text.Split(ConstInformation.SeparatorStream);
            var countNodes = words.Length / ConstInformation.CountSeparatorStream;
            CreateNodes(countNodes);
            SetConnectionNode(words);
            InitListRand();
            Check();
        }

        public void SetConnectionNode(string[] words)
        {
            var count = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (i % ConstInformation.CountSeparatorStream == 0)
                    count++;

                if (words[i] == String.Empty)
                    continue;

                var node = _dict[count - 1];

                switch (i % ConstInformation.CountSeparatorStream)
                {
                    case 0:
                        node.Data = words[i];
                        break;
                    case 1:
                        node.Prev = words[i] == NULL ? null : _dict[Convert.ToInt32(words[i])];
                        break;
                    case 2:
                        node.Next = words[i] == NULL ? null : _dict[Convert.ToInt32(words[i])];
                        break;
                    case 3:
                        node.Rand = words[i] == NULL ? null : _dict[Convert.ToInt32(words[i])];
                        break;

                }
            }
        }
    }
}
