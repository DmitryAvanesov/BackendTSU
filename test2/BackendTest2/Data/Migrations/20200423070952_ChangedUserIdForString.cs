using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackendTest2.Data.Migrations
{
    public partial class ChangedUserIdForString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_UserId1",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_UserId1",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Photo",
                newName: "Path");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_UserId",
                table: "Photo");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Photo",
                newName: "UserId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Photo",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId1",
                table: "Photo",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_UserId1",
                table: "Photo",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
