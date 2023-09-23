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
                name: "Technologies",
                schema: "panels",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                schema: "panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExplicitMeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryIndex = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    MaxInvitations = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "UpcomingMeetings",
                schema: "panels",
                columns: table => new
                {
                    ScheduledMeetingId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomingMeetings", x => new { x.ScheduledMeetingId, x.Id });
                    table.ForeignKey(
                        name: "FK_UpcomingMeetings_ScheduledMeetings_ScheduledMeetingId",
                        column: x => x.ScheduledMeetingId,
                        principalSchema: "panels",
                        principalTable: "ScheduledMeetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                schema: "panels",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "panels",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTechnology",
                schema: "panels",
                columns: table => new
                {
                    TechnologyIndex = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTechnology", x => new { x.UserId, x.TechnologyIndex });
                    table.ForeignKey(
                        name: "FK_UserTechnology_Technologies_TechnologyIndex",
                        column: x => x.TechnologyIndex,
                        principalSchema: "panels",
                        principalTable: "Technologies",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTechnology_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "panels",
                        principalTable: "Users",
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                schema: "panels",
                table: "MeetingCategories",
                columns: new[] { "Index", "Value" },
                values: new object[,]
                {
                    { 1, "Party" },
                    { 2, "Social" },
                    { 3, "Business" },
                    { 4, "SomeCoffee" },
                    { 5, "Mentoring" },
                    { 6, "Unknown" }
                });

            migrationBuilder.InsertData(
                schema: "panels",
                table: "Technologies",
                columns: new[] { "Index", "Value" },
                values: new object[,]
                {
                    { 1, ".NET" },
                    { 2, "Java" },
                    { 3, "Python" },
                    { 4, "C++" },
                    { 5, "R" },
                    { 6, "SQL" },
                    { 7, "PostgreSql" },
                    { 8, "Ruby" },
                    { 9, "DevOps" },
                    { 10, "MongoDB" }
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
                name: "IX_Meetings_ExplicitMeetingId",
                schema: "panels",
                table: "Meetings",
                column: "ExplicitMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTechnology_TechnologyIndex",
                schema: "panels",
                table: "UserTechnology",
                column: "TechnologyIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "UserImages",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "UserTechnology",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Meetings",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "ScheduledMeetings",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Technologies",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "panels");

            migrationBuilder.DropTable(
                name: "MeetingCategories",
                schema: "panels");
        }
    }
}
