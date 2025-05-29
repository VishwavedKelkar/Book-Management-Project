using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Title_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");
        }
    }
}
