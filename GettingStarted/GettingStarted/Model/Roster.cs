using System;
using System.Collections.Generic;
using System.Text;

namespace GettingStarted.Model
{
    class Roster
    {
        public List<Student> Students { get; }
        public int Count => Students.Count;

        public Roster()
        {
            this.Students = new List<Student>
            {
                new Student("Stacia", "Keller", 10),
                new Student("Jaden", "Brown", 8),
                new Student("Karlie", "Jean", 12)
            };
        }
        public Roster(List<Student> students)
        {
            this.Students = students ?? throw new ArgumentNullException(nameof(students));
        }


        public override string ToString()
        {
            string students = "";
            foreach (Student student in this.Students)
            {
                students += student + Environment.NewLine;
            }

            return students;
        }
    }
}
