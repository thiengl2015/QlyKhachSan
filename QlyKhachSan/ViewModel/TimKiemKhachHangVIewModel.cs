using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class TimKiemKhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<KhachHangCanTim> _dsKhachHang;
        public ObservableCollection<KhachHangCanTim> DSKhachHang
        {
            get { return _dsKhachHang; }
            set
            {
                if (_dsKhachHang != value)
                {
                    _dsKhachHang = value;
                    OnPropertyChanged(nameof(DSKhachHang));
                }
            }
        }

        private string _maKH;
        public string MaKH
        {
            get { return _maKH; }
            set
            {
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

        private ObservableCollection<LOAIKHACHHANG> _dsLoaiKhachHang;
        public ObservableCollection<LOAIKHACHHANG> DsLoaiKhachHang
        {
            get { return _dsLoaiKhachHang; }
            set
            {
                if (_dsLoaiKhachHang != value)
                {
                    _dsLoaiKhachHang = value;
                    OnPropertyChanged(nameof(DsLoaiKhachHang));
                }
            }
        }

        private LOAIKHACHHANG _loaiKH;
        public LOAIKHACHHANG LoaiKH
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

        public ICommand TimKhachHangCommand { get; set; }

        public TimKiemKhachHangViewModel()
        {
            DsLoaiKhachHang = new ObservableCollection<LOAIKHACHHANG>(DataProvider.Instance.DB.LOAIKHACHHANGs);
            TimKhachHangCommand = new RelayCommand<object>((p) => true, (p) => TimKhachHangMoi());
        }

        private void TimKhachHangMoi()
        {
            DSKhachHang = new ObservableCollection<KhachHangCanTim>();

            string maLoaiKH = LoaiKH?.MaLoaiKhachHang;

            var list = DataProvider.Instance.DB.KHACHHANGs
                .Where(kh =>
                    (string.IsNullOrEmpty(MaKH) || kh.MaKhachHang.Contains(MaKH)) &&
                    (string.IsNullOrEmpty(TenKH) || kh.TenKhachHang.Contains(TenKH)) &&
                    (string.IsNullOrEmpty(CMND) || kh.CMND.Contains(CMND)) &&
                    (string.IsNullOrEmpty(DiaChi) || kh.DiaChi.Contains(DiaChi)) &&
                    (string.IsNullOrEmpty(maLoaiKH) || kh.MaLoaiKhachHang == maLoaiKH))
                .ToList();

            MessageBox.Show($"Tìm thấy {list.Count} khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (var kh in list)
            {
                DSKhachHang.Add(new KhachHangCanTim { KhachHang = kh });
            }
        }

        public class KhachHangCanTim : INotifyPropertyChanged
        {
            public KHACHHANG KhachHang { get; set; }

            public string MaKH => KhachHang.MaKhachHang;

            public string TenKH => KhachHang.TenKhachHang;

            public string LoaiKH
            {
                get
                {
                    LOAIKHACHHANG loaiKhachHang = DataProvider.Instance.DB.LOAIKHACHHANGs.FirstOrDefault(l => l.MaLoaiKhachHang == KhachHang.MaLoaiKhachHang);
                    return loaiKhachHang?.TenLoaiKhachHang;
                }
            }

            public string CMND => KhachHang.CMND;
            public string DiaChi => KhachHang.DiaChi;

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
