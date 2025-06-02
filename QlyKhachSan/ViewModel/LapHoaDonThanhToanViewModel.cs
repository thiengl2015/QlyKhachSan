using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlyKhachSan.ViewModel
{
    class LapHoaDonThanhToanViewModel: BaseViewModel
    {
        public ObservableCollection<Phong> DSKhachHang { get; set; }

    }
}
public class Phong
{
    public long MaPhong { get; set; }
    public string TenPhong { get; set; }
    public long MaLoaiPhong { get; set; }
    public string GhiChu { get; set; }

    public Phong(long MaPhong, string TenPhong, long MaLoaiPhong, string GhiChu)
    {
        this.MaPhong = MaPhong;
        this.MaLoaiPhong = MaLoaiPhong;
        this.TenPhong = TenPhong;
        this.GhiChu = GhiChu;
    }
}
