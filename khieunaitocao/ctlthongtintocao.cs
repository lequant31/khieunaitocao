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
    public partial class Ctlthongtintocao : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctlthongtintocao()
        {
            InitializeComponent();
        }
        khieunaitocaoContextDataContext _khieunaitocaoContext;
        private void Tocao_load()
        {
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                var _list = _khieunaitocaoContext.list_thongtintocao_linq(Dinhdanh.madonvi).ToList();

                grd_thongtintocao.DataSource = _list;                
            }

        }
        private void Bar_them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_thongtintocao f = new Form_thongtintocao();
            f.ShowDialog();
        }

        private void Ctlthongtintocao_Load(object sender, EventArgs e)
        {
            Tocao_load();
        }

        private void Bar_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Tocao_load();
        }

        private void Bar_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int a = (int)grv_thongtintocao.GetFocusedRowCellValue("id_thongtintocao1");
            #region check permission
            if (Dinhdanh.quyenhan == 2)
            {
                XtraMessageBox.Show("Tài khoản chỉ có quyền xem.\n Không được phép xóa");
                return;
            }
            using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
            {
                var _edit = _khieunaitocaoContext.check_xoatocao(a).ToList();
                if (_edit.Count() != 0)
                {
                    XtraMessageBox.Show("Không được xóa đơn thư này");
                    return;
                }
            }
            #endregion
            var indexs = grv_thongtintocao.GetSelectedRows();
            if (indexs.Length < 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa");
                return;
            }
            try
            {
                using (_khieunaitocaoContext = new khieunaitocaoContextDataContext())
                {
                    var del = _khieunaitocaoContext.tb_thongtintocaos.Single(p => p.id_thongtintocao1 == a);
                    if (MessageBox.Show("Bạn có muốn xóa thông tin?", "Confirmation", MessageBoxButtons.YesNo) !=
                     DialogResult.Yes)
                        return;
                    _khieunaitocaoContext.tb_thongtintocaos.DeleteOnSubmit(del);
                    _khieunaitocaoContext.SubmitChanges();
                    grv_thongtintocao.DeleteSelectedRows();
                    MessageBox.Show("Xóa thông tin thành công");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa thông tin thất bại");
                //throw;
            }
        }

        private void Bar_sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_thongtintocao f = new Form_thongtintocao
            {
                bool_sua = true
            };
            try
            {
                var y =grv_thongtintocao.GetFocusedRowCellValue("id_thongtintocao1");
                int i = (int)grv_thongtintocao.GetFocusedRowCellValue("id_thongtintocao1");
                f.id_thongtintocao = i;
                f.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn thư cần sửa");
            }
        }

        private void Grv_thongtintocao_DoubleClick(object sender, EventArgs e)
        {
            Form_thongtintocao f = new Form_thongtintocao
            {
                bool_sua = true
            };
            try
            {
                int i = (int)grv_thongtintocao.GetFocusedRowCellValue("id_thongtintocao1");
                f.id_thongtintocao = i;
                f.ShowDialog();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn thư cần sửa");
            }
        }
    }
}
