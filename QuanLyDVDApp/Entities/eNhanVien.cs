using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eNhanVien
    {
        /*
         Id int identity primary key,
	    Password nvarchar(40) null,
	    Ten nvarchar(40) null,
	    DiaChi nvarchar(40) null,
	    Sdt nvarchar(40) null,
        */

        private int idNhanVien;
        private string pwdNhanVien, tenNhanVien, diaChiNhanVien, sdtNhanVien;

        public eNhanVien()
        {

        }

        public eNhanVien(int idNhanVien, string pwdNhanVien, string tenNhanVien, string diaChiNhanVien, string sdtNhanVien)
        {
            this.idNhanVien = idNhanVien;
            this.pwdNhanVien = pwdNhanVien;
            this.tenNhanVien = tenNhanVien;
            this.diaChiNhanVien = diaChiNhanVien;
            this.sdtNhanVien = sdtNhanVien;
        }

        public string DiaChiNhanVien
        {
            get
            {
                return diaChiNhanVien;
            }

            set
            {
                diaChiNhanVien = value;
            }
        }

        public int IdNhanVien
        {
            get
            {
                return idNhanVien;
            }

            set
            {
                idNhanVien = value;
            }
        }

        public string PwdNhanVien
        {
            get
            {
                return pwdNhanVien;
            }

            set
            {
                pwdNhanVien = value;
            }
        }

        public string SdtNhanVien
        {
            get
            {
                return sdtNhanVien;
            }

            set
            {
                sdtNhanVien = value;
            }
        }

        public string TenNhanVien
        {
            get
            {
                return tenNhanVien;
            }

            set
            {
                tenNhanVien = value;
            }
        }
    }
}
