namespace WebTravelMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [Key]
        public int MaBaiViet { get; set; }

        [Required]
        [StringLength(200)]
        public string TenBaiViet { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public string AnhBia { get; set; }

        public DateTime NgayDang { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? LuotThich { get; set; }

        public int? An { get; set; }

        public int? MaTheLoai { get; set; }

        [StringLength(50)]
        public string MaTaiKhoan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
