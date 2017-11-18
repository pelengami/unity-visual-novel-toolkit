using System;

namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal abstract class Filter<TInput, TOutput> : DataProcessor<TInput>, IFilter<TInput, TOutput>
    {
        public bool RegisterInput(TInput input)
        {
            throw new NotImplementedException();
        }

        public bool LinkTo(IInput<TOutput> output)
        {
            throw new NotImplementedException();
        }

        protected sealed override void ProcessData(TInput payload)
        {
        }

        protected abstract bool ProcessData(TInput input, out TOutput output);
    }
}