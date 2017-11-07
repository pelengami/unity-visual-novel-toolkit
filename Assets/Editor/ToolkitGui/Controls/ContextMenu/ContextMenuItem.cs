using System;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls.ContextMenu
{
	sealed class ContextMenuItem
	{
		public event Action<Vector2> Clicked;

		public string Title { get; set; }

		public void RaiseClicked(Vector2 mousePosition)
		{
			// ReSharper disable once UseNullPropagation
			if (Clicked != null)
				Clicked.Invoke(mousePosition);
		}
	}
}