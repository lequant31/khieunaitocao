//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAndSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_canbochiensy
    {
        public int ma_donvi_tb_donvi { get; set; }
        public int ma_canbo { get; set; }
        public string sohieu_cand { get; set; }
        public string hoten_chiensy { get; set; }
        public string ten_dangnhap { get; set; }
        public string matkhau_dangnhap { get; set; }
        public string capbac { get; set; }
        public string chucvu { get; set; }
        public int quyenhan { get; set; }
    
        public virtual tb_donvi tb_donvi { get; set; }
    }
}
