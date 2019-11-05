using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bChiTietPhieuMuon
    {
        DvdRentDbDataContext data;
        //getAll
        public List<eChiTietPhieuMuon> getAllChiTietPhieuMuon()
        {
            data = new DvdRentDbDataContext();
            List<eChiTietPhieuMuon> list = new List<eChiTietPhieuMuon>();
            var temp = data.ChiTietPhieuMuons;
            foreach (var item in temp)
            {
                list.Add(new eChiTietPhieuMuon(item.IdPhieuMuon,item.IdDVD,item.PhiTre,(DateTime)item.NgayTra,(bool)item.TinhTrang));
            }
            return list;
        }
        //getByDVDid
        public eChiTietPhieuMuon getIdByIdDVD(int id)
        {
            data = new DvdRentDbDataContext();
            ChiTietPhieuMuon item = data.ChiTietPhieuMuons.Single(n => n.IdDVD == id);
            return new eChiTietPhieuMuon(item.IdPhieuMuon, item.IdDVD, item.PhiTre, (DateTime)item.NgayTra, (bool)item.TinhTrang);
        }
        public eChiTietPhieuMuon getChiTietPhieuMuon(int idPhieuMuon,int idDvd)
        {
            data = new DvdRentDbDataContext();
            ChiTietPhieuMuon item = data.ChiTietPhieuMuons.Single(n => n.IdDVD == idDvd&&n.IdPhieuMuon==idPhieuMuon);
            return new eChiTietPhieuMuon(item.IdPhieuMuon, item.IdDVD, item.PhiTre, (DateTime)item.NgayTra, (bool)item.TinhTrang);
        }
        //insert
        public void insertChiTietPhieuMuon(eChiTietPhieuMuon item)
        {
            data = new DvdRentDbDataContext();
            data.ChiTietPhieuMuons.InsertOnSubmit(new ChiTietPhieuMuon()
            {
                IdPhieuMuon = item.IdPhieuMuon,
                IdDVD = item.IdDvd,
                PhiTre = item.PhiTre,
                NgayTra = item.NgayTra,
                TinhTrang = item.TinhTrang
            });
            data.SubmitChanges();
        }
        //update
        public void updateChiTietPhieuMuon(eChiTietPhieuMuon newitem)
        {
            data = new DvdRentDbDataContext();
            ChiTietPhieuMuon item = data.ChiTietPhieuMuons.Single(n => n.IdDVD == newitem.IdDvd && n.IdPhieuMuon == newitem.IdPhieuMuon);
            item.IdPhieuMuon = newitem.IdPhieuMuon;
            item.IdDVD = newitem.IdDvd;
            item.PhiTre = newitem.PhiTre;
            item.NgayTra = newitem.NgayTra;
            item.TinhTrang = newitem.TinhTrang;
            data.SubmitChanges();

        }
    }
}
