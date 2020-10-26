using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Backend6.Data.Migrations
{
    public partial class ChangedUserIdForString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessages_AspNetUsers_UserId1",
                table: "ForumMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopics_AspNetUsers_UserId1",
                table: "ForumTopics");

            migrationBuilder.DropIndex(
                name: "IX_ForumTopics_UserId1",
                table: "ForumTopics");

            migrationBuilder.DropIndex(
                name: "IX_ForumMessages_UserId1",
                table: "ForumMessages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ForumTopics");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ForumMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ForumTopics",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ForumMessages",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_ForumTopics_UserId",
                table: "ForumTopics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessages_UserId",
                table: "ForumMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessages_AspNetUsers_UserId",
                table: "ForumMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopics_AspNetUsers_UserId",
                table: "ForumTopics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessages_AspNetUsers_UserId",
                table: "ForumMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopics_AspNetUsers_UserId",
                table: "ForumTopics");

            migrationBuilder.DropIndex(
                name: "IX_ForumTopics_UserId",
                table: "ForumTopics");

            migrationBuilder.DropIndex(
                name: "IX_ForumMessages_UserId",
                table: "ForumMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ForumTopics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ForumTopics",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ForumMessages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ForumMessages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForumTopics_UserId1",
                table: "ForumTopics",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessages_UserId1",
                table: "ForumMessages",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessages_AspNetUsers_UserId1",
                table: "ForumMessages",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopics_AspNetUsers_UserId1",
                table: "ForumTopics",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
