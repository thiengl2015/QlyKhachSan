using QlyKhachSan.Model;
using QlyKhachSan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QlyKhachSan.ViewModel
{
    public class LapHoaDonThanhToanViewModel : BaseViewModel
    {
        private ObservableCollection<KHACHHANG> dsKhachHang;
        public ObservableCollection<KHACHHANG> DSKhachHang
        {
            get { return dsKhachHang; }
            set
            {
                if (dsKhachHang != value)
                {
                    dsKhachHang = value;
                    OnPropertyChanged(nameof(DSKhachHang));
                }
            }
        }

        private KHACHHANG khachHangDuocChon;
        public KHACHHANG KhachHangDuocChon
        {
            get { return khachHangDuocChon; }
            set
            {
                if (khachHangDuocChon != value)
                {
                    khachHangDuocChon = value;
                    OnPropertyChanged(nameof(KhachHangDuocChon));

                    MaHoaDon = TaoMaHoaDon();
                    DiaChi = khachHangDuocChon.DiaChi;

                }
            }
        }

        private string maHoaDon;
        public string MaHoaDon
        {
            get { return maHoaDon; }
            set
            {
                if (maHoaDon != value)
                {
                    maHoaDon = value;
                    OnPropertyChanged(nameof(MaHoaDon));
                }
            }
        }

        private string diaChi;
        public string DiaChi
        {
            get { return diaChi; }
            set
            {
                if (diaChi != value)
                {
                    diaChi = value;
                    OnPropertyChanged(nameof(DiaChi));
                }
            }
        }

        private long triGia;
        public long TriGia
        {
            get { return triGia; }
            set
            {
                if (triGia != value)
                {
                    triGia = value;
                    OnPropertyChanged(nameof(TriGia));
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

        private ObservableCollection<PhongThanhToan> danhSachPhongThanhToan;
        public ObservableCollection<PhongThanhToan> DanhSachPhongThanhToan
        {
            get { return danhSachPhongThanhToan; }
            set
            {
                if (danhSachPhongThanhToan != null)
                {
                    foreach (var item in danhSachPhongThanhToan)
                        item.PropertyChanged -= PhongThanhToan_PropertyChanged;
                }

                danhSachPhongThanhToan = value;

                if (danhSachPhongThanhToan != null)
                {
                    foreach (var item in danhSachPhongThanhToan)
                        item.PropertyChanged += PhongThanhToan_PropertyChanged;
                }

                OnPropertyChanged(nameof(DanhSachPhongThanhToan));
                TriGia = DanhSachPhongThanhToan.Sum(p => p.ThanhTien);
            }
        }

        private void PhongThanhToan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TriGia = DanhSachPhongThanhToan.Sum(p => p.ThanhTien);
        }

        public LapHoaDonThanhToanViewModel()
        {
            DSKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Instance.DB.KHACHHANGs);

            var allPhongs = new ObservableCollection<PHONG>(DataProvider.Instance.DB.PHONGs);
            DsPhong = new ObservableCollection<PHONG>(allPhongs.Where(p => p.PHIEUTHUEs.Any(pt => pt.MaPhong == p.MaPhong)));

            DanhSachPhongThanhToan = new ObservableCollection<PhongThanhToan>();
            for (int i = 0; i < DsPhong.Count; i++)
            {
                var newPhong = new PhongThanhToan
                {
                    STT = i + 1,
                    DsPhong = DsPhong,
                };

                newPhong.PropertyChanged += PhongThanhToan_PropertyChanged;

                DanhSachPhongThanhToan.Add(newPhong);
            }
        }

        private string TaoMaHoaDon()
        {
            int number = DataProvider.Instance.DB.PHIEUTHUEs.Count() + 1;
            return "HD" + number.ToString("D4");
        }
    }
}

public class PhongThanhToan : INotifyPropertyChanged
{
    public int STT { get; set; }

    private PHONG phong;
    public PHONG Phong
    {
        get => phong;
        set
        {
            phong = value;
            OnPropertyChanged(nameof(Phong));
            OnPropertyChanged(nameof(SoNgayThue));
            OnPropertyChanged(nameof(DonGia));
            OnPropertyChanged(nameof(SoKhach));
            OnPropertyChanged(nameof(ThanhTien));
        }
    }

    public int SoNgayThue
    {
        get
        {
            if (Phong == null)
                return 0;

            PHIEUTHUE phieuThue = DataProvider.Instance.DB.PHIEUTHUEs
                .FirstOrDefault(pt => pt.MaPhong == Phong.MaPhong);

            if (phieuThue != null)
            {
                int days = (phieuThue.NgayKetThucThue.Value - phieuThue.NgayBatDauThue.Value).Days;
                return days;
            }

            return 0;
        }
    }

    public int DonGia
    {
        get
        {
            if (Phong == null)
                return 0;

            LOAIPHONG loaiPhong = DataProvider.Instance.DB.LOAIPHONGs
                .FirstOrDefault(lp => lp.MaLoaiPhong == Phong.MaLoaiPhong);

            if (loaiPhong != null)
            {
                return loaiPhong.DonGia ?? 0;
            }

            return 0;
        }
    }

    public int SoKhach
    {
        get
        {
            if (Phong == null)
                return 0;

            PHIEUTHUE phieuThue = DataProvider.Instance.DB.PHIEUTHUEs
                .FirstOrDefault(pt => pt.MaPhong == Phong.MaPhong);

            if (phieuThue != null)
            {
                return phieuThue.KHACHHANGs.Count;
            }

            return 0;
        }
    }

    public long ThanhTien
    {
        get
        {
            if (Phong == null)
                return 0;

            PHIEUTHUE phieuThue = DataProvider.Instance.DB.PHIEUTHUEs
                .FirstOrDefault(pt => pt.MaPhong == Phong.MaPhong);

            if (phieuThue != null)
            {
                float phuThu = 0;
                if (phieuThue.KHACHHANGs.Count > 2)
                {
                    phuThu = (phieuThue.KHACHHANGs.Count - 2) * DonGia;
                }
                
                float heSo = 1;
                foreach (KHACHHANG kHACHHANG in phieuThue.KHACHHANGs)
                {
                    if (kHACHHANG.MaLoaiKhachHang == "LKH2")
                    {
                        heSo = 1.5f;
                        break;
                    }
                }

                return (long)((SoNgayThue * DonGia + phuThu) * heSo);
            }

            return 0;
        }
    }

    public ObservableCollection<PHONG> DsPhong { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
    // Model dữ liệu hàng trong bảng
    public class ChiTietHoaDon : INotifyPropertyChanged
{
    public int STT { get; set; }

    private Phong _phongDuocChon;
    public Phong PhongDuocChon
    {
        get => _phongDuocChon;
        set
        {
            _phongDuocChon = value;
            OnPropertyChanged(nameof(PhongDuocChon));
            CapNhatThongTinPhong(); // <-- gọi tính toán
        }
    }

    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }

    public int SoNgayThue => (NgayKetThuc - NgayBatDau).Days;

    private double _donGia;
    public double DonGia
    {
        get => _donGia;
        set { _donGia = value; OnPropertyChanged(nameof(DonGia)); OnPropertyChanged(nameof(ThanhTien)); }
    }

    public int SoKhach { get; set; } // Gán thủ công hoặc lấy từ DB
    public double PhuThu { get; set; }
    public double HeSo { get; set; } = 1;

    public double ThanhTien => (SoNgayThue * DonGia + PhuThu) * HeSo;

    public void CapNhatThongTinPhong(
    ObservableCollection<LoaiPhong> loaiPhongs,
    ObservableCollection<PhieuThue> phieuThues,
    ObservableCollection<ChiTietPhieuThue> ctPhieuThues,
    bool quyDinhPhuThu,
    bool quyDinhHeSo,
    double tyLePhuThu)
    {
        if (PhongDuocChon == null) return;

        // 1. Lấy đơn giá từ loại phòng
        var loai = loaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == PhongDuocChon.MaLoaiPhong);
        DonGia = loai?.DonGia ?? 0;

        // 2. Tìm phiếu thuê liên quan đến phòng
        var phieu = phieuThues.FirstOrDefault(p => p.MaPhong == PhongDuocChon.MaPhong);
        if (phieu != null)
        {
            NgayBatDau = phieu.NgayBatDau;
            NgayKetThuc = phieu.NgayKetThuc;

            // 3. Đếm số khách trong phiếu thuê
            var ct = ctPhieuThues.Where(c => c.MaPhieuThue == phieu.MaPhieuThue).ToList();
            SoKhach = ct.Count;

            // 4. Phụ thu
            PhuThu = (quyDinhPhuThu && SoKhach > 2)
                ? tyLePhuThu * (SoKhach - 2) * DonGia
                : 0;

            // 5. Hệ số khách nước ngoài
            HeSo = (quyDinhHeSo && ct.Any(c => c.LaKhachNuocNgoai)) ? 1.5 : 1;
        }

        OnPropertyChanged(nameof(ThanhTien));
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}


