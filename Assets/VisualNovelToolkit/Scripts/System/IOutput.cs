namespace Assets.VisualNovelToolkit.Scripts.System
{
    internal interface IOutput<out TOuput>
    {
        bool LinkTo(IInput<TOuput> output);
    }
}
