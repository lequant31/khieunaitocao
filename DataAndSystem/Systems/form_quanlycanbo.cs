using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAndSystem;

namespace DataAndSystem
{
    public partial class Form_quanlycanbo : DevExpress.XtraEditors.XtraForm
    {
        public Form_quanlycanbo()
        {
            InitializeComponent();
        }
        khieunaitocaoEntities _khieunaitocaoEntities;
        //tb_canbochiensy _tbcanbochiensy = new tb_canbochiensy();
        Mahoa mh = new Mahoa();
        public bool bool_sua=false;
        public int _macanbo;
        private int quyen_han=0;
        private void Fun_null()
        {
            bool_sua = false;
            txt_hoten.EditValue = null;
            txt_tendangnhap.EditValue = null;
            txt_matkhau.EditValue = null;
            com_capbac.EditValue = null;
            com_chucvu.EditValue = null;
            radio_quyenhan.SelectedIndex = 0;
            txt_sohieuCAND.EditValue = null;

        }
        private void Fun_save()
        {
            #region check permission
            if (Dinhdanh.quyenhan != 0)
            {
                XtraMessageBox.Show("Bạn không có quyền sửa");
                return;
            }
            #endregion
            using (_khieunaitocaoEntities = new khieunaitocaoEntities())
            {
                try
                {
                    #region rangbuoc
                    if (txt_hoten.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng nhập họ tên chính xác");
                        txt_hoten.Focus();
                        return;
                    }
                    if (txt_sohieuCAND.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng nhập số hiệu CAND");
                        txt_sohieuCAND.Focus();
                        return;
                    }
                    if (txt_tendangnhap.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng nhập tên đăng nhập");
                        txt_tendangnhap.Focus();
                        return;
                    }
                    if (com_capbac.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng chọn cấp bậc");
                        com_capbac.Focus();
                        return;
                    }
                    if (com_chucvu.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng chọn chức vụ");
                        com_chucvu.Focus();
                        return;
                    }
                    if (txt_matkhau.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Vui lòng nhập mật khẩu");
                        txt_matkhau.Focus();
                        return;
                    }
                    if (radio_quyenhan.SelectedIndex != 0 && radio_quyenhan.SelectedIndex != 1)
                    {
                        XtraMessageBox.Show("Vui lòng chọn quyền hạn tài khoản");
                        radio_quyenhan.Focus();
                        return;
                    }
                    #endregion
                    var _mk = mh.EncryptString(txt_matkhau.Text, "lamgico");
                    int _madonvi = Convert.ToInt16(search_coquandonvi.EditValue);
                    if (radio_quyenhan.SelectedIndex == 0)
                    {
                        quyen_han = 2;
                    }
                    if (radio_quyenhan.SelectedIndex == 1)
                    {
                        quyen_han = 1;
                    }
                    if (bool_sua == false)
                    {
                        _khieunaitocaoEntities.them_canbo(_madonvi, Int32.Parse(txt_sohieuCAND.Text), txt_hoten.Text, txt_tendangnhap.Text, _mk, com_capbac.Text, com_chucvu.Text, quyen_han);
                    }
                    else
                    {
                        _khieunaitocaoEntities.sua_canbo(_madonvi, _macanbo, txt_sohieuCAND.Text, txt_hoten.Text, txt_tendangnhap.Text, _mk, com_capbac.Text, com_chucvu.Text, quyen_han);
                    }
                    XtraMessageBox.Show("Lưu thông tin thành công");
                }
                catch (Exception)
                {

                    XtraMessageBox.Show("Vui lòng kiểm tra lại thông tin nhập vào.\n Không để trống các trường bắt buộc phải có.");
                }
            }                   
        }
        private void Loaditems()
        {
            using (_khieunaitocaoEntities = new khieunaitocaoEntities())
            {
                try
                {
                    var look = (from f in _khieunaitocaoEntities.tb_donvi
                                select new
                                {
                                    f.ma_donvi,
                                    f.kyhieu_donvi,
                                    f.ten_donvi
                                }).ToList();
                    search_coquandonvi.Properties.DataSource = look;
                    search_coquandonvi.Properties.DisplayMember = "ten_donvi";
                    search_coquandonvi.Properties.ValueMember = "ma_donvi";
                }
                catch (Exception)
                {

                    XtraMessageBox.Show("Vui lòng kiểm tra lại kết nối mạng");
                }

            }

        }
        private void Load_sua()
        {
            using (_khieunaitocaoEntities = new khieunaitocaoEntities())
            {
                var sua = _khieunaitocaoEntities.load_sua_canbo(_macanbo).SingleOrDefault();
                Loaditems();
                search_coquandonvi.EditValue = sua.ma_donvi;
                txt_hoten.Text = sua.hoten_chiensy;
                txt_sohieuCAND.EditValue = sua.sohieu_cand;
                txt_tendangnhap.Text = sua.ten_dangnhap;
                txt_tendangnhap.ReadOnly = true;
                txt_matkhau.Text = mh.Decrypt(sua.matkhau_dangnhap, "lamgico");
                com_capbac.Text = sua.capbac;
                com_chucvu.Text = sua.chucvu;
                if (sua.quyenhan == 1)
                {
                    radio_quyenhan.SelectedIndex = 1;
                }
                if (sua.quyenhan == 2)
                {
                    radio_quyenhan.SelectedIndex = 0;
                }
            }
           
        }
        private void Form_quanlycanbo_Load(object sender, EventArgs e)
        {
            if (bool_sua == false)
            {
                Loaditems();
            }
            if (bool_sua == true)
            {
                Load_sua();
            }
        }
        private void Btn_luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? i;
            using (_khieunaitocaoEntities = new khieunaitocaoEntities())
            {
                i = _khieunaitocaoEntities.check_update_insert_lnq(Convert.ToInt16(search_coquandonvi.EditValue), txt_tendangnhap.Text);
                if (i == 0 || bool_sua==true)
                {
                    Fun_save();
                    Fun_null();
                }
                else
                {
                    XtraMessageBox.Show("Tên đăng nhập đã bị tồn tại");
                }
            }
           
           
        }
        private void Btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}