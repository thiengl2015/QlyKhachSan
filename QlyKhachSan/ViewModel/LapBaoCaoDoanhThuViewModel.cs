using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    public class LapBaoCaoDoanhThuViewModel : BaseViewModel
    {
        private int namBaoCao;
        public int NamBaoCao
        {
            get { return namBaoCao; }
            set
            {
                if (namBaoCao != value)
                {
                    namBaoCao = value;
                    OnPropertyChanged();
                }
            }
        }

        private int thangBaoCao;
        public int ThangBaoCao
        {
            get { return thangBaoCao; }
            set
            {
                if (thangBaoCao != value)
                {
                    thangBaoCao = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tongDoanhThu;
        public int TongDoanhThuThang
        {
            get { return tongDoanhThu; }
            set
            {
                if (tongDoanhThu != value)
                {
                    tongDoanhThu = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<DoanhThuTheoLoaiPhong> dsDoanhThuTheoLoaiPhong;
        public ObservableCollection<DoanhThuTheoLoaiPhong> DsDoanhThuTheoLoaiPhong
        {
            get { return dsDoanhThuTheoLoaiPhong; }
            set
            {
                if (dsDoanhThuTheoLoaiPhong != value)
                {
                    dsDoanhThuTheoLoaiPhong = value;
                    OnPropertyChanged();
                }
            }
        }

        public LapBaoCaoDoanhThuViewModel()
        {
            NamBaoCao = DateTime.Now.Year;
            ThangBaoCao = DateTime.Now.Month;

            LapBaoCaoDoanhThuCommand = new RelayCommand<object>(
                (p) => true,
                (p) => LapBaoCaoDoanhThu()
            );
        }

        private void LapBaoCaoDoanhThu()
        {
            TongDoanhThuThang = 0;
            DsDoanhThuTheoLoaiPhong = new ObservableCollection<DoanhThuTheoLoaiPhong>();

            var dsHoaDon = DataProvider.Instance.DB.HOADONs
                .Where(hd => hd.NgayThanhToan.HasValue && hd.NgayThanhToan.Value.Month == ThangBaoCao && hd.NgayThanhToan.Value.Year == NamBaoCao)
                .ToList();

            foreach (var hd in dsHoaDon)
            {
                TongDoanhThuThang += hd.TriGia ?? 0;

                var dsCTHD = DataProvider.Instance.DB.CHITIETHOADONs
                    .Where(cthd => cthd.MaHoaDon == hd.MaHoaDon)
                    .ToList();

                foreach (var cthd in dsCTHD)
                {
                    PHIEUTHUE pHIEUTHUE = DataProvider.Instance.DB.PHIEUTHUEs
                        .FirstOrDefault(pt => pt.MaPhieuThue == cthd.MaPhieuThue);

                    PHONG pHONG = DataProvider.Instance.DB.PHONGs
                        .FirstOrDefault(p => p.MaPhong == pHIEUTHUE.MaPhong);

                    LOAIPHONG lOAIPHONG = DataProvider.Instance.DB.LOAIPHONGs
                        .FirstOrDefault(lp => lp.MaLoaiPhong == pHONG.MaLoaiPhong);

                    // Nếu loại phòng có tồn tại
                    DoanhThuTheoLoaiPhong dt = DsDoanhThuTheoLoaiPhong.FirstOrDefault(x => x.LoaiPhong.MaLoaiPhong == lOAIPHONG.MaLoaiPhong);
                    if (dt != null)
                    {
                        // Cộng doanh thu
                        dt.DoanhThu += cthd.ThanhTien ?? 0;
                        dt.TyLe = (dt.DoanhThu / tongDoanhThu);
                    }
                    else
                    {
                        // Tạo mới doanh thu theo loại phòng
                        dt = new DoanhThuTheoLoaiPhong
                        {
                            STT = (DsDoanhThuTheoLoaiPhong.Count + 1).ToString(),
                            LoaiPhong = lOAIPHONG,
                            DoanhThu = cthd.ThanhTien ?? 0,
                            TyLe = 0,
                        };

                        dt.TyLe = (dt.DoanhThu / tongDoanhThu);

                        DsDoanhThuTheoLoaiPhong.Add(dt);
                    }
                }    
            }
        }

        public ICommand LapBaoCaoDoanhThuCommand { get; set; }
    }

    public class DoanhThuTheoLoaiPhong :  INotifyPropertyChanged
    {
        public string STT { get; set; }

        public LOAIPHONG LoaiPhong { get; set; }

        public string TenLoaiPhong => LoaiPhong?.TenLoaiPhong;

        private int doanhThu;
        public int DoanhThu
        {
            get
            {
                return doanhThu;
            }
            set
            {
                if (doanhThu != value)
                {
                    doanhThu = value;
                    OnPropertyChanged(nameof(DoanhThu));
                    OnPropertyChanged(nameof(TyLe));
                }
            }
        }

        public float TyLe { get; set; } 

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
