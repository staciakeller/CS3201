using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Model;

namespace Covid19Analysis.OutputBuilder
{
    internal class StateDataCalculations
    {
        #region Data members

        private const int BucketsRange = 500;

        #endregion

        #region Properties

        public Covid19DataCollection DataToAnalyze { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="StateDataCalculations" originaltag="see">StateDataCalculations</a> class.
        /// </summary>
        /// <param name="dataToAnalyze">The data to analyze.</param>
        /// <exception cref="ArgumentNullException">dataToAnalyze</exception>
        public StateDataCalculations(Covid19DataCollection dataToAnalyze)
        {
            this.DataToAnalyze = dataToAnalyze ?? throw new ArgumentNullException(nameof(dataToAnalyze));
            var orderDatesQuery = this.DataToAnalyze.Covid19Data.OrderBy(data => data.DataDate).ToList();
            this.DataToAnalyze.Covid19Data = orderDatesQuery;

            //TODO use Sort Method to order the data by date
        }

        #endregion

        #region Methods

        public DateTime DateOfFirstPositive()
        {
            var data = this.DataToAnalyze.Covid19Data;
            var theDataItem = data.First(dataItem => dataItem.PositiveIncrease > 0);
            return theDataItem.DataDate;
        }

        public DateTime DateOfLastTest()
        {
            var data = this.DataToAnalyze.Covid19Data;
            var theDataItem = data[data.Count - 1];
            return theDataItem.DataDate;
        }

        public Covid19DailyData DataWithHighestNumberOfPositives()
        {
            var highestPos = this.DataToAnalyze.Covid19Data.Max(data => data.PositiveIncrease);
            var dataWithHighestPos = this.DataToAnalyze.Covid19Data.First(data => data.PositiveIncrease == highestPos);

            return dataWithHighestPos;
        }

        public Covid19DailyData DataWithHighestNumberOfNegatives()
        {
            var highestNeg = this.DataToAnalyze.Covid19Data.Max(data => data.NegativeIncrease);
            var dataWithHighestNeg = this.DataToAnalyze.Covid19Data.First(data => data.NegativeIncrease == highestNeg);
            return dataWithHighestNeg;
        }

        public Covid19DailyData DataWithHighestNumberTests()
        {
            var highestTests = this.DataToAnalyze.Covid19Data.Max(data => data.TotalTest);
            var dataWithHighest = this.DataToAnalyze.Covid19Data.First(data => data.TotalTest == highestTests);
            return dataWithHighest;
        }

        public Covid19DailyData DataWithHighestNumberDeaths()
        {
            var highestDeath = this.DataToAnalyze.Covid19Data.Max(data => data.DeathIncrease);
            var dataWithHighest = this.DataToAnalyze.Covid19Data.First(data => data.DeathIncrease == highestDeath);
            return dataWithHighest;
        }

        public Covid19DailyData DataWithHighestHospitalization()
        {
            var highestHospital = this.DataToAnalyze.Covid19Data.Max(data => data.HospitalizedIncrease);
            var dataWithHighest =
                this.DataToAnalyze.Covid19Data.First(data => data.HospitalizedIncrease == highestHospital);
            return dataWithHighest;
        }

        public Covid19DailyData DataWithHighestPercentPos()
        {
            var highestPercentPos = this.DataToAnalyze.Covid19Data.Max(data => data.FindPercentPositive());
            var dataWithHighest =
                this.DataToAnalyze.Covid19Data.First(data =>
                    Math.Abs(data.FindPercentPositive() - highestPercentPos) == 0);
            return dataWithHighest;
        }

        public double AverageNumberPositiveSinceFirst()
        {
            var dataAfterFirstPos =
                this.DataToAnalyze.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstPositive());
            var averagePos = dataAfterFirstPos.Average(data => data.PositiveIncrease);

            return averagePos;
        }

        public double FindPositivityRate()
        {
            var dataAfterFirstPos =
                this.DataToAnalyze.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstPositive());
            var dataAfterFirstArray = dataAfterFirstPos as Covid19DailyData[] ?? dataAfterFirstPos.ToArray();
            double sumPosTests = dataAfterFirstArray.Sum(data => data.PositiveIncrease);
            double sumTotalTests = dataAfterFirstArray.Sum(data => data.TotalTest);
            var rate = sumPosTests / sumTotalTests;
            return rate * 100;
        }

        /// <summary>Numbers the of days with positives greater than.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     int : number of days <br />
        /// </returns>
        public int NumberOfDaysWithPositivesGreaterThan(int value)
        {
            var dataAfterFirstPos =
                this.DataToAnalyze.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstPositive());
            var daysGreaterThanCount = dataAfterFirstPos.Count(data => data.PositiveIncrease > value);

            return daysGreaterThanCount;
        }

        /// <summary>Numbers the of days with positive less than.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     int : number of days   <br />
        /// </returns>
        public int NumberOfDaysWithPositiveLessThan(int value)
        {
            var dataAfterFirstPos =
                this.DataToAnalyze.Covid19Data.Where(data => data.DataDate >= this.DateOfFirstPositive());
            var daysLessThanCount = dataAfterFirstPos.Count(data => data.PositiveIncrease < value);

            return daysLessThanCount;
        }

        public Dictionary<string, int> PositiveCaseBreakdown()
        {
            var maxPos = this.DataWithHighestNumberOfPositives().PositiveIncrease;
            var numBuckets = (maxPos + BucketsRange - 1) / BucketsRange;
            var positiveCaseBreakdown = new Dictionary<string, int>();
            for (var i = 0; i < numBuckets; i++)
            {
                int startingValue;
                int endingValue;
                if (i == 0)
                {
                    startingValue = BucketsRange * i;
                    endingValue = startingValue + BucketsRange;
                }
                else
                {
                    startingValue = BucketsRange * i + 1;
                    endingValue = startingValue + (BucketsRange - 1);
                }

                var countDaysInBucket = this.DataToAnalyze.Covid19Data
                                            .Where(data => data.DataDate >= this.DateOfFirstPositive()).Count(data =>
                                                data.PositiveIncrease >= startingValue &&
                                                data.PositiveIncrease <= endingValue);
                positiveCaseBreakdown.Add($"{startingValue,5:N0} - {endingValue,5:N0}", countDaysInBucket);
            }

            return positiveCaseBreakdown;
        }

        #endregion
    }
}