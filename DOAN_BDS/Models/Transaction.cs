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
    
    public partial class Transaction
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> NgayGiaoDich { get; set; }
        public Nullable<double> GiaGiaoDich { get; set; }
        public string LoaiGiaoDich { get; set; }
        public Nullable<int> NguoiDungID { get; set; }
        public Nullable<int> BatDongSanID { get; set; }
    
        public virtual Property Property { get; set; }
        public virtual User User { get; set; }
    }
}
