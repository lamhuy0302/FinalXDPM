using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bDvd
    {
        DvdRentDbDataContext data;
        public List<eDvd> getAllDvd()
        {
            data = new DvdRentDbDataContext();
            List<eDvd> list = new List<eDvd>();
            var itemlist = data.DVDs;
            foreach (var item in itemlist)
            {
                list.Add(new eDvd(item.Id, item.IdTitle, item.TinhTrang,(bool)item.TrangThai));
            }
            return list;
        }
        public eDvd getDvd(int id)
        {
            data = new DvdRentDbDataContext();
            DVD item = data.DVDs.Single(n => n.Id == id);
            return new eDvd(item.Id,item.IdTitle,item.TinhTrang, (bool)item.TrangThai);
        }
        public void insertDvd(eDvd item)
        {
            data = new DvdRentDbDataContext();
            data.DVDs.InsertOnSubmit(new DVD()
            {
                IdTitle = item.IdTiltle,
                TinhTrang = item.TinhTrang,
                TrangThai=item.TrangThai1
            });
            data.SubmitChanges();
        }
        public void updateDvd(eDvd newitem)
        {
            DVD item = data.DVDs.Single(n => n.Id == newitem.IdDvd);
            item.Id = newitem.IdDvd;
            item.IdTitle = newitem.IdTiltle;
            item.TinhTrang = newitem.TinhTrang;
            item.TrangThai = newitem.TrangThai1;
            data.SubmitChanges();
        }
        public void removeDvd(int id)
        {
            data = new DvdRentDbDataContext();
            DVD item = data.DVDs.Single(n => n.Id == id);
            item.TrangThai = false;
            data.SubmitChanges();
        }
    }
}
