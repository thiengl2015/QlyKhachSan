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
    /// Interaction logic for LapBaoCaoPage.xaml
    /// </summary>
    public partial class LapBaoCaoPage : Page
    {
        public LapBaoCaoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapBaoCaoDoanhThuPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapBaoCaoMatDoPage();
        }
    }
}
