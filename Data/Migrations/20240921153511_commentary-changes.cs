using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class commentarychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentary_AspNetUsers_PublisherId",
                table: "Comentary");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentary_Book_BookId",
                table: "Comentary");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentary_AspNetUsers_PublisherId",
                table: "Comentary",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentary_Book_BookId",
                table: "Comentary",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentary_AspNetUsers_PublisherId",
                table: "Comentary");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentary_Book_BookId",
                table: "Comentary");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentary_AspNetUsers_PublisherId",
                table: "Comentary",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentary_Book_BookId",
                table: "Comentary",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
