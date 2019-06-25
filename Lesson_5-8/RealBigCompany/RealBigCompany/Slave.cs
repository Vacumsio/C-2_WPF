using System;
using System.ComponentModel;

namespace RealBigCompany
{
    [Serializable]
    public class Slave : ICloneable
    {
        /// <summary>
        /// Имя, Фамилия, Возраст, Опыт работы
        /// </summary>
        public string name;
        public string surname;
        public int age;
        public int experience;

        #region Конструкторы
        public Slave()
        { }

        public Slave(string _name, string _surname, int _age, int _exp)
        {
            this.Name = _name;
            this.Surname = _surname;
            this.Age = _age;
            this.Experience = _exp;
        }

        #endregion

        #region Свойства полей

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
        public string Surname
        {
            get => surname;
            set
            {
                if (value.Length >= 2) surname = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Surname), "Фамилия должна быть длиннее двух символов");
                }
            }
        }
        public int Age
        {
            get => age;
            set
            {
                if (value >= 18 && value <= 65) age = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Age), "Возраст должен быть в диапозоне от 18 до 65 лет");
                }
            }
        }
        public int Experience
        {
            get => experience;
            set
            {
                if (value <= Age) experience = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Experience), "Стаж работы не может быть большеуказанного возраста");
                }
            }
        }
        #endregion

        #region Метод интерфейса

        public virtual object Clone()
        {
            return new Slave(this.Name, this.Surname, this.Age, this.Experience);
        }
        #endregion
    }
}
