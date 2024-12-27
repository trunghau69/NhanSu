using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class ThreeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "ChuyenMonID",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaTrinhDo",
                table: "TrinhDo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaPhongBan",
                table: "PhongBan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "NhanVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "ChuyenNganhID",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CanCuocCongDan",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CoSoDaoTaoID",
                table: "NhanVien",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaKyLuat",
                table: "KyLuat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyLuat",
                table: "KyLuat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "MaKhenThuong",
                table: "KhenThuong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKhenThuong",
                table: "KhenThuong",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "MaHopDong",
                table: "HopDong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaChuyenNganh",
                table: "ChuyenNganh",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaChucVu",
                table: "ChucVu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CoSoDaoTao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCoSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenCoSo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoDaoTao", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_CoSoDaoTaoID",
                table: "NhanVien",
                column: "CoSoDaoTaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien",
                column: "ChuyenNganhID",
                principalTable: "ChuyenNganh",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_CoSoDaoTao_CoSoDaoTaoID",
                table: "NhanVien",
                column: "CoSoDaoTaoID",
                principalTable: "CoSoDaoTao",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_CoSoDaoTao_CoSoDaoTaoID",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "CoSoDaoTao");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_CoSoDaoTaoID",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "CoSoDaoTaoID",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "NgayKyLuat",
                table: "KyLuat");

            migrationBuilder.DropColumn(
                name: "NgayKhenThuong",
                table: "KhenThuong");

            migrationBuilder.AlterColumn<string>(
                name: "MaTrinhDo",
                table: "TrinhDo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaPhongBan",
                table: "PhongBan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "NhanVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChuyenNganhID",
                table: "NhanVien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CanCuocCongDan",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChuyenMonID",
                table: "NhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MaKyLuat",
                table: "KyLuat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaKhenThuong",
                table: "KhenThuong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaHopDong",
                table: "HopDong",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaChuyenNganh",
                table: "ChuyenNganh",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaChucVu",
                table: "ChucVu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChuyenNganh_ChuyenNganhID",
                table: "NhanVien",
                column: "ChuyenNganhID",
                principalTable: "ChuyenNganh",
                principalColumn: "ID");
        }
    }
}
