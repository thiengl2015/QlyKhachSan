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
    class ThemKhachHangViewModel : BaseViewModel
    {
        private string _maKH;
        public string MaKH
        {
            get { return _maKH; }
            set {
                if (_maKH != value)
                {
                    _maKH = value;
                    OnPropertyChanged(nameof(MaKH));
                }
            }
        }
        private string _tenKH;
        public string TenKH
        {
            get { return _tenKH; }
            set
            {
                if (_tenKH != value)
                {
                    _tenKH = value;
                    OnPropertyChanged(nameof(_tenKH));

                    //Tao ma khach hang moi neu ten khach hang thay doi
                    MaKH = TaoMaKhachHangMoi();
                }
            }
        }
        private string _cmnd;
        public string CMND
        {
            get { return _cmnd; }
            set
            {
                if (_cmnd != value)
                {
                    _cmnd = value;
                    OnPropertyChanged(nameof(CMND));
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

        private long _loaiKH;
        public long LoaiKH
        {
            get { return _loaiKH; }
            set
            {
                if (value != _loaiKH)
                {
                    _loaiKH = value;
                    OnPropertyChanged(nameof(LoaiKH));
                }
            }
        }

        private LOAIKHACHHANG _selectedLoaiKH;
        public LOAIKHACHHANG SelectedLoaiKH
        {
            get { return _selectedLoaiKH; }
            set
            {
                if (_selectedLoaiKH != value)
                {
                    _selectedLoaiKH = value;
                    OnPropertyChanged(nameof(SelectedLoaiKH));
                }
            }
        }

        private ObservableCollection<LOAIKHACHHANG> danhSachLoaiKH { get; set; }
        public ObservableCollection<LOAIKHACHHANG> DanhSachLoaiKH
        {
            get { return danhSachLoaiKH; }
            set
            {
                if (danhSachLoaiKH != value)
                {
                    danhSachLoaiKH = value;
                    OnPropertyChanged(nameof(DanhSachLoaiKH));
                }
            }
        }


        public ICommand ThemCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public ThemKhachHangViewModel(){
            ThemCommand = new RelayCommand<object>(CanExecuteThemKhachHang, (p) => ThemKhachHangMoi());
            ThoatCommand = new RelayCommand<object>((p) => true, (p) => 
            {
                if (p is System.Windows.Window window)
                {
                    window.Close();
                }
            });
            DanhSachLoaiKH = new ObservableCollection<LOAIKHACHHANG>(DataProvider.Instance.DB.LOAIKHACHHANGs.ToList());
        }

        private void ThemKhachHangMoi()
        {
            KHACHHANG kHACHHANG = new KHACHHANG
            {
                MaKhachHang = MaKH,
                TenKhachHang = _tenKH,
                CMND = _cmnd,
                DiaChi = _diaChi,
                MaLoaiKhachHang = SelectedLoaiKH.MaLoaiKhachHang
            };

            DataProvider.Instance.DB.KHACHHANGs.Add(kHACHHANG);
            DataProvider.Instance.DB.SaveChanges();

            TenKH = string.Empty;
            CMND = string.Empty;
            DiaChi = string.Empty;
            MaKH = string.Empty;
            SelectedLoaiKH = null;

            // Thông báo thành công
            System.Windows.MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private bool CanExecuteThemKhachHang(object p)
        {
            //kiem tra du lieu hop le
            if (string.IsNullOrEmpty(_tenKH) || string.IsNullOrEmpty(_cmnd) || string.IsNullOrEmpty(_diaChi) || string.IsNullOrEmpty(MaKH) || SelectedLoaiKH == null)
                return false;
            return true;
        }

        private string TaoMaKhachHangMoi()
        {
            int number = DataProvider.Instance.DB.KHACHHANGs.Count() + 1;
            string maKH = "KH" + number.ToString("D3");

            return maKH;
        }
    }
}

public class LoaiKhachHang
{
    public long maLoaiKH { get; set; }
    public String tenLoaiKH { get; set; }
    public String heSo {  get; set; }
}
