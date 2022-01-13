using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackMyWristAPI.Migrations
{
    public partial class Wearing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedDate",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Watches");

            migrationBuilder.CreateTable(
                name: "Wearings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WatchId = table.Column<int>(type: "int", nullable: true),
                    WorkDay = table.Column<bool>(type: "bit", nullable: false),
                    WorkFromHomeDay = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wearings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wearings_Watches_WatchId",
                        column: x => x.WatchId,
                        principalTable: "Watches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wearings_WatchId",
                table: "Wearings",
                column: "WatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wearings");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedDate",
                table: "Watches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Watches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
