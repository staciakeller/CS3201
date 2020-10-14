using System;

namespace GettingStarted.Model
{
    /// <summary>
    ///     <br />
    /// </summary>
    public class Student
    {
        #region Properties

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
        public int Grade { get; set; }

        #endregion

        #region Constructors

        public Student()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Grade = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the <a onclick="return false;" href="Student" originaltag="see">Student</a>
        ///     class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="grade">The grade.</param>
        /// <exception cref="ArgumentOutOfRangeException">grade - must be between 0 and 100</exception>
        /// <exception cref="ArgumentNullException">
        ///     firstName
        ///     or
        ///     lastName
        /// </exception>
        public Student(string firstName, string lastName, int grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), "must be between 0 and 100");
            }

            this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            this.Grade = grade;

            // TODO remember to compute average grade
        }

        #endregion

        #region Methods

        /// <summary>Converts to string.</summary>
        /// <returns>
        ///     A <a onclick="return false;" href="System.String" originaltag="see">System.String</a> that represents this
        ///     instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.FullName} : {this.Grade}";
        }

        #endregion
    }
}