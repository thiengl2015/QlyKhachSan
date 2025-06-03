using System;
using QlyKhachSan.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class TimKhachHangCoQuanViewModel : BaseViewModel
    {
        public ObservableCollection<NGUOITHANHTOAN> DSNguoiThanhToan { get; set; }

        private NGUOITHANHTOAN _nguoiThanhToanDuocChon;
        public NGUOITHANHTOAN NguoiThanhToanDuocChon
        {
            get { return _nguoiThanhToanDuocChon; }
            set
            {
                if (_nguoiThanhToanDuocChon != value)
                {
                    _nguoiThanhToanDuocChon = value;
                    OnPropertyChanged(nameof(NguoiThanhToanDuocChon));
                }
            }
        }
        private string _maNTT;
        public string MaNTT
        {
            get { return _maNTT; }
            set
            {
                if (_maNTT != value)
                {
                    _maNTT = value;
                    OnPropertyChanged(nameof(MaNTT));
                }
            }
        }
        private string _tenNTT;
        public string TenNTT
        {
            get
            {
                return _tenNTT;
            }
            set
            {
                if( _tenNTT != value)
                {
                    _tenNTT = value;
                    OnPropertyChanged(nameof(TenNTT));
                }
            }
        }
        private string _diaChi;
        public string DiaChi
        {
            get { return _diaChi; }
            set
            {
                if (value != _diaChi)
                {
                    _diaChi = value;
                    OnPropertyChanged(nameof(DiaChi));
                }
            }
        }
    }
}
