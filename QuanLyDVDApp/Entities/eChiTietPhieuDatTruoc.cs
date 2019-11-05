using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eChiTietPhieuDatTruoc
    {
        /*IdPhieuDatTruoc int not null,
	    IdDVD int not null,*/

        private int idPhieuDatTruoc, idTitle;
        private bool TrangThai;
        private bool huy;


        public eChiTietPhieuDatTruoc(int idPhieuDatTruoc, int idTitle, bool trangThai, bool huy)
        {
            this.idPhieuDatTruoc = idPhieuDatTruoc;
            this.idTitle = idTitle;
            TrangThai = trangThai;
            this.huy = huy;
        }

        public int IdPhieuDatTruoc
        {
            get
            {
                return idPhieuDatTruoc;
            }

            set
            {
                idPhieuDatTruoc = value;
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

        public bool Huy
        {
            get
            {
                return huy;
            }

            set
            {
                huy = value;
            }
        }
    }
}
