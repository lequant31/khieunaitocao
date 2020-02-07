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

namespace DataAndSystem
{
    public partial class Ctl_quanlycanbo : DevExpress.XtraEditors.XtraUserControl
    {
        khieunaitocaoEntities _khieunaitocaoEntities;
        public Ctl_quanlycanbo()
        {
            InitializeComponent();
        }
        private void Fun_reload()
        {
            _khieunaitocaoEntities = new khieunaitocaoEntities();
            //
            if (Dinhdanh.quyenhan == 0)
            {
                var thongtin = _khieunaitocaoEntities.xemcanbo().ToList();
                grd_thongtincanbo.DataSource = thongtin;
            }
            else
            {
                var thongtin = _khieunaitocaoEntities.list_thongtincanbo_linq(Dinhdanh.madonvi).ToList();
                grd_thongtincanbo.DataSource = thongtin;
            }

        }

        private void Bar_reload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Fun_reload();
        }

        private void Ctl_quanlycanbo_Load(object sender, EventArgs e)
        {
            Fun_reload();
        }

        private void Bar_suacanbo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_quanlycanbo f = new Form_quanlycanbo
            {
                bool_sua = true
            };
            try
            {
                int i = (int)grv_thongtincanbo.GetFocusedRowCellValue("ma_canbo");
                f._macanbo = i;
                f.ShowDialog();                
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn thông tin cần sửa");
            }
           
        }

        private void Bar_xoacanbo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (_khieunaitocaoEntities = new khieunaitocaoEntities())
                {
                    int i = (int)grv_thongtincanbo.GetFocusedRowCellValue("ma_canbo");
                    _khieunaitocaoEntities.xoacanbo(i);
                    Fun_reload();
                } 
               
                XtraMessageBox.Show("Xóa thông tin thành công");
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn thông tin cần xóa");
            }
        }

        private void Grv_thongtincanbo_DoubleClick(object sender, EventArgs e)
        {
            Form_quanlycanbo f = new Form_quanlycanbo
            {
                bool_sua = true
            };
            try
            {
                int i = (int)grv_thongtincanbo.GetFocusedRowCellValue("ma_canbo");
                f._macanbo = i;
                f.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn thông tin cần sửa");
            }
        }

        private void Bar_themcanbo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_quanlycanbo f = new Form_quanlycanbo();
            f.ShowDialog();
        }
    }
}
