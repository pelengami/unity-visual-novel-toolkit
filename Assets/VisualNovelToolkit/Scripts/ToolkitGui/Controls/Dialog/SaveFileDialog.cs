using UnityEditor;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.Dialog
{
	sealed class SaveFileDialog
	{
		private bool _result;
		private string _path;

		public bool Result => _result;
		public string Path => _path;

		public void ShowDialog()
		{
			var path = EditorUtility.SaveFilePanelInProject("VNT Config", "VNT Config", "txt", "Save Config");
			if (string.IsNullOrEmpty(path))
				return;

			_result = true;
			_path = path;
		}
	}
}
