using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class activity_participants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventParticipants",
                newName: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Activities_ActivityId",
                table: "EventParticipants",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Activities_ActivityId",
                table: "EventParticipants");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "EventParticipants",
                newName: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
