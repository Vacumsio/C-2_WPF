using System;
using System.Collections.ObjectModel;

namespace RealBigCompany
{
    public class DataCenter
    {
        public ObservableCollection<Slave> EmployData { get; set; }
        public ObservableCollection<Zone> DepData { get; set; }
        Random random = new Random();

        public void DeleteDepartment(int index)
        {
            for (int i = EmployData.Count - 1; i >= 0; i--)
            {
                if (EmployData[i].DepartamentId == index)
                {
                    EmployData.RemoveAt(i);
                    //Console.WriteLine(i);
                }
            }
            for (int i = DepData.Count - 1; i >= 0; i--)
                if (DepData[i].DepartmentId == index)
                {
                    DepData.RemoveAt(i);
                    //Console.WriteLine(">>> "+i);
                }
        }


        public DataCenter(int CountDepartment, int CountEmployee)
        {
            EmployData = new ObservableCollection<Slave>();
            DepData = new ObservableCollection<Zone>();

            for (int i = 0; i < CountDepartment; i++)
            {
                DepData.Add(new Zone($"Департамент {i + 1}", i));
            }

            for (int i = 0; i < CountEmployee; i++)
            {
                EmployData.Add(new Slave($"Имя_{i + 1}", $"Фамилия_{i + 1}", random.Next(10, 65), random.Next(DepData.Count)));
            }

        }

    }
}
