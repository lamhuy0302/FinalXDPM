using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eKhachHang
    {
        /*
         Id int identity primary key,
	    Ten nvarchar(40) null,
	    DiaChi nvarchar(40) null,
	    Sdt nvarchar(40) null,
         */

        private int idKhach;
        private string tenKhach, diaChiKhach, sdtKhach;
        private bool TrangThai;

        public eKhachHang()
        {
           
        }

        public eKhachHang(int idKhach, string tenKhach, string diaChiKhach, string sdtKhach, bool trangThai)
        {
            this.idKhach = idKhach;
            this.tenKhach = tenKhach;
            this.diaChiKhach = diaChiKhach;
            this.sdtKhach = sdtKhach;
            TrangThai = trangThai;
        }

        public string DiaChiKhach
        {
            get
            {
                return diaChiKhach;
            }

            set
            {
                diaChiKhach = value;
            }
        }

        public int IdKhach
        {
            get
            {
                return idKhach;
            }

            set
            {
                idKhach = value;
            }
        }

        public string SdtKhach
        {
            get
            {
                return sdtKhach;
            }

            set
            {
                sdtKhach = value;
            }
        }

        public string TenKhach
        {
            get
            {
                return tenKhach;
            }

            set
            {
                tenKhach = value;
            }
        }

        public bool TrangThai1
        {
            get
            {
                return TrangThai;
            }

            set
            {
                TrangThai = value;
            }
        }
    }
}
