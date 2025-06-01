using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlyKhachSan.ViewModel
{
    public class LapDanhMucPhongViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIPHONG> danhSachLoaiPhong;
        public ObservableCollection<LOAIPHONG> DanhSachLoaiPhong
        {
            get { return danhSachLoaiPhong; }
            set
            {
                danhSachLoaiPhong = value;
                OnPropertyChanged(nameof(DanhSachLoaiPhong));
            }
        }

        private string _selectedLoaiPhong;
        public string SelectedLoaiPhong
        {
            get => _selectedLoaiPhong;
            set
            {
                _selectedLoaiPhong = value;
                OnPropertyChanged(nameof(SelectedLoaiPhong));
            }
        }

        public LapDanhMucPhongViewModel()
        {
            DanhSachLoaiPhong = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs.ToList());

            SelectedLoaiPhong = DanhSachLoaiPhong.First().TenLoaiPhong;
        }
    }
}
