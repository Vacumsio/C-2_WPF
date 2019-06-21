using System.ComponentModel;

namespace RealBigCompany
{
    public class Employee : INotifyPropertyChanged
    {
        public string firstName;
        public string FirstName
        {
            get => firstName;

            set
            {
                firstName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }

        }

        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepartamentId { get; private set; }

        public Employee(string FName, string LName, int age, int DepId)
        {
            firstName = FName;
            LastName = LName;
            Age = age;
            this.DepartamentId = DepId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{FirstName,7} {LastName,12} {Age,3} {DepartamentId,3}";
        }
    }
}
