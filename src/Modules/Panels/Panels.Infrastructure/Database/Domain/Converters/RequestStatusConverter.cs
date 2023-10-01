using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panels.Domain.Meetings.Statuses;

namespace Panels.Infrastructure.Database.Domain.Converters;

public class RequestStatusConverter : ValueConverter<RequestStatus, int>
{
    public RequestStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<RequestStatus>(v))
    {
    }
}