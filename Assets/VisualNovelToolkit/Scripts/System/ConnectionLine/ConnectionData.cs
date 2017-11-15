using System;
using UnityEngine;

namespace Assets.Editor.System.ConnectionLine
{
	[Serializable]
	public sealed class ConnectionData
	{
		[SerializeField]
		private readonly Guid _id;
		[SerializeField]
		private Guid _from;
		[SerializeField]
		private Guid _to;

		public ConnectionData()
		{
			_id = Guid.NewGuid();
		}

		public Guid Id
		{
			get { return _id; }
		}

		public Guid From
		{
			get { return _from; }
			set { _from = value; }
		}

		public Guid To
		{
			get { return _to; }
			set { _to = value; }
		}
	}
}
