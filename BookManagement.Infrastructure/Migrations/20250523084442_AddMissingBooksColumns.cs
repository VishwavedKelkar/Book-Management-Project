using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingBooksColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "AuthorName");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserUserId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByUserUserId",
                table: "Books",
                column: "CreatedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UpdatedByUserUserId",
                table: "Books",
                column: "UpdatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_CreatedByUserUserId",
                table: "Books",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UpdatedByUserUserId",
                table: "Books",
                column: "UpdatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_CreatedByUserUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UpdatedByUserUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedByUserUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UpdatedByUserUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserUserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Authors",
                newName: "Name");
        }
    }
}
