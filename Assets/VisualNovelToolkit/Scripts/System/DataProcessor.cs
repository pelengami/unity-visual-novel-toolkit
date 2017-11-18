using System;

namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal abstract class DataProcessor<TData> : IDataProcessor, IOutput<TData>
    {
        public void Activate()
        {
        }

        protected DataProcessor()
        {
        }
        
        public bool LinkTo(IInput<TData> output)
        {
            throw new NotImplementedException();
        }
        
        protected abstract void ProcessData(TData payload);
        
        private void ProcessingLoop()
        {
        }
        
        private void ProcessDataInternal(TData payload)
        {
            try
            {
                ProcessData(payload);
            }
            catch (Exception e)
            {
                //todo raise exception
            }
        }
    }
}