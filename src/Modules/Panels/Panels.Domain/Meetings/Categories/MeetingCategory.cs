using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel;

namespace Panels.Domain.Meetings.Categories;

public class MeetingCategory : IndexValuePair
{
    /*
        Party
        Social
        Business
        SomeCoffee
        Unknown
     */

    public MeetingCategory(int index, string value) : base(index, value, Validator)
    {
    }

    public static void Validator(int index, string value)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Index));
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new IndexValuePairException("Incorrect value", "Meeting type value cannot be null or empty");
        }
    }
}