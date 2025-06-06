﻿using QlyKhachSan.View;
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

namespace QlyKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mainpage.Content = new TraCuuPhongPage();
        }

        private void mainbutton_Checked(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new TraCuuPhongPage();
        }

        private void borrowlend_Checked(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapDanhMucPhongPage();
        }

        private void lend_Checked(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapPhieuThuePage();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapHoaDonThanhToanPage();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new LapBaoCaoPage();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Mainpage.Content = new ThayDoiQuyDinhPage();
        }
    }
}
