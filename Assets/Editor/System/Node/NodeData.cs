using System;

namespace Assets.Editor.System.Node
{
	sealed class NodeData
	{
		public NodeData()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; }
	}
}
