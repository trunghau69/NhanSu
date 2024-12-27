using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Luong_KhoanTru_KyLuatID",
                table: "Luong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhoanTru",
                table: "KhoanTru");

            migrationBuilder.RenameTable(
                name: "KhoanTru",
                newName: "KyLuat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KyLuat",
                table: "KyLuat",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Luong_KyLuat_KyLuatID",
                table: "Luong",
                column: "KyLuatID",
                principalTable: "KyLuat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Luong_KyLuat_KyLuatID",
                table: "Luong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KyLuat",
                table: "KyLuat");

            migrationBuilder.RenameTable(
                name: "KyLuat",
                newName: "KhoanTru");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhoanTru",
                table: "KhoanTru",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Luong_KhoanTru_KyLuatID",
                table: "Luong",
                column: "KyLuatID",
                principalTable: "KhoanTru",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
