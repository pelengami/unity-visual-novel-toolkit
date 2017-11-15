using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Editor.ToolkitGui.Styles;
using Assets.Editor.ToolkitGui.Utils;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls.ParametersPanel
{
	sealed class NodeParametersPanel
	{
		private const float MarginTop = 110;

		private readonly List<NodeParameter> _parameters;
		private readonly Texture2D _backgroundTexture;
		private readonly GUIStyle _nodeParametersStyle;

		public NodeParametersPanel(List<NodeParameter> parameters)
		{
			_backgroundTexture = TextureUtils.LoadTexture("Assets/Editor/Textures/NodeParameters.psd");
			_parameters = parameters;
			_nodeParametersStyle = StylesCollection.GetStyle(VntStyles.NodeParameters);
			Width = 120;
		}

		public float Width;

		public void Draw(Rect rect)
		{
			rect.height = _parameters.Sum(p => p.Height);
			rect.width = Width;
			rect.y += MarginTop;

			GUILayout.BeginArea(rect, _nodeParametersStyle);
			GUILayout.BeginVertical();
			foreach (var nodeParameter in _parameters)
				nodeParameter.Draw(rect);
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}
