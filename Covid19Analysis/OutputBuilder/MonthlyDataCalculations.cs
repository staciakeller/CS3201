using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Model;

namespace Covid19Analysis.OutputBuilder
{
    internal class MonthlyDataCalculations
    {
        #region Data members

        public Covid19DataCollection MonthlyData = new Covid19DataCollection();

        #endregion

        #region Properties

        public int Month { get; set; }

        /// <summary>Gets or sets the data to format.</summary>
        /// <value>The data to format.</value>
        public Covid19DataCollection DataToFormat { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="MonthlyDataCollector" originaltag="see">MonthlyDataCollector</a> class.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="dataToFormat">The data to format.</param>
        /// <exception cref="ArgumentNullException">dataToFormat</exception>
        public MonthlyDataCalculations(int month, Covid19DataCollection dataToFormat)
        {
            this.Month = month;
            this.DataToFormat = dataToFormat ?? throw new ArgumentNullException(nameof(dataToFormat));
            var monthQuery = this.DataToFormat.Covid19Data.Where(data => data.DataDate.Month == this.Month);
            foreach (var data in monthQuery)
            {
                this.MonthlyData.AddDailyData(data);
            }
        }

        #endregion

        #region Methods

        public DateTime DateOfFirstTest()
        {
            var theDataItem = this.MonthlyData.Covid19Data.First(data => data.TotalTest > 0);
            return theDataItem.DataDate;
        }

        public List<Covid19DailyData> DataOfHightestPositive()
        {
            var highestPos = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                 .Max(data => data.PositiveIncrease);
            var dataWithHighest = this.MonthlyData.Covid19Data.Where(data =>
                data.DataDate >= this.DateOfFirstTest() && data.PositiveIncrease == highestPos);
            return dataWithHighest.ToList();
        }

        public List<Covid19DailyData> DataOfLowestPositive()
        {
            var lowestPos = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                .Min(data => data.PositiveIncrease);
            var dataWithLowest = this.MonthlyData.Covid19Data.ToList()
                                     .FindAll(data => data.PositiveIncrease == lowestPos);
            return dataWithLowest;
        }

        public List<Covid19DailyData> DataOfLowestTests()
        {
            var lowestTests = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                  .Min(data => data.TotalTest);
            var dataWithLowest = this.MonthlyData.Covid19Data.ToList().FindAll(data => data.TotalTest == lowestTests);
            return dataWithLowest;
        }

        public List<Covid19DailyData> DataOfHightestTests()
        {
            var highestTests = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                   .Max(data => data.TotalTest);
            var dataWithHighest = this.MonthlyData.Covid19Data.ToList().FindAll(data => data.TotalTest == highestTests);
            return dataWithHighest;
        }

        public double AverageNumberPositive()
        {
            var averagePos = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                 .Average(data => data.PositiveIncrease);
            return averagePos;
        }

        public double AverageNumberTests()
        {
            var averageTotal = this.MonthlyData.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstTest())
                                   .Average(data => data.TotalTest);
            return averageTotal;
        }

        #endregion
    }
}