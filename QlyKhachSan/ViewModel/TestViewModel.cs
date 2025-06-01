using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QlyKhachSan.ViewModel
{
    public class TestViewModel : BaseViewModel
    {
        private ObservableCollection<LOAIPHONG> _list;
        public ObservableCollection<LOAIPHONG> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {
            try
            {
                List = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách loại phòng. Lý do: " + ex.Message);
            }
        }
    }
}
