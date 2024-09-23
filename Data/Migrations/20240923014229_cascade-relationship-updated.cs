using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class cascaderelationshipupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Book_BookId",
                table: "History");

            migrationBuilder.AddForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Book_BookId",
                table: "History",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Book_BookId",
                table: "History");

            migrationBuilder.AddForeignKey(
                name: "FK_History_AspNetUsers_UserId",
                table: "History",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Book_BookId",
                table: "History",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
