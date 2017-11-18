using Assets.VisualNovelToolkit.Scripts.System;
using UnityEngine.UI;

namespace VisualNovelToolkit.Scripts.System.Filters
{
    internal sealed class ChangeImageFilter : Filter<Image, Image>
    {
        public Image ImageSource;
        
        protected override bool ProcessData(Image input, out Image output)
        {
            //todo change image source

            output = null;
            return true;
        }
    }
}