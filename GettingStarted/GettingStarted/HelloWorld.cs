using GettingStarted.Model;
using System;

namespace GettingStarted
{
    public class HelloWorld
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            
            //C# type
            string name = "Stacia";
            //.NET type
            String job = "Student";

            //C# type
            int i = 0;
            //.NET type
            Int32 j = 0;

            //implicit typing
            var k = 3;
            var m = "3.2";

            var student = new Student()
            {
                FirstName = "Zach",
                LastName = "Miller"
            };

            Roster myRoster = new Roster();
            Console.WriteLine("Count: {0} ", myRoster.Count);
            Console.WriteLine(myRoster.ToString());

        }
    }
}
