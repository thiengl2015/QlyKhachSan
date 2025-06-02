using System.Windows;

namespace QlyKhachSan.View
{
    public partial class ThayDoiSoLuongKhachWindow : Window
    {
        public ThayDoiSoLuongKhachWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.ThayDoiSoLuongKhachViewModel();
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
