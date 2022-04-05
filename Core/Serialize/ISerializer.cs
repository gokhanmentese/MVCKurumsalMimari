using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Core.Serialize
{
    public interface ISerializer
    {
        string Serialize<TEntity>(TEntity entity)
            where TEntity : class, new();

        TEntity Deserialize<TEntity>(string entity)
            where TEntity : class, new();
    }

    public class JsonSerializer : ISerializer
    {
        public string Serialize<TEntity>(TEntity entity)
            where TEntity : class, new()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TEntity));
                ser.WriteObject(ms, entity);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public TEntity Deserialize<TEntity>(string entity)
            where TEntity : class, new()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TEntity));
            byte[] result = Encoding.UTF8.GetBytes(entity);

            //using (MemoryStream stream = new MemoryStream(result))
            //{
            //    return ser.ReadObject(stream) as TEntity;
            //}

            using (var stream = JsonReaderWriterFactory.CreateJsonReader(result, XmlDictionaryReaderQuotas.Max))
            {
                return ser.ReadObject(stream) as TEntity;
            }
        }
    }

    public class DeserializeAsBaseResolver : DataContractResolver
    {
        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
        }

        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null) ?? declaredType;
        }
    }
}
