using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ToolkitGui.Utils
{
	static class TextureUtils
	{
		public static Texture2D LoadTexture(string texturePath)
		{
			var texture2D = (Texture2D)AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D));
			return texture2D;
		}
	}
}
