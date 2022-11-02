using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindWorkRazor.Migrations
{
    public partial class WithPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photoName",
                table: "workers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photoSrc",
                table: "workers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoName",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "photoSrc",
                table: "workers");
        }
    }
}
