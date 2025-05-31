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
        public ObservableCollection<KhachHang> DSKhachHang { get; set; }
        public ObservableCollection<KhachHangTrongPhieuThue> DSKhachHangTrongPhieuThue { get; set; }

        private int _soKhachToiDa = 3;
        public int SoKhachToiDa
        {
            get
            {
                return _soKhachToiDa;
            }
        }

        private KhachHang _khachHangDuocChon;
        public KhachHang KhachHangDuocChon
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
            DSKhachHangTrongPhieuThue = new ObservableCollection<KhachHangTrongPhieuThue>();
            for (int i = 0; i < SoKhachToiDa; i++) {
                DSKhachHangTrongPhieuThue.Add(new KhachHangTrongPhieuThue { STT = i + 1, DSKhachHang = DSKhachHang });
            }
            TaoPhieuThueCommand = new RelayCommand<object>( (p) => true ,(p) => TaoPhieuThue());
            ThemKhachHangCommand = new RelayCommand<object>((p) => true, (p) => { ThemKhachHangWindow window = new ThemKhachHangWindow(); window.Show(); });
            TimKiemKhachHangCommand = new RelayCommand<object>((p) => true, (p) => { TimKiemKhachHangWindow window = new TimKiemKhachHangWindow(); window.Show(); });

        }

        void TaoPhieuThue()
        {

        }

        private void CapnhatKhachHangDuocChon()
        {
            OnPropertyChanged(nameof(DSKhachHangTrongPhieuThue));
        }
    }
}
public class KhachHang
{
    public long MaKH { get; set; }
    public string Ten { get; set; }
    public long LoaiKH { get; set; }
    public string CMND { get; set; }
    public string DiaChi { get; set; }

     public KhachHang(long MaKH, string Ten, long MaLoaiKH, string CMND, string DiaCHi) {
        this.MaKH = MaKH;
        this.Ten = Ten;
        this.LoaiKH = MaLoaiKH;
        this.CMND = CMND;
        this.DiaChi = DiaCHi;
    }
}

// Model dữ liệu hàng trong bảng
public class KhachHangTrongPhieuThue : INotifyPropertyChanged
{
    public int STT { get; set; }
    public ObservableCollection<KhachHang> DSKhachHang { get; set; }

    private KhachHang _khachHangDuocChon;
    public KhachHang KhachHangDuocChon
    {
        get { return _khachHangDuocChon; }
        set
        {
            if (_khachHangDuocChon != value)
            {
                _khachHangDuocChon = value;
                OnPropertyChanged(nameof(KhachHangDuocChon));
                OnPropertyChanged(nameof(DSKhachHang));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}