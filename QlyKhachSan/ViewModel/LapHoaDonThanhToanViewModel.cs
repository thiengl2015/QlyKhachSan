using QlyKhachSan.Model;
using QlyKhachSan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ChiTietHoaDon;

namespace QlyKhachSan.ViewModel
{
    public class LapHoaDonThanhToanViewModel : BaseViewModel
    {
        public ObservableCollection<Phong> DSPhong { get; set; }
        public ObservableCollection<ChiTietHoaDon> DSPhongTrongHoaDon { get; set; }
        public ObservableCollection<LoaiPhong> DSLoaiPhong { get; set; }
        public ObservableCollection<KhachHang> DSKhachHang { get; set; }
        public ObservableCollection<PhieuThue> DSPhieuThue { get; set; }

        public ICommand LapHoaDonThanhToanCommand { get; }
        public ICommand TimKhachHangCoQuanCommand { get; }
        public ICommand ThoatCommand { get; }

        public LapHoaDonThanhToanViewModel()
        {
            DSPhong = new ObservableCollection<Phong>
        {
            new Phong("P001", "Phòng A", "LP01", "Note 1"),
            new Phong("P002", "Phòng B", "LP02", "Note 2")
        };

            DSPhongTrongHoaDon = new ObservableCollection<ChiTietHoaDon>
        {
            new ChiTietHoaDon { STT = 1, NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now.AddDays(2) },
            new ChiTietHoaDon { STT = 2, NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now.AddDays(1) }
        };

            LapHoaDonThanhToanCommand = new RelayCommand<object>((p) => true, (p) => TaoHoaDon());
            TimKhachHangCoQuanCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TimKhachHangCoQuanWindow window = new TimKhachHangCoQuanWindow();
                window.Show();
            });
            ThoatCommand = new RelayCommand<object>((p) => true, (p) => Application.Current.Shutdown());
        }

        void TaoHoaDon()
        {
            // Lưu DSPhongTrongHoaDon xuống DB
        }
    }
}
public class Phong
{
    public string MaPhong { get; set; }
    public string TenPhong { get; set; }
    public string MaLoaiPhong { get; set; }
    public string GhiChu { get; set; }

    public Phong(string maPhong, string tenPhong, string maLoaiPhong, string ghiChu)
    {
        MaPhong = maPhong;
        TenPhong = tenPhong;
        MaLoaiPhong = maLoaiPhong;
        GhiChu = ghiChu;
    }

}
public class LoaiPhong
{
    public string MaLoaiPhong { get; set; }
    public double DonGia { get; set; }
}
public class PhieuThue
{
    public string MaPhieuThue { get; set; }
    public string MaPhong { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public Boolean DaThanhToan
    {
        get; set;
    }

}
public class ChiTietPhieuThue
{
    public string MaPhieuThue
    {
        get; set;
    }
    public string MaKhachHang
    {
        get; set;

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


