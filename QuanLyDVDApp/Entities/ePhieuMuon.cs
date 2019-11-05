using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ePhieuMuon
    {
        /*Id int identity primary key,
	    IdKhachHang int not null,
	    NgayTao DATETIME*/
        private int  idKhach,idPhieuMuon;
        private DateTime ngayTao;

        public ePhieuMuon(int idPhieuMuon, int idKhach, DateTime ngayTao)
        {            
            this.idPhieuMuon = idPhieuMuon;
            this.idKhach = idKhach;
            this.ngayTao = ngayTao;
        }
        public ePhieuMuon() { }
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

        public DateTime NgayTao
        {
            get
            {
                return ngayTao;
            }

            set
            {
                ngayTao = value;
            }
        }
    }
}
