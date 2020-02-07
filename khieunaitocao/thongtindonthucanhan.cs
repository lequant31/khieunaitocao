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

namespace khieunaitocao
{
    public partial class Thongtindonthucanhan : DevExpress.XtraEditors.XtraForm
    {
        public Thongtindonthucanhan()
        {
            InitializeComponent();
        }
        public string Madonthu { get; set; }
        public bool bool_sua = false;
        public int id_thongtinKN;
        public int id_quatrinhgiaiquyet;
        public bool bool_chuyen = false;
        public string ngaysua = "";
        khieunaitocaoContextDataContext _khieunaitocaoContext = new khieunaitocaoContextDataContext();
        tb_thongtinkhieunai objKN;
        public int? _madonvinhan;

        private void Ma_donthu()
        {
            var _stt = _khieunaitocaoContext.stt_donthukhieunai_linq();
            if (_stt == 0)
            {
                txt_madonthu.Text = Dinhdanh.kyhieu_donvi + "00" + (_stt+1);
            }
            if (_stt > 0 && _stt <10)
            {
                txt_madonthu.Text = Dinhdanh.kyhieu_donvi + "00" + (_stt + 1);
            }
            if (_stt >= 10 && _stt <100)
            {
                txt_madonthu.Text = Dinhdanh.kyhieu_donvi + "0" + (_stt + 1);
            }
            if (_stt >= 100)
            {
                txt_madonthu.Text = Dinhdanh.kyhieu_donvi + "" + (_stt + 1);
            }
        }
        private void Thongtindonthucanhan_Load(object sender, EventArgs e)
        {
            //#region check permission
            //if (Dinhdanh.quyenhan == 2)
            //{
            //    bar_chuyendonvi.Visibility == false;
            //}
            //if (bool_chuyen == false)
            //{
            //    bar3.Visible = false;
            //}
            //if (bool_chuyen == true)
            //{
            //    bar2.Visible = false;
            //}
            //#endregion
            var list = _khieunaitocaoContext.list_phanloai_khieunai();
                treelook_phanloai_khieunai.Properties.DataSource = list;
                treelook_phanloai_khieunai.Properties.DisplayMember = "phanloai_khieunai";
                treelook_phanloai_khieunai.Properties.ValueMember = "ma_khieunai";
           
            if (bool_sua == false && bool_chuyen == false)
            {
                txt_madonthu.Text = Dinhdanh.kyhieu_donvi;
                Thongtin_addnew();
            }
            if (bool_sua == true && bool_chuyen == false)
            {
                Thongtin_edit();
            }
            if (bool_chuyen == true)
            {
 
                Thongtin_addnew();
                Thongtin_edit();
                //fun_chuyen();
            }
        }
        private void Thongtin_load()
        {
            /////////////////////////////////////////
            objKN = new tb_thongtinkhieunai();
            bool_sua = false;
            bool_chuyen = false;
            txt_madonthu.EditValue = Dinhdanh.kyhieu_donvi;
            memo_tomtatnoidung.EditValue = null;
            btn_dinhkem.Text = "Tài liệu đính kèm";
            rdb_canhan.SelectedIndex=0;
            radioGroup_hinhthuc.SelectedIndex = 0;
            txt_hoten_canhan.Text = null;
            txt_tencoquan_tochuc.Text = null;
            txt_sdt_canhan.Text = null;
            txt_sdt_tochuc.Text = null;
            txt_emailcanhan.Text = null;
            txt_email_tochuc.Text = null;
            txt_diachi_canhan.Text = null;
            txt_diachi_tochuc.Text = null;
            txt_coquan_lamviec.Text = null;
            txt_nguokytrongdon.Text = null;
            txt_noicap_cmnd.Text = null;
            txt_socmnd_canhan.Text = null;
            txt_ngaycap_cmnd.EditValue =null;
            txt_coquan_lamviec.Text = null;
            rdb_chuky.SelectedIndex = 0;
            com_loaidon.Text = "Loại đơn";
            rdb_dieukienxuly.SelectedIndex = 0;
            rdb_tailieukemtheo.SelectedIndex = 0;
            rdb_tinhchatvuviec.SelectedIndex = 0;
            treelook_phanloai_khieunai.EditValue = null;
            rdb_lienquandennhieu_cand.SelectedIndex = 0;
            rdb_khieunaicotocao.SelectedIndex = 0;
            com_lydokhongdudkxl.Text = "Lý do không đủ ĐKXL";
            grc_bidon.DataSource = null;
            grc_nhatkyguidon.DataSource = null;
            grc_bidon.DataSource = objKN.tb_bidons;
            grc_nhatkyguidon.DataSource = objKN.tb_nhatky_guidons;            
        }
        private void Thongtin_addnew()
        {
                objKN = new tb_thongtinkhieunai();
                grc_bidon.DataSource = objKN.tb_bidons;
                grc_nhatkyguidon.DataSource = objKN.tb_nhatky_guidons;
        }
        private void Thongtin_edit()
        {
                var _list_thongtindonthu = _khieunaitocaoContext.xem_thongtindonthu_gopbang_linq(id_thongtinKN).SingleOrDefault();
                txt_madonthu.Text = _list_thongtindonthu.ma_donthu_khieunai;
                rdb_canhan.EditValue = _list_thongtindonthu.tochuc_canhan;
                if (Convert.ToInt32(rdb_canhan.EditValue) == 0)
                {
                    txt_hoten_canhan.EditValue = _list_thongtindonthu.ten_canhan_tochuc;
                    txt_sdt_canhan.EditValue = _list_thongtindonthu.sdt;
                    txt_emailcanhan.EditValue = _list_thongtindonthu.email;
                    txt_socmnd_canhan.EditValue = _list_thongtindonthu.so_cmnd;
                    txt_ngaycap_cmnd.EditValue = _list_thongtindonthu.ngaycap_cmnd;
                    txt_noicap_cmnd.EditValue = _list_thongtindonthu.noicap_cmnd;
                    txt_diachi_canhan.EditValue = _list_thongtindonthu.dia_chi;
                    txt_coquan_lamviec.EditValue = _list_thongtindonthu.ten_cqdv_canhan;
                }
                if (Convert.ToInt32(rdb_canhan.EditValue) == 1)
                {
                    txt_tencoquan_tochuc.EditValue = _list_thongtindonthu.ten_canhan_tochuc;
                    txt_diachi_tochuc.EditValue = _list_thongtindonthu.dia_chi;
                    txt_nguokytrongdon.EditValue = _list_thongtindonthu.nguoi_ky_trong_don;
                    txt_sdt_tochuc.EditValue = _list_thongtindonthu.sdt;
                    txt_email_tochuc.EditValue = _list_thongtindonthu.email;
                }
                radioGroup_hinhthuc.EditValue = _list_thongtindonthu.nacdanh_codanh;
                rdb_chuky.EditValue = _list_thongtindonthu.chuky_nhieunguoi_motnguoi;
                com_loaidon.EditValue = _list_thongtindonthu.loai_don;
                memo_tomtatnoidung.EditValue = _list_thongtindonthu.tomtat_noidung;
                memo_ghichu.EditValue = _list_thongtindonthu.ghi_chu;
                rdb_dieukienxuly.EditValue = _list_thongtindonthu.dieukien_xuly_du_hoackhong;
                rdb_tailieukemtheo.EditValue = _list_thongtindonthu.giayto_tailieugoc_kemtheo;
                rdb_tinhchatvuviec.EditValue = _list_thongtindonthu.tinhchat_vuviec_phuctap_dongian;
                treelook_phanloai_khieunai.EditValue = _list_thongtindonthu.ma_khieunai;
                rdb_lienquandennhieu_cand.EditValue = _list_thongtindonthu.khieunai_lienquanden_thamquyen_nhieucand_co_khong;
                rdb_khieunaicotocao.EditValue = _list_thongtindonthu.khieunai_conoidung_tocao;
                com_lydokhongdudkxl.EditValue = _list_thongtindonthu.lydo_khongdu_dieukien;
                combo_noiduocguiden.EditValue = _list_thongtindonthu.noigui;
                btn_dinhkem.EditValue = _list_thongtindonthu.tailieu_dinhkem;
                ngaysua = _list_thongtindonthu.ngaygio_sua + "\n" + DateTime.Now.ToString();

                objKN = _khieunaitocaoContext.tb_thongtinkhieunais.Single(p => p.id_thongtinhieunai == id_thongtinKN);
                grc_bidon.DataSource = objKN.tb_bidons;
                grc_nhatkyguidon.DataSource = objKN.tb_nhatky_guidons;
                grv_bidon.RefreshData();
                grv_nhatkyguidon.RefreshData();            
               
        }
        public void Fun_chuyen()
        {
            try
            {
               // _khieunaitocaoContext = new khieunaitocaoContextDataContext();
                //objKN = new tb_thongtinkhieunai();
                //grc_bidon.DataSource = objKN.tb_bidons;
                //grc_nhatkyguidon.DataSource = objKN.tb_nhatky_guidons;
                var _list_thongtindonthu = _khieunaitocaoContext._thongtinkhieunai_canchuyen(id_thongtinKN).SingleOrDefault();
                    txt_madonthu.Text = _list_thongtindonthu.ma_donthu_khieunai;
                    rdb_canhan.EditValue = _list_thongtindonthu.tochuc_canhan;
                    if (Convert.ToInt32(rdb_canhan.EditValue) == 0)
                    {
                        txt_hoten_canhan.EditValue = _list_thongtindonthu.ten_canhan_tochuc;
                        txt_sdt_canhan.EditValue = _list_thongtindonthu.sdt;
                        txt_emailcanhan.EditValue = _list_thongtindonthu.email;
                        txt_socmnd_canhan.EditValue = _list_thongtindonthu.so_cmnd;
                        txt_ngaycap_cmnd.EditValue = _list_thongtindonthu.ngaycap_cmnd;
                        txt_noicap_cmnd.EditValue = _list_thongtindonthu.noicap_cmnd;
                        txt_diachi_canhan.EditValue = _list_thongtindonthu.dia_chi;
                        txt_coquan_lamviec.EditValue = _list_thongtindonthu.ten_cqdv_canhan;
                    }
                    if (Convert.ToInt32(rdb_canhan.EditValue) == 1)
                    {
                        txt_tencoquan_tochuc.EditValue = _list_thongtindonthu.ten_canhan_tochuc;
                        txt_diachi_tochuc.EditValue = _list_thongtindonthu.dia_chi;
                        txt_nguokytrongdon.EditValue = _list_thongtindonthu.nguoi_ky_trong_don;
                        txt_sdt_tochuc.EditValue = _list_thongtindonthu.sdt;
                        txt_email_tochuc.EditValue = _list_thongtindonthu.email;
                    }
                    radioGroup_hinhthuc.EditValue = _list_thongtindonthu.nacdanh_codanh;
                    rdb_chuky.EditValue = _list_thongtindonthu.chuky_nhieunguoi_motnguoi;
                    com_loaidon.EditValue = _list_thongtindonthu.loai_don;
                    memo_tomtatnoidung.EditValue = _list_thongtindonthu.tomtat_noidung;
                    rdb_dieukienxuly.EditValue = _list_thongtindonthu.dieukien_xuly_du_hoackhong;
                    rdb_tailieukemtheo.EditValue = _list_thongtindonthu.giayto_tailieugoc_kemtheo;
                    rdb_tinhchatvuviec.EditValue = _list_thongtindonthu.tinhchat_vuviec_phuctap_dongian;
                    treelook_phanloai_khieunai.EditValue = _list_thongtindonthu.ma_khieunai;
                    rdb_lienquandennhieu_cand.EditValue = _list_thongtindonthu.khieunai_lienquanden_thamquyen_nhieucand_co_khong;
                    rdb_khieunaicotocao.EditValue = _list_thongtindonthu.khieunai_conoidung_tocao;
                    com_lydokhongdudkxl.EditValue = _list_thongtindonthu.lydo_khongdu_dieukien;
                    combo_noiduocguiden.EditValue = _list_thongtindonthu.noigui;
                    btn_dinhkem.EditValue = _list_thongtindonthu.tailieu_dinhkem;
                    ngaysua = _list_thongtindonthu.ngaygio_sua + "\n" + DateTime.Now.ToString();

                var objKN1 = _khieunaitocaoContext.tb_thongtinkhieunais.Single(p => p.id_thongtinhieunai == id_thongtinKN);
                grc_bidon.DataSource = objKN1.tb_bidons;
                grc_nhatkyguidon.DataSource = objKN1.tb_nhatky_guidons;

                //var _list_bidon = _khieunaitocaoContext._bidon_canchuyen(id_thongtinKN).ToList();
                //var _list_nhatky = _khieunaitocaoContext.nhatky_guidon_canchuyen(id_thongtinKN).ToList();
                //grc_bidon.DataSource = _list_bidon;
                //grc_nhatkyguidon.DataSource = _list_nhatky;
                var madonvinhan = _khieunaitocaoContext.get_donvinhan(id_quatrinhgiaiquyet).SingleOrDefault();
                    _madonvinhan = madonvinhan.donvinhan;

            }
            catch (Exception)
            {

                XtraMessageBox.Show("Chưa chọn đơn vị cần chuyển đến");
            }
           
        }
        private void Rdb_canhan_EditValueChanged(object sender, EventArgs e)
        {
           
            if (rdb_canhan.SelectedIndex==0)
            {
                xtraTab_canhan.SelectedTabPage = xtraTab_canhan.TabPages[0];
                layoutControl_canhan.Enabled = true;
                layoutControl_tochuc.Enabled = false;
            }
            if (rdb_canhan.SelectedIndex == 1)
            {
                xtraTab_canhan.SelectedTabPage = xtraTab_canhan.TabPages[1];
                layoutControl_canhan.Enabled = false;
                layoutControl_tochuc.Enabled = true;
            }
            
        }
        private void XtraTab_canhan_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTab_canhan.SelectedTabPageIndex == 0)
            {
                rdb_canhan.SelectedIndex = 0;
            }
            else
            {
                rdb_canhan.SelectedIndex = 1;
            }

        }
        private void Com_loaidon_Enter(object sender, EventArgs e)
        {
            if(com_loaidon.Text.Trim()=="Loại đơn")
            {
                com_loaidon.Text = "";
            }
        }
        private void Com_loaidon_Leave(object sender, EventArgs e)
        {
            if (com_loaidon.Text.Trim() == "")
            {
                com_loaidon.Text = "Loại đơn";
            }
        }
        private void Com_lydokhongdudkxl_Leave(object sender, EventArgs e)
        {
            if (com_lydokhongdudkxl.Text.Trim() == "")
            {
                com_lydokhongdudkxl.Text = "Lý do không đủ ĐKXL";
            }
        }
        private void Com_lydokhongdudkxl_Enter(object sender, EventArgs e)
        {
            if (com_lydokhongdudkxl.Text.Trim() == "Lý do không đủ ĐKXL")
            {
                com_lydokhongdudkxl.Text = "";
            }
        }
        private void Btn_dinhkem_Leave(object sender, EventArgs e)
        {
            if (btn_dinhkem.Text.Trim() == "")
            {
                btn_dinhkem.Text = "Tài liệu đính kèm";
            }
        }
        private void Btn_dinhkem_Enter(object sender, EventArgs e)
        {
            if (btn_dinhkem.Text.Trim() == "Tài liệu đính kèm")
            {
                btn_dinhkem.Text = "";
            }
        }
        private void Bar_btn_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                #region kiemtra
               
                if (Dinhdanh.quyenhan == 2)
                {
                    XtraMessageBox.Show("Tài khoản chỉ có quyền xem.\n Không được thay đổi");
                    return;
                }
                if (com_loaidon.Text.Trim()== "Loại đơn"|| com_loaidon.Text.Trim()==null)
                {
                    XtraMessageBox.Show("Vui lòng chọn loại đơn khiếu nại");
                    com_loaidon.Focus();
                    return;
                }
                if (rdb_canhan.SelectedIndex==0)
                {
                    if (txt_hoten_canhan.Text.Trim() == null)
                    {
                        XtraMessageBox.Show("Vui lòng nhập tên cá nhân đứng đơn");
                        txt_hoten_canhan.Focus();
                        return;
                    }
                }
                if (rdb_canhan.SelectedIndex == 1)
                {
                    if (txt_tencoquan_tochuc.Text.Trim() == null)
                    {
                        XtraMessageBox.Show("Vui lòng nhập tên tổ chức đứng đơn");
                        txt_tencoquan_tochuc.Focus();
                        return;
                    }
                }
                if(bool_sua == false)
                {
                    //var _lst = _khieunaitocaoContext.tb_thongtinkhieunais.Where(p => p.ma_donthu_khieunai == txt_madonthu.Text.Trim()).FirstOrDefault();
                    int _lst = _khieunaitocaoContext.check_madonthu_linq(Dinhdanh.madonvi, txt_madonthu.Text.Trim());
                    if (_lst == 1)
                    {
                        XtraMessageBox.Show("Mã đơn thư khiếu nại đã tồn tại");
                        txt_madonthu.Focus();
                        return;
                    }
                }
                #endregion

                if (bool_sua == true)
                {
                    objKN = _khieunaitocaoContext.tb_thongtinkhieunais.Where(a => a.ma_donthu_khieunai == txt_madonthu.Text).SingleOrDefault();
                    
                }
                //if (bool_chuyen == true)
                //{
                //    //objKN = new tb_thongtinkhieunai();
                //    objKN.ma_donvi = _madonvinhan;
                //    //var _list_bidon = _khieunaitocaoContext._bidon_canchuyen(id_thongtinKN).ToArray();
                //    //int ii = _list_bidon.Count();
                //    //foreach (var i in _list_bidon)
                //    //{
                //    //    grv_bidon.SetFocusedRowCellValue( grv_bidon.Columns["bidon_tochuc_canhan"], _list_bidon.Select(p => p.bidon_tochuc_canhan));
                //    //}
                //    //var _list_nhatky = _khieunaitocaoContext.nhatky_guidon_canchuyen(id_thongtinKN).ToList();
                    
                //}
                //else
                //{
                //    objKN.ma_donvi = Dinhdanh.madonvi;
                //}
                objKN.ma_donvi_nhapdulieu = Dinhdanh.madonvi;
                objKN.ma_donthu_khieunai = txt_madonthu.Text;
                objKN.ma_canbo_nhapdulieu = Dinhdanh.ma_canbo;                             
                objKN.tochuc_canhan = Convert.ToInt32(rdb_canhan.EditValue);
                objKN.nacdanh_codanh = Convert.ToInt32(radioGroup_hinhthuc.EditValue);
                objKN.chuky_nhieunguoi_motnguoi = Convert.ToInt32(rdb_chuky.EditValue);
                objKN.loai_don = com_loaidon.Text;
                objKN.tomtat_noidung = memo_tomtatnoidung.Text;
                objKN.ghi_chu = memo_ghichu.Text;
                objKN.dieukien_xuly_du_hoackhong = Convert.ToInt32(rdb_dieukienxuly.EditValue);
                if (Convert.ToInt32(rdb_dieukienxuly.EditValue) == 0)
                {
                    objKN.lydo_khongdu_dieukien =null;
                }
                else
                {
                    objKN.lydo_khongdu_dieukien = com_lydokhongdudkxl.Text;
                }
                
                objKN.giayto_tailieugoc_kemtheo = Convert.ToInt32(rdb_dieukienxuly.EditValue);
                objKN.tinhchat_vuviec_phuctap_dongian = Convert.ToInt32(rdb_tinhchatvuviec.EditValue);
                objKN.ma_khieunai = treelook_phanloai_khieunai.EditValue.ToString();
                objKN.khieunai_lienquanden_thamquyen_nhieucand_co_khong = Convert.ToInt32(rdb_lienquandennhieu_cand.EditValue);
                objKN.khieunai_conoidung_tocao = Convert.ToInt32(rdb_khieunaicotocao.EditValue);
                objKN.noigui = combo_noiduocguiden.Text;
                if(btn_dinhkem.Text== "Tài liệu đính kèm")
                {
                    objKN.tailieu_dinhkem = null;
                }
                else
                {
                    objKN.tailieu_dinhkem = btn_dinhkem.Text;
                }                
                objKN.ngaygio_nhap = DateTime.Now.ToString();
                objKN.ngaygio_sua =ngaysua;


                if (rdb_canhan.SelectedIndex == 0)
                {
                    objKN.ten_canhan_tochuc = txt_hoten_canhan.Text;
                    objKN.sdt = txt_sdt_canhan.Text;
                    objKN.email = txt_emailcanhan.Text;
                    objKN.so_cmnd = txt_socmnd_canhan.Text;
                    objKN.ngaycap_cmnd = (DateTime?)txt_ngaycap_cmnd.EditValue;
                    objKN.noicap_cmnd = txt_noicap_cmnd.Text;
                    objKN.dia_chi = txt_diachi_canhan.Text;
                    objKN.ten_cqdv_canhan = txt_coquan_lamviec.Text;
                    objKN.nguoi_ky_trong_don = null;
                }
                if (rdb_canhan.SelectedIndex == 1)
                {
                    objKN.ten_canhan_tochuc = txt_tencoquan_tochuc.Text;
                    objKN.sdt = txt_sdt_tochuc.Text;
                    objKN.email = txt_email_tochuc.Text;
                    objKN.so_cmnd = null;
                    objKN.ngaycap_cmnd = null;
                    objKN.noicap_cmnd =null;
                    objKN.dia_chi = txt_diachi_tochuc.Text;
                    objKN.ten_cqdv_canhan = null;
                    objKN.nguoi_ky_trong_don = txt_nguokytrongdon.Text;
                }
                if (bool_sua == false)
                {
                    _khieunaitocaoContext.tb_thongtinkhieunais.InsertOnSubmit(objKN);
                }               
                _khieunaitocaoContext.SubmitChanges();              
                /////////////////////////////////////////////////////////
                XtraMessageBox.Show("Đã lưu được");
            }
            catch (Exception ex)
            {

                //throw;
                XtraMessageBox.Show(ex.Message);
            }
          
        }
        private void Grv_bidon_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //var bidon_tochuc_canhan = grv_bidon.GetRowCellValue(e.RowHandle,"bidon_tochuc_canhan").ToString().Trim();
            //var hoten_canhan = grv_bidon.GetRowCellValue(e.RowHandle, "hoten_canhan").ToString().Trim();
            //var dia_chi = grv_bidon.GetRowCellValue(e.RowHandle, "dia_chi").ToString().Trim();
            //var sohieu_cand = grv_bidon.GetRowCellValue(e.RowHandle, "sohieu_cand").ToString().Trim();
            //var capbac = grv_bidon.GetRowCellValue(e.RowHandle, "capbac").ToString().Trim();
            //var chucvu = grv_bidon.GetRowCellValue(e.RowHandle, "chucvu").ToString().Trim();
            //var nghe_nghiep = grv_bidon.GetRowCellValue(e.RowHandle, "nghe_nghiep").ToString().Trim();
            //var ten_coquandonvi = grv_bidon.GetRowCellValue(e.RowHandle, "ten_coquandonvi").ToString().Trim();
            //var thuoc_lucluong = grv_bidon.GetRowCellValue(e.RowHandle, "thuoc_lucluong").ToString().Trim();
            //if (bidon_tochuc_canhan == "")
            //{
            //    e.ErrorText = "Vui lòng không để trống";
            //    e.Valid = false;
            //    return;
            //}
            //if (hoten_canhan == "")
            //{
            //    e.ErrorText = "Vui lòng nhập tên cá nhân hoặc tổ chức";
            //    e.Valid = false;
            //    return;
            //}
            //if (grv_bidon.IsNewItemRow(e.RowHandle))
            //{
            //    //_khieunaitocaoEntities.them_bidon(txt_madonthu,grv_bidon.GetRowCellValue(e.RowHandle,grv_bidon.Columns[1]).ToString());
            //}
        }
        private void Grv_bidon_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes)
                    return;
                grv_bidon.DeleteRow(grv_bidon.FocusedRowHandle);
            }
        }
        private void Repository_btn_xoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                return;
            grv_bidon.DeleteSelectedRows();
        }
        private void RepositoryItem_capbac_EditValueChanged(object sender, EventArgs e)
        {
            grv_bidon.SetFocusedRowCellValue("capbac", ((ComboBoxEdit)sender).EditValue);
        }
        private void Repository_btn_xoa_nhatky_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                DialogResult.Yes)
                return;
            grv_nhatkyguidon.DeleteSelectedRows();
        }
        private void Bar_btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Thongtin_load();
        }
        private void Bar_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region check permission
            if (Dinhdanh.quyenhan == 2)
            {
                XtraMessageBox.Show("Tài khoản chỉ có quyền xem.\n Không được phép xóa");
                return;
            }
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                var _edit = _khieunaitocaoContext.check_xoadonthu(id_thongtinKN).ToList();
                if (_edit.Count() != 0)
                {
                    XtraMessageBox.Show("Không được xóa đơn thư này");
                    return;
                }
            }
            #endregion                
            try
            {
                using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
                {
                    var del = _khieunaitocaoContext.tb_thongtinkhieunais.Single(p => p.id_thongtinhieunai ==id_thongtinKN);
                    if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                     DialogResult.Yes)
                        return;
                    _khieunaitocaoContext.tb_thongtinkhieunais.DeleteOnSubmit(del);
                    _khieunaitocaoContext.SubmitChanges();
                    MessageBox.Show("Xóa thông tin thành công");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa thông tin thất bại");
                throw;
            }
        }
        private void Bar_chuyendonvi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region check permission
            if (Dinhdanh.quyenhan == 2)
            {
                XtraMessageBox.Show("Bạn không có quyền này");
                return;
            }
            #endregion
            bool_sua = false;
            bool_chuyen = false;
            objKN.ma_donthu_khieunai = txt_madonthu.Text;
            objKN.ma_canbo_nhapdulieu = Dinhdanh.ma_canbo;
            objKN.ma_donvi_nhapdulieu = _madonvinhan;
            objKN.tochuc_canhan = Convert.ToInt32(rdb_canhan.EditValue);
            objKN.nacdanh_codanh = Convert.ToInt32(radioGroup_hinhthuc.EditValue);
            objKN.chuky_nhieunguoi_motnguoi = Convert.ToInt32(rdb_chuky.EditValue);
            objKN.loai_don = com_loaidon.Text;
            objKN.tomtat_noidung = memo_tomtatnoidung.Text;
            objKN.dieukien_xuly_du_hoackhong = Convert.ToInt32(rdb_dieukienxuly.EditValue);
            if (Convert.ToInt32(rdb_dieukienxuly.EditValue) == 0)
            {
                objKN.lydo_khongdu_dieukien = null;
            }
            else
            {
                objKN.lydo_khongdu_dieukien = com_lydokhongdudkxl.Text;
            }

            objKN.giayto_tailieugoc_kemtheo = Convert.ToInt32(rdb_dieukienxuly.EditValue);
            objKN.tinhchat_vuviec_phuctap_dongian = Convert.ToInt32(rdb_tinhchatvuviec.EditValue);
            objKN.ma_khieunai = Convert.ToString(treelook_phanloai_khieunai.EditValue);
            objKN.khieunai_lienquanden_thamquyen_nhieucand_co_khong = Convert.ToInt32(rdb_lienquandennhieu_cand.EditValue);
            objKN.khieunai_conoidung_tocao = Convert.ToInt32(rdb_khieunaicotocao.EditValue);
            objKN.noigui = combo_noiduocguiden.Text;
            if (btn_dinhkem.Text == "Tài liệu đính kèm")
            {
                objKN.tailieu_dinhkem = null;
            }
            else
            {
                objKN.tailieu_dinhkem = btn_dinhkem.Text;
            }
            objKN.ngaygio_nhap = DateTime.Now.ToString();
            objKN.ngaygio_sua = ngaysua;


            if (rdb_canhan.SelectedIndex == 0)
            {
                objKN.ten_canhan_tochuc = txt_hoten_canhan.Text;
                objKN.sdt = txt_sdt_canhan.Text;
                objKN.email = txt_emailcanhan.Text;
                objKN.so_cmnd = txt_socmnd_canhan.Text;
                objKN.ngaycap_cmnd = (DateTime?)txt_ngaycap_cmnd.EditValue;
                objKN.noicap_cmnd = txt_noicap_cmnd.Text;
                objKN.dia_chi = txt_diachi_canhan.Text;
                objKN.ten_cqdv_canhan = txt_coquan_lamviec.Text;
                objKN.nguoi_ky_trong_don = null;
            }
            if (rdb_canhan.SelectedIndex == 1)
            {
                objKN.ten_canhan_tochuc = txt_tencoquan_tochuc.Text;
                objKN.sdt = txt_sdt_tochuc.Text;
                objKN.email = txt_email_tochuc.Text;
                objKN.so_cmnd = null;
                objKN.ngaycap_cmnd = null;
                objKN.noicap_cmnd = null;
                objKN.dia_chi = txt_diachi_tochuc.Text;
                objKN.ten_cqdv_canhan = null;
                objKN.nguoi_ky_trong_don = txt_nguokytrongdon.Text;
            }
            _khieunaitocaoContext.tb_thongtinkhieunais.InsertOnSubmit(objKN);
            _khieunaitocaoContext.SubmitChanges();
            /////////////////////////////////////////////////////////
            XtraMessageBox.Show("Đã chuyển thành công");

        }
    }
}