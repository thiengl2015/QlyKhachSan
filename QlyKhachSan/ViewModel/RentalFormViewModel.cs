using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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