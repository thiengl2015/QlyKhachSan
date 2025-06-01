using QlyKhachSan.Model;
using QlyKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QlyKhachSan.View
{
    /// <summary>
    /// Interaction logic for SearchRoomPage.xaml
    /// </summary>
    public partial class TraCuuPhongPage : Page
    {
        public TraCuuPhongPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TraCuuPhong();
        }

        private void TraCuuPhong()
        {
            using (var db = new QuanLyKhachSanEntities())
            {
                var query = from p in db.PHONGs
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            join pt in db.PHIEUTHUEs on p.MaPhong equals pt.MaPhong into ptGroup
                            from pt in ptGroup.DefaultIfEmpty()
                            where
                                (string.IsNullOrWhiteSpace(txtMaPhong.Text) || p.MaPhong.Contains(txtMaPhong.Text)) &&
                                (string.IsNullOrWhiteSpace(txtTenPhong.Text) || p.TenPhong.Contains(txtTenPhong.Text)) &&
                                (string.IsNullOrWhiteSpace(txtMaLoaiPhong.Text) || p.MaLoaiPhong.Contains(txtMaLoaiPhong.Text)) &&
                                (cbTenLoaiPhong.SelectedItem == null || lp.TenLoaiPhong.Contains(cbTenLoaiPhong.Text)) &&
                                (string.IsNullOrWhiteSpace(txtGhiChu.Text) || p.GhiChu.Contains(txtGhiChu.Text))
                            select new PhongDTO
                            {
                                TenPhong = p.TenPhong,
                                TenLoaiPhong = lp.TenLoaiPhong,
                                DonGia = lp.DonGia ?? 0,
                                TinhTrang = pt != null ? "Đang thuê" : "Trống"
                            };

                phong.ItemsSource = query.ToList();
            }
        }
    }
}
