using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlyKhachSan.ViewModel
{
    public class LapBaoCaoDoanhThuViewModel : BaseViewModel
    {
        private int namBaoCao;
        public int NamBaoCao
        {
            get { return namBaoCao; }
            set
            {
                if (namBaoCao != value)
                {
                    namBaoCao = value;
                    OnPropertyChanged();
                }
            }
        }

        private int thangBaoCao;
        public int ThangBaoCao
        {
            get { return thangBaoCao; }
            set
            {
                if (thangBaoCao != value)
                {
                    thangBaoCao = value;
                    OnPropertyChanged();
                }
            }
        }

        public LapBaoCaoDoanhThuViewModel()
        {
            // Initialize default values
            NamBaoCao = DateTime.Now.Year;
            ThangBaoCao = DateTime.Now.Month;
        }
    }
}
