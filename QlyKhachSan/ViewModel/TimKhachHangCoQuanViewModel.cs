using System;
using QlyKhachSan.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QlyKhachSan.View;

namespace QlyKhachSan.ViewModel
{
    public class TimKhachHangCoQuanViewModel : BaseViewModel
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
        public ICommand TimKhachHangCoQuanCommand { get; }
        public ICommand ThemKhachHangCoQuanCommand { get; }


        public TimKhachHangCoQuanViewModel()
        {
            TimKhachHangCoQuanCommand = new RelayCommand<object>((p) => true, (p) => TimKhachHangCoQuanMoi());
            ThemKhachHangCoQuanCommand = new RelayCommand<object>((p) => true, (p) => { ThemNguoiThanhToanWindow window = new ThemNguoiThanhToanWindow(); window.Show(); });
        }

        private void TimKhachHangCoQuanMoi()
        {
            //Truy van DB
        }
    }
}
