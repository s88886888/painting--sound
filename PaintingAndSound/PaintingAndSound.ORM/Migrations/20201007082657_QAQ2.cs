using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintingAndSound.ORM.Migrations
{
    public partial class QAQ2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Radio_RadioId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_RadioId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "RadioId",
                table: "Works");

            migrationBuilder.AddColumn<int>(
                name: "WorksId",
                table: "Radio",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Radio_WorksId",
                table: "Radio",
                column: "WorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Radio_Works_WorksId",
                table: "Radio",
                column: "WorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Radio_Works_WorksId",
                table: "Radio");

            migrationBuilder.DropIndex(
                name: "IX_Radio_WorksId",
                table: "Radio");

            migrationBuilder.DropColumn(
                name: "WorksId",
                table: "Radio");

            migrationBuilder.AddColumn<int>(
                name: "RadioId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_RadioId",
                table: "Works",
                column: "RadioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Radio_RadioId",
                table: "Works",
                column: "RadioId",
                principalTable: "Radio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
