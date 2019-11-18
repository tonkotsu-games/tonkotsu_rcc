
/// <summary>
/// Interface that describes a class that provides an InputPackage, probably something like the used Controller or an input  recording.
/// </summary>

public interface IInputProvider
{
    InputPackage GetPackage();
}

public class EmptyInput : IInputProvider
{
    public InputPackage GetPackage()
    {
        return InputPackage.Empty;
    }
}