using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    public class SelectDepartmentViewModel : DialogWindowViewModel
    {
        private int _selectedIndex;
        private string _selectedItem;
        private ObservableCollection<String> _departmentsName = new ObservableCollection<string>();
        public SelectDepartmentViewModel(List<string> list)
        {
            foreach (string s in list)
            {
                DepartmentsName.Add(s);
            }
        }

        public ObservableCollection<string> DepartmentsName
        {
            get { return _departmentsName; }
            set
            {
                _departmentsName = value;
                OnPropertyChanged(nameof(DepartmentsName));
            }
        }

        public string SelectedItem
        {
            get { return DepartmentsName[SelectedIndex]; }
            private set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }
}
