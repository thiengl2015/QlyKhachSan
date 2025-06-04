
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows;
using System.Windows.Input;
using QlyKhachSan.Model;
using QlyKhachSan.ViewModel;

namespace QlyKhachSan.ViewModel
{
    public class LapBaoCaoMatDoViewModel : BaseViewModel
    {
        private int _namBaoCao;
        public int NamBaoCao
        {
            get => _namBaoCao;
            set
            {
                if (_namBaoCao != value)
                {
                    _namBaoCao = value;
                    OnPropertyChanged(nameof(NamBaoCao));
                }
            }
        }

        private int _thangBaoCao;
        public int ThangBaoCao
        {
            get => _thangBaoCao;
            set
            {
                if (_thangBaoCao != value)
                {
                    _thangBaoCao = value;
                    OnPropertyChanged(nameof(ThangBaoCao));
                }
            }
        }

        private ObservableCollection<MatDoPhong> _dsMatDoPhong;
        public ObservableCollection<MatDoPhong> DsMatDoPhong
        {
            get => _dsMatDoPhong;
            set
            {
                if (_dsMatDoPhong != value)
                {
                    _dsMatDoPhong = value;
                    OnPropertyChanged(nameof(DsMatDoPhong));
                }
            }
        }

        public ICommand LapBaoCaoCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public LapBaoCaoMatDoViewModel()
        {
            NamBaoCao = DateTime.Now.Year;
            ThangBaoCao = DateTime.Now.Month;

            LapBaoCaoCommand = new RelayCommand<object>((p) => CanLapBaoCao(), (p) => LapBaoCao());
        }
       
        private bool CanLapBaoCao()
        {
            return true;
        }

        private void LapBaoCao()
        {
            try
            {
                int tongNgayThue = 0;

                DsMatDoPhong = new ObservableCollection<MatDoPhong>();

                var dsPhieuThue = DataProvider.Instance.DB.PHIEUTHUEs
                    .Where(pt => pt.NgayBatDauThue.HasValue && pt.NgayKetThucThue.HasValue &&
                    ((pt.NgayBatDauThue.Value.Month == ThangBaoCao && pt.NgayBatDauThue.Value.Year == NamBaoCao) ||
                    (pt.NgayKetThucThue.Value.Month == ThangBaoCao && pt.NgayKetThucThue.Value.Year == NamBaoCao)))
                    .ToList();

                foreach (var phieuThue in dsPhieuThue)
                {
                    var phong = DataProvider.Instance.DB.PHONGs.FirstOrDefault(p => p.MaPhong == phieuThue.MaPhong);
                    
                    var matDoPhong = DsMatDoPhong.FirstOrDefault(m => m.Phong.MaPhong == phong.MaPhong);

                    tongNgayThue += (phieuThue.NgayKetThucThue.Value - phieuThue.NgayBatDauThue.Value).Days;

                    if (matDoPhong == null)
                    {
                        matDoPhong = new MatDoPhong
                        {
                            STT = (DsMatDoPhong.Count + 1).ToString(),
                            Phong = phong,
                            SoNgayThue = (phieuThue.NgayKetThucThue.Value - phieuThue.NgayBatDauThue.Value).Days,
                            TyLe = 0
                        };

                        matDoPhong.TyLe = (float)matDoPhong.SoNgayThue / (float)tongNgayThue;

                        DsMatDoPhong.Add(matDoPhong);
                    }
                    else
                    {
                        matDoPhong.SoNgayThue += (phieuThue.NgayKetThucThue.Value - phieuThue.NgayBatDauThue.Value).Days;
                        matDoPhong.TyLe = (float)matDoPhong.SoNgayThue / (float)tongNgayThue;
                    }
                }

                foreach (var matDoPhong in DsMatDoPhong)
                {
                    matDoPhong.TyLe = (float)matDoPhong.SoNgayThue / (float)tongNgayThue;
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


    public class MatDoPhong : INotifyPropertyChanged
    {
        public string STT { get; set; }

        public PHONG Phong { get; set; }

        public string TenPhong => Phong?.TenPhong;

        public int SoNgayThue { get; set; }

        public float TyLe { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}