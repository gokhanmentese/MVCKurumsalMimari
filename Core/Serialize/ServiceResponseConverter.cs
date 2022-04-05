
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Serialize
{
    public class ServiceResponseConverter : Newtonsoft.Json.JsonConverter
    {
        private readonly Dictionary<string, string> mappings = new Dictionary<string, string>
        {
            {"operation_name", "function_name"},
            {"state", "status"},
            {"additional_info", "description"},
            {"time","time"}
        };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var instance = Activator.CreateInstance(objectType);
            var properties = objectType.GetTypeInfo().DeclaredProperties.ToList();

            var payload = JObject.Load(reader);
            foreach (var property in payload.Properties())
            {
                if (!mappings.TryGetValue(property.Name, out var name))
                    name = property.Name;

                var instanceProperty = properties.FirstOrDefault(p => p.CanWrite && p.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == name);
                instanceProperty?.SetValue(instance, property.Value.ToObject(instanceProperty.PropertyType, serializer));
            }

            return instance;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsClass;
        }
    }
}
