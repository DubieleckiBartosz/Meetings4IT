using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel;

namespace Panels.Domain.Users.Technologies;

public class Technology : IndexValuePair
{
    public Technology(string value) : base(value, Validator)
    {
    }

    public static void Validator(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new IndexValuePairException("Incorrect value", "Technology value cannot be null or empty.");
        }
    }
}