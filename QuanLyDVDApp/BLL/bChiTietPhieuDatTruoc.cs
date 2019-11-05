using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bChiTietPhieuDatTruoc
    {
        DvdRentDbDataContext data;
        public List<eChiTietPhieuDatTruoc> getAllChiTietPhieuDatTruoc()
        {
            data = new DvdRentDbDataContext();
            List<eChiTietPhieuDatTruoc> list = new List<eChiTietPhieuDatTruoc>();
            var temp = data.ChiTietPhieuDatTruocs;
            foreach (var item in temp)
            {
                list.Add(new eChiTietPhieuDatTruoc(item.IdPhieuDatTruoc,item.IdTitle,item.TrangThai,(bool)item.Huy));
            }
            return list;
        }
        public void insertChiPhieuDatTruoc(eChiTietPhieuDatTruoc item)
        {
            data = new DvdRentDbDataContext();
            data.ChiTietPhieuDatTruocs.InsertOnSubmit(new ChiTietPhieuDatTruoc()
            {
                IdPhieuDatTruoc = item.IdPhieuDatTruoc,
                IdTitle = item.IdTitle,
                TrangThai = item.TrangThai1,
               Huy=item.Huy
            });
            data.SubmitChanges();
        }
        public void updateChiTietPhieuDatTruoc(eChiTietPhieuDatTruoc newitem)
        {
            data = new DvdRentDbDataContext();
            ChiTietPhieuDatTruoc item = data.ChiTietPhieuDatTruocs.Single(n => n.IdPhieuDatTruoc == newitem.IdPhieuDatTruoc && n.IdTitle == newitem.IdTitle);
            item.IdPhieuDatTruoc = newitem.IdPhieuDatTruoc;
            item.IdTitle = newitem.IdTitle;
            item.TrangThai = newitem.TrangThai1;
            item.Huy = newitem.Huy;
            data.SubmitChanges();

        }
        public void removeChiTietDatTruoc(int idDat,int idTitle)
        {
            data = new DvdRentDbDataContext();
            ChiTietPhieuDatTruoc item = data.ChiTietPhieuDatTruocs.Single(n => n.IdPhieuDatTruoc == idDat&&n.IdTitle==idTitle);
            item.Huy = false;
            data.SubmitChanges();
        }
    }
}
