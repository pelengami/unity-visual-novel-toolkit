using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Editor.Serialization
{
	static class XmlReadWriter
	{
		public static void Write<T>(string path, object obj)
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream, obj);
				stream.Close();
			}
		}
	}
}
