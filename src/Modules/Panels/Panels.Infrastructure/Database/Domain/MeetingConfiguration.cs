﻿using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.ValueObjects;
using Panels.Infrastructure.Database.Domain.Converters;

namespace Panels.Infrastructure.Database.Domain;

internal class MeetingConfiguration : WatcherConfiguration, IEntityTypeConfiguration<Meeting>
{
    internal const string Invitations = "_invitations";
    internal const string Images = "_images";
    internal const string InvitationRequests = "_requests";
    internal const string Comments = "_comments";

    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings", "panels");
        builder.HasKey(_ => _.Id);

        builder.Property(x => x.ExplicitMeetingId)
          .HasColumnName("ExplicitMeetingId")
          .HasConversion(x => x.Value, v => new MeetingId(v))
          .IsRequired();

        builder.HasIndex("ExplicitMeetingId");

        builder.Ignore(x => x.Events);
        builder.Ignore(x => x.Invitations);
        builder.Ignore(x => x.Comments);
        builder.Ignore(x => x.MeetingImages);
        builder.Ignore(x => x.InvitationRequests);

        builder.Property(_ => _.IsPublic)
            .HasColumnName("IsPublic")
            .IsRequired();

        builder.Property(_ => _.HasPanelVisibility)
            .HasColumnName("HasPanelVisibility")
            .IsRequired();

        builder.Property(_ => _.AcceptedInvitations)
            .HasColumnName("AcceptedInvitations")
            .IsRequired();

        builder.Property(_ => _.MaxInvitations)
            .HasColumnName("MaxInvitations")
            .HasColumnType("int")
            .IsRequired(false);

        builder.Property(_ => _.Created).HasColumnName("Created").IsRequired();

        builder.Property(_ => _.Description)
          .HasColumnName("Description")
          .HasConversion(x => x.Value, x => new Description(x)).IsRequired();

        builder.Property(p => p.Status)
          .HasColumnName("Status")
          .HasColumnType("tinyint")
          .HasConversion<MeetingStatusConverter>()
          .IsRequired();

        builder.HasOne(_ => _.Category).WithMany().HasForeignKey(x => x.CategoryIndex);

        builder.HasMany(Comments).WithOne().HasForeignKey("MeetingId");
        builder.HasMany(InvitationRequests).WithOne().HasForeignKey("MeetingId");
        builder.HasMany(Invitations).WithOne().HasForeignKey("MeetingId");

        builder.OwnsOne<UserInfo>("Organizer", _ =>
        {
            _.Property(p => p.Name)
                .HasColumnName("OrganizerName")
                .HasColumnType("varchar(50)").IsRequired();

            _.Property(p => p.Identifier).HasColumnName("OrganizerId").IsRequired();
        });

        builder.OwnsOne(_ => _.Date, b =>
        {
            b.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            b.Property(p => p.EndDate).HasColumnName("EndDate").IsRequired(false);

            b.Ignore(p => p.DurationInMinutes);
            b.Ignore(p => p.DurationInHours);
        });

        builder.OwnsOne(_ => _.Cancellation, b =>
        {
            b.Property(p => p.CancellationDate)
              .HasColumnName("CancellationDate")
              .HasConversion(x => x.Value, x => new Date(x))
              .IsRequired();

            b.Property(_ => _.Reason).IsRequired(false);
        });

        builder.OwnsOne(_ => _.Address, b =>
        {
            //b.WithOwner().HasForeignKey("MeetingId");
            //b.ToTable("Addresses", "panels");

            //b.Property<int>("AddressId");
            //b.HasKey("AddressId", "MeetingId");

            b.Property(p => p.City)
              .HasColumnType("varchar(50)")
              .HasColumnName("City")
              .IsRequired();

            b.Property(p => p.Street)
              .HasColumnType("varchar(50)")
              .HasColumnName("Street")
              .IsRequired();

            b.Property(p => p.NumberStreet)
              .HasColumnType("varchar(15)")
              .HasColumnName("NumberStreet")
              .IsRequired();
        });

        builder.OwnsMany<MeetingImage>(Images, _ =>
        {
            _.WithOwner().HasForeignKey("MeetingId");
            _.ToTable("Images", "panels");

            _.Property<int>("Id");
            _.HasKey("Id");

            _.Property(p => p.MeetingId).HasColumnName("MeetingId").IsRequired();
            _.Property(p => p.Key).HasColumnName("Key").IsRequired();
        });
    }
}