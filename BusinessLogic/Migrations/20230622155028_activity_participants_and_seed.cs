using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class activity_participants_and_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUser", x => new { x.ActivitiesId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ActivityUser_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUser_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { new Guid("c9c70b4e-6d0c-4a6f-85f9-7a6b3c8d2f9e"), "admin@gmail.com", "Admin", "admin", 0, "admin" },
                    { new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3"), "organizer@gmail.com", "Organizer", "organizer", 937654321, "organizer" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Category", "Date", "Description", "Location", "Name", "OrganizerId" },
                values: new object[] { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"), "Festival", new DateOnly(1, 1, 1), "A maior corrida do Douro.", "Porto", "Love Tiles Douro Granfondo", new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3") });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_ParticipantsId",
                table: "ActivityUser",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9c70b4e-6d0c-4a6f-85f9-7a6b3c8d2f9e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => new { x.ActivityId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_EventParticipants_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipants_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ParticipantId",
                table: "EventParticipants",
                column: "ParticipantId");
        }
    }
}
