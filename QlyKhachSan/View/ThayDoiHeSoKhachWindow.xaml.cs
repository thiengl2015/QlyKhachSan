using System.Windows;

namespace QlyKhachSan.View
{
    public partial class ThayDoiHeSoKhachWindow : Window
    {
        public ThayDoiHeSoKhachWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.ThayDoiHeSoKhachViewModel();
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
