using System.Collections.Generic;

namespace GettingStarted.Model
{
    /// <summary>
    ///     Defines a class roster.
    /// </summary>
    public class Roster
    {
        #region Properties

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public int Count => this.Students.Count;

        /// <summary>
        ///     Gets the students.
        /// </summary>
        /// <value>
        ///     The students.
        /// </value>
        public IList<Student> Students { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Roster" /> class.
        ///     Postcondition: Count == 3; Students populated with three demo students
        /// </summary>
        public Roster()
        {
            // Demos instantiating an object and init with an object initializer
            this.Students = new List<Student> { new Student("John", "Doe", 75) };

            this.addStudentsForDemoPurposes();
        }

        #endregion

        #region Methods

        private void addStudentsForDemoPurposes()
        {
            // Demos implicit typing with var; and demos creating and adding an object on separate lines of code
            var student = new Student("Jane", "Doe", 95);
            this.Students.Add(student);

            // Demo create and add on same line of code
            this.Students.Add(new Student("Sally", "White", 82));
        }

        #endregion

    }
}
