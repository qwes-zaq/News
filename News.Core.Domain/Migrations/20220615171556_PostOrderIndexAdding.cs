using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Core.Domain.Migrations
{
    public partial class PostOrderIndexAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "PostTranslations");

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "PostTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
