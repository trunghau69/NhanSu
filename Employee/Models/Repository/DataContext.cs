using Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Models.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<CoSoDaoTao> CoSoDaoTaos { get; set; }
        public DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public DbSet<TrinhDo> TrinhDos { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<KhenThuong> KhenThuongs { get; set; }
        public DbSet<KyLuat> KyLuats { get; set; }
        public DbSet<NghiPhep> NghiPheps { get; set; }
        public DbSet<Luong> Luongs { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<ThoiGianLamViec> ThoiGianLamViecs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HopDong>().ToTable("HopDong");
            modelBuilder.Entity<ChucVu>().ToTable("ChucVu");
            modelBuilder.Entity<ChuyenNganh>().ToTable("ChuyenNganh");
            modelBuilder.Entity<CoSoDaoTao>().ToTable("CoSoDaoTao");
            modelBuilder.Entity<KhenThuong>().ToTable("KhenThuong");
            modelBuilder.Entity<KyLuat>().ToTable("KyLuat");
            modelBuilder.Entity<NghiPhep>().ToTable("NghiPhep"); 
            modelBuilder.Entity<Luong>().ToTable("Luong");
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<PhongBan>().ToTable("PhongBan");
            modelBuilder.Entity<TrinhDo>().ToTable("TrinhDo");
            modelBuilder.Entity<ChamCong>().ToTable("ChamCong");
            modelBuilder.Entity<ThoiGianLamViec>().ToTable("ThoiGianLamViec");
        }
    }
}
