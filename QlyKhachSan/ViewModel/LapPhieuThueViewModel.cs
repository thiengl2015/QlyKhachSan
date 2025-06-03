using QlyKhachSan.Model;
using QlyKhachSan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class LapPhieuThueViewModel : BaseViewModel
    {
        public ObservableCollection<KHACHHANG> DSKhachHang { get; set; }
        public ObservableCollection<KhachHangTrongPhieuThue> DSKhachHangTrongPhieuThue { get; set; }

        private int _soKhachToiDa;
        public int SoKhachToiDa
        {
            get
            {
                return _soKhachToiDa;
            }
            set
            {
                if (_soKhachToiDa != value)
                {
                    _soKhachToiDa = value;
                    OnPropertyChanged(nameof(SoKhachToiDa));
                }
            }
        }

        private ObservableCollection<PHONG> dsPhong;
        public ObservableCollection<PHONG> DsPhong
        {
            get { return dsPhong; }
            set
            {
                if (dsPhong != value)
                {
                    dsPhong = value;
                    OnPropertyChanged(nameof(DsPhong));
                }
            }
        }

        private PHONG phongDuocChon;
        public PHONG PhongDuocChon
        {
            get { return phongDuocChon; }
            set
            {
                if (phongDuocChon != value)
                {
                    phongDuocChon = value;
                    OnPropertyChanged(nameof(PhongDuocChon));

                    MaPhieuThue = TaoMaPhieuThue();
                }
            }
        }

        private string maPhieuThue;
        public string MaPhieuThue
        {
            get { return maPhieuThue; }
            set
            {
                if (maPhieuThue != value)
                {
                    maPhieuThue = value;
                    OnPropertyChanged(nameof(MaPhieuThue));
                }
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        private KHACHHANG _khachHangDuocChon;
        public KHACHHANG KhachHangDuocChon
        {
            get { return _khachHangDuocChon; }
            set
            {
                if (_khachHangDuocChon != value)
                {
                    _khachHangDuocChon = value;
                    OnPropertyChanged(nameof(KhachHangDuocChon));
                    CapnhatKhachHangDuocChon();
                }
            }
        }
        public ICommand TaoPhieuThueCommand { get; }
        public ICommand ThemKhachHangCommand { get; }
        public ICommand TimKiemKhachHangCommand { get; }
        public LapPhieuThueViewModel()
        {
            DSKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Instance.DB.KHACHHANGs);
            DsPhong = new ObservableCollection<PHONG>(DataProvider.Instance.DB.PHONGs);

            var thamsos = DataProvider.Instance.DB.THAMSOes.FirstOrDefault();
            SoKhachToiDa = thamsos?.SoKhachHangToiDa ?? 3;

            DSKhachHangTrongPhieuThue = new ObservableCollection<KhachHangTrongPhieuThue>();
            for (int i = 0; i < SoKhachToiDa; i++) {
                DSKhachHangTrongPhieuThue.Add(new KhachHangTrongPhieuThue { STT = i + 1, DSKhachHang = DSKhachHang });
            }
            TaoPhieuThueCommand = new RelayCommand<object>( (p) => CanTaoPhieuThue() ,(p) => TaoPhieuThue());
            ThemKhachHangCommand = new RelayCommand<object>((p) => true, (p) => { ThemKhachHangWindow window = new ThemKhachHangWindow(); window.Show(); });
            TimKiemKhachHangCommand = new RelayCommand<object>((p) => true, (p) => { TimKiemKhachHangWindow window = new TimKiemKhachHangWindow(); window.Show(); });

        }

        bool CanTaoPhieuThue()
        {
            if (PhongDuocChon == null || string.IsNullOrEmpty(MaPhieuThue) || DSKhachHangTrongPhieuThue.Count == 0 || StartDate == null || EndDate == null)
            {
                return false;
            }

            // Kiểm tra ngày thuê
            if (StartDate >= EndDate)
            {
                MessageBox.Show("Ngày bắt đầu thuê phải trước ngày kết thúc thuê.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                StartDate = null;
                EndDate = null;
                return false;
            }

            // Kiểm tra phòng đã được thuê chưa
            var existingPhieuThue = DataProvider.Instance.DB.PHIEUTHUEs
                .FirstOrDefault(pt => pt.MaPhong == PhongDuocChon.MaPhong &&
                                      pt.NgayBatDauThue < EndDate &&
                                      pt.NgayKetThucThue > StartDate);

            if (existingPhieuThue != null)
            {
                MessageBox.Show("Phòng đã được thuê trong khoảng thời gian này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                StartDate = null;
                EndDate = null;
                return false;
            }    

             return true;
        }

        void TaoPhieuThue()
        {
            List<KHACHHANG> KHACHHANGs = new List<KHACHHANG>();
            foreach (var kh in DSKhachHangTrongPhieuThue)
            {
                if (kh.KhachHang != null)
                {
                    KHACHHANGs.Add(kh.KhachHang);
                }
            }

            PHIEUTHUE phieuThue = new PHIEUTHUE
            {
                MaPhieuThue = MaPhieuThue,
                MaPhong = PhongDuocChon.MaPhong,
                NgayBatDauThue = StartDate,
                NgayKetThucThue = EndDate,
                KHACHHANGs = KHACHHANGs,
                PHONG = PhongDuocChon,
            };

            DataProvider.Instance.DB.PHIEUTHUEs.Add(phieuThue);
            DataProvider.Instance.DB.SaveChanges();

            // Reset các trường
            PhongDuocChon = null;
            MaPhieuThue = string.Empty;
            StartDate = null;
            EndDate = null;
            DSKhachHangTrongPhieuThue.Clear();

            MessageBox.Show("Tạo phiếu thuê thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string TaoMaPhieuThue()
        {
            int number = DataProvider.Instance.DB.PHIEUTHUEs.Count() + 1;
            return "PT" + number.ToString("D4");
        }

        private void CapnhatKhachHangDuocChon()
        {
            OnPropertyChanged(nameof(DSKhachHangTrongPhieuThue));
        }
    }
}

// Model dữ liệu hàng trong bảng
public class KhachHangTrongPhieuThue : INotifyPropertyChanged
{
    public int STT { get; set; }

    private KHACHHANG _khachHang;
    public KHACHHANG KhachHang
    {
        get => _khachHang;
        set
        {
            _khachHang = value;
            OnPropertyChanged(nameof(KhachHang));
            OnPropertyChanged(nameof(LoaiKhach));
            OnPropertyChanged(nameof(CMND));
            OnPropertyChanged(nameof(DiaChi));
        }
    }

    public string LoaiKhach
    {
        get
        {
            if (KhachHang != null)
            {
                LOAIKHACHHANG loaiKH = DataProvider.Instance.DB.LOAIKHACHHANGs
                    .FirstOrDefault(l => l.MaLoaiKhachHang == KhachHang.MaLoaiKhachHang);

                return loaiKH?.TenLoaiKhachHang;
            }

            return string.Empty;
        }
    }

    public string CMND => KhachHang?.CMND ?? "";
    public string DiaChi => KhachHang?.DiaChi ?? "";

    public ObservableCollection<KHACHHANG> DSKhachHang { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}