using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThemLoaiPhongViewModel : BaseViewModel
    {
        private string _maLoaiPhong;
        public string MaLoaiPhong
        {
            get => _maLoaiPhong;
            set
            {
                _maLoaiPhong = value;
                OnPropertyChanged(nameof(MaLoaiPhong));
            }
        }

        private string _tenLoaiPhong;
        public string TenLoaiPhong
        {
            get => _tenLoaiPhong;
            set
            {
                _tenLoaiPhong = value;
                OnPropertyChanged(nameof(TenLoaiPhong));

                MaLoaiPhong = value;
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

        public ThemLoaiPhongViewModel()
        {
            ThemLoaiPhongCommand = new RelayCommand<object>(CanExecuteThemLoaiPhong, ThemLoaiPhong);
            ThoatCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (p is Window window)
                {
                    window.Close();
                }
            });
        }

        private bool CanExecuteThemLoaiPhong(object p)
        {
            if (string.IsNullOrEmpty(MaLoaiPhong) || string.IsNullOrEmpty(TenLoaiPhong) || string.IsNullOrEmpty(DonGia))
            {
                return false;
            }

            var existingLoaiPhong = DataProvider.Instance.DB.LOAIPHONGs
                .FirstOrDefault(lp => lp.MaLoaiPhong == MaLoaiPhong);

            return existingLoaiPhong == null;
        }

        private void ThemLoaiPhong(object p)
        {
            DataProvider.Instance.DB.LOAIPHONGs.Add(new LOAIPHONG
            {
                MaLoaiPhong = MaLoaiPhong,
                TenLoaiPhong = TenLoaiPhong,
                DonGia = int.Parse(DonGia)
            });

            DataProvider.Instance.DB.SaveChanges();

            MaLoaiPhong = string.Empty;
            TenLoaiPhong = string.Empty;
            DonGia = string.Empty;

            MessageBox.Show("Thêm loại phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            OnLoaiPhongAdded?.Invoke();
        }

        public ICommand ThemLoaiPhongCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public Action OnLoaiPhongAdded { get; set; }
    }
}
