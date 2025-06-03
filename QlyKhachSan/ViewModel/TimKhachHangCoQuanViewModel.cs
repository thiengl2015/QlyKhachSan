using System;
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
        public ObservableCollection<NguoiThanhToan> DSNguoiThanhToan { get; set; }

        private NguoiThanhToan _nguoiThanhToanDuocChon;
        public NguoiThanhToan NguoiThanhToanDuocChon
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
        private string _maNguoiThanhToan;
        public string MaNguoiThanhToan
        {
            get { return _maNguoiThanhToan; }
            set
            {
                if (_maNguoiThanhToan != value)
                {
                    _maNguoiThanhToan = value;
                    OnPropertyChanged(nameof(MaNguoiThanhToan));
                }
            }
        }
        private string _tenNguoiThanhToan;
        public string TenNguoiThanhToan
        {
            get { return _tenNguoiThanhToan; }
            set
            {
                if (_tenNguoiThanhToan != value)
                {
                    _tenNguoiThanhToan = value;
                    OnPropertyChanged(nameof(TenNguoiThanhToan));
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
        public ICommand TimKhachHangCoQuan { get; }

        public TimKhachHangCoQuanViewModel()
        {
            TimKhachHangCoQuan = new RelayCommand<object>((p) => true, (p) => TimKhachHangCoQuanMoi());
        }

        private void TimKhachHangCoQuanMoi()
        {
            //Truy van DB
        }
    }
    public class NguoiThanhToan
    {
        private string MaNguoiThanhToan { get; set; }
        private string TenNguoiThanhToan { get; set; }
        private string DiaChi { get; set; }
        public NguoiThanhToan(string maNguoiThanhToan, string tenNguoiThanhToan, string diaChi)
        {
            MaNguoiThanhToan = maNguoiThanhToan;
            TenNguoiThanhToan = tenNguoiThanhToan;
            DiaChi = diaChi;
        }
    }
}
