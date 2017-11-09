using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.Editor.System.ConnectionLine;
using Assets.Editor.System.Node;

namespace Assets.Editor.Vnt
{
	[XmlRoot("VntData")]
	public sealed class VntData
	{
		private readonly List<NodeData> _nodeDatas = new List<NodeData>();
		private readonly List<ConnectionData> _connectionDatas = new List<ConnectionData>();

		public List<NodeData> NodeDatas
		{
			get { return _nodeDatas; }
		}

		public List<ConnectionData> ConnectionDatas
		{
			get { return _connectionDatas; }
		}

		public void LoadNodes()
		{

		}

		public void AddNodeData(NodeData nodeData)
		{
			NodeDatas.Add(nodeData);
		}

		public void AddConnectionData(ConnectionData connectionData)
		{
			ConnectionDatas.Add(connectionData);
		}

		public void RemoveNodeData(NodeData nodeData)
		{
			NodeDatas.Remove(nodeData);
		}

		public void RemoveConnectionData(ConnectionData connectionData)
		{
			ConnectionDatas.Remove(connectionData);
		}
	}
}
