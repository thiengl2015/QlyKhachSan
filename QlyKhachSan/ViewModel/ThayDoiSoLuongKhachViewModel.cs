using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiSoLuongKhachViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> LoaiPhongList { get; set; }

        private string _loaiPhongSelected;
        public string LoaiPhongSelected
        {
            get => _loaiPhongSelected;
            set
            {
                _loaiPhongSelected = value;
                OnPropertyChanged(nameof(LoaiPhongSelected));
                // Cập nhật số lượng hiện tại khi chọn loại phòng
                SoLuongHienTai = "4"; // sau này load từ DB
            }
        }

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
            LoaiPhongList = new ObservableCollection<string> { "Phòng đơn", "Phòng đôi", "Phòng VIP" };
            LuuCommand = new RelayCommand(Luu);
        }

        private void Luu()
        {
            System.Windows.MessageBox.Show($"Đã cập nhật số lượng khách tối đa cho '{LoaiPhongSelected}' thành: {SoLuongMoi}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
