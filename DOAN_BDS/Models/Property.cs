//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOAN_BDS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int ID { get; set; }
        public string TenBatDongSan { get; set; }
        public string DiaChi { get; set; }
        public string DienTich { get; set; }
        public Nullable<double> Gia { get; set; }
        public string DVT { get; set; }
        public string LoaiBatDongSan { get; set; }
        public Nullable<int> SoPhongNgu { get; set; }
        public Nullable<int> SoPhongTam { get; set; }
        public string TienIch { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> NguoiDangTinID { get; set; }
        public string PhiDangTin { get; set; }
        public Nullable<int> IDdanhmuc { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
