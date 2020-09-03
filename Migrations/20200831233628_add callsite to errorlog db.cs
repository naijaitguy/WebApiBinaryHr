using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiBinaryHr.Migrations
{
    public partial class addcallsitetoerrorlogdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallSite",
                table: "ErrorLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallSite",
                table: "ErrorLogs");
        }
    }
}
