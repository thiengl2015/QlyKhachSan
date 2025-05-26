using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class LapPhieuThueViewModel : BaseViewModel
    {
        public ObservableCollection<KhachHang> DSKhachHang { get; set; }
        public ObservableCollection<KhachHangTrongPhieuThue> DSKhachHangTrongPhieuThue { get; set; }

        private int _soKhachToiDa = 3;
        public int SoKhachToiDa
        {
            get
            {
                return _soKhachToiDa;
            }
        }

        private KhachHang _khachHangDuocChon;
        public KhachHang KhachHangDuocChon
        {
            get { return _khachHangDuocChon; }
            set
            {
                if (_khachHangDuocChon != value)
                {
                    _khachHangDuocChon = value;
                    OnPropertyChanged(nameof(KhachHangDuocChon));
                    CapnhatKhachHangDuocChon();
                }
            }
        }
        public ICommand ShowCustomerCommand { get; }
        public LapPhieuThueViewModel()
        {
            DSKhachHang = new ObservableCollection<KhachHang>
            {
            new KhachHang { Ten = "Nguyễn Văn A", LoaiKH = "VIP", CMND = "123456", DiaChi = "Hà Nội" },
            new KhachHang { Ten = "Trần Thị B", LoaiKH = "Thường", CMND = "654321", DiaChi = "TP.HCM" },
            new KhachHang { Ten = "Lê Văn C", LoaiKH = "VIP", CMND = "987654", DiaChi = "Đà Nẵng" }
            };

            DSKhachHangTrongPhieuThue = new ObservableCollection<KhachHangTrongPhieuThue>();
            for (int i = 0; i < SoKhachToiDa; i++) {
                DSKhachHangTrongPhieuThue.Add(new KhachHangTrongPhieuThue { STT = i + 1, DSKhachHang = DSKhachHang });
            }
            ShowCustomerCommand = new RelayCommand<object>( (p) => true ,(p) => HienDSThongTinKH());
        }
        void HienDSThongTinKH()
        {
            if (DSKhachHangTrongPhieuThue != null && DSKhachHangTrongPhieuThue.Count > 0)
            {
                StringBuilder messageBuilder = new StringBuilder();

                foreach (var customerRow in DSKhachHangTrongPhieuThue)
                {
                    messageBuilder.AppendLine($"STT: {customerRow.STT}");
                    messageBuilder.AppendLine($"Tên: {customerRow.KhachHangDuocChon?.Ten ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"Loại: {customerRow.KhachHangDuocChon?.LoaiKH ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"CMND: {customerRow.KhachHangDuocChon?.CMND ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"Địa chỉ: {customerRow.KhachHangDuocChon?.DiaChi ?? "Chưa chọn"}");
                    messageBuilder.AppendLine("-------------------------------------");
                }

                MessageBox.Show(messageBuilder.ToString(), "Thông tin toàn bộ khách hàng", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Danh sách khách hàng trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CapnhatKhachHangDuocChon()
        {
            OnPropertyChanged(nameof(DSKhachHangTrongPhieuThue));
        }
    }
}
public class KhachHang
{
    public string Ten { get; set; }
    public string LoaiKH { get; set; }
    public string CMND { get; set; }
    public string DiaChi { get; set; }
}

// Model dữ liệu hàng trong bảng
public class KhachHangTrongPhieuThue : INotifyPropertyChanged
{
    public int STT { get; set; }
    public ObservableCollection<KhachHang> DSKhachHang { get; set; }

    private KhachHang _khachHangDuocChon;
    public KhachHang KhachHangDuocChon
    {
        get { return _khachHangDuocChon; }
        set
        {
            if (_khachHangDuocChon != value)
            {
                _khachHangDuocChon = value;
                OnPropertyChanged(nameof(KhachHangDuocChon));
                OnPropertyChanged(nameof(DSKhachHang));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}