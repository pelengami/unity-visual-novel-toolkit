using UnityEditor;

namespace Assets.VisualNovelToolkit.Scripts.ToolkitGui.Controls.Dialog
{
	sealed class LoadTextureDialog
	{
		private bool _result;
		private string _path;

		public bool Result => _result;
		public string Path => _path;

		public void ShowDialog()
		{
			var path = EditorUtility.OpenFilePanel("Texture", "", "png,psd,jpeg,jpg,bmp");
			if (string.IsNullOrEmpty(path))
				return;

			_result = true;
			_path = path;
		}
	}
}
