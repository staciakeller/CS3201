using System;

namespace Covid19Analysis.Model
{
    /// <summary>
    ///     A COVID19 data that contains information about COVID19 on a certain day in a certain state
    /// </summary>
    public class Covid19DailyData : IComparable<Covid19DailyData>
    {
        #region Properties

        /// <summary>Gets or sets the data date.</summary>
        /// <value>The data date.</value>
        public DateTime DataDate { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public StateAbbreviations? State { get; set; }

        /// <summary>Gets or sets the positive increase.</summary>
        /// <value>The positive test increase.</value>
        public int PositiveIncrease { get; set; }

        /// <summary>Gets or sets the negative increase.</summary>
        /// <value>The negative test increase.</value>
        public int NegativeIncrease { get; set; }

        /// <summary>
        ///     The total number of tests given
        /// </summary>
        public int TotalTest => Math.Abs(this.PositiveIncrease) + Math.Abs(this.NegativeIncrease);

        /// <summary>Gets or sets the death increase.</summary>
        /// <value>The death increase.</value>
        public int DeathIncrease { get; set; }

        /// <summary>Gets or sets the hospitalized increase.</summary>
        /// <value>The hospitalized increase.</value>
        public int HospitalizedIncrease { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="Covid19DailyData" originaltag="see">Covid19DailyData</a> class.
        /// </summary>
        public Covid19DailyData()
        {
            this.DataDate = DateTime.MinValue;
            this.State = null;
            this.PositiveIncrease = 0;
            this.NegativeIncrease = 0;
            this.DeathIncrease = 0;
            this.HospitalizedIncrease = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="Covid19DailyData" originaltag="see">Covid19DailyData</a> class.
        /// </summary>
        /// <param name="dataDate">The data date.</param>
        /// <param name="state">The state.</param>
        /// <param name="positiveIncrease">The positive increase.</param>
        /// <param name="negativeIncrease">The negative increase.</param>
        /// <param name="deathIncrease">The death increase.</param>
        /// <param name="hospitalizedIncrease">The hospitalized increase.</param>
        public Covid19DailyData(DateTime dataDate, StateAbbreviations state, int positiveIncrease, int negativeIncrease,
            int deathIncrease, int hospitalizedIncrease)
        {
            this.DataDate = dataDate;
            this.State = state;
            this.PositiveIncrease = positiveIncrease;
            this.NegativeIncrease = negativeIncrease;
            this.DeathIncrease = deathIncrease;
            this.HospitalizedIncrease = hospitalizedIncrease;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public int CompareTo(Covid19DailyData compareData)
        {
            return compareData == null ? 1 : this.DataDate.CompareTo(compareData.DataDate);
        }
        //TODO update CompareToMethod

        /// <summary>Finds the percent positive.</summary>
        /// <returns>
        ///     double value of percent <br />
        /// </returns>
        public double FindPercentPositive()
        {
            if (this.TotalTest != 0)
            {
                return this.PositiveIncrease / this.TotalTest * 100;
            }

            return 0;
        }
        //TODO make PercentPositive an expression body property

        /// <summary>Converts to string.</summary>
        /// <returns>
        ///     A <a onclick="return false;" href="System.String" originaltag="see">System.String</a> that represents this
        ///     instance.
        /// </returns>
        public override string ToString()
        {
            return
                $"On {this.DataDate.ToShortDateString()} in {this.State}: Positive Tests= {this.PositiveIncrease}";
        }

        #endregion
    }
}