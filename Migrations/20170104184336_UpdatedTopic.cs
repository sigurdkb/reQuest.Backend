using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reQuest.Backend.Migrations
{
    public partial class UpdatedTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Competencies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Competencies");
        }
    }
}
