using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bTitle
    {
        DvdRentDbDataContext data;
        public List<eTitle> getAllTitle()
        {
            data = new DvdRentDbDataContext();
            List<eTitle> list = new List<eTitle>();
            var itemlist = data.Titles;
            foreach (var item in itemlist)
            {
                list.Add(new eTitle(item.Id,(int)item.PhiTre, (int)item.ThoiGianThue, (int)item.Gia,item.Ten,(bool)item.TheLoai,(bool)item.TrangThai));
            }
            return list;
        }
        public eTitle getTitle(int id)
        {
            data = new DvdRentDbDataContext();
            Title item = data.Titles.Single(n => n.Id == id);
            return new eTitle(
                item.Id,
                int.Parse(item.PhiTre.ToString()),
                int.Parse(item.ThoiGianThue.ToString()),
                int.Parse(item.Gia.ToString()),
                item.Ten,
                (bool)item.TheLoai,
                (bool)item.TrangThai
                );
        }
        public void insertTitle(eTitle item)
        {
            data = new DvdRentDbDataContext();
            data.Titles.InsertOnSubmit(new Title()
            {
                PhiTre = item.PhiTre,
                ThoiGianThue = item.ThoiGianThue,
                Gia = item.GiaThue,
                Ten = item.TenTitle,
                TheLoai = item.TheLoai,
                TrangThai=item.TrangThai1
            });
            data.SubmitChanges();
        }
        public void removeTitle(int id)
        {
            data = new DvdRentDbDataContext();
            Title item = data.Titles.Single(n => n.Id == id);
            item.TrangThai = false;
            data.SubmitChanges();
        }
    }
}
