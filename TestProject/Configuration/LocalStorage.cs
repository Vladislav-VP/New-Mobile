using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestProject.Configurations.Interfaces;
using System.IO;

namespace TestProject.Configurations
{
    public class LocalStorage<T> : ILocalStorage<T>
    {
        private static readonly string _path;

        private readonly XmlSerializer _serializer;

        static LocalStorage()
        {
            string docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            _path = Path.Combine(docsFolder, Constants.LocalStorageName);
        }

        public LocalStorage()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public void Clear()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }

        public T Get()
        {
            T obj;
            using (Stream fileStream = File.OpenRead(_path))
            {
                obj = (T)_serializer.Deserialize(fileStream);
            }
            return obj;
        }

        public void Store(T obj)
        {
            using (Stream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                _serializer.Serialize(fileStream, obj);
            }
        }
    }
}
