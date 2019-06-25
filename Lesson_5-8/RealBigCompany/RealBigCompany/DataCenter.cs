using System;
using System.Collections.ObjectModel;

namespace RealBigCompany
{
    public class DataCenter
    {
        public ObservableCollection<Slave> SlaveDc { get; set; }
        public ObservableCollection<Master> MasterDc { get; set; }

        static Random rnd;

        public void DeleteMaster(int index)
        {
            for (int i = SlaveDc.Count - 1; i >= 0; i--)
            {
                if (SlaveDc[i].Experience == index)
                {
                    SlaveDc.RemoveAt(i);
                }
            }
        }

        public DataCenter(int countMaster, int countSlave)
        {
            rnd = new Random();
            SlaveDc = new ObservableCollection<Slave>();
            MasterDc = new ObservableCollection<Master>();

            for (int i = 0; i < countMaster; i++)
            {
                MasterDc.Add(new Master($"Хозяин {i + 1}"));
            }

            for (int i = 0; i < countSlave; i++)
            {
                SlaveDc.Add(new Slave($"Имя_{i + 1}", $"Фамилия_{i + 1}", rnd.Next(18, 65), rnd.Next(15)));
            }
        }
    }
}
