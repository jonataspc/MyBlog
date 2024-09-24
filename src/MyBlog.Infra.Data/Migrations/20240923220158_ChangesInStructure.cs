using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_IsActive",
                table: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsActive_PublishDate",
                table: "Post",
                columns: new[] { "IsActive", "PublishDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_IsActive_PublishDate",
                table: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Post_IsActive",
                table: "Post",
                column: "IsActive");
        }
    }
}
