namespace WebTravelMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoai")]
    public partial class TheLoai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLoai()
        {
            BaiViets = new HashSet<BaiViet>();
        }

        [Key]
        public int MaTheLoai { get; set; }

        [Required]
        [StringLength(200)]
        public string TenTheLoai { get; set; }

        public int? SoLuongBaiViet { get; set; }

        public int? MaDanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
