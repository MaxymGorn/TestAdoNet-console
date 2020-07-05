using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Student
    {
        public string Name { get; set; }
        [Range(0, 90000000000, ErrorMessage = "Eror age")]
        public int Age { get; set; }
        [Range(0, 90000000000, ErrorMessage = "Eror rate")]
        public double Rate { get; set; }

        public Student(string Name, int Age, double Rate)
        {
            this.Age = Age;
            this.Name = Name;
            this.Rate = Rate;
        }

        public override string ToString()
        {
            return $"{Name,20}, {Age,8}, {Rate, 8}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
