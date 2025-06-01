using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class LapDanhMucPhongViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIPHONG> danhSachLoaiPhong;
        public ObservableCollection<LOAIPHONG> DanhSachLoaiPhong
        {
            get { return danhSachLoaiPhong; }
            set
            {
                danhSachLoaiPhong = value;
                OnPropertyChanged(nameof(DanhSachLoaiPhong));
            }
        }

        private LOAIPHONG _selectedLoaiPhong;
        public LOAIPHONG SelectedLoaiPhong
        {
            get => _selectedLoaiPhong;
            set
            {
                _selectedLoaiPhong = value;
                OnPropertyChanged(nameof(SelectedLoaiPhong));

                if (_selectedLoaiPhong != null)
                {
                    DonGia = _selectedLoaiPhong.DonGia?.ToString() ?? "0";
                }
            }
        }

        private string _maPhong;
        public string MaPhong
        {
            get => _maPhong;
            set
            {
                _maPhong = value;
                OnPropertyChanged(nameof(MaPhong));
            }
        }

        private string _tenPhong;
        public string TenPhong
        {
            get => _tenPhong;
            set
            {
                _tenPhong = value;
                OnPropertyChanged(nameof(TenPhong));

                MaPhong = value;
            }
        }

        private string _donGia;
        public string DonGia
        {
            get => _donGia;
            set
            {
                _donGia = value;
                OnPropertyChanged(nameof(DonGia));
            }
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set
            {
                _ghiChu = value;
                OnPropertyChanged(nameof(GhiChu));
            }
        }

        public LapDanhMucPhongViewModel()
        {
            DanhSachLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs);

            SelectedLoaiPhong = DanhSachLoaiPhong.First();

            ThemPhongCommand = new RelayCommand<object>(CanExecuteThemPhong, ThemPhong);
            ThemLoaiPhongCommand = new RelayCommand<object>(CanExecuteThemLoaiPhong, ThemLoaiPhong);
            ThoatCommand = new RelayCommand<object>(CanExecuteThoat, Thoat);
        }

        private void LoadDanhSachLoaiPhong()
        {
            DanhSachLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs);
            SelectedLoaiPhong = DanhSachLoaiPhong.FirstOrDefault();
        }

        private bool CanExecuteThemLoaiPhong(object parameter)
        {
            return true; // Luôn cho phép thêm loại phòng
        }

        private void ThemLoaiPhong(object parameter)
        {
            var view = new QlyKhachSan.View.ThemLoaiPhong();

            var vm = new ThemLoaiPhongViewModel();
            vm.OnLoaiPhongAdded = LoadDanhSachLoaiPhong;
            view.DataContext = vm;

            view.ShowDialog();
        }

        private bool CanExecuteThoat(object parameter)
        {
            return true; // Luôn cho phép thoát
        }

        private void Thoat(object parameter)
        {
            // Xử lý thoát nếu cần, ví dụ: đóng cửa sổ hoặc reset các trường
            TenPhong = string.Empty;
            MaPhong = string.Empty;
            GhiChu = string.Empty;
            DonGia = string.Empty;
            SelectedLoaiPhong = DanhSachLoaiPhong.FirstOrDefault();
        }

        private bool CanExecuteThemPhong(object parameter)
        {
            if (String.IsNullOrEmpty(TenPhong))
                return false;

            var existingPhong = DataProvider.Instance.DB.PHONGs.FirstOrDefault(x => x.MaPhong == MaPhong);
            if (existingPhong != null)
                return false;

            return true;
        }

        private void ThemPhong(object parameter)
        {
            DataProvider.Instance.DB.PHONGs.Add(new PHONG
            {
                MaPhong = MaPhong,
                TenPhong = TenPhong,
                MaLoaiPhong = SelectedLoaiPhong.MaLoaiPhong,
                GhiChu = GhiChu
            });

            DataProvider.Instance.DB.SaveChanges();

            System.Windows.MessageBox.Show("Thêm phòng thành công!", "Thông báo");

            TenPhong = string.Empty;
            MaPhong = string.Empty;
            GhiChu = string.Empty;
            DonGia = string.Empty;
            SelectedLoaiPhong = DanhSachLoaiPhong.FirstOrDefault();
        }

        public ICommand ThemPhongCommand { get; set; }
        public ICommand ThemLoaiPhongCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
    }
}
