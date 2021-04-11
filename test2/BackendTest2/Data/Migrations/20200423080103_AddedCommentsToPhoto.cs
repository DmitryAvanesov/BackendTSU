using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackendTest2.Data.Migrations
{
    public partial class AddedCommentsToPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PhotoId",
                table: "Comments",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Photo_PhotoId",
                table: "Comments",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Photo_PhotoId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PhotoId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Comments");
        }
    }
}
