using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorRemoveUnusedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Author_Slug",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Author",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Author",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Author_Slug",
                table: "Author",
                column: "Slug",
                unique: true);
        }
    }
}
