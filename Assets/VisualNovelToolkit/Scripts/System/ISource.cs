namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal interface ISource<out TOutput> : IDataProcessor, IOutput<TOutput>
    {
    }
}