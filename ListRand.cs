using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    internal class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            string extension = Path.GetExtension(s.Name);
            if (extension == ".txt")
            {
                SerializerStream serializerData = new SerializerStream(this);
                serializerData.SerializeHandler(s);
            }
            else if (extension == ".json")
            {
                SerializerJson serializerJson = new SerializerJson(this);
                serializerJson.SerializeHandler(s);
            }
            else
                throw new Exception("Error param");
           
        }

        public void Deserialize(FileStream s)
        {
            string extension = Path.GetExtension(s.Name);
            if (extension == ".txt")
            {
                DeserializerStream data = new DeserializerStream(this);
                data.DeserializeHandler(s);
            }
            else if (extension == ".json")
            {
                DeserializerJson deserializerJson = new DeserializerJson(this);
                deserializerJson.DeserializeHandler(s);
            }
            else
                throw new Exception("Error param");
        }
    }
}
