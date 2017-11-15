using System.IO;
using System.Xml.Serialization;

namespace Editor.Serialization
{
    static class XmlReadWriter
    {
        public static void Write<T>(string path, object obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Create))
                    serializer.Serialize(stream, obj);
        }
    }
}