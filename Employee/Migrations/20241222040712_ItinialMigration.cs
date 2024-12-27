using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class ItinialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChamCong_TrinhDoID",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChuyenMon_ChuyenNganhID",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "ChamCongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChuyenMon",
                table: "ChuyenMon");

            migrationBuilder.DropColumn(
                name: "MaTrinhDo",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "TenTrinhDo",
                table: "ChamCong");

            migrationBuilder.RenameTable(
                name: "ChuyenMon",
                newName: "ChuyenNganh");

            migrationBuilder.AddColumn<bool>(
                name: "DiLam",
                table: "ChamCong",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioRa",
                table: "ChamCong",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioVao",
                table: "ChamCong",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayChamCong",
                table: "ChamCong",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NhanVienID",
                table: "ChamCong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChuyenNganh",
                table: "ChuyenNganh",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "TrinhDo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTrinhDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTrinhDo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDo", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChamCong_NhanVienID",
                table: "ChamCong",
                column: "NhanVienID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChamCong_NhanVien_NhanVienID",
                table: "ChamCong",
                column: "NhanVienID",
                principalTable: "NhanVien",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien",
                column: "ChuyenNganhID",
                principalTable: "ChuyenNganh",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_TrinhDo_TrinhDoID",
                table: "NhanVien",
                column: "TrinhDoID",
                principalTable: "TrinhDo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChamCong_NhanVien_NhanVienID",
                table: "ChamCong");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_TrinhDo_TrinhDoID",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "TrinhDo");

            migrationBuilder.DropIndex(
                name: "IX_ChamCong_NhanVienID",
                table: "ChamCong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChuyenNganh",
                table: "ChuyenNganh");

            migrationBuilder.DropColumn(
                name: "DiLam",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "GioRa",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "GioVao",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "NgayChamCong",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "NhanVienID",
                table: "ChamCong");

            migrationBuilder.RenameTable(
                name: "ChuyenNganh",
                newName: "ChuyenMon");

            migrationBuilder.AddColumn<string>(
                name: "MaTrinhDo",
                table: "ChamCong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenTrinhDo",
                table: "ChamCong",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChuyenMon",
                table: "ChuyenMon",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "ChamCongs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    DiLam = table.Column<bool>(type: "bit", nullable: false),
                    GioRa = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioVao = table.Column<TimeSpan>(type: "time", nullable: false),
                    NgayChamCong = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCongs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChamCongs_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChamCongs_NhanVienID",
                table: "ChamCongs",
                column: "NhanVienID");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChamCong_TrinhDoID",
                table: "NhanVien",
                column: "TrinhDoID",
                principalTable: "ChamCong",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChuyenMon_ChuyenNganhID",
                table: "NhanVien",
                column: "ChuyenNganhID",
                principalTable: "ChuyenMon",
                principalColumn: "ID");
        }
    }
}
