using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eChiTietPhieuMuon
    {
        /*IdPhieuMuon int not null,
	    IdDVD int not null,
	    NgayTra datetime null,
	    PhiTre int NOT NULL,
	    TinhTrang BIT*/

        private int idPhieuMuon, idDvd, phiTre;
        private DateTime ngayTra;
        private bool tinhTrang;

        public eChiTietPhieuMuon(int idPhieuMuon, int idDvd, int phiTre, DateTime ngayTra, bool tinhTrang)
        {
            this.idPhieuMuon = idPhieuMuon;
            this.idDvd = idDvd;
            this.phiTre = phiTre;
            this.ngayTra = ngayTra;
            this.tinhTrang = tinhTrang;
        }
        public eChiTietPhieuMuon() { }

        public int IdDvd
        {
            get
            {
                return idDvd;
            }

            set
            {
                idDvd = value;
            }
        }

        public int IdPhieuMuon
        {
            get
            {
                return idPhieuMuon;
            }

            set
            {
                idPhieuMuon = value;
            }
        }

        public DateTime NgayTra
        {
            get
            {
                return ngayTra;
            }

            set
            {
                ngayTra = value;
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

        public bool TinhTrang
        {
            get
            {
                return tinhTrang;
            }

            set
            {
                tinhTrang = value;
            }
        }
    }
}
