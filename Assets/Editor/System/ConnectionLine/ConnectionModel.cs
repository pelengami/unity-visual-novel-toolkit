using System;

namespace Assets.Editor.System.ConnectionLine
{
	sealed class ConnectionModel
	{
		public ConnectionModel()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; }
	}
}
