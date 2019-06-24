using System.ComponentModel;

namespace RealBigCompany
{
    public class Slave : INotifyPropertyChanged
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
        public int MasterId { get; private set; }

        public Slave(string FName, string LName, int age, int DepId)
        {
            firstName = FName;
            LastName = LName;
            Age = age;
            this.MasterId = DepId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{FirstName,1} {LastName,13} {Age,27} {MasterId,1}";
        }
    }
}
