using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Utils;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Style
{
	sealed class GuiStylesDictionary
	{
		private readonly Dictionary<string, GUIStyle> _styles = new Dictionary<string, GUIStyle>();

		public void LoadStyles()
		{
			var style = CreateStyle("Assets/Editor/Textures/circle.png");
			_styles[NodeStyleNames.LoadScene] = style;
		}

		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle style;
			if (!_styles.TryGetValue(styleName, out style))
				throw new InvalidOperationException(styleName + " not found");
			return style;
		}

		private static GUIStyle CreateStyle(string textureName)
		{
			var texture = TextureUtils.LoadTexture(textureName);

			var style = new GUIStyle
			{
				normal = { background = texture },
				border = new RectOffset(2, 2, 2, 2)
			};

			return style;
		}
	}
}
