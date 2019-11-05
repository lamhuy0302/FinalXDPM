using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ePhieuDatTruoc
    {
        /*Id int identity primary key,
	    IdKhachHang int not null,
	    NgayTao DATETIME*/

        private int idDatTruoc, idKhach;
        private DateTime ngayTao;

        public ePhieuDatTruoc(int idDatTruoc, int idKhach, DateTime ngayTao)
        {
            this.idDatTruoc = idDatTruoc;
            this.idKhach = idKhach;
            this.ngayTao = ngayTao;
        }

        public int IdDatTruoc
        {
            get
            {
                return idDatTruoc;
            }

            set
            {
                idDatTruoc = value;
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
