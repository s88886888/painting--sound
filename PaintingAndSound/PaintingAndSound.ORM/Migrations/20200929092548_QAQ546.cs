using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintingAndSound.ORM.Migrations
{
    public partial class QAQ546 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingComments_Users_UserId",
                table: "PaintingComments");

            migrationBuilder.DropIndex(
                name: "IX_PaintingComments_UserId",
                table: "PaintingComments");

            migrationBuilder.CreateIndex(
                name: "IX_PaintingComments_PaintingId",
                table: "PaintingComments",
                column: "PaintingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingComments_Paintings_PaintingId",
                table: "PaintingComments",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingComments_Paintings_PaintingId",
                table: "PaintingComments");

            migrationBuilder.DropIndex(
                name: "IX_PaintingComments_PaintingId",
                table: "PaintingComments");

            migrationBuilder.CreateIndex(
                name: "IX_PaintingComments_UserId",
                table: "PaintingComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingComments_Users_UserId",
                table: "PaintingComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
