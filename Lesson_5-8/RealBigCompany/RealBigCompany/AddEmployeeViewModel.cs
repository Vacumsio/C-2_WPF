using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    public class AddEmployeeViewModel : DialogWindowViewModel, IDataErrorInfo
    {
        private string _name = string.Empty;
        private string _surName = string.Empty;
        private int? _age;
        private int? _experience;
        private Professions _profession = default(Professions);
        public BaseEmployee _employee;

        private readonly List<BaseEmployee> _existingEmployees;
        public AddEmployeeViewModel(BaseEmployee employee, List<BaseEmployee> existingEmployees)
        {
            _existingEmployees = existingEmployees;
            Name = employee.Name;
            SurName = employee.SurName;
            Age = employee.Age;
            Experience = employee.Experience;
            Profession = employee.Profession;
        }

        public AddEmployeeViewModel(List<BaseEmployee> existingEmployees)
        {
            _existingEmployees = existingEmployees;
        }

        protected override bool CanPressOk(object obj)
        {
            try
            {
                BaseEmployee t = this.Employee;

                return !(_existingEmployees.Contains(t));
            }
            catch
            {
                return false;
            }
        }


        public Professions Profession
        {
            get { return _profession; }
            set
            {
                _profession = value;
                OnPropertyChanged(nameof(Profession));
            }
        }

        public int? Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                OnPropertyChanged(nameof(Experience));
            }
        }

        public int? Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }

        }

        public string SurName
        {
            get { return _surName; }
            set
            {
                _surName = value;
                OnPropertyChanged(nameof(SurName));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string this[string columnName]
        {
            get
            {
                Error = String.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if ((Name.Length < 2))
                        {
                            Error = "Имя должно содержать более одного символа";
                        }
                        break;
                    case nameof(SurName):
                        if ((SurName.Length < 2))
                        {
                            Error = "Фамилия должна содержать более одного символа";
                        }
                        break;
                    case nameof(Age):
                        if (!(Age >= 14 && Age <= 85))
                        {
                            Error = "Возраст должен быть от 14 до 85 лет";
                        }
                        break;
                    case nameof(Experience):
                        if (!(Experience < Age))
                        {
                            Error = "Опыт не может быть больше или равным возрасту";
                        }
                        break;
                }
                return Error;
            }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        public BaseEmployee Employee
        {
            get { return new BaseEmployee(Name, SurName, (int)Age, Profession, (int)Experience); }
        }
    }
}
