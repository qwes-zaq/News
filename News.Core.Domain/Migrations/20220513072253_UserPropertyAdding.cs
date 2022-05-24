using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Core.Domain.Migrations
{
    public partial class UserPropertyAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "PostTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "Languages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Languages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "CategoryTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "CategoryTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AddedById",
                table: "Tags",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UpdatedById",
                table: "Tags",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_AddedById",
                table: "PostTranslations",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UpdatedById",
                table: "Posts",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_AddedById",
                table: "Languages",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UpdatedById",
                table: "Languages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_AddedById",
                table: "CategoryTranslations",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_UpdatedById",
                table: "CategoryTranslations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AddedById",
                table: "Categories",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedById",
                table: "Categories",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AddedById",
                table: "Categories",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_AspNetUsers_AddedById",
                table: "CategoryTranslations",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_AspNetUsers_UpdatedById",
                table: "CategoryTranslations",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_AddedById",
                table: "Languages",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_UpdatedById",
                table: "Languages",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UpdatedById",
                table: "Posts",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTranslations_AspNetUsers_AddedById",
                table: "PostTranslations",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_AddedById",
                table: "Tags",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_UpdatedById",
                table: "Tags",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AddedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_AspNetUsers_AddedById",
                table: "CategoryTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_AspNetUsers_UpdatedById",
                table: "CategoryTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_AddedById",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_UpdatedById",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UpdatedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTranslations_AspNetUsers_AddedById",
                table: "PostTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_AddedById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_UpdatedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_AddedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UpdatedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_PostTranslations_AddedById",
                table: "PostTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UpdatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Languages_AddedById",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_UpdatedById",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_AddedById",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTranslations_UpdatedById",
                table: "CategoryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AddedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "PostTranslations");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "CategoryTranslations");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Categories");
        }
    }
}
