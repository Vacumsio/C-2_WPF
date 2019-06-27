using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    class AddNewDepartmentViewModel : DialogWindowViewModel, IDataErrorInfo
    {
        protected override bool CanPressOk(object obj)
        {
            return Error == String.Empty ? true : false;
        }

        private string _newDepartmentName = default(string);
        public string NewDepartmentName
        {
            get { return _newDepartmentName; }
            set
            {
                _newDepartmentName = value;
                OnPropertyChanged(nameof(NewDepartmentName));
            }
        }

        private readonly List<string> existingDepartments;
        public AddNewDepartmentViewModel(string name, List<string> existingDepartments)
        {
            NewDepartmentName = name;
            this.existingDepartments = existingDepartments;
        }

        public string this[string columnName]
        {
            get
            {
                Error = String.Empty;
                switch (columnName)
                {
                    case nameof(NewDepartmentName):
                        if ((NewDepartmentName.Length < 2))
                        {
                            Error = "Название отдела должно содержать более одного символа";
                        }
                        if (existingDepartments.Select(o => o.ToLower().Trim()).Contains(NewDepartmentName.ToLower().Trim()))
                        {
                            Error = "Отдел с таким названием уже существует";
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

    }
}
