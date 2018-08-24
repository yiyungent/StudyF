using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Model.Migrations
{
    public partial class addTableRL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RankingList",
                columns: table => new
                {
                    RID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    SendMessage = table.Column<string>(maxLength: 100, nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    SenderIP = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingList", x => x.RID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingList");
        }
    }
}
