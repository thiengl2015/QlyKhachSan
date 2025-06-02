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
    /// Interaction logic for ThayDoiQuyDinhPage.xaml
    /// </summary>
    public partial class ThayDoiQuyDinhPage : Page
    {
        public ThayDoiQuyDinhPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThayDoiDonGiaWindow window = new ThayDoiDonGiaWindow();
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThayDoiHeSoKhachWindow window = new ThayDoiHeSoKhachWindow();
            window.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ThayDoiSoLuongKhachWindow window = new ThayDoiSoLuongKhachWindow();
            window.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ThayDoiTiLePhuThuWindow window = new ThayDoiTiLePhuThuWindow();
            window.ShowDialog();
        }
    }
}
