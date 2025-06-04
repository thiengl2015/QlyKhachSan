using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Documents;
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

        private string tenKhachHang;
        public string TenKhachHang
        {
            get => tenKhachHang;
            set
            {
                tenKhachHang = value;
                OnPropertyChanged(nameof(TenKhachHang));
            }
        }

        private string maKhachHang;
        public string MaKhachHang
        {
            get => maKhachHang;
            set
            {
                maKhachHang = value;
                OnPropertyChanged(nameof(MaKhachHang));
            }
        }

        private string diaChi;
        public string DiaChi
        {
            get => diaChi;
            set
            {
                diaChi = value;
                OnPropertyChanged(nameof(DiaChi));
            }
        }

        private string cmnd;
        public string CMND
        {
            get => cmnd;
            set
            {
                cmnd = value;
                OnPropertyChanged(nameof(CMND));
            }
        }

        private string tenLoaiKhachHang;
        public string TenLoaiKhachHang
        {
            get => tenLoaiKhachHang;
            set
            {
                tenLoaiKhachHang = value;
                OnPropertyChanged(nameof(TenLoaiKhachHang));
            }
        }

        private int donGiaFrom;
        public int DonGiaFrom
        {
            get => donGiaFrom;
            set
            {
                donGiaFrom = value;
                OnPropertyChanged(nameof(DonGiaFrom));
            }
        }

        private int donGiaTo;
        public int DonGiaTo
        {
            get => donGiaTo;
            set
            {
                donGiaTo = value;
                OnPropertyChanged(nameof(DonGiaTo));
            }
        }

        private DateTime ngayBatDauFrom;
        public DateTime NgayBatDauFrom
        {
            get => ngayBatDauFrom;
            set
            {
                ngayBatDauFrom = value;
                OnPropertyChanged(nameof(NgayBatDauFrom));
            }
        }

        private DateTime ngayBatDauTo;
        public DateTime NgayBatDauTo
        {
            get => ngayBatDauTo;
            set
            {
                ngayBatDauTo = value;
                OnPropertyChanged(nameof(NgayBatDauTo));
            }
        }

        private DateTime ngayKetThucFrom;
        public DateTime NgayKetThucFrom
        {
            get => ngayKetThucFrom;
            set
            {
                ngayKetThucFrom = value;
                OnPropertyChanged(nameof(NgayKetThucFrom));
            }
        }

        private DateTime ngayKetThucTo;
        public DateTime NgayKetThucTo
        {
            get => ngayKetThucTo;
            set
            {
                ngayKetThucTo = value;
                OnPropertyChanged(nameof(NgayKetThucTo));
            }
        }

        private string maPTTu;
        public string MaPTTu
        {
            get => maPTTu;
            set
            {
                maPTTu = value;
                OnPropertyChanged(nameof(MaPTTu));
            }
        }

        private string maPTDen;
        public string MaPTDen
        {
            get => maPTDen;
            set
            {
                maPTDen = value;
                OnPropertyChanged(nameof(MaPTDen));
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

            NgayBatDauFrom = DateTime.Now;
            NgayBatDauTo = DateTime.Now;

            NgayKetThucFrom = DateTime.Now;
            NgayKetThucTo = DateTime.Now;

            // Khởi tạo danh sách loại phòng
            DanhSachLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs);
        }

        private bool CanExecuteTraCuu(object parameter)
        {
            return true; // Luôn cho phép tìm kiếm
        }

        private void TraCuuPhong(object parameter)
        {
            MessageBox.Show("Đang tìm kiếm...");
            var danhSachPhongTimThay = DataProvider.Instance.DB.PHONGs.Where(p =>
                (string.IsNullOrEmpty(MaPhong) || p.MaPhong.Contains(MaPhong)) &&
                (string.IsNullOrEmpty(TenPhong) || p.TenPhong.Contains(TenPhong)) &&
                (string.IsNullOrEmpty(MaLoaiPhong) || p.MaLoaiPhong.Contains(MaLoaiPhong)) &&
                (string.IsNullOrEmpty(TenLoaiPhong) || p.LOAIPHONG.TenLoaiPhong.Contains(TenLoaiPhong)) &&
                (string.IsNullOrEmpty(GhiChu) || p.GhiChu.Contains(GhiChu))
            ).ToList();

            bool isLocKhachHang = false;

            if (string.IsNullOrEmpty(TenKhachHang) && string.IsNullOrEmpty(MaKhachHang) && string.IsNullOrEmpty(DiaChi) && string.IsNullOrEmpty(CMND) && string.IsNullOrEmpty(TenLoaiKhachHang))
            {
                isLocKhachHang = false;
            }
            else
            {
                isLocKhachHang = true;
            }

            MessageBox.Show($"Số lượng phòng tìm thấy: {danhSachPhongTimThay.Count}");

            if (isLocKhachHang)
            {
                for (int i = danhSachPhongTimThay.Count - 1; i >= 0; i--)
                {
                    var phieuThues = danhSachPhongTimThay[i].PHIEUTHUEs.ToList();

                    List<KHACHHANG> khachHangs = new List<KHACHHANG>();

                    foreach (PHIEUTHUE phieuThue in phieuThues)
                    {
                        khachHangs.AddRange(phieuThue.KHACHHANGs.ToList());
                    }

                    bool isFound = false;

                    foreach (var kh in khachHangs)
                    {
                        if ((string.IsNullOrEmpty(TenKhachHang) || kh.TenKhachHang.Contains(TenKhachHang)) &&
                            (string.IsNullOrEmpty(MaKhachHang) || kh.MaKhachHang.Contains(MaKhachHang)) &&
                            (string.IsNullOrEmpty(DiaChi) || kh.DiaChi.Contains(DiaChi)) &&
                            (string.IsNullOrEmpty(CMND) || kh.CMND.Contains(CMND)) &&
                            (string.IsNullOrEmpty(TenLoaiKhachHang) || kh.LOAIKHACHHANG.TenLoaiKhachHang.Contains(TenLoaiKhachHang)))
                        {
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        danhSachPhongTimThay.RemoveAt(i);
                    }
                }
                MessageBox.Show($"Số lượng phòng sau khi lọc khách hàng: {danhSachPhongTimThay.Count}");
            }    
       

            for (int i = danhSachPhongTimThay.Count - 1; i >= 0; i--)
            {
                bool isFound = false;

                if (DonGiaTo <= DonGiaFrom) continue;

                if (DonGiaTo > DonGiaFrom &&
                    (danhSachPhongTimThay[i].LOAIPHONG.DonGia >= DonGiaFrom && danhSachPhongTimThay[i].LOAIPHONG.DonGia <= DonGiaTo))
                {
                    isFound = true;
                }

                if (!isFound)
                {
                    danhSachPhongTimThay.RemoveAt(i);
                }
            }
            MessageBox.Show($"Số lượng phòng sau khi lọc đơn giá: {danhSachPhongTimThay.Count}");

            // Lọc Ngày
            for (int i = danhSachPhongTimThay.Count - 1; i >= 0; i--)
            {
                bool isFound = false;

                var phieuThues = danhSachPhongTimThay[i].PHIEUTHUEs.ToList();

                foreach (var phieuThue in phieuThues)
                {
                    if ((phieuThue.NgayBatDauThue >= NgayBatDauFrom && phieuThue.NgayBatDauThue <= NgayBatDauTo) ||
                        (phieuThue.NgayKetThucThue >= NgayKetThucFrom && phieuThue.NgayKetThucThue <= NgayKetThucTo))
                    {
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    danhSachPhongTimThay.RemoveAt(i);
                }
            }
            MessageBox.Show($"Số lượng phòng sau khi lọc ngày: {danhSachPhongTimThay.Count}");

            // Lọc phiếu thuê
            for (int i = danhSachPhongTimThay.Count - 1; i >= 0; i--)
            {
                bool isFound = false;

                var phieuThues = danhSachPhongTimThay[i].PHIEUTHUEs.ToList();

                foreach (var phieuThue in phieuThues)
                {
                    if ((string.IsNullOrEmpty(MaPTTu) || phieuThue.MaPhieuThue.CompareTo(MaPTTu) >= 0) &&
                        (string.IsNullOrEmpty(MaPTDen) || phieuThue.MaPhieuThue.CompareTo(MaPTDen) <= 0))
                    {
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    danhSachPhongTimThay.RemoveAt(i);
                }
            }
            MessageBox.Show($"Số lượng phòng sau khi lọc phiếu thuê: {danhSachPhongTimThay.Count}");



            DanhSachPhong.Clear();

            foreach (var phong in danhSachPhongTimThay)
            {
                DanhSachPhong.Add(phong);
            }

            MessageBox.Show($"Tìm kiếm hoàn tất. Số lượng phòng tìm thấy: {DanhSachPhong.Count}");

        }
    }
}