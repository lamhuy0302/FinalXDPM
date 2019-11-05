using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entities;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace DvdRentApp
{
    public partial class App : System.Windows.Forms.Form
    {
        bKhachHang bCus;
        bDvd bDVD;
        bTitle bTua;
        bPhieuMuon bRentOrder;
        bChiTietPhieuMuon bRentOrderDetail;
        bNhanVien bEmp;
        bPhieuDatTruoc bReserve;
        bChiTietPhieuDatTruoc bReserveDetail;
        bool changeFunctionTitle;
        public App()
        {
            InitializeComponent();
            KhoiTao();       
        }
        //Hàm tính số ngày trễ hạn
        public int lateDateCounting(DateTime a, DateTime b,int c)
        {
            return (a.Date - b.Date).Days-c;
        }
        //Update phí trễ
        public void updateLateFee(int idDvd,int idOrder,int fee)
        {
            bRentOrderDetail = new bChiTietPhieuMuon();
            eChiTietPhieuMuon item = new eChiTietPhieuMuon();
            item.IdDvd = idDvd;
            item.IdPhieuMuon = idOrder;
            item.PhiTre = fee;
            item.NgayTra = DateTime.Now;
            item.TinhTrang = false;
            bRentOrderDetail.updateChiTietPhieuMuon(item);
        }
        //Hàm tính giá trả
        public int feeCounting(int idDvd)
        {
            int fee = 0;
            DateTime startDate;
            int idOrder = 0;
            int soNgayTre = 0;
            bDVD = new bDvd();
            bTua = new bTitle();
            bRentOrder = new bPhieuMuon();
            bRentOrderDetail = new bChiTietPhieuMuon();
            int idTitle = bDVD.getDvd(idDvd).IdTiltle;
            int freeDay = bTua.getTitle(idTitle).ThoiGianThue;
            var list = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdDvd == idDvd && n.TinhTrang==false).ToList();
            foreach (var item in list)
            {
                idOrder = item.IdPhieuMuon;                
            }
            var listNgayTao = bRentOrder.getAllPhieuMuon().Where(n => n.IdPhieuMuon == idOrder).ToList();
            startDate = bRentOrder.getPhieuMuon(idOrder).NgayTao;
            soNgayTre = lateDateCounting(DateTime.Now, startDate, freeDay);
            if (soNgayTre > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Dvd này tồn tại phí trễ chưa thanh toán! Bạn có muốn thay toán ngay bây giờ không? :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    fee = bTua.getTitle(idTitle).GiaThue * bTua.getTitle(idTitle).ThoiGianThue + bTua.getTitle(idTitle).PhiTre * soNgayTre;

                    dgvListTra.Rows.Add(idDvd, idOrder, startDate.Date, bTua.getTitle(idTitle).PhiTre * soNgayTre);
                    updateLateFee(idDvd, idOrder, bTua.getTitle(idTitle).PhiTre * soNgayTre);
                }
                else
                {
                    fee = bTua.getTitle(idTitle).GiaThue * bTua.getTitle(idTitle).ThoiGianThue;
                    dgvListTra.Rows.Add(idDvd, idOrder, startDate.Date, 0);
                    updateLateFee(idDvd, idOrder, bTua.getTitle(idTitle).PhiTre * soNgayTre);
                }
            }
            else
            {
                fee = bTua.getTitle(idTitle).GiaThue * bTua.getTitle(idTitle).ThoiGianThue;
                dgvListTra.Rows.Add(idDvd, idOrder, startDate.Date, 0);
                updateLateFee(idDvd, idOrder, 0);
            }
            
            return fee;
        }
            

        //Giao diện và tinh chỉnh enable hoặc xoá text
        #region Giao diện và tinh chỉnh enable hoặc xoá text
        private void KhoiTao()
        {
            gbThongTinKhachHang.Visible = false;
            tabCtrlTemp.SelectedTab = tabPageHome;
            //Style tab Xuat thong tin
            tabCtrlTemp.Appearance = TabAppearance.FlatButtons;
            tabCtrlTemp.ItemSize = new Size(0, 1);
            tabCtrlTemp.SizeMode = TabSizeMode.Fixed;
            //Style tab Nhap
            tabCtrlInput.Appearance = TabAppearance.FlatButtons;
            tabCtrlInput.ItemSize = new Size(0, 1);
            tabCtrlInput.SizeMode = TabSizeMode.Fixed;
            //Style Menu
            tabCtrlMenuTong.Appearance = TabAppearance.FlatButtons;
            tabCtrlMenuTong.ItemSize = new Size(0, 1);
            tabCtrlMenuTong.SizeMode = TabSizeMode.Fixed;
        }
        private bool rangBuoc()
        {
            if (txtTenKhachHang.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng! :D");
                return false;
            }
            if (txtDiaChiKhachHang.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập địa chỉ khách hàng! :D");
                return false;
            }
            if (txtSoDienThoaiKhachHang.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập sdt khách hàng! :D");
                return false;
            }
            return true;
        }
        private void textClear()
        {
            txtDiaChiKhachHang.Text = txtSoDienThoaiKhachHang.Text = txtTenKhachHang.Text = txtTenTitle.Text = txtGiaTitle.Text = txtPhiTreTitle.Text = txtThoiGianThueTitle.Text = "";
        }
        private void enableItem(bool a)
        {
            txtDiaChiKhachHang.Enabled = txtSoDienThoaiKhachHang.Enabled = txtTenKhachHang.Enabled = a;
        }

        private void txtIdKhachSua_Enter(object sender, EventArgs e)
        {
            if (txtIdKhachSua.Text == "Nhập ID khách hàng")
            {
                txtIdKhachSua.Text = "";
                txtIdKhachSua.ForeColor = Color.LightYellow;
            }
        }



        private void txtIdKhachSua_Leave(object sender, EventArgs e)
        {
            if (txtIdKhachSua.Text == "")
            {
                txtIdKhachSua.Text = "Nhập ID khách hàng";
                txtIdKhachSua.ForeColor = Color.LightGray;
            }
        }
        #endregion

        //Các nút điều hướng ở Menu Tổng
        #region Các nút điều hướng ở Menu Tổng
        private void btnThueFunction_Click(object sender, EventArgs e)
        {
            lbNhapThongTinThue.Text = "Nhập Id khách hàng: ";
            lbDiaChiKhach.Text = lbSoKhach.Text = lbTenKhach.Text = lbMaKhachHang.Text = "";
            dgvThue.Rows.Clear();
            gbThongTinKhachHang.Visible = true;
            tabCtrlTemp.SelectedTab = tabPageThueTra;
            tabCtrlInput.SelectedTab = tabPageThue;
            tabCtrlMenuTong.SelectedTab = tabPageThueFunction;
            txtThongTinThue.Focus();
        }
        private void btnXoaKhachHangFunction_Click(object sender, EventArgs e)
        {
           
            if (lbHello.Text != "Hello!")
            {
                tabCtrlTemp.SelectedTab = tabPageDsKhachHang;
                LoadDSKhachHang();
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                MessageBox.Show("Bạn hãy NHẤP ĐÚP vào khách hàng cần xoá trong danh sách! :D");
            }
            else checkRoleFunction();
        }
        private void btnCancelFunctionThue_Click(object sender, EventArgs e)
        {
            tabCtrlTemp.SelectedTab = tabPageHome;
            gbThongTinKhachHang.Visible = false;
            tabCtrlInput.SelectedTab = tabPageLogin;
            tabCtrlMenuTong.SelectedTab = tabPageMenuTong;
        }
        private void btnTraFunction_Click(object sender, EventArgs e)
        {
            tabCtrlTemp.SelectedTab = tabPageDsTra;
            tabCtrlInput.SelectedTab = tabPageTra;
            tabCtrlMenuTong.SelectedTab = tabPageMenuTra;
            dgvListTra.Rows.Clear();
            lbTongTra.Text = "0";
            txtIdDvdTra.Clear();
            txtIdDvdTra.Focus();
        }
        private void btnThemKhachHangFunction_Click(object sender, EventArgs e)
        {
            enableItem(true);
            LoadDSKhachHang();
            textClear();
            gbThongTinKhachHang.Visible = true;
            tabCtrlTemp.SelectedTab = tabPageDsKhachHang;
            tabCtrlInput.SelectedTab = tabPageKhachHang;
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            txtTenKhachHang.Focus();
        }
        private void btnSuaKhachHangFunction_Click(object sender, EventArgs e)
        {
            txtIdKhachSua.Visible = true;
            LoadDSKhachHang();
            textClear();
            gbThongTinKhachHang.Visible = true;
            tabCtrlTemp.SelectedTab = tabPageDsKhachHang;
            tabCtrlInput.SelectedTab = tabPageKhachHang;
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            txtTenKhachHang.Focus();
        }
        private void btnBackAll_Click(object sender, EventArgs e)
        {
            txtIdKhachSua.Enabled = true;
            txtIdKhachSua.Text = "Nhập ID khách hàng";
            txtIdKhachSua.ForeColor = Color.LightGray;
            textClear();
            enableItem(false);
            txtIdKhachSua.Visible = false;
            tabCtrlTemp.SelectedTab = tabPageHome;
            gbThongTinKhachHang.Visible = false;
            tabCtrlInput.SelectedTab = tabPageLogin;
            tabCtrlMenuTong.SelectedTab = tabPageMenuTong;
        }
        private void btnThemTitleFunction_Click(object sender, EventArgs e)
        {
            
            if (lbHello.Text != "Hello!")
            {
                tabCtrlInput.SelectedTab = tabPageTitle;
                tabCtrlTemp.SelectedTab = tabPageDsTitle;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                LoadDSTitle();
                textClear();
                txtTenTitle.Focus();
            }
            else
            {
                checkRoleFunction();
            }         
        }
        private void btnMenuTiep_Click(object sender, EventArgs e)
        {
            tabCtrlMenuTong.SelectedTab = tabPageMenuTiep;
        }

        private void btnBackMenuTiep_Click(object sender, EventArgs e)
        {
            btnBackAll_Click(sender, e);
        }

        #endregion

        //Các nút truy xuất Databse
        #region Các nút truy xuất Databse
        private void btnXacNhanThongTinThue_Click(object sender, EventArgs e)
        {
            if (txtThongTinThue.Text.Length == 0)
            {
                if (lbNhapThongTinThue.Text == "Nhập Id khách hàng: ")
                {
                    MessageBox.Show("Vui lòng nhập id của khách hàng! :D");
                    txtThongTinThue.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập id của DVD! :D");
                    txtThongTinThue.Focus();
                }
            }
            else
            {

                if (lbNhapThongTinThue.Text == "Nhập Id khách hàng: ")
                {
                    int tongTre = 0;
                    string idKhach = txtThongTinThue.Text;
                    bRentOrder = new bPhieuMuon();
                    bRentOrderDetail = new bChiTietPhieuMuon();
                    var listPhieuMuon = bRentOrder.getAllPhieuMuon().Where(n=>n.IdKhach== int.Parse(txtThongTinThue.Text)).ToList();
                    foreach (var phieumuon in listPhieuMuon)
                    {
                        int a = phieumuon.IdPhieuMuon;
                        var list = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdPhieuMuon == a && n.TinhTrang == true && n.PhiTre > 0);
                        if (list.Count() > 0)
                        {

                            foreach (var item in list)
                            {
                                tongTre += item.PhiTre;
                            }
                            
                        }
                        
                    }
                    if (tongTre > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có phí trễ chưa thanh toán! Bạn có muốn thay toán ngay bây giờ không? :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DialogResult dialogResult1 = MessageBox.Show("Bạn muốn trả hết nợ hay trả từng cái? (Chọn yes để trả hết, no để đến chi tiết! :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
                            if (dialogResult1 == DialogResult.Yes)
                            {
                                foreach (var phieumuon in listPhieuMuon)
                                {
                                    int a = phieumuon.IdPhieuMuon;
                                    var list = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdPhieuMuon == a && n.TinhTrang == true && n.PhiTre > 0);
                                    if (list.Count() > 0)
                                    {

                                        foreach (var item in list)
                                        {
                                            bRentOrderDetail.updateChiTietPhieuMuon(new eChiTietPhieuMuon(item.IdPhieuMuon, item.IdDvd, 0, item.NgayTra, item.TinhTrang));
                                        }

                                    }

                                }
                                MessageBox.Show("Bạn đã hết nợ! :D");
                            }
                            else
                            {
                                btnTraTienPhatFuncTion_Click(sender, e);
                                txtKhachNo.Text = idKhach;
                                btnXacNhanNo_Click(sender, e);
                            }
                        }
                    }
                    bCus = new bKhachHang();
                    try
                    {
                        eKhachHang temp = bCus.getKhachHang(int.Parse(txtThongTinThue.Text));
                        lbMaKhachHang.Text = temp.IdKhach.ToString();
                        lbTenKhach.Text = temp.TenKhach;
                        lbDiaChiKhach.Text = temp.DiaChiKhach;
                        lbSoKhach.Text = temp.SdtKhach;
                        lbNhapThongTinThue.Text = "Nhập Id Dvd: ";
                        txtThongTinThue.Clear();
                        txtThongTinThue.Focus();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Khách hàng không tồn tại! :D");
                    }
                    
                }
                else
                {
                    bDVD = new bDvd();
                    if (bDVD.getDvd(int.Parse(txtThongTinThue.Text.ToString())).TinhTrang == "Thue")
                    {
                        MessageBox.Show("DVD đã được thuê! Vui lòng chọn DVD khác! :D");
                        txtThongTinThue.Clear();
                        txtThongTinThue.Focus();
                    }
                    else
                    {
                        try
                        {
                            bTua = new bTitle();
                            dgvThue.Rows.Add(txtThongTinThue.Text,
                                             bTua.getTitle(bDVD.getDvd(int.Parse(txtThongTinThue.Text)).IdTiltle).TenTitle,
                                             bTua.getTitle(bDVD.getDvd(int.Parse(txtThongTinThue.Text)).IdTiltle).GiaThue);
                            txtThongTinThue.Clear();
                            txtThongTinThue.Focus();
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Dvd không tồn tại! :D");
                        }
                        
                    }
                }

            }
        }
        private void btnThue_Click(object sender, EventArgs e)
        {
            if (dgvThue.RowCount == 0)
            {
                MessageBox.Show("Vui lòng thêm DVD! :D");
                txtThongTinThue.Focus();
                return;
            }
            bRentOrder = new bPhieuMuon();
            bRentOrder.insertPhieuMuon(new ePhieuMuon(0, int.Parse(lbMaKhachHang.Text.ToString()), DateTime.Now));
            int idPhieuMuon = bRentOrder.getAllPhieuMuon().Last().IdPhieuMuon;
            bRentOrderDetail = new bChiTietPhieuMuon();
            bDVD = new bDvd();
            eDvd newitem = new eDvd();
            foreach (DataGridViewRow dgvr in dgvThue.Rows)
            {
                newitem.IdDvd = Convert.ToInt32(dgvr.Cells[0].Value);
                newitem.IdTiltle = bDVD.getDvd(newitem.IdDvd).IdTiltle;
                newitem.TinhTrang = "Thue";
                bDVD.updateDvd(newitem);
                bRentOrderDetail.insertChiTietPhieuMuon(new eChiTietPhieuMuon(idPhieuMuon, newitem.IdDvd, 0, DateTime.Now, false));
            }
            MessageBox.Show("Bạn đã thuê thành công! :D");
            btnThueFunction_Click(sender, e);
        }
        private void btnHuyThue_Click(object sender, EventArgs e)
        {
            lbNhapThongTinThue.Text = "Nhập Id khách hàng: ";
            lbTenKhach.Text = lbDiaChiKhach.Text = lbSoKhach.Text = lbMaKhachHang.Text = "";
            dgvThue.Rows.Clear();
        }
        private void LoadDSKhachHang()
        {
            dgvListKhachHang.Rows.Clear();
            bCus = new bKhachHang();
            foreach (var item in bCus.getAllKhachHang().Where(n=>n.TrangThai1==true))
            {
                dgvListKhachHang.Rows.Add(item.IdKhach, item.TenKhach, item.DiaChiKhach, item.SdtKhach);
            }
        }
        private void LoadDSTitle()
        {
            dgvListTitle.Rows.Clear();
            bTua = new bTitle();
            foreach (var item in bTua.getAllTitle().Where(n=>n.TrangThai1==true))
            {
                dgvListTitle.Rows.Add(item.IdTitle,item.TenTitle);
            }
        }
        private void btnXacNhanThemKhachHang_Click(object sender, EventArgs e)
        {
            bCus = new bKhachHang();
            if (txtIdKhachSua.Visible == true)
            {
                if (txtIdKhachSua.Enabled == true)
                {
                    if (txtIdKhachSua.Text == "Nhập ID khách hàng")
                    {
                        MessageBox.Show("Vui lòng nhập ID khách hàng! :D");
                        return;
                    }
                    else
                    {
                        txtIdKhachSua.Enabled = false;
                        int id = int.Parse(txtIdKhachSua.Text);
                        txtTenKhachHang.Text = bCus.getKhachHang(id).TenKhach;
                        txtDiaChiKhachHang.Text = bCus.getKhachHang(id).DiaChiKhach;
                        txtSoDienThoaiKhachHang.Text = bCus.getKhachHang(id).SdtKhach;
                        txtIdKhachSua.Enabled = false;
                        enableItem(true);
                        
                        txtTenKhachHang.Focus();
                    }
                }
                else
                {
                    if(rangBuoc()==false) return;
                    bCus.updateKhachHang(new eKhachHang(int.Parse(txtIdKhachSua.Text), txtTenKhachHang.Text, txtDiaChiKhachHang.Text, txtSoDienThoaiKhachHang.Text,true));
                    LoadDSKhachHang();
                    MessageBox.Show("Sửa khách hàng thành công! :D");
                    txtIdKhachSua.Enabled = true;
                    txtIdKhachSua.Text = "Nhập ID khách hàng";
                    enableItem(false);
                }
            }
            else
            {
                if (rangBuoc() == false) return;
                bCus.insertKhachHang(new eKhachHang(0, txtTenKhachHang.Text, txtDiaChiKhachHang.Text, txtSoDienThoaiKhachHang.Text,true));
                LoadDSKhachHang();
                textClear();
                MessageBox.Show("Thêm khách hàng thành công! :D");
            }
        }
        private void dgvListKhachHang_DoubleClick(object sender, EventArgs e)
        {
            if (dgvListKhachHang.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("Bạn chưa chọn khách hàng! :D");
                return;
            }
            bCus = new bKhachHang();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá người này không :D", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int id = int.Parse(dgvListKhachHang.CurrentRow.Cells[0].Value.ToString());
                string ten = bCus.getKhachHang(int.Parse(dgvListKhachHang.CurrentRow.Cells[0].Value.ToString())).TenKhach;
                bCus.removeKhachHang(id);
                MessageBox.Show("Xoá khách hàng " + ten + " thành công! :D");
                LoadDSKhachHang();
            }
        }
        #endregion

        //Login
        #region Login
        private void checkRoleFunction()
        {
            if (lbHello.Text == "Hello!")
            {
                DialogResult dialogResult = MessageBox.Show("Bạn cần được cấp quyền Quản lý để sử dụng tính năng này! Bạn có muốn đăng nhập không? :D", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    tabCtrlInput.SelectedTab = tabPageDangNhap;
                    txtLoginId.Focus();
                }
            }
        }

   
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            bEmp = new bNhanVien();
            if (txtLoginId.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập ID! :D");
                return;
            }
            if (txtLoginPwd.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu! :D");
                return;
            }
            var item = bEmp.getAllNhanVien().Where(n => n.IdNhanVien == int.Parse(txtLoginId.Text) && n.PwdNhanVien == txtLoginPwd.Text);
            if (item.Any())
            {
                string ten = bEmp.getNhanVien(int.Parse(txtLoginId.Text)).TenNhanVien;
                lbHello.Text = "Hello! " + ten;
                MessageBox.Show("Đăng nhập thành công! Chào mừng " + ten + " đã quay trở lại! :D");
                btnBackAll_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Sai ID hoặc mật khẩu, vui lòng thử lại! :D");
                txtLoginId.Focus();
            }
        }

        private void btnHuyLogin_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn thoát khỏi trình đăng nhập không? :D", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                btnBackAll_Click(sender, e);
            }            
        }

        #endregion

        private void btnXacNhanIdTra_Click(object sender, EventArgs e)
        {
            bRentOrderDetail = new bChiTietPhieuMuon();
            int idDvd = int.Parse(txtIdDvdTra.Text);
            var list = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdDvd == idDvd && n.TinhTrang == false).ToList();
            if (list.Any())
            {
                int tong = feeCounting(idDvd);
                lbTongTra.Text = (int.Parse(lbTongTra.Text) + tong).ToString();
            }
            else
            {
                MessageBox.Show("Dvd này đã được trả trước đó, vui lòng nhập lại! :D");
            }
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            if (dgvListTra.RowCount == 0)
            {
                MessageBox.Show("Vui lòng nhập DVD cần trả! :D");
                txtIdDvdTra.Focus();
                return;
            }
            bRentOrderDetail = new bChiTietPhieuMuon();
            eChiTietPhieuMuon item = new eChiTietPhieuMuon();
            bReserve = new bPhieuDatTruoc();
            bReserveDetail = new bChiTietPhieuDatTruoc();
            foreach (DataGridViewRow dgvr in dgvListTra.Rows)
            {
                item.IdPhieuMuon = Convert.ToInt32(dgvr.Cells[1].Value);
                item.IdDvd = Convert.ToInt32(dgvr.Cells[0].Value);
                item.PhiTre = bRentOrderDetail.getChiTietPhieuMuon(item.IdPhieuMuon,item.IdDvd).PhiTre- Convert.ToInt32(dgvr.Cells[3].Value);
                item.NgayTra = DateTime.Now;
                item.TinhTrang = true;
                bDVD.updateDvd(new eDvd( item.IdDvd, bDVD.getDvd(item.IdDvd).IdTiltle, "TrenKe",true));
                bRentOrderDetail.updateChiTietPhieuMuon(item);
                int idTitle = bDVD.getDvd(item.IdDvd).IdTiltle;
                var listReserve = bReserveDetail.getAllChiTietPhieuDatTruoc().Where(n => n.IdTitle == idTitle && n.TrangThai1 == false&&n.Huy==true);
                if (listReserve.Any())
                {
                    bDVD.updateDvd(new eDvd(item.IdDvd, bDVD.getDvd(item.IdDvd).IdTiltle, "DatTruoc",true));
                    int idReserve = listReserve.First().IdPhieuDatTruoc;
                    bReserveDetail.updateChiTietPhieuDatTruoc(new eChiTietPhieuDatTruoc(idReserve, idTitle, true,true));
                }
            }
            bDVD = new bDvd();

            MessageBox.Show("Bạn đã trả thành công! :D");


            btnTraFunction_Click(sender, e);
        }

        private void btnBackTra_Click(object sender, EventArgs e)
        {
            btnBackAll_Click(sender, e);
        }

        private void btnLoginFunction_Click(object sender, EventArgs e)
        {
            tabCtrlInput.SelectedTab = tabPageDangNhap;
            txtLoginId.Focus();
        }

        private void btnXacNhanThemTitle_Click(object sender, EventArgs e)
        {
            if (txtTenTitle.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên title! :D");
                return;
            }
            if (txtPhiTreTitle.Text.Length == 0 || int.Parse(txtPhiTreTitle.Text)<=0)
            {
                MessageBox.Show("Vui lòng nhập phí trễ title! :D");
                return;
            }
            if (txtThoiGianThueTitle.Text.Length == 0 || int.Parse(txtThoiGianThueTitle.Text) <= 0)
            {
                MessageBox.Show("Vui lòng nhập thời gian thuê title! :D");
                return;
            }
            if (txtGiaTitle.Text.Length == 0 || int.Parse(txtGiaTitle.Text) <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá title! :D");
                return;
            }
            bTua = new bTitle();
            bool theLoai;
            if (rbPhim.Checked == true)
            {
                theLoai = true;
            } else theLoai = false;
            bTua.insertTitle(new eTitle(
                0,
                int.Parse(txtPhiTreTitle.Text),
                int.Parse(txtThoiGianThueTitle.Text),
                int.Parse(txtGiaTitle.Text), 
                txtTenTitle.Text, 
                theLoai,true));
            LoadDSTitle();
            MessageBox.Show("Đã thêm title thành công! :D");
        }

        private void dgvListTitle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void btnThongTinTitleFunction_Click(object sender, EventArgs e)
        {
            if (lbHello.Text != "Hello!")
            {
                changeFunctionTitle = false;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                tabCtrlInput.SelectedTab = tabPageTitleInfo;
                tabCtrlTemp.SelectedTab = tabPageDsTitle;
                LoadDSTitle();
                MessageBox.Show("Nhấp vào Title để được thông tin! :D");
            }
            else
            {
                checkRoleFunction();
            }           
        }

        private void btnThemDvdFunction_Click(object sender, EventArgs e)
        {
            if (lbHello.Text != "Hello!")
            {
                bTua = new bTitle();
                tabCtrlTemp.SelectedTab = tabPageDsDvd;
                tabCtrlInput.SelectedTab = tabPageDvd;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                cbTitle.DataSource = bTua.getAllTitle().ToList();
                cbTitle.DisplayMember = "tenTitle";
                cbTitle.ValueMember = "idTitle";
                LoadDSDvd();
            }
            else
            {
                checkRoleFunction();
            }
            
        }
        private void LoadDSDvd()
        {
            bTua = new bTitle();
            bDVD = new bDvd();
            dgvListDvd.Rows.Clear();
            var list = bTua.getAllTitle().Where(n=>n.TrangThai1==true);
            var listDvd = bDVD.getAllDvd().Where(n => n.TrangThai1 == true).ToList();
            foreach (var item in list)
            {
                int listDvdSameTitle = listDvd.Where(n => n.IdTiltle == item.IdTitle).Count() ;
                dgvListDvd.Rows.Add(item.TenTitle, listDvdSameTitle);   
            }

        }

        private void cbTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnXacNhanThemDvd_Click(object sender, EventArgs e)
        {
            if (cbTitle.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Title! :D");
                return;
            }
            if (numberDvd.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng đĩa! :D");
                return;
            }
            
            bDVD = new bDvd();
            int soLuong = int.Parse(numberDvd.Text);
            for(int i = 1; i <= soLuong; i++)
            {
                bDVD.insertDvd(new eDvd(0, int.Parse(cbTitle.SelectedValue.ToString()), "TrenKe",true));
            }
            LoadDSDvd();
            MessageBox.Show("Đã có "+soLuong+" đĩa đã thêm");
        }

        private void btnBackMenu2_Click(object sender, EventArgs e)
        {
            tabCtrlMenuTong.SelectedTab = tabPageMenuTong;
        }
        private void btnXoaTitleFunction_Click(object sender, EventArgs e)
        {
            
            if (lbHello.Text != "Hello!")
            {
                changeFunctionTitle = true;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                tabCtrlTemp.SelectedTab = tabPageDsTitle;
                LoadDSTitle();
                MessageBox.Show("Nhấp đúp vào Title cần xoá! :D");
            }
            else
            {
                checkRoleFunction();
            }
        }

        private void dgvListTitle_DoubleClick(object sender, EventArgs e)
        {
            if (changeFunctionTitle == true)
            {

                if (dgvListTitle.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("Bạn chưa chọn Title! :D");
                    return;
                }
                bTua = new bTitle();
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá Title này không :D", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int id = int.Parse(dgvListTitle.CurrentRow.Cells[0].Value.ToString());
                    string ten = bTua.getTitle(int.Parse(dgvListTitle.CurrentRow.Cells[0].Value.ToString())).TenTitle;
                    bDVD = new bDvd();
                    var listDvd = bDVD.getAllDvd().Where(n => n.IdTiltle == id);
                    foreach (var item in listDvd)
                    {
                        bDVD.removeDvd(item.IdDvd);
                    }
                    bTua.removeTitle(id);
                    LoadDSTitle();
                    MessageBox.Show("Xoá Title " + ten + " thành công! :D");
                }
            }
            else
            {
                bTua = new bTitle();
                int idTitle = int.Parse(dgvListTitle.CurrentRow.Cells[0].Value.ToString());
                lbTenTitle.Text = bTua.getTitle(idTitle).TenTitle;
                lbPhiTreTitle.Text = bTua.getTitle(idTitle).PhiTre.ToString() + " VNĐ";
                lbThoiGianThue.Text = bTua.getTitle(idTitle).ThoiGianThue.ToString() + " ngày";
                lbGiaTitle.Text = bTua.getTitle(idTitle).PhiTre.ToString() + " VNĐ";
                if (bTua.getTitle(idTitle).TheLoai == true)
                {
                    lbTheLoaiTitle.Text = "Phim";
                }
                else lbTheLoaiTitle.Text = "Game";
                bDVD = new bDvd();
                int listDvd = bDVD.getAllDvd().Where(n => n.IdTiltle == idTitle && n.TinhTrang == "TrenKe").Count();
                MessageBox.Show("Hiện đang có " + listDvd.ToString() + " sẵn sàng cho thuê! :D");
            }
        }

        private void btnXacNhanNo_Click(object sender, EventArgs e)
        {
            dgvListNo.Rows.Clear();
            dgvListNo.Columns.Clear();
            dgvListNo.Columns.Add("0", "Id Phiếu mượn");
            dgvListNo.Columns.Add("1", "Id Dvd");
            dgvListNo.Columns.Add("2", "Số tiền nợ");
            int tongTre = 0;
            bRentOrder = new bPhieuMuon();
            bRentOrderDetail = new bChiTietPhieuMuon();
            var listPhieuMuon = bRentOrder.getAllPhieuMuon().Where(n => n.IdKhach == int.Parse(txtKhachNo.Text)).ToList();
            int idOrder = 0;
            int idDvd = 0;
            foreach (var phieumuon in listPhieuMuon)
            {
                idOrder = phieumuon.IdPhieuMuon;
                var list = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdPhieuMuon == idOrder && n.TinhTrang == true && n.PhiTre > 0);
                if (list.Count() > 0)
                {

                    foreach (var item in list)
                    {
                        idDvd = item.IdDvd;
                        tongTre += item.PhiTre;                        
                        dgvListNo.Rows.Add(idOrder, idDvd, item.PhiTre);
                    }
                }               
            }
            
        }
        private void dgvListNo_DoubleClick(object sender, EventArgs e)
        {
            bRentOrderDetail = new bChiTietPhieuMuon();
            eChiTietPhieuMuon item = new eChiTietPhieuMuon();
            item.IdPhieuMuon = Convert.ToInt32(dgvListNo.CurrentRow.Cells[0].Value);
            item.IdDvd= Convert.ToInt32(dgvListNo.CurrentRow.Cells[1].Value);
            item.PhiTre = 0;
            item.NgayTra = bRentOrderDetail.getChiTietPhieuMuon(item.IdPhieuMuon, item.IdDvd).NgayTra;
            item.TinhTrang = true;
            DialogResult dialogResult1 = MessageBox.Show("Bạn muốn trả nợ đĩa"+dgvListNo.CurrentRow.Cells[0].Value.ToString()+"không? :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
            if (dialogResult1 == DialogResult.Yes)
            {
                bRentOrderDetail.updateChiTietPhieuMuon(item);
                MessageBox.Show("Bạn đã trả nợ đĩa " + item.IdDvd + "! :D");
            }
        }

        private void btnTraHet_Click(object sender, EventArgs e)
        {
            if (dgvListNo.Rows.Count == 0)
            {
                MessageBox.Show("Bạn không có nợ để trả! :D");
                return;
            }
            bRentOrderDetail = new bChiTietPhieuMuon();
            eChiTietPhieuMuon item = new eChiTietPhieuMuon();
            foreach (DataGridViewRow dgvr in dgvListNo.Rows)
            {
                item.IdPhieuMuon = Convert.ToInt32(dgvr.Cells[0].Value);
                item.IdDvd = Convert.ToInt32(dgvr.Cells[1].Value);
                item.PhiTre = 0;
                item.NgayTra = DateTime.Now;
                item.TinhTrang = true;
                bRentOrderDetail.updateChiTietPhieuMuon(item);
            }
            MessageBox.Show("Bạn đã trả mọi nợ đĩa! :D");
        }
        private void btnTraTienPhatFuncTion_Click(object sender, EventArgs e)
        {
            tabCtrlTemp.SelectedTab = tabPageDsNo;
            tabCtrlInput.SelectedTab = tabPageLate;
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            txtKhachNo.Clear();
            txtKhachNo.Focus();
        }

        private void btnThongTinDvdFunction_Click(object sender, EventArgs e)
        {
            LoadDsDvdInfo();
            tabCtrlTemp.SelectedTab = tabPageDvdInfo;
            tabCtrlInput.SelectedTab = tabPageNhapDvdInfo;
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            txtIdDvdInfo.Clear();
            txtIdDvdInfo.Focus();
        }
        private void LoadDsDvdInfo()
        {
            bDVD = new bDvd();
            bTua = new bTitle();
            dgvDvdInfo.Rows.Clear();
            foreach (var item in bDVD.getAllDvd().Where(n=>n.TrangThai1==true))
            {
                    dgvDvdInfo.Rows.Add(item.IdDvd, bTua.getTitle(item.IdTiltle).TenTitle, item.TinhTrang);
            }
        }

        private void btnXacNhanTimDvd_Click(object sender, EventArgs e)
        {
            if (txtIdDvdInfo.Text.Length == 0)
            {
                LoadDsDvdInfo();
            }
            else
            {
                int i = 0;
                foreach (DataGridViewRow item in dgvDvdInfo.Rows)
                {
                    if (dgvDvdInfo.Rows[item.Index].Cells[0].Value.ToString() == txtIdDvdInfo.Text)
                    {
                        dgvDvdInfo.Rows[item.Index].Visible = true;
                    }
                    else
                    {
                        dgvDvdInfo.Rows[item.Index].Visible = false;
                        i++;
                    }
                    
                }
                if (dgvDvdInfo.RowCount == i)
                {
                    MessageBox.Show("Dvd không tồn tại! :D");
                    LoadDsDvdInfo();
                    txtIdDvdInfo.Focus();
                }
            }
        }

        private void btnDatTruocFunction_Click(object sender, EventArgs e)
        {
            txtIdKhachDatTruoc.Clear();
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            tabCtrlInput.SelectedTab = tabPageDatTruoc;
            bTua = new bTitle();
            cbTitleDatTruoc.DataSource = bTua.getAllTitle().ToList();
            cbTitleDatTruoc.DisplayMember = "tenTitle";
            cbTitleDatTruoc.ValueMember = "idTitle";
            txtIdKhachDatTruoc.Focus();
        }

        private void btnXacNhanDatTruoc_Click(object sender, EventArgs e)
        {
            bReserve = new bPhieuDatTruoc();
            bReserveDetail = new bChiTietPhieuDatTruoc();
            bDVD = new bDvd();
            bTua = new bTitle();
            if (txtIdKhachDatTruoc.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập id khách hàng! :D");
                return;
            }
            try
            {
                bReserve.insertPhieuDatTruoc(new ePhieuDatTruoc(0, int.Parse(txtIdKhachDatTruoc.Text), DateTime.Now));
                int id = bReserve.getAllPhieuDatTruoc().Last().IdDatTruoc;
                
                int idtitle = int.Parse(cbTitleDatTruoc.SelectedValue.ToString());
                var listDvd = bDVD.getAllDvd().Where(n => n.IdTiltle == idtitle && n.TinhTrang == "TrenKe").ToList();
                
                if (listDvd.Any())
                {
                    int idDvd = listDvd.First().IdDvd;
                    MessageBox.Show(idDvd + "");
                    bDVD.updateDvd(new eDvd(idDvd, idtitle, "DatTruoc",true));
                    bReserveDetail.insertChiPhieuDatTruoc(new eChiTietPhieuDatTruoc(id, idtitle, true,true));
                    MessageBox.Show("Bạn đã đặt trước Dvd thành công!");
                }
                else
                {
                    bReserveDetail.insertChiPhieuDatTruoc(new eChiTietPhieuDatTruoc(id, idtitle, false,true));
                    MessageBox.Show("Bạn đã đặt trước Dvd thành công!");
                }
   
            }
            catch (Exception)
            {

                MessageBox.Show("Vui lòng nhập id khách hợp lệ! :D");
            }
        }

        private void dgvThue_DoubleClick(object sender, EventArgs e)
        {
            dgvThue.Rows.RemoveAt(dgvThue.CurrentRow.Index);
        }
        private void LoadDsReportCus()
        {
            dgvThongKe.Rows.Clear();
            bCus = new bKhachHang();
            bTua = new bTitle();
            bDVD = new bDvd();
            bRentOrder = new bPhieuMuon();
            bRentOrderDetail = new bChiTietPhieuMuon();
            var list = bCus.getAllKhachHang();
            foreach (var item in list)
            {
                int i = 0;
                int idKhach = item.IdKhach;
                int soLuongDiaMuon = 0;
                int soLuongDiaQuaHan = 0;
                int tongPhiTre = 0;
                var listRent = bRentOrder.getAllPhieuMuon().Where(n => n.IdKhach == idKhach);
                foreach (var itemMuon in listRent)
                {
                    int idMuon = itemMuon.IdPhieuMuon;
                    var listChiTiet = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdPhieuMuon == idMuon);
                    foreach (var itemChitiet in listChiTiet)
                    {
                        
                        int ngayFree = bTua.getTitle(bDVD.getDvd(itemChitiet.IdDvd).IdTiltle).ThoiGianThue;
                        tongPhiTre += itemChitiet.PhiTre;
                        soLuongDiaQuaHan = listChiTiet.Where(n=>lateDateCounting(n.NgayTra,itemMuon.NgayTao,ngayFree)>0).Count();
                    }
                    soLuongDiaMuon += listChiTiet.Count();
                }
                dgvThongKe.Rows.Add(bCus.getKhachHang(idKhach).TenKhach, bCus.getKhachHang(idKhach).SdtKhach, soLuongDiaMuon, soLuongDiaQuaHan, tongPhiTre);
            }
        }

        private void btnBaoCaoKhachHangFunction_Click(object sender, EventArgs e)
        {
            tabCtrlTemp.SelectedTab = tabPageThongKe;
            tabCtrlInput.SelectedTab = tabPageInputThongKeCus;
            tabCtrlMenuTong.SelectedTab = tabPageBack;
            LoadDsReportCus();
        }

        private void btnXacNhanCusReport_Click(object sender, EventArgs e)
        {
            if (rbAllCus.Checked == true)
            {
                LoadDsReportCus();
            }
            if(rbAllLateCus.Checked == true)
            {
                LoadDsReportCus();
                foreach (DataGridViewRow item in dgvThongKe.Rows)
                {
                    int index = item.Index;
                    if ((int)dgvThongKe.Rows[item.Index].Cells[3].Value > 0)
                    {
                        dgvThongKe.Rows[index].Visible = true;
                    }
                    else
                    {
                        dgvThongKe.Rows[index].Visible = false;
                    }
                }
            
                
            }
            if (rbAllFeeCus.Checked == true)
            {
                LoadDsReportCus();
                foreach (DataGridViewRow item in dgvThongKe.Rows)
                {
                    int index = item.Index;
                    if ((int)dgvThongKe.Rows[item.Index].Cells[4].Value > 0)
                    {
                        dgvThongKe.Rows[index].Visible = true;
                    }
                    else
                    {
                        dgvThongKe.Rows[index].Visible = false;
                    }
                }


            }
        }

        private void dgvThongKe_DoubleClick(object sender, EventArgs e)
        {
            dgvChiTietCusReport.Rows.Clear();
            bCus = new bKhachHang();
            bRentOrder = new bPhieuMuon();
            bRentOrderDetail = new bChiTietPhieuMuon();
           
            int idkhach = bCus.getAllKhachHang().Single(n => n.TenKhach == dgvThongKe.CurrentRow.Cells[0].Value.ToString()).IdKhach;
            var listRent = bRentOrder.getAllPhieuMuon().Where(n => n.IdKhach == idkhach);
            foreach(var item in listRent)
            {
                var listChiTiet = bRentOrderDetail.getAllChiTietPhieuMuon().Where(n => n.IdPhieuMuon == item.IdPhieuMuon);
                foreach (var itemChiTier in listChiTiet)
                {
                    dgvChiTietCusReport.Rows.Add(itemChiTier.IdDvd, item.NgayTao, itemChiTier.NgayTra, itemChiTier.PhiTre);
                }
            }
        }

        private void btnXoaDvdFunction_Click(object sender, EventArgs e)
        {
            if (lbHello.Text != "Hello!")
            {
                bTua = new bTitle();
                tabCtrlTemp.SelectedTab = tabPageDsDvd;
                tabCtrlInput.SelectedTab = tabPageXoaDvd;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                LoadDSDvd();
            }
            else
            {
                checkRoleFunction();
            }
        }

        private void btnXacNhanXoaDvd_Click(object sender, EventArgs e)
        {
            bDVD = new bDvd();
            DialogResult dialogResult1 = MessageBox.Show("Bạn có muốn xoá Dvd " + numbIdDvdXoa.Text + "không? :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
            if (dialogResult1 == DialogResult.Yes)
            {
                bDVD.removeDvd(int.Parse(numbIdDvdXoa.Text));
                LoadDSDvd();
                MessageBox.Show("Bạn đã xoá đĩa "+ numbIdDvdXoa.Text+" thành công! :D");
            }

        }

        private void btnXoaTienPhatFunction_Click(object sender, EventArgs e)
        {
            if (lbHello.Text != "Hello!")
            {
                tabCtrlTemp.SelectedTab = tabPageDsNo;
                tabCtrlInput.SelectedTab = tabPageLate;
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                txtKhachNo.Clear();
                MessageBox.Show("Nhấp đúp vào tiền phát muốn xoá! :D");
                txtKhachNo.Focus();
            }
            else
            {
                checkRoleFunction();
            }
        }

        private void btnBaoCaoTitleFunction_Click(object sender, EventArgs e)
        {
            
            if (lbHello.Text != "Hello!")
            {
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                tabCtrlTemp.SelectedTab = tabPageDsThongKeDvd;
                LoadDsThongKeTitle();
            }
            else
            {
                checkRoleFunction();
            }
        }
        private void LoadDsThongKeTitle()
        {
            bTua = new bTitle();
            bReserveDetail = new bChiTietPhieuDatTruoc();
            bDVD = new bDvd();
            dgvThongKeTitle.Rows.Clear();
            var list = bTua.getAllTitle().Where(n => n.TrangThai1 == true);
            foreach (var item in list)
            {
                string name = item.TenTitle;
                string theLoai;
                if (item.TheLoai == true)
                {
                    theLoai = "Phim";
                }
                else theLoai = "Game";
                var listDvd = bDVD.getAllDvd().Where(n => n.IdTiltle == item.IdTitle&&n.TrangThai1==true);
                int soLuongThue = listDvd.Where(n => n.TinhTrang == "Thue").Count();
                int soLuongDatTruoc = listDvd.Where(n => n.TinhTrang == "DatTruoc").Count();
                int soLuongStock = listDvd.Where(n => n.TinhTrang == "TrenKe").Count();
                int tongCopy = listDvd.Count();
                int soLuongCho = bReserveDetail.getAllChiTietPhieuDatTruoc().Where(n => n.IdTitle == item.IdTitle && n.TrangThai1 == false).Count();
                dgvThongKeTitle.Rows.Add(name, theLoai, tongCopy, soLuongThue, soLuongDatTruoc, soLuongStock, soLuongCho);
            }
        }

        private void btnHuyDatTruocFunction_Click(object sender, EventArgs e)
        {
            if (lbHello.Text != "Hello!")
            {
                tabCtrlMenuTong.SelectedTab = tabPageBack;
                tabCtrlTemp.SelectedTab = tabPageDsDatTruoc;
                tabCtrlInput.SelectedTab = tabPageInputHuyDatTruoc;
            }
            else
            {
                checkRoleFunction();
            }
        }

        private void btnXacNhanIdHuyDatTruoc_Click(object sender, EventArgs e)
        {
            dgvDsDatTruoc.Rows.Clear();
            bReserve = new bPhieuDatTruoc();
            bReserveDetail = new bChiTietPhieuDatTruoc();
            bTua = new bTitle();
            int idkhach = int.Parse(numbIdHuyDatTruoc.Text);
            var list = bReserve.getAllPhieuDatTruoc().Where(n => n.IdKhach == idkhach);
            foreach (var item in list)
            {
                var listChiTiet = bReserveDetail.getAllChiTietPhieuDatTruoc().Where(n => n.IdPhieuDatTruoc == item.IdDatTruoc && n.Huy == true);
                foreach (var itemCHiTiet in listChiTiet)
                {
                    dgvDsDatTruoc.Rows.Add(itemCHiTiet.IdPhieuDatTruoc, itemCHiTiet.IdTitle, bTua.getTitle(itemCHiTiet.IdTitle).TenTitle);
                }
            }
        }

        private void dgvDsDatTruoc_DoubleClick(object sender, EventArgs e)
        {
            bReserveDetail = new bChiTietPhieuDatTruoc();
            string name = dgvDsDatTruoc.CurrentRow.Cells[2].Value.ToString();
            DialogResult dialogResult1 = MessageBox.Show("Bạn có muốn huỷ đặt trước " + name + " không? :D", "Thông báo phí trễ", MessageBoxButtons.YesNo);
            if (dialogResult1 == DialogResult.Yes)
            {
                bReserveDetail.removeChiTietDatTruoc((int)dgvDsDatTruoc.CurrentRow.Cells[0].Value, (int)dgvDsDatTruoc.CurrentRow.Cells[1].Value);
                MessageBox.Show("Bạn đã huỷ đặt trước " + name + " thành công! :D");
                btnXacNhanIdHuyDatTruoc_Click(sender, e);
            }
        }
    }
}
