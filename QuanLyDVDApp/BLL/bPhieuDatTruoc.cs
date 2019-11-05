using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class bPhieuDatTruoc
    {
        DvdRentDbDataContext data;

        //getAll
        public List<ePhieuDatTruoc> getAllPhieuDatTruoc()
        {
            data = new DvdRentDbDataContext();
            List<ePhieuDatTruoc> list = new List<ePhieuDatTruoc>();
            var temp = data.PhieuDatTruocs;
            foreach (var item in temp)
            {
                list.Add(new ePhieuDatTruoc(item.Id, item.IdKhachHang, DateTime.Parse(item.NgayTao.ToString())));
            }
            return list;
        }
        //get
        public ePhieuDatTruoc getPhieuDatTruocByKhachHangId(int id)
        {
            data = new DvdRentDbDataContext();
            PhieuDatTruoc item = data.PhieuDatTruocs.Single(n => n.IdKhachHang == id);
            return new ePhieuDatTruoc(item.Id, item.IdKhachHang, DateTime.Parse(item.NgayTao.ToString()));
        }
        public ePhieuDatTruoc getPhieuDatTruoc(int id)
        {
            data = new DvdRentDbDataContext();
            PhieuDatTruoc item = data.PhieuDatTruocs.Single(n => n.Id == id);
            return new ePhieuDatTruoc(item.Id, item.IdKhachHang, DateTime.Parse(item.NgayTao.ToString()));
        }
        //insert
        public void insertPhieuDatTruoc(ePhieuDatTruoc item)
        {
            data = new DvdRentDbDataContext();
            data.PhieuDatTruocs.InsertOnSubmit(new PhieuDatTruoc()
            {
                IdKhachHang = item.IdKhach,
                NgayTao = item.NgayTao
            });
            data.SubmitChanges();
        }
        
    }
}
