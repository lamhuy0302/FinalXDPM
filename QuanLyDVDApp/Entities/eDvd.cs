using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eDvd
    {
        /*Id int identity primary key,
	    IdTitle int not null,*/

        private int idDvd, idTiltle;
        private string tinhTrang;
        private bool TrangThai;


        public eDvd() { }

        public eDvd(int idDvd, int idTiltle, string tinhTrang, bool trangThai)
        {
            this.idDvd = idDvd;
            this.idTiltle = idTiltle;
            this.tinhTrang = tinhTrang;
            TrangThai = trangThai;
        }

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

        public int IdTiltle
        {
            get
            {
                return idTiltle;
            }

            set
            {
                idTiltle = value;
            }
        }

        public string TinhTrang
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
