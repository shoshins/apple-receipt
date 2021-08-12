using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Apple.Receipt.Verificator.Models.IAPVerification
{
    /// <summary>
    /// Converter for multi typed JSON object. When field can be array and object both.
    /// More info here https://blog.bitscry.com/2017/08/31/single-or-array-json-converter/
    /// </summary>
    /// <typeparam name="T"> Array object type </typeparam>
    internal class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(ICollection<T>);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            return token.Type == JTokenType.Array 
                ? token.ToObject<Collection<T>>() 
                : new Collection<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (Collection<T>) value;
            if (list.Count == 1)
            {
                value = list[0];
            }
            serializer.Serialize(writer, value);
        }

        public override bool CanWrite => true;
    }
}
