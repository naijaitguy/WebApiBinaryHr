using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiBinaryHr.Migrations
{
    public partial class restructurejobandapplicationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hired",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Interview",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Pending",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ShortListed",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "Hired",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Interview",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pending",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rejected",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortListed",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hired",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Interview",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Pending",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ShortListed",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "Hired",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Interview",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pending",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rejected",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortListed",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
