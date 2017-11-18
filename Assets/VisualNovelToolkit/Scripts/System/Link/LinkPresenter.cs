using System;
using Assets.VisualNovelToolkit.Scripts.System.Port;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Link
{
	sealed class LinkPresenter
	{
		private readonly LinkView _linkView;
		private readonly LinkData _linkData;
		private Vector2 _mousePosition;

		public LinkPresenter(LinkView linkView, LinkData linkData)
		{
			_linkView = linkView;
			_linkData = linkData;
		}

		public PortPresenter PortFrom { get; private set; }
		public PortPresenter PortTo { get; private set; }

		public LinkData LinkData => _linkData;

		public void Draw()
		{
			if (PortFrom != null && PortTo != null)
				_linkView.Draw(PortFrom.Rect, PortTo.Rect);
		}

		public void Draw(Rect rect)
		{
			_linkView.Draw(rect, _mousePosition);
		}

		public void ProcessEvents(Event e)
		{
			_mousePosition = e.mousePosition;
		}

		public void SetFrom(PortPresenter portPresenter, Guid nodeId)
		{
			PortFrom = portPresenter;
			_linkData.From = nodeId;
		}

		public void SetTo(PortPresenter portPresenter, Guid nodeId)
		{
			PortTo = portPresenter;
			_linkData.To = nodeId;
		}
	}
}
