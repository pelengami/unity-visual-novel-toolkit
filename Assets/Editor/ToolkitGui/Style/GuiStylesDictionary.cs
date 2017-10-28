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
			var style = CreateNodeStyle("Assets/Editor/Textures/unity_icon_1.png");
			_styles[StyleNames.LoadSceneNode] = style;

			style = CreateNodeStyle("Assets/Editor/Textures/circle.png");
			_styles[StyleNames.ChangeBackgroundNode] = style;

			style = CreateConnectionPointStyle("Assets/Editor/Textures/circle.png", "Assets/Editor/Textures/circle.png");
			_styles[StyleNames.ConnectionIn] = style;

			style = CreateConnectionPointStyle("Assets/Editor/Textures/circle.png", "Assets/Editor/Textures/circle.png");
			_styles[StyleNames.ConnectionOut] = style;
		}

		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle style;
			if (!_styles.TryGetValue(styleName, out style))
				throw new InvalidOperationException(styleName + " not found");
			return style;
		}

		private static GUIStyle CreateNodeStyle(string textureName)
		{
			var texture = TextureUtils.LoadTexture(textureName);

			var style = new GUIStyle
			{
				normal = { background = texture },
				border = new RectOffset(2, 2, 2, 2),
			};

			return style;
		}

		private static GUIStyle CreateConnectionPointStyle(string textureNameNormal, string textureNameActive)
		{
			var style = new GUIStyle
			{
				normal = { background = TextureUtils.LoadTexture(textureNameNormal) },
				active = { background = TextureUtils.LoadTexture(textureNameActive) },
				border = new RectOffset(4, 4, 12, 12)
			};

			return style;
		}
	}
}
