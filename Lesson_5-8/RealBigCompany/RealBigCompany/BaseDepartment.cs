using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RealBigCompany
{
    [Serializable]
    public class BaseDepartment : Notify
    {
        public BaseDepartment()
        {
        }
        public BaseDepartment(string name)
        {
            Name = name;
        }

        public BaseDepartment(BaseDepartment dep)
        {
            this.Name = dep.Name;
            Employees = dep.Employees;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 2) _name = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Name), "Должно иметь два и более символа");
                }
            }
        }

        public void EditEmployee(int index, BaseEmployee employee)
        {
            if (index >= 0 && index < Employees.Count) Employees[index] = employee;
            OnPropertyChanged(nameof(Employees));
        }


        public void RemoveEmployee(int index)
        {
            if (index >= 0 && index < Employees.Count) Employees.RemoveAt(index);
            OnPropertyChanged(nameof(Employees));
        }
        public void AddEmployee(BaseEmployee employee)
        {
            if (Employees.Contains(employee))
                throw new ArgumentOutOfRangeException(nameof(BaseDepartment.Name), "Такой работник уже существует");
            Employees.Add(employee);
            OnPropertyChanged(nameof(Employees));
        }

        public ObservableCollection<BaseEmployee> Employees { get; set; } = new ObservableCollection<BaseEmployee>();
    }
}
