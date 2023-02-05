using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberTask
{
    public interface IJsonElement { }
    public abstract class JsonElement<T>: IJsonElement
    {
        protected T _value;
        protected string _propertyName;

        public JsonElement(string name, T value)
        {
            _propertyName = name;
            _value = value;
        }

    }

    public class JsonString : JsonElement<string>
    {
        public JsonString(string name, string value) : base(name, value) { }
        public override string ToString()
        {
            return  $"\"{_propertyName}\":\"{/*ConstInformation.SeparatorStream +*/ _value /*+ ConstInformation.SeparatorStream*/}\"";
        }

    }

    public class JsonNullable : JsonElement<Nullable<int>>
    {
        public JsonNullable(string name, Nullable<int> value) : base(name, value) { }

        public override string ToString()
        {
            var tmp = _value == null ? "null" : _value.ToString();
            return  $"\"{_propertyName}\":{tmp}";
        }
    }


    public class JsonNode : IJsonElement
    {
        private JsonString Data;
        private JsonNullable Prev;
        private JsonNullable Next;
        private JsonNullable Rand;

        public JsonNode(string data, Nullable<int> prev , Nullable<int> next, Nullable<int> rand)
        {
            Data = new JsonString("Data", data);
            Prev = new JsonNullable("Prev", prev);
            Next = new JsonNullable("Next", next);
            Rand = new JsonNullable("Rand", rand);
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append(ConstInformation.NodeSeparatorJSON[0].ToString());
            str.Append($"{Data},");
            str.Append($"{Prev},");
            str.Append($"{Next},");
            str.Append($"{Rand}");
          
            str.Append(ConstInformation.NodeSeparatorJSON[1].ToString());

            return str.ToString();
        }
    }

    
    public class JsonArray
    {
       private List<IJsonElement> _elements;

        public JsonArray()
            => _elements = new List<IJsonElement>();

        public void AddElement(JsonNode element)
            => _elements.Add(element);

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append(ConstInformation.ArrayNodeSeparatorJSON[0].ToString());
            foreach (var element in _elements) {

                if (element == _elements[^1])
                    str.Append(element.ToString());
                else
                    str.Append(element.ToString() + ConstInformation.SeparatorJSON);

            }
            str.Append(ConstInformation.ArrayNodeSeparatorJSON[1].ToString());
            
            return str.ToString();
        }
    }
}
