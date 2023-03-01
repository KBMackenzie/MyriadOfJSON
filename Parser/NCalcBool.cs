namespace MiscellaneousJSON.Parser;

public class NCalcBool
{
    public bool Boolean { get; private set; }

    public NCalcBool(bool b)
    {
        Boolean = b;
    }

    public override string ToString()
        => Boolean.ToString().ToLower(); 

    public static implicit operator NCalcBool(bool b)
        => new(b);

    public static implicit operator string(NCalcBool n)
        => n.ToString(); 
}
