using System;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.ContextMenu
{
	sealed class ContextMenuItem
	{
		public event Action<Vector2> Clicked;

		public string Title { get; set; }

		public void RaiseClicked(Vector2 mousePosition)
		{
			Clicked?.Invoke(mousePosition);
		}
	}
}