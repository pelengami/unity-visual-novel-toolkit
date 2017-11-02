using System;
using System.Collections.Generic;
using Assets.Editor.ToolkitGui.Utils;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Styles
{
	sealed class StylesCollection
	{
		private readonly Dictionary<string, GUIStyle> _styles = new Dictionary<string, GUIStyle>();

		public void LoadStyles()
		{
			var style = CreateNodeStyle("Assets/Editor/Textures/SetBackgroundNode.psd");
			_styles[StyleNames.SetBackgroundNode] = style;

			style = CreateConnectionPointStyle("Assets/Editor/Textures/ConnectionPointNormal.psd", "Assets/Editor/Textures/ConnectionPointActive.psd");
			_styles[StyleNames.ConnectionIn] = style;

			style = CreateConnectionPointStyle("Assets/Editor/Textures/ConnectionPointNormal.psd", "Assets/Editor/Textures/ConnectionPointActive.psd");
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
				border = new RectOffset(1, 1, 1, 1),
			};

			return style;
		}

		private static GUIStyle CreateConnectionPointStyle(string textureNameNormal, string textureNameActive)
		{
			var style = new GUIStyle
			{
				normal = { background = TextureUtils.LoadTexture(textureNameNormal) },
				active = { background = TextureUtils.LoadTexture(textureNameActive) },
				border = new RectOffset(1, 1, 1, 1)
			};

			return style;
		}
	}
}
