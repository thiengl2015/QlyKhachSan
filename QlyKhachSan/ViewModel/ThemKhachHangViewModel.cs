using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class ThemKhachHangViewModel : BaseViewModel
    {
        private string _maKH;
        public string MaKH
        {
            get { return _maKH; }
            set {
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

        private long _loaiKH;
        public long LoaiKH
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

        private ObservableCollection<LoaiKhachHang> DSLoaiKhachHang { get; set; }

        public ICommand ThemKhachHang { get; }

        public ThemKhachHangViewModel(){
            ThemKhachHang = new RelayCommand<object>((p) => true, (p) => ThemKhachHangMoi());
        }

        private void ThemKhachHangMoi()
        {
            KhachHang newKH = new KhachHang(TaoMaKhachHangMoi(),_tenKH,_loaiKH,_cmnd,_diaChi);
            //Luu vao DB
        }

        private long TaoMaKhachHangMoi()
        {
            //tao random id
            return 1;
        }
    }
}

public class LoaiKhachHang
{
    public long maLoaiKH { get; set; }
    public String tenLoaiKH { get; set; }
    public String heSo {  get; set; }
}
