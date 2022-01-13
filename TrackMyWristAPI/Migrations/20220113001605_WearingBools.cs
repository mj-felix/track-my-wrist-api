using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyWristAPI.Migrations
{
    public partial class WearingBools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WorkDay",
                table: "Wearings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WorkFromHomeDay",
                table: "Wearings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDay",
                table: "Wearings");

            migrationBuilder.DropColumn(
                name: "WorkFromHomeDay",
                table: "Wearings");
        }
    }
}
