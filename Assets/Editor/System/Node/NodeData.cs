using System;

namespace Assets.Editor.System.Node
{
	abstract class NodeData
	{
		protected NodeData()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; }
	}
}
