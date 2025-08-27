using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonPlaceholderImporter.Migrations
{
    /// <inheritdoc />
    public partial class RenameTodoIsFinishedToCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Todos",
                newName: "Completed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "Todos",
                newName: "IsFinished");
        }
    }
}
