using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindWorkRazor.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "resumeSrc",
                table: "workers",
                newName: "imageName");

            migrationBuilder.RenameColumn(
                name: "resumeName",
                table: "workers",
                newName: "employmentDegree");

            migrationBuilder.AlterColumn<int>(
                name: "age",
                table: "workers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "educationDegree",
                table: "workers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "graduationYear",
                table: "workers",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "educationDegree",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "graduationYear",
                table: "workers");

            migrationBuilder.RenameColumn(
                name: "imageName",
                table: "workers",
                newName: "resumeSrc");

            migrationBuilder.RenameColumn(
                name: "employmentDegree",
                table: "workers",
                newName: "resumeName");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "age",
                table: "workers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
