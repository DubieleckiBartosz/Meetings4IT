using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings;
using Panels.Domain.ScheduledMeetings;
using Panels.Domain.ScheduledMeetings.ValueObjects;

namespace Panels.Infrastructure.Database.Domain;

internal class ScheduledMeetingConfiguration : IEntityTypeConfiguration<ScheduledMeeting>
{
    internal const string UpcomingMeetings = "_upcomingMeetings";

    public void Configure(EntityTypeBuilder<ScheduledMeeting> builder)
    {
        builder.ToTable("ScheduledMeetings", "panels");
        builder.HasKey(_ => _.Id);
        builder.Ignore(x => x.Events);

        builder.OwnsOne<UserInfo>("ScheduleOwner", _ =>
        {
            _.Property(p => p.Name).HasColumnName("Name").IsRequired();
            _.Property(p => p.Identifier).HasColumnName("Identifier").IsRequired();
        });

        builder.OwnsMany<UpcomingMeeting>(UpcomingMeetings, _ =>
        {
            _.WithOwner().HasForeignKey("ScheduledMeetingId");
            _.ToTable("UpcomingMeetings", "panels");

            _.HasOne<Meeting>().WithOne().HasForeignKey<UpcomingMeeting>(m => m.MeetingId);
            _.Property(_ => _.MeetingId).HasColumnName("MeetingId").IsRequired();
            _.OwnsOne<DateRange>("MeetingDateRange", b =>
            {
                b.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
                b.Property(p => p.EndDate).HasColumnName("EndDate").IsRequired(false);
            });
        });

        builder.Ignore(i => i.UpcomingMeetings);
    }
}