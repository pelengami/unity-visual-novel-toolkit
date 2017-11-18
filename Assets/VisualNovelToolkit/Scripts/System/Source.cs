using System;

namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal abstract class Source<TData> : ISource<TData>
    {
        protected Source()
        {
        }

        public void Activate()
        {
            
        }

        public bool LinkTo(IInput<TData> output)
        {
            throw new NotImplementedException();
        }
    }
}