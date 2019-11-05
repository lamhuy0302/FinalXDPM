using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eTitle
    {
        /* Id int identity primary key,
         Ten nvarchar(40) null,
         TheLoai bit null,
         Gia int null,
         PhiTre int null,
         ThoiGianThue int null
         */

        private int idTitle, phiTre, thoiGianThue,giaThue;
        private string tenTitle;
        private bool theLoai;
        private bool TrangThai;

       
        public eTitle() { }

        public eTitle(int idTitle, int phiTre, int thoiGianThue, int giaThue, string tenTitle, bool theLoai, bool trangThai)
        {
            this.idTitle = idTitle;
            this.phiTre = phiTre;
            this.thoiGianThue = thoiGianThue;
            this.giaThue = giaThue;
            this.tenTitle = tenTitle;
            this.theLoai = theLoai;
            TrangThai = trangThai;
        }

        public int GiaThue
        {
            get
            {
                return giaThue;
            }

            set
            {
                giaThue = value;
            }
        }

        public int IdTitle
        {
            get
            {
                return idTitle;
            }

            set
            {
                idTitle = value;
            }
        }

        public int PhiTre
        {
            get
            {
                return phiTre;
            }

            set
            {
                phiTre = value;
            }
        }

        public string TenTitle
        {
            get
            {
                return tenTitle;
            }

            set
            {
                tenTitle = value;
            }
        }

        public bool TheLoai
        {
            get
            {
                return theLoai;
            }

            set
            {
                theLoai = value;
            }
        }

        public int ThoiGianThue
        {
            get
            {
                return thoiGianThue;
            }

            set
            {
                thoiGianThue = value;
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
