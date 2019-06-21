using System.ComponentModel;

namespace RealBigCompany
{
    public class Zone : INotifyPropertyChanged
    {

        string departmentName;
        public string DepartmentName
        {
            get { return this.departmentName; }
            set
            {
                this.departmentName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DepartmentName)));
            }
        }
        public int DepartmentId { get; set; }

        public Zone(string Name, int Id)
        {
            DepartmentName = Name;
            DepartmentId = Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
