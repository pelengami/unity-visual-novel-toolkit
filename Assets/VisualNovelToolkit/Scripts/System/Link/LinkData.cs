using System;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Link
{
	[Serializable]
	public sealed class LinkData
	{
		[SerializeField]
		private readonly Guid _id;
		[SerializeField]
		private Guid _from;
		[SerializeField]
		private Guid _to;

		public LinkData()
		{
			_id = Guid.NewGuid();
		}

		public Guid Id => _id;

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
