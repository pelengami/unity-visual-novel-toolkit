using System;
using System.Xml.Serialization;

namespace Assets.Editor.System.Node.SetBackgroundNode
{
	public class SetBackgroundNodeData : NodeData
	{
		private string _texturePath;

		public string TexturePath
		{
			get { return _texturePath; }
			set { _texturePath = value; }
		}
	}
}