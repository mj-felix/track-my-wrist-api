using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyWristAPI.Migrations
{
    public partial class UserWatchRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Watches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Watches_UserId",
                table: "Watches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watches_Users_UserId",
                table: "Watches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watches_Users_UserId",
                table: "Watches");

            migrationBuilder.DropIndex(
                name: "IX_Watches_UserId",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Watches");
        }
    }
}
