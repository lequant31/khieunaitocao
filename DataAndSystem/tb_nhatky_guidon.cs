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
    
    public partial class tb_nhatky_guidon
    {
        public int id_nhatky_guidon { get; set; }
        public Nullable<int> id_thongtinkhieunai { get; set; }
        public string nguondon_bandau { get; set; }
        public Nullable<System.DateTime> ngay_vietdon { get; set; }
        public Nullable<System.DateTime> ngay_nhandon_bandau { get; set; }
        public string canbo_tiepnhan_bandau { get; set; }
        public string coquan_chuyenden { get; set; }
        public string kyhieu_donvi { get; set; }
        public string so_congvan { get; set; }
        public string hinhthuc_bandau_cua_donthu { get; set; }
        public string hinhthuc_tiepnhan { get; set; }
        public Nullable<System.DateTime> ngay_tiepnhan { get; set; }
    
        public virtual tb_thongtinkhieunai tb_thongtinkhieunai { get; set; }
    }
}
