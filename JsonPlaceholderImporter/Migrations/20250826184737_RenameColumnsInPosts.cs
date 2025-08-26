using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonPlaceholderImporter.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsInPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "Body");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Posts",
                newName: "Description");
        }
    }
}
