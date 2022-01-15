﻿using BUS;
using DevComponents.DotNetBar;
using DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyHocSinh
{
    public partial class frmHocSinh : Office2007Form
    {
        public frmHocSinh()
        {
            InitializeComponent();
        }

        private void frmHocSinh_Load(object sender, EventArgs e)
        {
          

            bindingNavigatorRefreshItem_Click(sender, e);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.RowCount == 0) bindingNavigatorDeleteItem.Enabled = true;

            BindingSource bindingSource = bindingNavigatorHocSinh.BindingSource;
            DataTable dataTable = (DataTable)bindingSource.DataSource;
            DataRow dataRow = dataTable.NewRow();

            string stt = Utilities.LaySTT(dgvHocSinh.Rows.Count + 1);
            dataRow["MaHocSinh"] = "HS" + stt;
            dataRow["HoTen"] = "";
            dataRow["GioiTinh"] = false;
            dataRow["NgaySinh"] = DateTime.Today;
            dataRow["DiaChi"] = "";
            //dataRow["MaDanToc"] = "";
            //dataRow["MaTonGiao"] = "";
            //dataRow["HoTenCha"] = "";
            //dataRow["MaNNghiepCha"] = "";
            //dataRow["HoTenMe"] = "";
            //dataRow["MaNNghiepMe"] = "";
            //dataRow["Email"] = "";

            dataTable.Rows.Add(dataRow);
            bindingSource.MoveLast();
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            HocSinhBUS.Instance.HienThi(
                bindingNavigatorHocSinh,
                dgvHocSinh,
                txtMaHocSinh,
                txtTenHocSinh,
                txtGioiTinh,
                ckbGTinhNam,
                ckbGTinhNu,
                dtpNgaySinh,
                txtDiaChi
              
            );
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.RowCount == 0) bindingNavigatorDeleteItem.Enabled = false;
            else if (
                MessageBox.Show(
                    "Bạn có chắc chắn xóa dòng này không ?",
                    "Xóa lớp học",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                ) == DialogResult.OK
            ) bindingNavigatorHocSinh.BindingSource.RemoveCurrent();
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            string[] colNames = { 
                "colMaHocSinh", 
                "colHoTen",
                "colDiaChi",
                //"colMaDanToc",
                //"colMaTonGiao",
                //"colHoTenCha",
                //"colMaNNghiepCha",
                //"colHoTenMe",
                //"colMaNNghiepMe",
                //"colEmail",
            };
            if (KiemTraTruocKhiLuu.KiemTraDataGridView(dgvHocSinh, colNames) &&
                KiemTraTruocKhiLuu.KiemTraDoTuoi(dgvHocSinh, "colNgaySinh"))
            {
                bindingNavigatorPositionItem.Focus();
                BindingSource bindingSource = bindingNavigatorHocSinh.BindingSource;
                HocSinhBUS.Instance.CapNhatHocSinh((DataTable)bindingSource.DataSource);

                MessageBox.Show(
                    "Dữ liệu đã được lưu vào CSDL",
                    "Cập nhật thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void btnLuuVaoDS_Click(object sender, EventArgs e)
        {
            bool gioiTinh = false;
            if (ckbGTinhNu.Checked) gioiTinh = true;

            if (string.IsNullOrWhiteSpace(txtMaHocSinh.Text) ||
                string.IsNullOrWhiteSpace(txtTenHocSinh.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                
                dtpNgaySinh.Value == null
                )
                MessageBox.Show(
                    "Giá trị của các ô không được rỗng !",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            else
            {
                if (QuyDinhBUS.Instance.KiemTraDoTuoi(dtpNgaySinh.Value))
                {
                    HocSinhDTO hocSinh = new HocSinhDTO(
                        txtMaHocSinh.Text,
                        txtTenHocSinh.Text,
                        gioiTinh,
                        dtpNgaySinh.Value,
                        txtDiaChi.Text
                      
                    );
                    HocSinhBUS.Instance.ThemHocSinh(hocSinh);
                    bindingNavigatorRefreshItem_Click(sender, e);
                }
                else MessageBox.Show(
                    $"Tuổi của học sinh {txtTenHocSinh.Text} không hợp lệ !",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            } 
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (chkTimTheoMa.Checked) HocSinhBUS.Instance.TimTheoMa(txtTimKiem.Text);
            else HocSinhBUS.Instance.TimTheoTen(txtTimKiem.Text);
        }
    }
}
