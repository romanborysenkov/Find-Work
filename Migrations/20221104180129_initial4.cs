using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindWorkRazor.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageName",
                table: "workers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageName",
                table: "workers",
                type: "TEXT",
                nullable: true);
        }
    }
}
