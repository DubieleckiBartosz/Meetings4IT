namespace Meetings4IT.Shared.Abstractions.Kernel;

public class IndexValuePair
{
    public int Index { get; }
    public string Value { get; }

    protected IndexValuePair(string value, Action<string>? actionValidator = null)
    {
        actionValidator?.Invoke(value);
        Value = value;
    }

    protected IndexValuePair(int index, string value, Action<int, string>? actionValidator = null)
    {
        actionValidator?.Invoke(index, value);

        Index = index;
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        IndexValuePair other = (IndexValuePair)obj;

        return Value.ToUpper() == other.Value.ToUpper();
    }
}