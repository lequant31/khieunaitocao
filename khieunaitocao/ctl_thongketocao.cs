using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAndSystem;

namespace khieunaitocao
{
    public partial class ctl_thongketocao : UserControl
    {
        khieunaitocaoEntities _khieunaitocaoEntities;
        public ctl_thongketocao()
        {
            InitializeComponent();
        }
        private void Fun_load()
        {
            using (_khieunaitocaoEntities = new khieunaitocaoEntities())
            {
                var list = _khieunaitocaoEntities.thongketonghop_tocao(date_tungay.DateTime, date_denngay.DateTime, Dinhdanh.madonvi).ToList();
                grc_thongketocao.DataSource = list;
            }
        }

        private void Date_tungay_EditValueChanged(object sender, EventArgs e)
        {
            Fun_load();
        }

        private void Date_denngay_EditValueChanged(object sender, EventArgs e)
        {
            Fun_load();
        }
    }

}
