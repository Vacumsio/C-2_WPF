using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RealBigCompany
{
    [Serializable]
    public class Master : Notify
    {
        public ObservableCollection<Slave> Slaves { get; set; } = new ObservableCollection<Slave>();

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

        public Master(string _name)
        {
            Name = _name;
        }

        public Master(Master m)
        {
            this.Name = m.Name;
            this.Slaves = m.Slaves;
        }
        #endregion

        #region Methods
        public void EditSlave(int index, Master slave)
        {
            if (index >= 0 && index < Slaves.Count) Slaves[index] = slave;
            OnPropertyChanged(nameof(Slaves));
        }

        public void RemoveSlave(int index)
        {
            if (index >= 0 && index < Slaves.Count) Slaves.RemoveAt(index);
            OnPropertyChanged(nameof(Slaves));
        }
        public void AddSlave(Master slave)
        {
            if (Slaves.Contains(slave))
                throw new ArgumentOutOfRangeException(nameof(Master.Name), "Такой работник уже существует");
            Slaves.Add(slave);
            OnPropertyChanged(nameof(Slaves));
        }
        #endregion
    }
}
