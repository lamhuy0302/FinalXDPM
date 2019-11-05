using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bKhachHang
    {
        DvdRentDbDataContext data;
        //getAll
        public List<eKhachHang> getAllKhachHang()
        {
            data = new DvdRentDbDataContext();
            List<eKhachHang> list = new List<eKhachHang>();
            var temp = data.KhachHangs;
            foreach (var item in temp)
            {
                list.Add(new eKhachHang(item.Id,item.Ten,item.DiaChi,item.Sdt,(bool)item.TrangThai));
            }
            return list;
        }
        //get
        public eKhachHang getKhachHang(int id)
        {
            data = new DvdRentDbDataContext();
            KhachHang item = data.KhachHangs.Single(n => n.Id == id);
            return new eKhachHang(item.Id, item.Ten, item.DiaChi, item.Sdt,(bool)item.TrangThai);
        }
        //insert
        public void insertKhachHang(eKhachHang item)
        {
            data = new DvdRentDbDataContext();
            data.KhachHangs.InsertOnSubmit(new KhachHang()
            {
                Ten = item.TenKhach,
                DiaChi = item.DiaChiKhach,
                Sdt = item.SdtKhach,
                TrangThai=item.TrangThai1
            });
            data.SubmitChanges();
        }
        //update
        public void updateKhachHang(eKhachHang newitem)
        {
            data = new DvdRentDbDataContext();
            KhachHang item = data.KhachHangs.Single(n => n.Id == newitem.IdKhach);
            item.Id = newitem.IdKhach;
            item.Ten = newitem.TenKhach;
            item.DiaChi = newitem.DiaChiKhach;
            item.Sdt = newitem.SdtKhach;
            item.TrangThai = newitem.TrangThai1;
            data.SubmitChanges();
        }
        //remove 
        public void removeKhachHang(int id)
        {
            data = new DvdRentDbDataContext();
            KhachHang item = data.KhachHangs.Single(n=>n.Id==id);
            item.TrangThai = false;
            data.SubmitChanges();
        }
    }
}
