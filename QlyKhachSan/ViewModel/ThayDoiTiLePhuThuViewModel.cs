using QlyKhachSan.Model;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiTiLePhuThuViewModel : BaseViewModel
    {
        private float _tiLeHienTai;
        public float TiLeHienTai
        {
            get => _tiLeHienTai;
            set
            {
                _tiLeHienTai = value;
                OnPropertyChanged(nameof(TiLeHienTai));
            }
        }

        private float _tiLeMoi;
        public float TiLeMoi
        {
            get => _tiLeMoi;
            set
            {
                _tiLeMoi = value;
                OnPropertyChanged(nameof(TiLeMoi));
            }
        }

        public ICommand LuuCommand { get; set; }

        public ThayDoiTiLePhuThuViewModel()
        {
            THAMSO thamSo = DataProvider.Instance.DB.THAMSOes.FirstOrDefault();

            TiLeHienTai = (float)(thamSo?.TyLePhuThu ?? 0);

            LuuCommand = new RelayCommand<object>(p => CanLuu(), p => Luu());
        }

        private bool CanLuu()
        {
            return TiLeMoi > 0 && TiLeMoi != TiLeHienTai;
        }

        private void Luu()
        {
            THAMSO thamSo = DataProvider.Instance.DB.THAMSOes.FirstOrDefault();
            thamSo.TyLePhuThu = TiLeMoi;

            DataProvider.Instance.DB.SaveChanges();

            TiLeHienTai = TiLeMoi;
        }
    }
}
