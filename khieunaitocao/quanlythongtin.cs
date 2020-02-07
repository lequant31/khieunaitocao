using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.LookAndFeel;
using System.Threading;
using System.Data.Linq.SqlClient;
using DevExpress.XtraCharts;
using System.Collections;
using DevExpress.XtraEditors;
using DataAndSystem;

namespace khieunaitocao
{
    public partial class Quanlythongtin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int tabPageIndex = 0;

        public Quanlythongtin()
        {
            InitializeComponent();
        }
        bool NotTabExist(string tabName)
        {
            for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            {
                if (xtraTabControl1.TabPages[i].Text == tabName)
                    return false;
            }
            return true;
        }
        khieunaitocaoContextDataContext _khieunaitocaoContext;
        int TabPageIndex(string tabName)
        {
            for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            {
                if (xtraTabControl1.TabPages[i].Text == tabName)
                    return i;
            }
            return -1;
        }
        public void LoadForm(UserControl ctl, string tabName)
        {
            try
            {
                if (NotTabExist(tabName))
                {
                    ctl.Dock = DockStyle.Fill;
                    using (DevExpress.XtraTab.XtraTabPage xtra = new DevExpress.XtraTab.XtraTabPage())
                    {
                        xtra.Text = tabName;
                        xtra.Controls.Add(ctl);
                        xtra.Padding = new System.Windows.Forms.Padding(2);
                        xtraTabControl1.TabPages.Add(xtra);
                    }
                }

                tabPageIndex = TabPageIndex(tabName);
                if (tabPageIndex == -1)
                    xtraTabControl1.SelectedTabPageIndex = 0;
                else
                {
                    try
                    {
                        xtraTabControl1.SelectedTabPageIndex = tabPageIndex;
                    }
                    catch
                    {
                        XtraMessageBox.Show("Có lỗi xảy ra");
                    }
                }

            }
            catch
            {
                XtraMessageBox.Show("Có lỗi xảy ra");
            }

        }
        private void XtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
             try
            {
                XtraTabPage xtra = (XtraTabPage)(e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs).Page;
                if (xtra.Name != "pageMain")
                {
                    xtraTabControl1.TabPages.Remove(xtra);
                    xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
                    foreach (Control ctl in xtra.Controls)
                    {
                        ctl.Dispose();
                    }
                    xtra.Dispose();
                }
            }
            catch { XtraMessageBox.Show("Có lỗi xảy ra"); }
        }

        private void Bar_giaiquyetkhieunai_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Ctl_quatrinhgiaiquyetkhieunai(),"Quá trình giải quyết khiếu nại");
        }

        private void Bar_thongtintocao_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Ctlthongtintocao(),"Thông tin đơn thư tố cáo");
        }

        private void Bar_quatrinhgiaiquyettocao_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new ctlquatrinhgiaiquyettocao(), "Quá trình giải quyết tố cáo");
        }

        private void Bar_thongtincanbo_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Ctl_quanlycanbo(), "Thông tin cán bộ");
        }

        private void Btn_thaydoimatkhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            form_thaydoimatkhau f = new form_thaydoimatkhau();
            f.ShowDialog();
        }

        private void Quanlythongtin_Load(object sender, EventArgs e)
        {
            UserLookAndFeel.Default.SkinName = Properties.Settings.Default.skin;
            barStaticItem1.Caption = "Người dùng: " + Dinhdanh.tencanbo;
            barStaticItem2.Caption = "Số hiệu CAND: "+Dinhdanh.sohieu_cand;
        }

        private void Quanlythongtin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.skin = UserLookAndFeel.Default.SkinName;
            Properties.Settings.Default.Save();
        }

        private void Bar_thongtindonthu_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Ctlmanagedonthu(), "Thông tin đơn thư khiếu nại");
        }

        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        delegate void Updateprocess();

        void LoadData()
        {
           
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                try
                {
                    var series1 = chartControl1.Series[0];
                    series1.Points.Clear();
                    var _result = _khieunaitocaoContext.thongke_giaiquyetkhieunai(date_tungay.DateTime,date_denngay.DateTime,Dinhdanh.madonvi).ToList();
                    foreach (var item in _result)
                    {
                        using (var point = new SeriesPoint())
                        {
                            point.Argument = item.statuss;
                            point.Tag = item.statuss;
                            point.Values = new double[] { Convert.ToDouble(item.soluong) };
                            series1.Points.Add(point);
                        }
                        series1.Label.TextPattern = "{A}: {VP:p0}";
                    }


                }
                catch
                {
                    XtraMessageBox.Show("Có lỗi xảy ra");
                }
            }
            
        }
        void LoadData1()
        {

            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                try
                {
                  
                    var series2 = chartControl2.Series[0];
                    series2.Points.Clear();
                    var _result2 = _khieunaitocaoContext.thongke_giaiquyettocao(date_tungay_tocao.DateTime, date_denngay_tocao.DateTime, Dinhdanh.madonvi).ToList();
                    foreach (var item in _result2)
                    {
                        using (var point = new SeriesPoint())
                        {
                            point.Argument = item.statuss;
                            point.Tag = item.statuss;
                            point.Values = new double[] { Convert.ToDouble(item.soluong) };
                            series2.Points.Add(point);
                        }
                        series2.Label.TextPattern = "{A}: {VP:p0}";
                    }
                }
                catch { XtraMessageBox.Show("Có lỗi xảy ra"); }
            }

        }
        void LoadData3()
        {
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                try
                {
                    List<BieuDo> bieudo = new List<BieuDo>();
                    var db = new BieuDo();
                    var _bieudo1 = _khieunaitocaoContext.bieudo_cot_donthukhieunai(Dinhdanh.madonvi,Int16.Parse(cmb_nam_khieunai.Text)).ToList();
                    foreach (var item in _bieudo1)
                    {
                        db.Thang = item.thang;
                        db.Tong = item.tong;
                        bieudo.Add(db);
                    }
                    for (int i = 1; i < 13; i++)
                    {
                        if (_bieudo1.Count(p => p.thang == i) == 0)
                        {
                            db.Thang = i;
                            db.Tong = 0;
                            bieudo.Add(db);
                        }

                    }
                    bieudocot_khieunai.DataSource = bieudo.ToList();
                }
                catch {
                    XtraMessageBox.Show("Có lỗi xảy ra");
                }
            }
        }
        void LoadData4() {
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                try
                {
                    List<BieuDo4> bieudo = new List<BieuDo4>();
                    var db = new BieuDo4();
                    var _bieudo4 = _khieunaitocaoContext.bieudo_cot_donthutocao(Dinhdanh.madonvi, Int16.Parse(cmb_nam_khieunai.Text)).ToList();
                    foreach (var item in _bieudo4)
                    {
                        db.Thang = item.thang;
                        db.Tong = item.tong;
                        bieudo.Add(db);
                    }
                    for (int i = 1; i < 13; i++)
                    {
                        if (_bieudo4.Count(p => p.thang == i) == 0)
                        {
                            db.Thang = i;
                            db.Tong = 0;
                            bieudo.Add(db);
                        }

                    }
                    bieudocot_tocao.DataSource = bieudo.ToList();
                }
                catch
                {
                    XtraMessageBox.Show("Có lỗi xảy ra");
                }
            }
        }
        void Process1()
        {
            this.BeginInvoke(new Updateprocess(LoadData));
        }
        void Process2()
        {
            this.BeginInvoke(new Updateprocess(LoadData1));
        }
        void Process3()
        {
            this.BeginInvoke(new Updateprocess(LoadData3));
        }
        void Process4()
        {
            this.BeginInvoke(new Updateprocess(LoadData4));
        }
        void BaoCaoLoad()
        {
            thread1 = new Thread(new ThreadStart(Process1));
            thread2 = new Thread(new ThreadStart(Process2));
            thread3 = new Thread(new ThreadStart(Process3));
            thread4 = new Thread(new ThreadStart(Process4));
            thread1.IsBackground = true;
            thread2.IsBackground = true;
            thread3.IsBackground = true;
            thread4.IsBackground = true;
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
        }
        struct BieuDo
        {
            public int? Tong { get; set; }
            public int? Thang { get; set; }
        }
        struct BieuDo4
        {
            public int? Tong { get; set; }
            public int? Thang { get; set; }
        }
        private void Date_tungay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Date_denngay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Date_tungay_tocao_EditValueChanged(object sender, EventArgs e)
        {
            LoadData1();
        }
        private void Date_denngay_tocao_EditValueChanged(object sender, EventArgs e)
        {
            LoadData1();
        }
        private void Bar_thongke_khieunai_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Ctl_thongkedonthu(), "Thống kê đơn thư khiếu nại");
        }
        private void Cmb_nam_khieunai_EditValueChanged(object sender, EventArgs e)
        {
            LoadData3();
        }
        private void Cmb_chonnam_tocao_EditValueChanged(object sender, EventArgs e)
        {
            LoadData4();
        }
        private void Bar_thongketocao_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new ctl_thongketocao(), "Thống kê đơn thư tố cáo");
        }
    }
}