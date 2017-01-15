using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reQuest.Backend.Migrations
{
    public partial class UpdatedQuest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Quests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Quests");
        }
    }
}
