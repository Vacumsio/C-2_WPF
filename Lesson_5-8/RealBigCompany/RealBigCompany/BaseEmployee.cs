using System;

namespace RealBigCompany
{
    [Serializable]
    public class BaseEmployee : ICloneable
    {
        private string _name;
        private string _surName;
        private int _age;
        private int _experience;

        public BaseEmployee()
        {

        }
        public BaseEmployee(string Name, string SurName, int Age, Professions Prof, int Exp)
        {
            this.Name = Name;
            this.SurName = SurName;
            this.Age = Age;
            this.Experience = Exp;
            this.Profession = Prof;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 2) _name = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Name), "Должно иметь два и более символа");
                }

            }
        }

        public string SurName
        {
            get { return _surName; }
            set
            {
                if (value.Length >= 2) _surName = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(SurName), "Должно иметь два и более символа");
                }
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value >= 14 && value <= 85) _age = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Age), "Должен быть от 14 до 85 лет");
                }
            }
        }

        public int Experience
        {
            get { return _experience; }
            set
            {
                if (value <= Age) _experience = value;
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Experience), "Не может быть больше возраста");
                }
            }
        }
        public Professions Profession { get; }
        public virtual object Clone()
        {
            return new BaseEmployee(this.Name, this.SurName, this.Age, this.Profession, this.Experience);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseEmployee)) return false;
            BaseEmployee var = (BaseEmployee)obj;
            return var.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            string var = Name.ToLower().Trim()
                     + SurName.ToLower().Trim()
                     + Age
                     + Experience
                     + Profession;
            return var.ToString().GetHashCode();
        }

        public static bool operator ==(BaseEmployee emp1, BaseEmployee emp2)
        {
            if ((emp1 is null) && (emp2 is null)) return true;
            if ((emp1 is null) || (emp2 is null)) return false;
            return emp1.Equals(emp2);
        }
        public static bool operator !=(BaseEmployee emp1, BaseEmployee emp2)
        {
            if ((emp1 is null) && (emp2 is null)) return false;
            if ((emp1 is null) || (emp2 is null)) return true;
            return !emp1.Equals(emp2);
        }
    }
}
