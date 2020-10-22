using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaintingAndSound.ORM.Migrations
{
    public partial class QAQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TeamNumber = table.Column<int>(nullable: false),
                    TeamSynopsis = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Synopsis = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true),
                    FansCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fans_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Painting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    PaintingUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Painting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Painting_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Radio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RadioUrl = table.Column<string>(nullable: true),
                    Iamge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Radio_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTeam",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeam", x => new { x.UserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_UserTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeam_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FansAndUsers",
                columns: table => new
                {
                    FansId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FansAndUsers", x => new { x.UserId, x.FansId });
                    table.ForeignKey(
                        name: "FK_FansAndUsers_Fans_FansId",
                        column: x => x.FansId,
                        principalTable: "Fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FansAndUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaintionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ImagesUrl = table.Column<string>(nullable: true),
                    PaintingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaintionPhotos_Painting_PaintingId",
                        column: x => x.PaintingId,
                        principalTable: "Painting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RadioMusic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    RadioMusicUrl = table.Column<string>(nullable: true),
                    Detailed = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    RadioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioMusic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadioMusic_Radio_RadioId",
                        column: x => x.RadioId,
                        principalTable: "Radio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    RadiosId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    PaintingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Painting_PaintingsId",
                        column: x => x.PaintingsId,
                        principalTable: "Painting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_Radio_RadiosId",
                        column: x => x.RadiosId,
                        principalTable: "Radio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorksComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    WorksId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorksComments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorksComments_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fans_UsersId",
                table: "Fans",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_FansAndUsers_FansId",
                table: "FansAndUsers",
                column: "FansId");

            migrationBuilder.CreateIndex(
                name: "IX_Painting_UserId",
                table: "Painting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaintionPhotos_PaintingId",
                table: "PaintionPhotos",
                column: "PaintingId");

            migrationBuilder.CreateIndex(
                name: "IX_Radio_UserId",
                table: "Radio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RadioMusic_RadioId",
                table: "RadioMusic",
                column: "RadioId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_TeamId",
                table: "UserTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_PaintingsId",
                table: "Works",
                column: "PaintingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_RadiosId",
                table: "Works",
                column: "RadiosId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_UserId",
                table: "Works",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorksComments_UserId",
                table: "WorksComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorksComments_WorksId",
                table: "WorksComments",
                column: "WorksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FansAndUsers");

            migrationBuilder.DropTable(
                name: "PaintionPhotos");

            migrationBuilder.DropTable(
                name: "RadioMusic");

            migrationBuilder.DropTable(
                name: "UserTeam");

            migrationBuilder.DropTable(
                name: "WorksComments");

            migrationBuilder.DropTable(
                name: "Fans");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Painting");

            migrationBuilder.DropTable(
                name: "Radio");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
