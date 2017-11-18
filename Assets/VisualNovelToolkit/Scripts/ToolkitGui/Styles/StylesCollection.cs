using System;
using System.Collections.Generic;
using Assets.VisualNovelToolkit.Scripts.ToolkitGui.Utils;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Styles
{
	static class StylesCollection
	{
		private static readonly Dictionary<VntStyles, GUIStyle> Styles = new Dictionary<VntStyles, GUIStyle>();

		public static void LoadStyles()
		{
			var style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeBackground.psd");
			Styles[VntStyles.SetBackgroundNode] = style;

			style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeCharacter.psd");
			Styles[VntStyles.CharacterNode] = style;

			style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeCharacterDialogue.psd");
			Styles[VntStyles.DialogueNode] = style;

			style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeCharacterAnswer.psd");
			Styles[VntStyles.AnswerNode] = style;

			style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeCharacterQuestion.psd");
			Styles[VntStyles.QuestionNode] = style;

			style = CreateConnectionPointStyle("Assets/VisualNovelToolkit/Textures/ConnectionPointNormal.psd", "Assets/Editor/Textures/ConnectionPointActive.psd");
			Styles[VntStyles.PortIn] = style;

			style = CreateConnectionPointStyle("Assets/VisualNovelToolkit/Textures/ConnectionPointNormal.psd", "Assets/Editor/Textures/ConnectionPointActive.psd");
			Styles[VntStyles.PortOut] = style;

			style = CreateNodeStyle("Assets/VisualNovelToolkit/Textures/NodeParameters.psd");
			Styles[VntStyles.NodeParameters] = style;

			style = CreateLabelStyle();
			Styles[VntStyles.Label] = style;

			style = CreateButtonStyle("Assets/VisualNovelToolkit/Textures/ConnectionPointNormal.psd");
			Styles[VntStyles.Button] = style;

			style = CreateButtonStyle("Assets/VisualNovelToolkit/Textures/Down.psd");
			Styles[VntStyles.ButtonArrow] = style;

			style = CreateTextFieldStyle();
			Styles[VntStyles.TextField] = style;
		}

		public static GUIStyle GetStyle(VntStyles vntStyle)
		{
			GUIStyle style;
			if (!Styles.TryGetValue(vntStyle, out style))
				throw new InvalidOperationException(vntStyle + " not found");
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

		private static GUIStyle CreateLabelStyle()
		{
			var style = new GUIStyle
			{
				normal = { textColor = Color.white },
				fontSize = 12
			};

			return style;
		}

		private static GUIStyle CreateTextFieldStyle()
		{
			var style = new GUIStyle
			{
				normal = { textColor = Color.black },
				fontSize = 13,
				alignment = TextAnchor.UpperCenter
			};

			return style;
		}

		private static GUIStyle CreateButtonStyle(string textureNormal)
		{
			var style = new GUIStyle
			{
				normal =
				{
					textColor = Color.white,
					background = TextureUtils.LoadTexture(textureNormal)
				},
			};
			return style;
		}
	}
}
