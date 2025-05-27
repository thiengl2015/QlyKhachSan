using QlyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List = new ObservableCollection<LOAIPHONG>(DataProvider.Instance.DB.LOAIPHONGs.ToList());
        }
    }
}
