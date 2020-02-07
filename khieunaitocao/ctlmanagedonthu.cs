﻿using System;
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
    public partial class Ctlmanagedonthu : DevExpress.XtraEditors.XtraUserControl
    {
        //private quanlythongtin ql = new quanlythongtin();
        public Ctlmanagedonthu()
        {
            InitializeComponent();
        }
        khieunaitocaoContextDataContext _khieunaitocaoContext;
        private void Donthu_load()
        {
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                var _list = _khieunaitocaoContext.list_thongtindonthukhieunai_gopbang_linq(Dinhdanh.madonvi).ToList();

                grc_quanlydonthukhieunai.DataSource = _list;
                //grv_quanlydonthukhieunai.SetFocusedRowCellValue( "ma_donthu_khieunai", 123);
            }
           
        }
        private void Bar_them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thongtindonthucanhan f = new Thongtindonthucanhan();
            f.ShowDialog();
        }

        private void Ctlmanagedonthu_Load(object sender, EventArgs e)
        {
            try
            {
                Donthu_load();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void Bar_btn_sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            try
            {
                int i = (int)grv_quanlydonthukhieunai.GetFocusedRowCellValue("id_thongtinhieunai");
                #region check edit
                using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
                {
                    var _edit=_khieunaitocaoContext.check_xoadonthu(i).ToList();
                    if (_edit.Count()!=0)
                    {
                        XtraMessageBox.Show("Không được sửa nội dung đơn thư này");
                    }
                }
                #endregion
                Thongtindonthucanhan f = new Thongtindonthucanhan
                {
                    bool_sua = true,

                    id_thongtinKN = i
                };
                f.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn thư cần sửa");
            }
        }

        private void Grv_quanlydonthukhieunai_DoubleClick(object sender, EventArgs e)
        {
            Thongtindonthucanhan f = new Thongtindonthucanhan
            {
                bool_sua = true
            };
            try
            {
                int i = (int)grv_quanlydonthukhieunai.GetFocusedRowCellValue("id_thongtinhieunai");
                f.id_thongtinKN = i;
                f.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn thư cần sửa");
            }
        }

        private void Bar_btn_refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Donthu_load();
        }
        private void Bar_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region check permission
            if (Dinhdanh.quyenhan == 2)
            {
                XtraMessageBox.Show("Tài khoản chỉ có quyền xem.\n Không được phép xóa");
                return;
            }
            int i = (int)grv_quanlydonthukhieunai.GetFocusedRowCellValue("id_thongtinhieunai");
          
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                var _edit = _khieunaitocaoContext.check_xoadonthu(i).ToList();
                if (_edit.Count() != 0)
                {
                    XtraMessageBox.Show("Không được xóa đơn thư này");
                    return;
                }
            }
            
            #endregion

            try
            {
                var indexs = grv_quanlydonthukhieunai.GetSelectedRows();
                if (indexs[0] < 0)
                {
                    MessageBox.Show("Đây là thanh tìm kiếm. Không thể xóa được");
                    return;
                }
                if (indexs.Length < 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin cần xóa");
                    return;
                }
                int a = (int)grv_quanlydonthukhieunai.GetFocusedRowCellValue("id_thongtinhieunai");
                using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
                {
                    var del = _khieunaitocaoContext.tb_thongtinkhieunais.Single(p => p.id_thongtinhieunai ==a );
                    if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                     DialogResult.Yes)
                        return;
                    _khieunaitocaoContext.tb_thongtinkhieunais.DeleteOnSubmit(del);
                    _khieunaitocaoContext.SubmitChanges();
                    grv_quanlydonthukhieunai.DeleteSelectedRows();
                    MessageBox.Show("Xóa thông tin thành công");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa thông tin thất bại");
                //throw;
            }
           
        }

        private void Bar_quatrinhgiaiquyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thongtindonthucanhan f = new Thongtindonthucanhan();
            try
            {
                int i = (int)grv_quanlydonthukhieunai.GetFocusedRowCellValue("id_thongtinhieunai");
                f.bool_chuyen = true;
                f.id_thongtinKN = i;
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
                //XtraMessageBox.Show("Vui lòng chọn đơn thư cần chuyển");
            }
        }

        private void Grv_quanlydonthukhieunai_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
