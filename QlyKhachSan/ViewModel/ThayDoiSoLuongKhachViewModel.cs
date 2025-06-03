using QlyKhachSan.Model;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiSoLuongKhachViewModel : BaseViewModel
    {
        private int _soLuongHienTai;
        public int SoLuongHienTai
        {
            get => _soLuongHienTai;
            set
            {
                _soLuongHienTai = value;
                OnPropertyChanged(nameof(SoLuongHienTai));
            }
        }

        private int _soLuongMoi;
        public int SoLuongMoi
        {
            get => _soLuongMoi;
            set
            {
                _soLuongMoi = value;
                OnPropertyChanged(nameof(SoLuongMoi));
            }
        }

        public ICommand LuuCommand { get; set; }

        public ThayDoiSoLuongKhachViewModel()
        {
            THAMSO thamSo = DataProvider.Instance.DB.THAMSOes.FirstOrDefault();

            SoLuongHienTai = thamSo?.SoKhachHangToiDa ?? 3;
            LuuCommand = new RelayCommand<object>(p => CanLuu(), p => Luu());
        }

        private bool CanLuu()
        {
            return SoLuongMoi > 0 && SoLuongMoi != SoLuongHienTai;
        }

        private void Luu()
        {
            THAMSO thamSo = DataProvider.Instance.DB.THAMSOes.FirstOrDefault();

            thamSo.SoKhachHangToiDa = SoLuongMoi;
            DataProvider.Instance.DB.SaveChanges();

            SoLuongHienTai = SoLuongMoi;
        }
    }
}
