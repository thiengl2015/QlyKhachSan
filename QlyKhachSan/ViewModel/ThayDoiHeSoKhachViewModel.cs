using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiHeSoKhachViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> LoaiKhachList { get; set; }
        private string _loaiKhachSelected;
        public string LoaiKhachSelected
        {
            get => _loaiKhachSelected;
            set
            {
                _loaiKhachSelected = value;
                OnPropertyChanged(nameof(LoaiKhachSelected));
                // Cập nhật hệ số hiện tại tương ứng
                HeSoHienTai = "1.0"; // sau này load từ DB
            }
        }

        private string _heSoHienTai;
        public string HeSoHienTai
        {
            get => _heSoHienTai;
            set
            {
                _heSoHienTai = value;
                OnPropertyChanged(nameof(HeSoHienTai));
            }
        }

        private string _heSoMoi;
        public string HeSoMoi
        {
            get => _heSoMoi;
            set
            {
                _heSoMoi = value;
                OnPropertyChanged(nameof(HeSoMoi));
            }
        }

        public ICommand LuuCommand { get; }

        public ThayDoiHeSoKhachViewModel()
        {
            // Danh sách loại khách giả lập, bạn thay bằng truy vấn từ DB
            LoaiKhachList = new ObservableCollection<string> { "Nội địa", "Nước ngoài", "VIP" };
            LuuCommand = new RelayCommand(Luu);
        }

        private void Luu()
        {
            System.Windows.MessageBox.Show($"Đã cập nhật hệ số '{LoaiKhachSelected}' thành: {HeSoMoi}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
