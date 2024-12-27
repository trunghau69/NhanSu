using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class CreateThoiGianLamViec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChamCong_NhanVien_NhanVienID",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "DiLam",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "GioRa",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "GioVao",
                table: "ChamCong");

            migrationBuilder.RenameColumn(
                name: "NhanVienID",
                table: "ChamCong",
                newName: "ThoiGianLamViecID");

            migrationBuilder.RenameColumn(
                name: "NgayChamCong",
                table: "ChamCong",
                newName: "Ngay");

            migrationBuilder.RenameIndex(
                name: "IX_ChamCong_NhanVienID",
                table: "ChamCong",
                newName: "IX_ChamCong_ThoiGianLamViecID");

            migrationBuilder.AddColumn<int>(
                name: "ThoiGianLamViecID",
                table: "NghiPhep",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThoiGianLamViecID",
                table: "Luong",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ChamCong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoGioLam",
                table: "ChamCong",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "ChamCong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ThoiGianLamViec",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    SoNgayNghi = table.Column<int>(type: "int", nullable: false),
                    SoNgayCong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGianLamViec", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ThoiGianLamViec_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NghiPhep_ThoiGianLamViecID",
                table: "NghiPhep",
                column: "ThoiGianLamViecID");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_ThoiGianLamViecID",
                table: "Luong",
                column: "ThoiGianLamViecID");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGianLamViec_NhanVienID",
                table: "ThoiGianLamViec",
                column: "NhanVienID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChamCong_ThoiGianLamViec_ThoiGianLamViecID",
                table: "ChamCong",
                column: "ThoiGianLamViecID",
                principalTable: "ThoiGianLamViec",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Luong_ThoiGianLamViec_ThoiGianLamViecID",
                table: "Luong",
                column: "ThoiGianLamViecID",
                principalTable: "ThoiGianLamViec",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NghiPhep_ThoiGianLamViec_ThoiGianLamViecID",
                table: "NghiPhep",
                column: "ThoiGianLamViecID",
                principalTable: "ThoiGianLamViec",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChamCong_ThoiGianLamViec_ThoiGianLamViecID",
                table: "ChamCong");

            migrationBuilder.DropForeignKey(
                name: "FK_Luong_ThoiGianLamViec_ThoiGianLamViecID",
                table: "Luong");

            migrationBuilder.DropForeignKey(
                name: "FK_NghiPhep_ThoiGianLamViec_ThoiGianLamViecID",
                table: "NghiPhep");

            migrationBuilder.DropTable(
                name: "ThoiGianLamViec");

            migrationBuilder.DropIndex(
                name: "IX_NghiPhep_ThoiGianLamViecID",
                table: "NghiPhep");

            migrationBuilder.DropIndex(
                name: "IX_Luong_ThoiGianLamViecID",
                table: "Luong");

            migrationBuilder.DropColumn(
                name: "ThoiGianLamViecID",
                table: "NghiPhep");

            migrationBuilder.DropColumn(
                name: "ThoiGianLamViecID",
                table: "Luong");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "SoGioLam",
                table: "ChamCong");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "ChamCong");

            migrationBuilder.RenameColumn(
                name: "ThoiGianLamViecID",
                table: "ChamCong",
                newName: "NhanVienID");

            migrationBuilder.RenameColumn(
                name: "Ngay",
                table: "ChamCong",
                newName: "NgayChamCong");

            migrationBuilder.RenameIndex(
                name: "IX_ChamCong_ThoiGianLamViecID",
                table: "ChamCong",
                newName: "IX_ChamCong_NhanVienID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChamCong_NhanVien_NhanVienID",
                table: "ChamCong",
                column: "NhanVienID",
                principalTable: "NhanVien",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
