using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend5.Migrations
{
    public partial class AddPlacements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Labs_LabId",
                table: "Analyzes");

            migrationBuilder.DropForeignKey(
                name: "FK_WardStaff_Wards_WardHospitalId_WardId1",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Analyzes",
                table: "Analyzes");

            migrationBuilder.RenameTable(
                name: "WardStaff",
                newName: "WardStaffs");

            migrationBuilder.RenameTable(
                name: "Analyzes",
                newName: "Analyses");

            migrationBuilder.RenameIndex(
                name: "IX_WardStaff_WardHospitalId_WardId1",
                table: "WardStaffs",
                newName: "IX_WardStaffs_WardHospitalId_WardId1");

            migrationBuilder.RenameIndex(
                name: "IX_Analyzes_LabId",
                table: "Analyses",
                newName: "IX_Analyses_LabId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WardStaffs",
                table: "WardStaffs",
                columns: new[] { "WardId", "WardStaffId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardId = table.Column<int>(nullable: false),
                    WardHospitalId = table.Column<int>(nullable: false),
                    WardId1 = table.Column<int>(nullable: false),
                    Bed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Placements_Wards_WardHospitalId_WardId1",
                        columns: x => new { x.WardHospitalId, x.WardId1 },
                        principalTable: "Wards",
                        principalColumns: new[] { "HospitalId", "WardId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Placements_WardHospitalId_WardId1",
                table: "Placements",
                columns: new[] { "WardHospitalId", "WardId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_Labs_LabId",
                table: "Analyses",
                column: "LabId",
                principalTable: "Labs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WardStaffs_Wards_WardHospitalId_WardId1",
                table: "WardStaffs",
                columns: new[] { "WardHospitalId", "WardId1" },
                principalTable: "Wards",
                principalColumns: new[] { "HospitalId", "WardId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_Labs_LabId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_WardStaffs_Wards_WardHospitalId_WardId1",
                table: "WardStaffs");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WardStaffs",
                table: "WardStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses");

            migrationBuilder.RenameTable(
                name: "WardStaffs",
                newName: "WardStaff");

            migrationBuilder.RenameTable(
                name: "Analyses",
                newName: "Analyzes");

            migrationBuilder.RenameIndex(
                name: "IX_WardStaffs_WardHospitalId_WardId1",
                table: "WardStaff",
                newName: "IX_WardStaff_WardHospitalId_WardId1");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_LabId",
                table: "Analyzes",
                newName: "IX_Analyzes_LabId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WardStaff",
                table: "WardStaff",
                columns: new[] { "WardId", "WardStaffId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analyzes",
                table: "Analyzes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Labs_LabId",
                table: "Analyzes",
                column: "LabId",
                principalTable: "Labs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WardStaff_Wards_WardHospitalId_WardId1",
                table: "WardStaff",
                columns: new[] { "WardHospitalId", "WardId1" },
                principalTable: "Wards",
                principalColumns: new[] { "HospitalId", "WardId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
