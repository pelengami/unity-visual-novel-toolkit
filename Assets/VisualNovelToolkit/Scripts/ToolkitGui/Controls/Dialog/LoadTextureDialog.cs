using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Controls.Dialog
{
	sealed class LoadTextureDialog
	{
		private bool _result;
		private string _path;

		public bool Result { get { return _result; } }
		public string Path { get { return _path; } }

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
