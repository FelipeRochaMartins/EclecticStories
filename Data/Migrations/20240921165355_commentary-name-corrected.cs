using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class commentarynamecorrected : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentary",
                table: "Comentary");

            migrationBuilder.RenameTable(
                name: "Comentary",
                newName: "Commentary");

            migrationBuilder.RenameIndex(
                name: "IX_Comentary_PublisherId",
                table: "Commentary",
                newName: "IX_Commentary_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentary_BookId",
                table: "Commentary",
                newName: "IX_Commentary_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commentary",
                table: "Commentary",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_AspNetUsers_PublisherId",
                table: "Commentary",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_Book_BookId",
                table: "Commentary",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_AspNetUsers_PublisherId",
                table: "Commentary");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_Book_BookId",
                table: "Commentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commentary",
                table: "Commentary");

            migrationBuilder.RenameTable(
                name: "Commentary",
                newName: "Comentary");

            migrationBuilder.RenameIndex(
                name: "IX_Commentary_PublisherId",
                table: "Comentary",
                newName: "IX_Comentary_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentary_BookId",
                table: "Comentary",
                newName: "IX_Comentary_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentary",
                table: "Comentary",
                column: "CommentId");

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
    }
}
