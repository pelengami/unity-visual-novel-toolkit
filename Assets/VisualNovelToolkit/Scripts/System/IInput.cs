namespace Assets.VisualNovelToolkit.Scripts.System
{
    public interface IInput<in TInput>
    {
	    bool RegisterInput(TInput input);
	}
}
