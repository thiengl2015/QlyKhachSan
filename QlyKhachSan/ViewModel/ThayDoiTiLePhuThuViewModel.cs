using System.ComponentModel;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiTiLePhuThuViewModel : INotifyPropertyChanged
    {
        private string _tiLeHienTai;
        public string TiLeHienTai
        {
            get => _tiLeHienTai;
            set
            {
                _tiLeHienTai = value;
                OnPropertyChanged(nameof(TiLeHienTai));
            }
        }

        private string _tiLeMoi;
        public string TiLeMoi
        {
            get => _tiLeMoi;
            set
            {
                _tiLeMoi = value;
                OnPropertyChanged(nameof(TiLeMoi));
            }
        }

        public ICommand LuuCommand { get; }

        public ThayDoiTiLePhuThuViewModel()
        {
            TiLeHienTai = "25"; // Giả định phụ thu hiện tại là 25%
            LuuCommand = new RelayCommand(Luu);
        }

        private void Luu()
        {
            System.Windows.MessageBox.Show($"Đã cập nhật tỉ lệ phụ thu mới thành: {TiLeMoi}%");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
