using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend5.Migrations
{
    public partial class AddIdDependenceForWards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WardStaff_Wards_WardId",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff");

            migrationBuilder.DropIndex(
                name: "IX_WardStaff_WardId",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wards",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_HospitalId",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WardStaff");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wards");

            migrationBuilder.AddColumn<int>(
                name: "WardStaffId",
                table: "WardStaff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardHospitalId",
                table: "WardStaff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId1",
                table: "WardStaff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Wards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff",
                columns: new[] { "WardId", "WardStaffId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wards",
                table: "Wards",
                columns: new[] { "HospitalId", "WardId" });

            migrationBuilder.CreateIndex(
                name: "IX_WardStaff_WardHospitalId_WardId1",
                table: "WardStaff",
                columns: new[] { "WardHospitalId", "WardId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_WardStaff_Wards_WardHospitalId_WardId1",
                table: "WardStaff",
                columns: new[] { "WardHospitalId", "WardId1" },
                principalTable: "Wards",
                principalColumns: new[] { "HospitalId", "WardId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WardStaff_Wards_WardHospitalId_WardId1",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff");

            migrationBuilder.DropIndex(
                name: "IX_WardStaff_WardHospitalId_WardId1",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wards",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "WardStaffId",
                table: "WardStaff");

            migrationBuilder.DropColumn(
                name: "WardHospitalId",
                table: "WardStaff");

            migrationBuilder.DropColumn(
                name: "WardId1",
                table: "WardStaff");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Wards");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WardStaff",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wards",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wards",
                table: "Wards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WardStaff_WardId",
                table: "WardStaff",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_HospitalId",
                table: "Wards",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_WardStaff_Wards_WardId",
                table: "WardStaff",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
