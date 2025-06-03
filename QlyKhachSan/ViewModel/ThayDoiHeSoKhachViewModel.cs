using QlyKhachSan.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiHeSoKhachViewModel : BaseViewModel
    {
        private float heSoHienTai;
        public float HeSoHienTai
        {
            get => heSoHienTai;
            set
            {
                heSoHienTai = value;
                OnPropertyChanged(nameof(HeSoHienTai));
            }
        }

        private float heSoMoi;
        public float HeSoMoi
        {
            get => heSoMoi;
            set
            {
                heSoMoi = value;
                OnPropertyChanged(nameof(HeSoMoi));
            }
        }

        private ObservableCollection<LOAIKHACHHANG> loaiKhachHangList;
        public ObservableCollection<LOAIKHACHHANG> LoaiKhachHangList
        {
            get => loaiKhachHangList;
            set
            {
                loaiKhachHangList = value;
                OnPropertyChanged(nameof(LoaiKhachHangList));
            }
        }

        private LOAIKHACHHANG loaiKhachHangSelected;
        public LOAIKHACHHANG LoaiKhachHangSelected
        {
            get => loaiKhachHangSelected;
            set
            {
                loaiKhachHangSelected = value;
                OnPropertyChanged(nameof(LoaiKhachHangSelected));
                if (loaiKhachHangSelected != null)
                {
                    HeSoHienTai = (float)(loaiKhachHangSelected.HeSo ?? 0);
                }
            }
        }

        public ICommand LuuCommand { get; set; }

        public ThayDoiHeSoKhachViewModel()
        {
            LoaiKhachHangList = new ObservableCollection<LOAIKHACHHANG>(DataProvider.Instance.DB.LOAIKHACHHANGs);
            LoaiKhachHangSelected = LoaiKhachHangList.FirstOrDefault();

            LuuCommand = new RelayCommand<object>((p) => CanExecuteLuu(), (p) => LuuHeSo());
        }

        private bool CanExecuteLuu()
        {
            return LoaiKhachHangSelected != null && HeSoMoi > 0;
        }

        private void LuuHeSo()
        {
            if (LoaiKhachHangSelected != null)
            {
                LoaiKhachHangSelected.HeSo = HeSoMoi;
                DataProvider.Instance.DB.SaveChanges();
                HeSoHienTai = HeSoMoi;
            }
        }
    }
}
