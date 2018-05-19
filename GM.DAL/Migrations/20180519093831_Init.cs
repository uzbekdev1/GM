using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GM.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "GameMode",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_GameMode", x => x.Id); });

            migrationBuilder.CreateTable(
                "Map",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Map", x => x.Id); });

            migrationBuilder.CreateTable(
                "Player",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Player", x => x.Id); });

            migrationBuilder.CreateTable(
                "Servers",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Hostname = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Servers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Matche",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    FragLimit = table.Column<int>(nullable: false),
                    GameModeId = table.Column<long>(nullable: false),
                    MapId = table.Column<long>(nullable: false),
                    ServerId = table.Column<long>(nullable: false),
                    StartTimeStamp = table.Column<DateTime>("datetime2", nullable: false),
                    TimeLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matche", x => x.Id);
                    table.ForeignKey(
                        "FK_Matche_GameMode_GameModeId",
                        x => x.GameModeId,
                        "GameMode",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Matche_Map_MapId",
                        x => x.MapId,
                        "Map",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Matche_Servers_ServerId",
                        x => x.ServerId,
                        "Servers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Scoreboard",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Deaths = table.Column<int>(nullable: false),
                    Frags = table.Column<int>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    MatcheId = table.Column<long>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoreboard", x => x.Id);
                    table.ForeignKey(
                        "FK_Scoreboard_Matche_MatcheId",
                        x => x.MatcheId,
                        "Matche",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Scoreboard_Player_PlayerId",
                        x => x.PlayerId,
                        "Player",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Matche_GameModeId",
                "Matche",
                "GameModeId");

            migrationBuilder.CreateIndex(
                "IX_Matche_MapId",
                "Matche",
                "MapId");

            migrationBuilder.CreateIndex(
                "IX_Matche_ServerId",
                "Matche",
                "ServerId");

            migrationBuilder.CreateIndex(
                "IX_Scoreboard_MatcheId",
                "Scoreboard",
                "MatcheId");

            migrationBuilder.CreateIndex(
                "IX_Scoreboard_PlayerId",
                "Scoreboard",
                "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Scoreboard");

            migrationBuilder.DropTable(
                "Matche");

            migrationBuilder.DropTable(
                "Player");

            migrationBuilder.DropTable(
                "GameMode");

            migrationBuilder.DropTable(
                "Map");

            migrationBuilder.DropTable(
                "Servers");
        }
    }
}