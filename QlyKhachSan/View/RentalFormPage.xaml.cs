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
    public partial class RentalFormPage : Page
    {
        public RentalFormPage()
        {
            InitializeComponent();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Customer selectedCustomer)
            {
                CustomerRow row = comboBox.DataContext as CustomerRow;
                if (row != null)
                {
                    row.SelectedCustomer = selectedCustomer;
                }
            }
        }
    }
}
