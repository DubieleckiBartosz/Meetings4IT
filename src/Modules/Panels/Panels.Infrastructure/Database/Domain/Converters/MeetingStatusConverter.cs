using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panels.Domain.Meetings.Statuses;

namespace Panels.Infrastructure.Database.Domain.Converters;

public class MeetingStatusConverter : ValueConverter<MeetingStatus, int>
{
    public MeetingStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<MeetingStatus>(v))
    {
    }
}