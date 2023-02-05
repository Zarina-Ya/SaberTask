using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    interface ISerializer
    {
        void SerializeHandler(FileStream fileStream);
        string GetTestString(ListNode node);
        byte[] CreateData(List<ListNode> nodes);
        void ConvertListBlockToDict(List<ListNode> list);
        List<ListNode> GetAllNode();
    }
}
