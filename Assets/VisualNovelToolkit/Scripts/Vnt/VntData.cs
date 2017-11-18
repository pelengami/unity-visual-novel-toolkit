using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.VisualNovelToolkit.Scripts.System.Link;
using Assets.VisualNovelToolkit.Scripts.System.Node;

namespace Assets.VisualNovelToolkit.Scripts.Vnt
{
	[XmlRoot("VntData")]
	[Serializable]
	internal sealed class VntData
	{
		private readonly List<NodeData> _nodeDatas = new List<NodeData>();
		private readonly List<LinkData> _connectionDatas = new List<LinkData>();

		private List<NodeData> NodeDatas => _nodeDatas;

		private List<LinkData> ConnectionDatas => _connectionDatas;

		public void LoadNodes()
		{

		}

		public void AddNodeData(NodeData nodeData)
		{
			NodeDatas.Add(nodeData);
		}

		public void AddConnectionData(LinkData linkData)
		{
			ConnectionDatas.Add(linkData);
		}
	}
}
