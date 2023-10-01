﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panels.Infrastructure.Database;

#nullable disable

namespace Panels.Infrastructure.Database.Migrations
{
    [DbContext(typeof(PanelContext))]
    [Migration("20231001183554_Init1")]
    partial class Init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("panels")
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Meetings4IT.Shared.Implementations.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Categories.MeetingCategory", b =>
                {
                    b.Property<byte>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Index"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Value");

                    b.HasKey("Index");

                    b.ToTable("MeetingCategories", "panels");

                    b.HasData(
                        new
                        {
                            Index = (byte)1,
                            Value = "Party"
                        },
                        new
                        {
                            Index = (byte)2,
                            Value = "Social"
                        },
                        new
                        {
                            Index = (byte)3,
                            Value = "Business"
                        },
                        new
                        {
                            Index = (byte)4,
                            Value = "SomeCoffee"
                        },
                        new
                        {
                            Index = (byte)5,
                            Value = "Mentoring"
                        },
                        new
                        {
                            Index = (byte)6,
                            Value = "Unknown"
                        });
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatorName");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int")
                        .HasColumnName("MeetingId");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2")
                        .HasColumnName("Modified");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingComments", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Entities.InvitationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<string>("ReasonRejection")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReasonRejection");

                    b.Property<int>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.ToTable("InvitationRequests", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AcceptedInvitations")
                        .HasColumnType("int")
                        .HasColumnName("AcceptedInvitations");

                    b.Property<byte>("CategoryIndex")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<Guid>("ExplicitMeetingId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ExplicitMeetingId");

                    b.Property<bool>("HasPanelVisibility")
                        .HasColumnType("bit")
                        .HasColumnName("HasPanelVisibility");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit")
                        .HasColumnName("IsPublic");

                    b.Property<int?>("MaxInvitations")
                        .HasColumnType("int")
                        .HasColumnName("MaxInvitations");

                    b.Property<int>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("Status");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryIndex");

                    b.HasIndex("ExplicitMeetingId");

                    b.ToTable("Meetings", "panels");
                });

            modelBuilder.Entity("Panels.Domain.ScheduledMeetings.ScheduledMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ScheduledMeetings", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Users.Entities.Opinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatorId");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatorName");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModified");

                    b.Property<int?>("RatingSoftSkills")
                        .HasColumnType("tinyint")
                        .HasColumnName("RatingSoftSkills");

                    b.Property<int?>("RatingTechnicalSkills")
                        .HasColumnType("tinyint")
                        .HasColumnName("RatingTechnicalSkills");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserOpinions", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Users.Technologies.Technology", b =>
                {
                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Index"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Value");

                    b.HasKey("Index");

                    b.ToTable("Technologies", "panels");

                    b.HasData(
                        new
                        {
                            Index = 1,
                            Value = ".NET"
                        },
                        new
                        {
                            Index = 2,
                            Value = "JAVA"
                        },
                        new
                        {
                            Index = 3,
                            Value = "PYTHON"
                        },
                        new
                        {
                            Index = 4,
                            Value = "C++"
                        },
                        new
                        {
                            Index = 5,
                            Value = "R"
                        },
                        new
                        {
                            Index = 6,
                            Value = "SQL"
                        },
                        new
                        {
                            Index = 7,
                            Value = "PostgreSQL"
                        },
                        new
                        {
                            Index = 8,
                            Value = "RUBY"
                        },
                        new
                        {
                            Index = 9,
                            Value = "DEVOPS"
                        },
                        new
                        {
                            Index = 10,
                            Value = "MONGODB"
                        },
                        new
                        {
                            Index = 11,
                            Value = "DOCKER"
                        });
                });

            modelBuilder.Entity("Panels.Domain.Users.Technologies.UserTechnology", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TechnologyIndex")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TechnologyIndex");

                    b.HasIndex("TechnologyIndex");

                    b.ToTable("UserTechnology", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("City");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Identifier");

                    b.Property<bool>("IsEager")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", "panels");
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Entities.Comment", b =>
                {
                    b.HasOne("Panels.Domain.Meetings.Meeting", null)
                        .WithMany("_comments")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Entities.InvitationRequest", b =>
                {
                    b.HasOne("Panels.Domain.Meetings.Meeting", null)
                        .WithMany("_requests")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Meetings4IT.Shared.Abstractions.Kernel.ValueObjects.UserInfo", "RequestCreator", b1 =>
                        {
                            b1.Property<int>("InvitationRequestId")
                                .HasColumnType("int");

                            b1.Property<string>("Identifier")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CreatorId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("InvitationRequestId");

                            b1.ToTable("InvitationRequests", "panels");

                            b1.WithOwner()
                                .HasForeignKey("InvitationRequestId");
                        });

                    b.Navigation("RequestCreator")
                        .IsRequired();
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Meeting", b =>
                {
                    b.HasOne("Panels.Domain.Meetings.Categories.MeetingCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryIndex")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Panels.Domain.Meetings.Entities.Invitation", "_invitations", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("varchar(20)")
                                .HasColumnName("Code");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2")
                                .HasColumnName("Created");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("DeletedAt");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("Email");

                            b1.Property<DateTime>("ExpirationDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpirationDate");

                            b1.Property<DateTime>("LastModified")
                                .HasColumnType("datetime2")
                                .HasColumnName("LastModified");

                            b1.Property<int>("MeetingId")
                                .HasColumnType("int");

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("RecipientName");

                            b1.Property<int>("Status")
                                .HasColumnType("tinyint")
                                .HasColumnName("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("MeetingId");

                            b1.ToTable("Invitations", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.OwnsOne("Meetings4IT.Shared.Abstractions.Kernel.ValueObjects.DateRange", "Date", b1 =>
                        {
                            b1.Property<int>("MeetingId")
                                .HasColumnType("int");

                            b1.Property<DateTime?>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartDate");

                            b1.HasKey("MeetingId");

                            b1.ToTable("Meetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.OwnsOne("Meetings4IT.Shared.Abstractions.Kernel.ValueObjects.UserInfo", "Organizer", b1 =>
                        {
                            b1.Property<int>("MeetingId")
                                .HasColumnType("int");

                            b1.Property<string>("Identifier")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("OrganizerId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("OrganizerName");

                            b1.HasKey("MeetingId");

                            b1.ToTable("Meetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.OwnsOne("Panels.Domain.Meetings.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("MeetingId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("City");

                            b1.Property<string>("NumberStreet")
                                .IsRequired()
                                .HasColumnType("varchar(15)")
                                .HasColumnName("NumberStreet");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("Street");

                            b1.HasKey("MeetingId");

                            b1.ToTable("Meetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.OwnsOne("Panels.Domain.Meetings.ValueObjects.MeetingCancellation", "Cancellation", b1 =>
                        {
                            b1.Property<int>("MeetingId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CancellationDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("CancellationDate");

                            b1.Property<string>("Reason")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MeetingId");

                            b1.ToTable("Meetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.OwnsMany("Panels.Domain.Meetings.ValueObjects.MeetingImage", "_images", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Key");

                            b1.Property<int>("MeetingId")
                                .HasColumnType("int")
                                .HasColumnName("MeetingId");

                            b1.HasKey("Id");

                            b1.HasIndex("MeetingId");

                            b1.ToTable("Images", "panels");

                            b1.WithOwner()
                                .HasForeignKey("MeetingId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Cancellation");

                    b.Navigation("Category");

                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("Organizer")
                        .IsRequired();

                    b.Navigation("_images");

                    b.Navigation("_invitations");
                });

            modelBuilder.Entity("Panels.Domain.ScheduledMeetings.ScheduledMeeting", b =>
                {
                    b.OwnsOne("Meetings4IT.Shared.Abstractions.Kernel.ValueObjects.UserInfo", "ScheduleOwner", b1 =>
                        {
                            b1.Property<int>("ScheduledMeetingId")
                                .HasColumnType("int");

                            b1.Property<string>("Identifier")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Identifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ScheduledMeetingId");

                            b1.ToTable("ScheduledMeetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("ScheduledMeetingId");
                        });

                    b.OwnsMany("Panels.Domain.ScheduledMeetings.ValueObjects.UpcomingMeeting", "_upcomingMeetings", b1 =>
                        {
                            b1.Property<int>("ScheduledMeetingId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<Guid>("MeetingId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("MeetingId");

                            b1.HasKey("ScheduledMeetingId", "Id");

                            b1.ToTable("UpcomingMeetings", "panels");

                            b1.WithOwner()
                                .HasForeignKey("ScheduledMeetingId");

                            b1.OwnsOne("Meetings4IT.Shared.Abstractions.Kernel.ValueObjects.DateRange", "MeetingDateRange", b2 =>
                                {
                                    b2.Property<int>("UpcomingMeetingScheduledMeetingId")
                                        .HasColumnType("int");

                                    b2.Property<int>("UpcomingMeetingId")
                                        .HasColumnType("int");

                                    b2.Property<DateTime?>("EndDate")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("EndDate");

                                    b2.Property<DateTime>("StartDate")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("StartDate");

                                    b2.HasKey("UpcomingMeetingScheduledMeetingId", "UpcomingMeetingId");

                                    b2.ToTable("UpcomingMeetings", "panels");

                                    b2.WithOwner()
                                        .HasForeignKey("UpcomingMeetingScheduledMeetingId", "UpcomingMeetingId");
                                });

                            b1.Navigation("MeetingDateRange")
                                .IsRequired();
                        });

                    b.Navigation("ScheduleOwner")
                        .IsRequired();

                    b.Navigation("_upcomingMeetings");
                });

            modelBuilder.Entity("Panels.Domain.Users.Entities.Opinion", b =>
                {
                    b.HasOne("Panels.Domain.Users.User", null)
                        .WithMany("_opinions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Panels.Domain.Users.Technologies.UserTechnology", b =>
                {
                    b.HasOne("Panels.Domain.Users.Technologies.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyIndex")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Panels.Domain.Users.User", "User")
                        .WithMany("_stack")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Technology");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Panels.Domain.Users.User", b =>
                {
                    b.OwnsOne("Panels.Domain.Users.ValueObjects.UserImage", "Image", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2")
                                .HasColumnName("Created");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("DeletedAt");

                            b1.Property<string>("Key")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Key");

                            b1.Property<DateTime>("LastModified")
                                .HasColumnType("datetime2")
                                .HasColumnName("LastModified");

                            b1.HasKey("UserId");

                            b1.ToTable("UserImages", "panels");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Panels.Domain.Meetings.Meeting", b =>
                {
                    b.Navigation("_comments");

                    b.Navigation("_requests");
                });

            modelBuilder.Entity("Panels.Domain.Users.User", b =>
                {
                    b.Navigation("_opinions");

                    b.Navigation("_stack");
                });
#pragma warning restore 612, 618
        }
    }
}
