using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindWorkRazor.Migrations
{
    public partial class highLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "responses",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "responses");
        }
    }
}
