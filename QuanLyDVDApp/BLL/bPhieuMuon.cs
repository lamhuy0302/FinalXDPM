using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bPhieuMuon
    {
        DvdRentDbDataContext data;
        ePhieuMuon a;

        //getAll
        public List<ePhieuMuon> getAllPhieuMuon()
        {
            data = new DvdRentDbDataContext();
            List<ePhieuMuon> list = new List<ePhieuMuon>();
            var temp = data.PhieuMuons;
            foreach (var item in temp)
            {
                list.Add(new ePhieuMuon(item.Id,item.IdKhachHang,DateTime.Parse(item.NgayTao.ToString())));
            }
            return list;
        }
        //get
        public ePhieuMuon getPhieuMuonByKhachHangId(int id)
        {
            data = new DvdRentDbDataContext();
            PhieuMuon item = data.PhieuMuons.Single(n => n.IdKhachHang == id);
            return new ePhieuMuon(item.Id, item.IdKhachHang, DateTime.Parse(item.NgayTao.ToString()));
        }
        public ePhieuMuon getPhieuMuon(int id)
        {
            data = new DvdRentDbDataContext();
            PhieuMuon item = data.PhieuMuons.Single(n => n.Id == id);
            return new ePhieuMuon(item.Id, item.IdKhachHang, DateTime.Parse(item.NgayTao.ToString()));
        }
        //insert
        public void insertPhieuMuon(ePhieuMuon item)
        {
            data = new DvdRentDbDataContext();
            data.PhieuMuons.InsertOnSubmit(new PhieuMuon() {
                IdKhachHang = item.IdKhach,
                NgayTao = item.NgayTao
            });
            data.SubmitChanges();
        }

    }
}
