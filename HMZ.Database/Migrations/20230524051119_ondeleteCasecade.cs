using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMZ.Database.Migrations
{
    /// <inheritdoc />
    public partial class ondeleteCasecade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ClassRooms_RoomId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ClassRooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ClassRooms_RoomId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ClassRooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id");
        }
    }
}
