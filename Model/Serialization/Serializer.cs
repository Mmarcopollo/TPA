﻿using System.IO;
using System.Runtime.Serialization;

namespace Model.Serialization
{
    public class Serializer
    {
        public void Write<T>(T obj, string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());

            using (FileStream stream = File.Create(filePath))
            {
                serializer.WriteObject(stream, obj);
            }
        }

        public T Read<T>(string filePath)
        {
            T result;
            DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
            using (FileStream stream = File.OpenRead(filePath))
            {
                result = (T)deserializer.ReadObject(stream);
            }

            return result;
        }
    }
}
