using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintingAndSound.ORM.Migrations
{
    public partial class QAQ1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaintingfComments");

            migrationBuilder.CreateTable(
                name: "PaintingComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    PaintingId = table.Column<int>(nullable: false),
                    PaintingsId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintingComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaintingComments_Paintings_PaintingsId",
                        column: x => x.PaintingsId,
                        principalTable: "Paintings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaintingComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaintingComments_PaintingsId",
                table: "PaintingComments",
                column: "PaintingsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaintingComments_UserId",
                table: "PaintingComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaintingComments");

            migrationBuilder.CreateTable(
                name: "PaintingfComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintingfComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaintingfComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaintingfComments_UserId",
                table: "PaintingfComments",
                column: "UserId");
        }
    }
}
