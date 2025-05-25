using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QlyKhachSan.ViewModel
{
    class RentalFormViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> CustomerList { get; set; }
        public ObservableCollection<CustomerRow> Customers { get; set; }

        private int _maxNumberCustomers = 3;
        public int MaxNumberCustomers
        {
            get
            {
                return _maxNumberCustomers;
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
                    UpdateSelectedCustomerData();
                }
            }
        }
        public ICommand ShowCustomerCommand { get; }
        public RentalFormViewModel()
        {
            CustomerList = new ObservableCollection<Customer>
            {
            new Customer { Name = "Nguyễn Văn A", Type = "VIP", ID = "123456", Address = "Hà Nội" },
            new Customer { Name = "Trần Thị B", Type = "Thường", ID = "654321", Address = "TP.HCM" },
            new Customer { Name = "Lê Văn C", Type = "VIP", ID = "987654", Address = "Đà Nẵng" }
            };

            Customers = new ObservableCollection<CustomerRow>();
            for (int i = 0; i < MaxNumberCustomers; i++) {
                Customers.Add(new CustomerRow { STT = i + 1, CustomerList = CustomerList });
            }
            ShowCustomerCommand = new RelayCommand<object>( (p) => true ,(p) => ShowCustomerInfo());
        }
        void ShowCustomerInfo()
        {
            if (Customers != null && Customers.Count > 0)
            {
                StringBuilder messageBuilder = new StringBuilder();

                foreach (var customerRow in Customers)
                {
                    messageBuilder.AppendLine($"STT: {customerRow.STT}");
                    messageBuilder.AppendLine($"Tên: {customerRow.SelectedCustomer?.Name ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"Loại: {customerRow.SelectedCustomer?.Type ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"CMND: {customerRow.SelectedCustomer?.ID ?? "Chưa chọn"}");
                    messageBuilder.AppendLine($"Địa chỉ: {customerRow.SelectedCustomer?.Address ?? "Chưa chọn"}");
                    messageBuilder.AppendLine("-------------------------------------");
                }

                MessageBox.Show(messageBuilder.ToString(), "Thông tin toàn bộ khách hàng", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Danh sách khách hàng trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void UpdateSelectedCustomerData()
        {
            OnPropertyChanged(nameof(Customers));
        }
    }
}
public class Customer
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string ID { get; set; }
    public string Address { get; set; }
}

// Model dữ liệu hàng trong bảng
public class CustomerRow : INotifyPropertyChanged
{
    public int STT { get; set; }
    public ObservableCollection<Customer> CustomerList { get; set; }

    private Customer _selectedCustomer;
    public Customer SelectedCustomer
    {
        get { return _selectedCustomer; }
        set
        {
            if (_selectedCustomer != value)
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
                OnPropertyChanged(nameof(CustomerList));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}