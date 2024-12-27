using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChamCong",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTrinhDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTrinhDo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCong", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MoTa = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChuyenMon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChuyenNganh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenMon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KhenThuong",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhenThuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LyDoKhenThuong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TienThuong = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuong", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KhoanTru",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LyDoKyLuat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoTienKyLuat = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoanTru", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNhanVien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CanCuocCongDan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DanToc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NoiO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QueQuan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ChucVuID = table.Column<int>(type: "int", nullable: false),
                    ChuyenMonID = table.Column<int>(type: "int", nullable: false),
                    PhongBanId = table.Column<int>(type: "int", nullable: false),
                    TrinhDoID = table.Column<int>(type: "int", nullable: false),
                    ChuyenNganhID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChamCong_TrinhDoID",
                        column: x => x.TrinhDoID,
                        principalTable: "ChamCong",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChucVu_ChucVuID",
                        column: x => x.ChucVuID,
                        principalTable: "ChucVu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChuyenMon_ChuyenNganhID",
                        column: x => x.ChuyenNganhID,
                        principalTable: "ChuyenMon",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NhanVien_PhongBan_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "PhongBan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChamCongs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    NgayChamCong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiLam = table.Column<bool>(type: "bit", nullable: false),
                    GioVao = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioRa = table.Column<TimeSpan>(type: "time", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenHopDong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "ntext", nullable: true),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NhanVienID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HopDong_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Luong",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    ThangNhanID = table.Column<int>(type: "int", nullable: false),
                    LuongCoBanID = table.Column<int>(type: "int", nullable: false),
                    TinhTrangLuong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TongThuNhap = table.Column<float>(type: "real", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDoKyLuatID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KyLuatID = table.Column<int>(type: "int", nullable: false),
                    LyDoKhenThuongID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhenThuongID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Luong", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Luong_KhenThuong_KhenThuongID",
                        column: x => x.KhenThuongID,
                        principalTable: "KhenThuong",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Luong_KhoanTru_KyLuatID",
                        column: x => x.KyLuatID,
                        principalTable: "KhoanTru",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Luong_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NghiPhep",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoNgay = table.Column<int>(type: "int", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NghiPhep", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NghiPhep_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienID = table.Column<int>(type: "int", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Quyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NguoiDung_NhanVien_NhanVienID",
                        column: x => x.NhanVienID,
                        principalTable: "NhanVien",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChamCongs_NhanVienID",
                table: "ChamCongs",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_NhanVienID",
                table: "HopDong",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_KhenThuongID",
                table: "Luong",
                column: "KhenThuongID");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_KyLuatID",
                table: "Luong",
                column: "KyLuatID");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_NhanVienID",
                table: "Luong",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_NghiPhep_NhanVienID",
                table: "NghiPhep",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_NhanVienID",
                table: "NguoiDung",
                column: "NhanVienID");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ChucVuID",
                table: "NhanVien",
                column: "ChucVuID");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ChuyenNganhID",
                table: "NhanVien",
                column: "ChuyenNganhID");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_PhongBanId",
                table: "NhanVien",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TrinhDoID",
                table: "NhanVien",
                column: "TrinhDoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChamCongs");

            migrationBuilder.DropTable(
                name: "HopDong");

            migrationBuilder.DropTable(
                name: "Luong");

            migrationBuilder.DropTable(
                name: "NghiPhep");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "KhenThuong");

            migrationBuilder.DropTable(
                name: "KhoanTru");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "ChamCong");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "ChuyenMon");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
