
using QlyKhachSan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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
    /// Interaction logic for LapBaoCaoMatDoPage.xaml
    /// </summary>
    public partial class LapBaoCaoMatDoPage : Page
    {
        public LapBaoCaoMatDoPage()
        {
            InitializeComponent();
            this.DataContext = new LapBaoCaoMatDoViewModel();
            MessageBox.Show("LapBaoCaoMatDoPage Loaded");
        }
    }
}
