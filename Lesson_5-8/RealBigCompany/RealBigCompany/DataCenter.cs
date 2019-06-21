using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    public class DataCenter
    {
        public ObservableCollection<Employee> EmployeesDb { get; set; }
        public ObservableCollection<Department> DepartmentDb { get; set; }
        static Random random = new Random();

        public void DeleteDepartment(int index)
        {
            for (int i = EmployeesDb.Count - 1; i >= 0; i--)
            {
                if (EmployeesDb[i].DepartamentId == index)
                {
                    EmployeesDb.RemoveAt(i);
                    //Console.WriteLine(i);
                }
            }
            for (int i = DepartmentDb.Count - 1; i >= 0; i--)
                if (DepartmentDb[i].DepartmentId == index)
                {
                    DepartmentDb.RemoveAt(i);
                    //Console.WriteLine(">>> "+i);
                }
        }



        public DataCenter(int CountDepartment, int CountEmployee)
        {
            EmployeesDb = new ObservableCollection<Employee>();
            DepartmentDb = new ObservableCollection<Department>();

            for (int i = 0; i < CountDepartment; i++)
            {
                DepartmentDb.Add(new Department($"Департамент {i + 1}", i));
            }

            for (int i = 0; i < CountEmployee; i++)
            {
                EmployeesDb.Add(
                        new Employee($"Имя_{i + 1}",
                                     $"Фамилия_{i + 1}",
                                     random.Next(18, 30),
                                     random.Next(DepartmentDb.Count)));
            }


            //foreach (var item in DepartmentDb) Console.WriteLine(item);


            //Debug.WriteLine("-----");


            //foreach (var item in EmployeesDb) Console.WriteLine(item);


        }

    }
}
