using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Core.Domain.Migrations
{
    public partial class PinnedPostConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PinnedPosts_PinnedPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PinnedPostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PinnedPostId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedPosts_PostId",
                table: "PinnedPosts",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PinnedPosts_Posts_PostId",
                table: "PinnedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PinnedPosts_Posts_PostId",
                table: "PinnedPosts");

            migrationBuilder.DropIndex(
                name: "IX_PinnedPosts_PostId",
                table: "PinnedPosts");

            migrationBuilder.AddColumn<int>(
                name: "PinnedPostId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PinnedPostId",
                table: "Posts",
                column: "PinnedPostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PinnedPosts_PinnedPostId",
                table: "Posts",
                column: "PinnedPostId",
                principalTable: "PinnedPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
