//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QlyKhachSan.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETHOADON
    {
        public string MaHoaDon { get; set; }
        public string MaPhieuThue { get; set; }
        public Nullable<int> SoNgayThue { get; set; }
        public Nullable<int> SoKhach { get; set; }
        public Nullable<int> ThanhTien { get; set; }
    
        public virtual HOADON HOADON { get; set; }
        public virtual PHIEUTHUE PHIEUTHUE { get; set; }
    }
}
