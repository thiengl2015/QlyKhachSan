
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QlyKhachSan.Model;
using QlyKhachSan.ViewModel;

namespace QlyKhachSan.ViewModel
{
    public class LapBaoCaoMatDoViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<string> DanhSachNam { get; set; }
        public ObservableCollection<string> DanhSachThang { get; set; }

        private string _namDuocChon;
        public string NamDuocChon
        {
            get => _namDuocChon;
            set
            {
                if (_namDuocChon != value)
                {
                    _namDuocChon = value;
                    OnPropertyChanged(nameof(NamDuocChon));
                }
            }
        }

        private string _thangDuocChon;
        public string ThangDuocChon
        {
            get => _thangDuocChon;
            set
            {
                if (_thangDuocChon != value)
                {
                    _thangDuocChon = value;
                    OnPropertyChanged(nameof(ThangDuocChon));
                }
            }
        }

        
        private ObservableCollection<PhongTrongPhieuThue> _dsPhongTrongPhieuThue;
        public ObservableCollection<PhongTrongPhieuThue> DSPhongTrongPhieuThue
        {
            get => _dsPhongTrongPhieuThue;
            set
            {
                _dsPhongTrongPhieuThue = value;
                OnPropertyChanged(nameof(DSPhongTrongPhieuThue));
            }
        }

        private ICommand _lapBaoCaoCommand;
        public ICommand LapBaoCaoCommand
        {
            get => _lapBaoCaoCommand ?? (_lapBaoCaoCommand = new RelayCommand(
                param => LapBaoCao(),
                param => CanLapBaoCao()
            ));
        }

        private ICommand _thoatCommand;
        public ICommand ThoatCommand
        {
            get => _thoatCommand ?? (_thoatCommand = new RelayCommand(
                param => Thoat()
            ));
        }

        public LapBaoCaoMatDoViewModel()
        {
            
            DanhSachNam = new ObservableCollection<string>();
            DanhSachThang = new ObservableCollection<string>();

            DanhSachNam.Add("Năm hiện hành");
            int namHienTai = DateTime.Now.Year;
            for (int i = 2020; i <= namHienTai; i++)
                DanhSachNam.Add(i.ToString());

            DanhSachThang.Add("Tháng hiện hành");
            for (int i = 1; i <= 12; i++)
                DanhSachThang.Add(i.ToString());

            NamDuocChon = "Năm hiện hành";
            ThangDuocChon = "Tháng hiện hành";

            
            DSPhongTrongPhieuThue = new ObservableCollection<PhongTrongPhieuThue>();
        }

        public int LayNamThucTe()
        {
            return NamDuocChon == "Năm hiện hành" ? DateTime.Now.Year : int.Parse(NamDuocChon);
        }

        public int LayThangThucTe()
        {
            return ThangDuocChon == "Tháng hiện hành" ? DateTime.Now.Month : int.Parse(ThangDuocChon);
        }

       
        private bool CanLapBaoCao()
        {
            // Kiểm tra điều kiện có thể lập báo cáo
            return true; // Hoặc logic kiểm tra phù hợp
        }

        private void LapBaoCao()
        {
            try
            {
                int nam = LayNamThucTe();
                int thang = LayThangThucTe();

                DateTime ngayDauThang = new DateTime(nam, thang, 1);
                DateTime ngayCuoiThang = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));

                // Giả lập dữ liệu - thay bằng truy vấn CSDL thực tế
                var danhSachPhongThue = new List<PhongThue>
                {
                    new PhongThue { MaPhong = "P101", TenPhong = "Phòng 101", NgayBatDauThue = new DateTime(nam, thang, 5), NgayKetThucThue = new DateTime(nam, thang, 15) },
                    new PhongThue { MaPhong = "P102", TenPhong = "Phòng 102", NgayBatDauThue = new DateTime(nam, thang, 10), NgayKetThucThue = new DateTime(nam, thang, 20) },
                    new PhongThue { MaPhong = "P103", TenPhong = "Phòng 103", NgayBatDauThue = new DateTime(nam, thang, 1), NgayKetThucThue = new DateTime(nam, thang, 30) }
                };

                DSPhongTrongPhieuThue.Clear();

                int stt = 1;
                int tongSoNgayTrongThang = ngayCuoiThang.Day;

                foreach (var phong in danhSachPhongThue)
                {
                    int soNgayThue = (phong.NgayKetThucThue - phong.NgayBatDauThue).Days + 1;
                    double tyLe = (double)soNgayThue / tongSoNgayTrongThang * 100;

                    DSPhongTrongPhieuThue.Add(new PhongTrongPhieuThue
                    {
                        STT = stt++,
                        TenPhong = phong.TenPhong,
                        SoNgayThue = soNgayThue,
                        TyLe = Math.Round(tyLe, 2),
                        MaPhong = phong.MaPhong
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lập báo cáo: {ex.Message}");
            }
        }

        private void Thoat()
        {
            
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }
    }

  
    public class PhongTrongPhieuThue
    {
        public int STT { get; set; }
        public string TenPhong { get; set; }
        public int SoNgayThue { get; set; }
        public double TyLe { get; set; }
        public string MaPhong { get; set; }
    }

    public class PhongThue
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public DateTime NgayBatDauThue { get; set; }
        public DateTime NgayKetThucThue { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeCommand;
        private readonly Predicate<object> _canExecutePredicate;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _executeCommand = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecutePredicate = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecutePredicate?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _executeCommand(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}