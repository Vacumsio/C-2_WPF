using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace RealBigCompany
{
    public class ViewModelMV : Notify
    {
        readonly CollectionDepartments _model = new CollectionDepartments();


        public RelayCommand MoveEmployeeCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand AddDepartmentCommand { get; }
        public RelayCommand EditNameDepartmentCommand { get; }
        public RelayCommand RemoveDepartmentCommand { get; }

        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand RemoveEmployeeCommand { get; }
        public ViewModelMV()
        {
            _model.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };

            ExitCommand = new RelayCommand(o => Exit());
            SaveCommand = new RelayCommand(o => Save(), u => _model.Departments.Count > 0);
            OpenCommand = new RelayCommand(o => Open());

            AddDepartmentCommand = new RelayCommand(o => ExecuteAddDepartment());
            RemoveDepartmentCommand = new RelayCommand(o => ExecuteRemoveDepartment(), u => (_model.Departments.Count > 0 && SelectedIndex >= 0));
            EditNameDepartmentCommand = new RelayCommand(o => ExecuteEditNameDepartment(), u => SelectedIndex >= 0);

            MoveEmployeeCommand = new RelayCommand(o => ExecuteMoveEmployeeCommand(), u => (_model.Departments.Count > 1 && SelectedItemBaseEmployee != null));
            AddEmployeeCommand = new RelayCommand(o => ExecuteAddEmployeeCommand(), u => SelectedItem != null);
            RemoveEmployeeCommand = new RelayCommand(o => ExecuteRemoveEmployeeCommand(), (u => SelectedItem != null && SelectedItemBaseEmployee != null));
            EditEmployeeCommand = new RelayCommand(o => ExecuteEditEmployeeCommand(), (u => SelectedItem != null && SelectedItemBaseEmployee != null));
        }

        private void ExecuteMoveEmployeeCommand()
        {
            var dc = new SelectDepartmentViewModel(Departments.Select(u => u.Name).Where(o => o != SelectedItem.Name).ToList());
            new SelectDepartmentWindow() { DataContext = dc, Owner = Application.Current.MainWindow }.ShowDialog();
            if (dc.DialogResult == true)
            {
                int toIndex = Departments.Select(o => o.Name).ToList().FindIndex(c => c == dc.SelectedItem);
                _model.MoveEmployee(SelectedIndex, toIndex, SelectedIndexEmployee);
            }
        }

        private void Exit()
        {
            App.Current.MainWindow.Close();
        }

        private void Open()
        {
            var dial = new OpenFileDialog { Filter = "*.xml|*.xml" };
            if (dial.ShowDialog() ?? false)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<BaseDepartment>));
                using (FileStream fs = new FileStream(dial.FileName, FileMode.Open))
                {
                    _model.RemoveAllDepartment();
                    ObservableCollection<BaseDepartment> collection =
                        (ObservableCollection<BaseDepartment>)formatter.Deserialize(fs);
                    foreach (var department in collection)
                    {
                        _model.AddDepartment(department);
                    }
                    OnPropertyChanged(nameof(_model));
                    OnPropertyChanged(nameof(_departments));

                }
            }
        }

        private void Save()
        {
            var dial = new SaveFileDialog() { Filter = "*.xml|*.xml" };
            if (dial.ShowDialog() ?? false)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<BaseDepartment>));
                using (FileStream fs = new FileStream(dial.FileName, FileMode.Create))
                {
                    formatter.Serialize(fs, Departments);
                }
            }
        }

        private void ExecuteRemoveEmployeeCommand()
        {
            int count = SelectedItem.Employees.Count;
            if (SelectedIndexEmployee >= 0 && SelectedIndexEmployee < count)
            {
                int curIndex = count - 1 == SelectedIndexEmployee ? SelectedIndexEmployee - 1 : SelectedIndexEmployee;
                _model.Departments[SelectedIndex].RemoveEmployee(SelectedIndexEmployee);
                SelectedIndexEmployee = curIndex;
            }
        }

        private void ExecuteEditEmployeeCommand()
        {
            var dc = new AddEmployeeViewModel(SelectedItemBaseEmployee, SelectedItem.Employees.ToList());
            new AddEmployeeWindow() { DataContext = dc, Owner = Application.Current.MainWindow }.ShowDialog();
            if (dc.DialogResult == true)
            {
                SelectedItem.EditEmployee(SelectedIndexEmployee, dc.Employee);
            }
        }

        private void ExecuteAddEmployeeCommand()
        {
            var dc = new AddEmployeeViewModel(SelectedItem.Employees.ToList());
            new AddEmployeeWindow() { DataContext = dc, Owner = Application.Current.MainWindow }.ShowDialog();
            if (dc.DialogResult == true)
            {
                SelectedItem.AddEmployee(dc.Employee);
            }
        }

        private void ExecuteEditNameDepartment()
        {
            var dc = new AddNewDepartmentViewModel(SelectedItem.Name, _model.Departments.Select(o => o.Name).ToList());
            new AddNewDepartmentWindow { DataContext = dc, Owner = App.Current.MainWindow }.ShowDialog();
            if (dc.DialogResult == true)
            {
                _model.EditNameDepartment(SelectedIndex, dc.NewDepartmentName);
            }
        }

        private void ExecuteRemoveDepartment()
        {
            if (SelectedIndex >= 0 && SelectedIndex < _model.Departments.Count)
            {
                int curIndex = Departments.Count - 1 == SelectedIndex ? SelectedIndex - 1 : SelectedIndex;
                _model.RemoveDepartment(SelectedIndex);
                SelectedIndex = curIndex;
            }
        }


        private void ExecuteAddDepartment()
        {
            var dc = new AddNewDepartmentViewModel("", _model.Departments.Select(o => o.Name).ToList());
            new AddNewDepartmentWindow { DataContext = dc, Owner = Application.Current.MainWindow }.ShowDialog();
            if (dc.DialogResult == true)
            {
                _model.AddDepartment(dc.NewDepartmentName);
            }
        }


        private ObservableCollection<BaseDepartment> _departments;
        public ObservableCollection<BaseDepartment> Departments
        {
            get { return _model.Departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }
        public BaseDepartment SelectedItem
        {
            get
            {
                if (_model.Departments.Count > 0 && SelectedIndex > -1) return _model.Departments[SelectedIndex];
                else return null;
            }
            set
            {
                _selectedIndex = _model.Departments.IndexOf(value);
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private int _selectedIndex = -1;
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



        private int _selectedIndexEmployee = -1;
        public int SelectedIndexEmployee
        {
            get { return _selectedIndexEmployee; }
            set
            {
                _selectedIndexEmployee = value;
                OnPropertyChanged(nameof(SelectedIndexEmployee));
                OnPropertyChanged(nameof(SelectedItemBaseEmployee));
            }
        }

        private BaseEmployee _selectedItemBaseEmployee;
        public BaseEmployee SelectedItemBaseEmployee
        {
            get
            {
                if (SelectedItem?.Employees.Count > 0 && SelectedIndexEmployee > -1) return SelectedItem.Employees[SelectedIndexEmployee];
                else return null;
            }
            set
            {
                _selectedItemBaseEmployee = value;
                OnPropertyChanged(nameof(SelectedIndexEmployee));
                OnPropertyChanged(nameof(SelectedItemBaseEmployee));
            }
        }
    }
}
