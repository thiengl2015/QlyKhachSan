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
    public partial class LapHoaDonThanhToanPage : Page
    {
        public LapHoaDonThanhToanPage()
        {
            InitializeComponent();
        }
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Phong selectedRoom)
            {
                PhongTrongHoaDon row = comboBox.DataContext as PhongTrongHoaDon;
                if (row != null)
                {
                    row.PhongDuocChon = selectedRoom;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TimKhachHangCoQuanWindow window = new TimKhachHangCoQuanWindow();
            window.ShowDialog();
        }
    }
}
