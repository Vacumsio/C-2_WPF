using System.ComponentModel;

namespace RealBigCompany
{
    public class Master : INotifyPropertyChanged
    {

        string masterName;
        public string MasterName
        {
            get { return this.masterName; }
            set
            {
                this.masterName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.MasterName)));
            }
        }
        public int MasterId { get; set; }

        public Master(string Name, int Id)
        {
            MasterName = Name;
            MasterId = Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
