using System.Windows;

namespace QlyKhachSan.View
{
    public partial class ThayDoiTiLePhuThuWindow : Window
    {
        public ThayDoiTiLePhuThuWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.ThayDoiTiLePhuThuViewModel();
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
