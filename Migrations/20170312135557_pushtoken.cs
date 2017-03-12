using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reQuest.Backend.Migrations
{
    public partial class pushtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PushToken",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushToken",
                table: "Players");
        }
    }
}
