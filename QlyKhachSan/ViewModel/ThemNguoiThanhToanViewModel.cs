using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class ThemNguoiThanhToanViewModel : BaseViewModel
    {
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
            get { return _tenNTT; }
            set
            {
                if (_tenNTT != value)
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
                if (_diaChi != value)
                {
                    _diaChi = value;
                    OnPropertyChanged(nameof(DiaChi));
                }
            }
        }
        public ICommand ThemCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public ThemNguoiThanhToanViewModel()
        {
            ThemCommand = new RelayCommand<object>(CanExecuteThemNguoiThanhToan, (p) => ThemNguoiThanhToanMoi());
            ThoatCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (p is System.Windows.Window window)
                {
                    window.Close();
                }
            });
        }

        private void ThemNguoiThanhToanMoi()
        {
            NGUOITHANHTOAN nGUOITHANHTOAN = new NGUOITHANHTOAN
            {
                MaNguoiThanhToan = MaNTT,
                TenNguoiThanhToan = TenNTT,
                DiaChi = DiaChi
            };

            DataProvider.Instance.DB.NGUOITHANHTOANs.Add(nGUOITHANHTOAN);
            DataProvider.Instance.DB.SaveChanges();

            TenNTT = string.Empty;
            DiaChi = string.Empty;
            MaNTT = string.Empty;

            // Thông báo thành công
            System.Windows.MessageBox.Show("Thêm người thanh toán thành công!", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private bool CanExecuteThemNguoiThanhToan(object p)
        {
            //kiem tra du lieu hop le
            if (string.IsNullOrEmpty(_tenNTT) || string.IsNullOrEmpty(_diaChi) || string.IsNullOrEmpty(_maNTT))
                return false;
            return true;
        }

        private string TaoMaNguoiThanhToanMoi()
        {
            int number = DataProvider.Instance.DB.NGUOITHANHTOANs.Count() + 1;
            string maKH = "NTT" + number.ToString("D3");

            return maKH;
        }
    }
}
