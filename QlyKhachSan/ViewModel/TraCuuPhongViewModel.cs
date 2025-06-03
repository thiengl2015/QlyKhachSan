﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QlyKhachSan.Model;

namespace QlyKhachSan.ViewModel
{
    public class TraCuuPhongViewModel : BaseViewModel
    {
        // Các thuộc tính tìm kiếm
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
            }
        }

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

        // Danh sách loại phòng
        private ObservableCollection<LOAIPHONG> _danhSachLoaiPhong;
        public ObservableCollection<LOAIPHONG> DanhSachLoaiPhong
        {
            get => _danhSachLoaiPhong;
            set
            {
                _danhSachLoaiPhong = value;
                OnPropertyChanged(nameof(DanhSachLoaiPhong));
            }
        }

        // Danh sách phòng tìm thấy
        private ObservableCollection<PHONG> _danhSachPhong;
        public ObservableCollection<PHONG> DanhSachPhong
        {
            get => _danhSachPhong;
            set
            {
                _danhSachPhong = value;
                OnPropertyChanged(nameof(DanhSachPhong));
            }
        }

        // Command xử lý tìm kiếm
        public ICommand TraCuuCommand { get; set; }

        public TraCuuPhongViewModel()
        {
            DanhSachPhong = new ObservableCollection<PHONG>();
            TraCuuCommand = new RelayCommand<object>(CanExecuteTraCuu, TraCuuPhong);

            // Khởi tạo danh sách loại phòng
            DanhSachLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs);
        }

        private bool CanExecuteTraCuu(object parameter)
        {
            return true; // Luôn cho phép tìm kiếm
        }

        private void TraCuuPhong(object parameter)
        {
            var danhSachPhongTimThay = DataProvider.Instance.DB.PHONGs.Where(p =>
                (string.IsNullOrEmpty(MaPhong) || p.MaPhong.Contains(MaPhong)) &&
                (string.IsNullOrEmpty(TenPhong) || p.TenPhong.Contains(TenPhong)) &&
                (string.IsNullOrEmpty(MaLoaiPhong) || p.MaLoaiPhong.Contains(MaLoaiPhong)) &&
                (string.IsNullOrEmpty(TenLoaiPhong) || p.LOAIPHONG.TenLoaiPhong.Contains(TenLoaiPhong)) &&
                (string.IsNullOrEmpty(GhiChu) || p.GhiChu.Contains(GhiChu))
            ).ToList();

            DanhSachPhong.Clear();
            foreach (var phong in danhSachPhongTimThay)
            {
                DanhSachPhong.Add(phong);
            }

            //if (DanhSachPhong.Count == 0)
            //{
            //    // Nếu không tìm thấy phòng nào, có thể hiển thị thông báo
            //    MessageBox.Show("Không tìm thấy phòng nào phù hợp với tiêu chí tìm kiếm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    // Nếu tìm thấy phòng, có thể hiển thị số lượng phòng tìm thấy
            //    MessageBox.Show($"Đã tìm thấy {DanhSachPhong.Count} phòng phù hợp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}