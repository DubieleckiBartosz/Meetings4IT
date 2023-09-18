using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panels.Infrastructure.Database.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "panels");

            migrationBuilder.CreateTable(
                name: "MeetingCategories",
                schema: "panels",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingCategories", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledMeetings",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledMeetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryIndex = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    MaxInvitations = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cancellation_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_MeetingCategories_CategoryIndex",
                        column: x => x.CategoryIndex,
                        principalSchema: "panels",
                        principalTable: "MeetingCategories",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "panels",
                columns: table => new
                {
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberStreet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.MeetingId);
                    table.ForeignKey(
                        name: "FK_Addresses_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "panels",
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "panels",
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "panels",
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpcomingMeetings",
                schema: "panels",
                columns: table => new
                {
                    ScheduledMeetingId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomingMeetings", x => new { x.ScheduledMeetingId, x.Id });
                    table.ForeignKey(
                        name: "FK_UpcomingMeetings_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "panels",
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpcomingMeetings_ScheduledMeetings_ScheduledMeetingId",
                        column: x => x.ScheduledMeetingId,
                        principalSchema: "panels",
                        principalTable: "ScheduledMeetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_MeetingId",
                schema: "panels",
                table: "Images",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_MeetingId",
                schema: "panels",
                table: "Invitations",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CategoryIndex",
                schema: "panels",
                table: "Meetings",
                column: "CategoryIndex");

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingMeetings_MeetingId",
                schema: "panels",
                table: "UpcomingMeetings",
                column: "MeetingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "UpcomingMeetings",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Meetings",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "ScheduledMeetings",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "MeetingCategories",
                schema: "panels");
        }
    }
}
