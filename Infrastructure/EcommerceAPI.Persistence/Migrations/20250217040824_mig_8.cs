﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Persistence.Migrations
{
    public partial class mig_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowCase",
                table: "Files",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowCase",
                table: "Files");
        }
    }
}
