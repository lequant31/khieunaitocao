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
using DevExpress.LookAndFeel;


namespace DataAndSystem
{
    public partial class Frm_dangnhap : DevExpress.XtraEditors.XtraForm
    {
        public Frm_dangnhap()
        {
            InitializeComponent();
        }
         khieunaitocaoEntities _khieunaitocaoEntities;
        private void Load_items_donvi()
        {
            try
            {
                _khieunaitocaoEntities = new khieunaitocaoEntities();
                var look = (from f in _khieunaitocaoEntities.tb_donvi
                            select new
                            {
                                f.ma_donvi,
                                f.kyhieu_donvi,
                                f.ten_donvi
                            }).ToList();
                com_tendonvi.Properties.DataSource = look;
                com_tendonvi.Properties.DisplayMember = "ten_donvi";
                com_tendonvi.Properties.ValueMember = "ma_donvi";
            }
            catch (Exception)
            {
                //throw;
                XtraMessageBox.Show("Vui lòng kiểm tra lại kết nối mạng");
            }
        }
        private void Dangnhap()
        {
            Mahoa mh = new Mahoa();
            string matkhau = mh.EncryptString(txt_matkhaudangnhap.Text,"lamgico");
            try
            {
                              
                int? status = _khieunaitocaoEntities.login_canbo_linq(Convert.ToInt16(com_tendonvi.EditValue), txt_tendangnhap.Text, matkhau);
                if (status == 1)
                {
                    
                    this.DialogResult = DialogResult.OK;
                    var _dinhdanh_canbo = _khieunaitocaoEntities.dinhdanh_canbo(Convert.ToInt16(com_tendonvi.EditValue),txt_tendangnhap.Text).SingleOrDefault();
                    Dinhdanh.madonvi = _dinhdanh_canbo.ma_donvi;
                    Dinhdanh.ma_canbo = _dinhdanh_canbo.ma_canbo;
                    Dinhdanh.sohieu_cand = _dinhdanh_canbo.sohieu_cand;
                    Dinhdanh.quyenhan = _dinhdanh_canbo.quyenhan;
                    Dinhdanh.kyhieu_donvi = _dinhdanh_canbo.kyhieu_donvi.Trim();
                    Dinhdanh.tencanbo = _dinhdanh_canbo.hoten_chiensy;
                    this.Close();
                }

                if (status == 0)
                {
                    XtraMessageBox.Show("Sai thông tin đăng nhập");
                }
            }
            catch (Exception)
            {

                XtraMessageBox.Show("Kiểm tra lại kết nối");
            }
            SaveSetting();
        }
        private void Frm_dangnhap_Load(object sender, EventArgs e)
        {
            Load_items_donvi();
            ReadingSetting();
        }
        private void Btn_dangnhap_Click(object sender, EventArgs e)
        {
            Dangnhap();
        }
        private void Btn_huydangnhap_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ReadingSetting()
        {
            UserLookAndFeel.Default.SkinName = DataAndSystem.Properties.Settings.Default.skin;
            if (DataAndSystem.Properties.Settings.Default.RememberMe == "true")
            {
                txt_tendangnhap.Text = DataAndSystem.Properties.Settings.Default.Username;
                txt_matkhaudangnhap.Text = DataAndSystem.Properties.Settings.Default.Password;
                com_tendonvi.EditValue = DataAndSystem.Properties.Settings.Default.donvi;
                check_rememberme.Checked = true;
            }
            else
            {
                txt_tendangnhap.Text = "";
                txt_matkhaudangnhap.Text = "";
                com_tendonvi.Text = "";
                check_rememberme.Checked = false;
            }
        }
        private void SaveSetting()
        {
            if (check_rememberme.Checked)
            {
                DataAndSystem.Properties.Settings.Default.Username = txt_tendangnhap.Text;
                DataAndSystem.Properties.Settings.Default.Password = txt_matkhaudangnhap.Text;
                DataAndSystem.Properties.Settings.Default.donvi = Convert.ToInt16(com_tendonvi.EditValue);
                DataAndSystem.Properties.Settings.Default.RememberMe = "true";
                DataAndSystem.Properties.Settings.Default.Save();
            }
            else
            {
                DataAndSystem.Properties.Settings.Default.Username = "";
                DataAndSystem.Properties.Settings.Default.Password = "";
                DataAndSystem.Properties.Settings.Default.donvi =0;
                DataAndSystem.Properties.Settings.Default.RememberMe = "false";
                DataAndSystem.Properties.Settings.Default.Save();
            }
        }

    }
}