﻿namespace Meetings4IT.Shared.Domain.Kernel;

public abstract class IndexValuePair 
{
    public int Index { get; }
    public string Value { get; }

    protected IndexValuePair(int index, string value, Action<int, string>? actionValidator = null)
    {
        actionValidator?.Invoke(index, value);

        Index = index;
        Value = value;
    }
}