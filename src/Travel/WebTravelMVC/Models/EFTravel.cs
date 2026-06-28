using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebTravelMVC.Models
{
    public partial class EFTravel : DbContext
    {
        public EFTravel()
            : base("name=EFTravel")
        {
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiViet>()
                .Property(e => e.MaTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SDT)
                .IsUnicode(false);
        }
    }
}
