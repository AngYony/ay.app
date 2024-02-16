using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WY.EntityFramework.Migrations.SqlServer.Migrations
{
    public partial class wytest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Traffic",
                schema: "wy",
                table: "Article");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Traffic",
                schema: "wy",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
