using System;
using GettingStarted.Model;

namespace GettingStarted
{
    /// <summary>
    /// Entry point for the program.
    /// </summary>
    public class GettingStarted
    {
        #region Methods 

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Hello World!");

            // Demo instantiating a student and initialize properties with object initializer
            var student = new Student
            {
                FirstName = "Zach",
                LastName = "Miller"
            };

            Console.WriteLine(student.FullName);
            Console.WriteLine(student);
            Console.WriteLine(Environment.NewLine);

            var roster = new Roster();

            foreach (var currStudent in roster.Students)
            {
                Console.WriteLine($"{currStudent.FullName}: {currStudent.Grade}");

                // Another way to format the string output
                //Console.WriteLine("{0}: {1}", currStudent.FullName, currStudent.Grade);

                // Yet another to format the string using string concatenation
                //Console.WriteLine(currStudent.FullName + ": " + currStudent.Grade);

                // Of course, could use ToString, but remember to string is mostly used for
                // just identifying the object, e.g., when debugging, so shouldn't be used
                // for complex formatting.
                //Console.WriteLine(currStudent);
            }

            Console.WriteLine("Count: " + roster.Count);
        }

        #endregion
    }

}