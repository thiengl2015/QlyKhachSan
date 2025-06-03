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
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class LapHoaDonThanhToanViewModel : BaseViewModel
    {
        private ObservableCollection<NGUOITHANHTOAN> dsNguoiThanhToan;
        public ObservableCollection<NGUOITHANHTOAN> DSNguoiThanhToan
        {
            get { return dsNguoiThanhToan; }
            set
            {
                if (dsNguoiThanhToan != value)
                {
                    dsNguoiThanhToan = value;
                    OnPropertyChanged(nameof(DSNguoiThanhToan));
                }
            }
        }

        private NGUOITHANHTOAN nguoiThanhToanDuocChon;
        public NGUOITHANHTOAN NguoiThanhToanDuocChon
        {
            get { return nguoiThanhToanDuocChon; }
            set
            {
                if (nguoiThanhToanDuocChon != value)
                {
                    nguoiThanhToanDuocChon = value;
                    OnPropertyChanged(nameof(NguoiThanhToanDuocChon));

                    MaHoaDon = TaoMaHoaDon();
                    DiaChi = nguoiThanhToanDuocChon.DiaChi;

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

        private DateTime ngayThanhToan;
        public DateTime NgayThanhToan
        {
            get { return ngayThanhToan; }
            set
            {
                if (ngayThanhToan != value)
                {
                    ngayThanhToan = value;
                    OnPropertyChanged(nameof(NgayThanhToan));
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
        public ICommand LapHoaDonCommand { get; }
        public ICommand TimKhachHangCoQuanCommand { get; }
        public ICommand Thoat { get; }


        public LapHoaDonThanhToanViewModel()
        {
            DSNguoiThanhToan = new ObservableCollection<NGUOITHANHTOAN>(DataProvider.Instance.DB.NGUOITHANHTOANs);
            NgayThanhToan = DateTime.Now;

            var allPhongs = new ObservableCollection<PHONG>(DataProvider.Instance.DB.PHONGs);
            DsPhong = new ObservableCollection<PHONG>(allPhongs.Where(p => p.PHIEUTHUEs.Any(pt => pt.MaPhong == p.MaPhong && pt.DaThanhToan == 0)));

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
            TimKhachHangCoQuanCommand = new RelayCommand<object>((p) => true, (p) => { TimKhachHangCoQuanWindow window = new TimKhachHangCoQuanWindow(); window.Show(); });
            LapHoaDonCommand = new RelayCommand<object>((p) => CanLapHoaDon(), (p) => LapHoaDon());
        }

        private bool CanLapHoaDon()
        {
            return NguoiThanhToanDuocChon != null && DanhSachPhongThanhToan.Any(p => p.SoNgayThue > 0);
        }

        private void LapHoaDon()
        {
            HOADON hoaDon = new HOADON
            {
                MaHoaDon = MaHoaDon,
                MaNguoiThanhToan = NguoiThanhToanDuocChon.MaNguoiThanhToan,
                TriGia = (int)TriGia,
                NgayThanhToan = NgayThanhToan,
            };

            DataProvider.Instance.DB.HOADONs.Add(hoaDon);

            foreach (var phong in DanhSachPhongThanhToan)
            {
                if (phong.Phong == null || phong.SoNgayThue <= 0)
                    continue;

                PHIEUTHUE pThue = DataProvider.Instance.DB.PHIEUTHUEs
                    .FirstOrDefault(pt => pt.MaPhong == phong.Phong.MaPhong);

                CHITIETHOADON chiTietHoaDon = new CHITIETHOADON
                {
                    MaHoaDon = hoaDon.MaHoaDon,
                    MaPhieuThue = pThue?.MaPhieuThue,
                    SoNgayThue = phong.SoNgayThue,
                    SoKhach = phong.SoKhach,
                    ThanhTien = (int)phong.ThanhTien
                };

                pThue.DaThanhToan = 1;

                DataProvider.Instance.DB.CHITIETHOADONs.Add(chiTietHoaDon);
            }

            DataProvider.Instance.DB.SaveChanges();

            // Reset fields
            MaHoaDon = string.Empty;
            NguoiThanhToanDuocChon = null;
            TriGia = 0;
            NgayThanhToan = DateTime.Now;
            DiaChi = string.Empty;
            DanhSachPhongThanhToan.Clear();

            MessageBox.Show("Lập hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string TaoMaHoaDon()
        {
            int number = DataProvider.Instance.DB.HOADONs.Count() + 1;
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
