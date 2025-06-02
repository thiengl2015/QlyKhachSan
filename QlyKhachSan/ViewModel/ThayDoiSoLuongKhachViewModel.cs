using System.ComponentModel;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiSoLuongKhachViewModel : INotifyPropertyChanged
    {
        private string _soLuongHienTai;
        public string SoLuongHienTai
        {
            get => _soLuongHienTai;
            set
            {
                _soLuongHienTai = value;
                OnPropertyChanged(nameof(SoLuongHienTai));
            }
        }

        private string _soLuongMoi;
        public string SoLuongMoi
        {
            get => _soLuongMoi;
            set
            {
                _soLuongMoi = value;
                OnPropertyChanged(nameof(SoLuongMoi));
            }
        }

        public ICommand LuuCommand { get; }

        public ThayDoiSoLuongKhachViewModel()
        {
            SoLuongHienTai = "4"; // Giả định từ CSDL
            LuuCommand = new RelayCommand(Luu);
        }

        private void Luu()
        {
            System.Windows.MessageBox.Show($"Đã cập nhật số lượng khách tối đa thành: {SoLuongMoi}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
