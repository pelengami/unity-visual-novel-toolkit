using System;
using UnityEngine;

namespace Assets.VisualNovelToolkit.Scripts.System.Port
{
    internal sealed class PortPresenter
    {
        private bool _isSelected;
        private readonly PortView _portView;

        public PortPresenter(PortView portView)
        {
            _portView = portView;
            _portView.Clicked += OnClicked;
        }

        public event Action Selected;
        public event Action UnSelected;

        public Rect Rect => _portView.Rect;

        public void Draw(Rect rect)
        {
            _portView.Draw(rect);
        }

        public void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.mouseUp:
                    if (_isSelected)
                    {
                        _isSelected = false;
                        UnSelected?.Invoke();
                    }
                    break;
            }
        }

        private void OnClicked()
        {
            _isSelected = true;
            Selected?.Invoke();
        }
    }
}