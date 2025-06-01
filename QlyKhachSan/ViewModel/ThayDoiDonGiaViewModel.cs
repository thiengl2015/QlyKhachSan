using System.ComponentModel;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class ThayDoiDonGiaViewModel : INotifyPropertyChanged
    {
        private string _donGia;
        public string DonGia
        {
            get => _donGia;
            set
            {
                _donGia = value;
                OnPropertyChanged(nameof(DonGia));
            }
        }

        public ICommand ThayDoiCommand { get; }

        public ThayDoiDonGiaViewModel()
        {
            ThayDoiCommand = new RelayCommand(ThayDoi);
            DonGia = "<Giá trị cũ>"; // Giá trị mặc định
        }

        private void ThayDoi()
        {
            // Thực hiện xử lý thay đổi đơn giá
            System.Windows.MessageBox.Show($"Đã thay đổi đơn giá thành: {DonGia}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
