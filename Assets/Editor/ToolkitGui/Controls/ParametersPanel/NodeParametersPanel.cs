using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Editor.ToolkitGui.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls.ParametersPanel
{
	sealed class NodeParametersPanel
	{
		private const float ParametersWidth = 125;
		private const float MarginTop = 75;

		private readonly List<NodeParameter> _parameters;

		private readonly GUIStyle _nodeParametersStyle;

		public NodeParametersPanel(List<NodeParameter> parameters)
		{
			_parameters = parameters;
			_nodeParametersStyle = StylesCollection.GetStyle(VntStyles.NodeParameters);
		}

		public void Draw(Rect rect)
		{
			rect.width = ParametersWidth;
			rect.y += MarginTop;

			GUI.Box(rect, string.Empty, _nodeParametersStyle);

			foreach (var parameter in _parameters)
			{
				parameter.Draw(rect);
				rect.y += 20;
			}
		}
	}
}
