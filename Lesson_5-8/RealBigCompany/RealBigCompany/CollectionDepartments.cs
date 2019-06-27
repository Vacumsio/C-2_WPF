using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    [Serializable]
    public class CollectionDepartments : Notify
    {
        public CollectionDepartments()
        {
        }

        private ObservableCollection<BaseDepartment> _departments = new ObservableCollection<BaseDepartment>();

        public void EditNameDepartment(int index, string name)
        {
            if (index >= 0 && index < Departments.Count) Departments[index].Name = name;
            OnPropertyChanged(nameof(Departments));
        }

        public void MoveEmployee(int IndexFromDepartment, int IndexToDepartment, int IndexEmployee)
        {
            BaseEmployee temp = (BaseEmployee)Departments[IndexFromDepartment].Employees[IndexEmployee].Clone();
            Departments[IndexFromDepartment].RemoveEmployee(IndexEmployee);
            Departments[IndexToDepartment].AddEmployee(temp);
        }

        public void RemoveAllDepartment()
        {
            Departments.Clear();
            OnPropertyChanged(nameof(Departments));
        }

        public void RemoveDepartment(int index)
        {
            if (index >= 0 && index < Departments.Count) Departments.RemoveAt(index);
            OnPropertyChanged(nameof(Departments));
        }
        public void AddDepartment(string name)
        {
            if (Departments.Select(o => o.Name.ToLower().Trim()).Contains(name.ToLower().Trim()))
                throw new ArgumentOutOfRangeException(nameof(BaseDepartment.Name), "Имя должно быть уникальным");
            Departments.Add(new BaseDepartment(name));
            OnPropertyChanged(nameof(Departments));
        }

        public void AddDepartment(BaseDepartment dep)
        {
            Departments.Add(dep);
            OnPropertyChanged(nameof(Departments));
        }
        public ObservableCollection<BaseDepartment> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }
    }
}
