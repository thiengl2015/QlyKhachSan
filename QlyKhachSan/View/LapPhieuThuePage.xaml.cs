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
    /// Interaction logic for RentalForm.xaml
    /// </summary>
    public partial class LapPhieuThuePage : Page
    {
        public LapPhieuThuePage()
        {
            InitializeComponent();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is KhachHang selectedCustomer)
            {
                KhachHangTrongPhieuThue row = comboBox.DataContext as KhachHangTrongPhieuThue;
                if (row != null)
                {
                    row.KhachHangDuocChon = selectedCustomer;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemKhachHangWindow window = new ThemKhachHangWindow();
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TimKiemKhachHangWindow window = new TimKiemKhachHangWindow();
            window.ShowDialog();
        }
    }
}
