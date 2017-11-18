namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal interface IFilter<in TInput, out TOutput> : IDataProcessor, IOutput<TOutput>, IInput<TInput>
    {
    }
}