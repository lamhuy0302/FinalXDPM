using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class bNhanVien
    {
        DvdRentDbDataContext data;
        //getAll
        public List<eNhanVien> getAllNhanVien()
        {
            data = new DvdRentDbDataContext();
            List<eNhanVien> list = new List<eNhanVien>();
            var temp = data.NhanViens;
            foreach (var item in temp)
            {
                list.Add(new eNhanVien(item.Id,item.Password,item.Ten,item.DiaChi,item.Sdt));
            }
            return list;
        }
        //get
        public eNhanVien getNhanVien(int id)
        {
            data = new DvdRentDbDataContext();
            NhanVien item = data.NhanViens.Single(n => n.Id == id);
            return new eNhanVien(item.Id,item.Password,item.Ten, item.DiaChi, item.Sdt);
        }
    }
}
