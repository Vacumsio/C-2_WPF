using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RealBigCompany
{
    public class Master : Notify
    {
        private ObservableCollection<Slave> slaves = new ObservableCollection<Slave>;

        ///Поле и его свойство
        public string name;
        public string Name
        {
            get => name;
            set
            {
                if (value.Length >= 2) name = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Name), "Имя должно быть длиннее двух символов");
                }
            }
        }
        
        #region Constructs
        public Master() { }

        public Master(string name)
        {
            Name = name;
        }

        public Master(Master m)
        {
            this.Name = m.Name;
            this.slaves = m.slaves;
        }
        #endregion



    }
}
