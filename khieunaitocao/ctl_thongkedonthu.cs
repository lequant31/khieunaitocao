using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAndSystem;

namespace khieunaitocao
{
    public partial class Ctl_thongkedonthu : DevExpress.XtraEditors.XtraUserControl
    {
        khieunaitocaoContextDataContext khieunaitocaoContextDataContext;
        public Ctl_thongkedonthu()
        {
            InitializeComponent();
        }

        private void Ctl_thongkedonthu_Load(object sender, EventArgs e)
        {
            
        }
        private void Fun_load()
        {
            using (khieunaitocaoContextDataContext = new khieunaitocaoContextDataContext())
            {
                var list=khieunaitocaoContextDataContext.thongketonghop_khieunai(date_tungay.DateTime,date_denngay.DateTime,Dinhdanh.madonvi).ToList();
                grc_thongkedonthu.DataSource = list;
            }
        }

        private void Date_denngay_EditValueChanged(object sender, EventArgs e)
        {
            Fun_load();
        }

        private void Date_tungay_EditValueChanged(object sender, EventArgs e)
        {
            Fun_load();
        }
    }
}
