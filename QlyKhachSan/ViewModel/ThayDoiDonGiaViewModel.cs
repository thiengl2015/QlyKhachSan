using QlyKhachSan.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiDonGiaViewModel : BaseViewModel
    {
        private int _donGiaHienTai;
        public int DonGiaHienTai
        {
            get => _donGiaHienTai;
            set
            {
                _donGiaHienTai = value;
                OnPropertyChanged(nameof(DonGiaHienTai));
            }
        }

        private int donGiaMoi;
        public int DonGiaMoi
        {
            get => donGiaMoi;
            set
            {
                donGiaMoi = value;
                OnPropertyChanged(nameof(DonGiaMoi));
            }
        }

        private ObservableCollection<LOAIPHONG> loaiPhongList;
        public ObservableCollection<LOAIPHONG> LoaiPhongList
        {
            get => loaiPhongList;
            set
            {
                loaiPhongList = value;
                OnPropertyChanged(nameof(LoaiPhongList));
            }
        }

        private LOAIPHONG loaiPhongSelected;
        public LOAIPHONG LoaiPhongSelected
        {
            get => loaiPhongSelected;
            set
            {
                loaiPhongSelected = value;
                OnPropertyChanged(nameof(LoaiPhongSelected));
                if (loaiPhongSelected != null)
                {
                    DonGiaHienTai = loaiPhongSelected.DonGia ?? 0;
                }
            }
        }

        public ICommand LuuCommand { get; set; }

        public ThayDoiDonGiaViewModel()
        {
            LoaiPhongList = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs);
            LoaiPhongSelected = LoaiPhongList.FirstOrDefault();

            LuuCommand = new RelayCommand<object>((p) => CanExecuteLuu(), (p) => LuuDonGia());
        }

        private bool CanExecuteLuu()
        {
            return LoaiPhongSelected != null && DonGiaMoi > 0;
        }

        private void LuuDonGia()
        {
            if (LoaiPhongSelected != null)
            {
                LoaiPhongSelected.DonGia = DonGiaMoi;
                DataProvider.Instance.DB.SaveChanges();
                DonGiaHienTai = DonGiaMoi;
            }
        }
    }
}
