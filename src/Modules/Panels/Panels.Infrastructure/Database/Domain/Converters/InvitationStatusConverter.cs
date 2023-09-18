using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panels.Domain.Meetings.Statuses;

namespace Panels.Infrastructure.Database.Domain.Converters;

public class InvitationStatusConverter : ValueConverter<InvitationStatus, int>
{
    public InvitationStatusConverter() : base(v => v.Id,
        v => Enumeration.GetById<InvitationStatus>(v))
    {
    }
}