using System;

namespace GettingStarted.Model
{
    /// <summary>Defines a Student data class.</summary>
    public class Student
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public string FullName => $"{this.FirstName} {this.LastName}";

        /// <summary>
        ///     Gets or sets the grade.
        /// </summary>
        /// <value>
        ///     The grade.
        /// </value>
        public int Grade { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Student" /> class.
        ///     Precondition: none
        ///     Postcondition: FirstName and LastName empty strings; Grade == 0
        /// </summary>
        public Student()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Grade = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Student" /> class.
        ///     Postcondition: FirstName == firstName; LastName == lastName; Grade == grade
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="grade">The grade.</param>
        /// <exception cref="ArgumentOutOfRangeException">grade - Must be between 0 and 100, inclusive.</exception>
        /// <exception cref="ArgumentNullException">
        ///     firstName
        ///     or
        ///     lastName
        /// </exception>
        public Student(string firstName, string lastName, int grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), "Must be between 0 and 100, inclusive.");
            }

            this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            this.Grade = grade;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Converts to string.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}: {this.Grade}";
        }

        #endregion
    }
}
