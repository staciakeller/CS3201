using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Covid19Analysis.Model;

namespace Covid19Analysis.OutputBuilder
{
    /// <summary>
    ///     The DataFormatter class which formats the data output about COVID19 data
    /// </summary>
    public class DataFormatter
    {
        //TODO rename DataFormatter
        #region Properties

        /// <summary>
        ///     The Covid19Data that will be formatted to be displayed
        /// </summary>
        public Covid19DataCollection Covid19Data { get; set; }

        /// <summary>
        ///     the lower threshold for finding number days with positive cases below
        /// </summary>
        public int LowerThreshold { get; set; }

        /// <summary>
        ///     The upper threshold for finding days with positive cases above
        /// </summary>
        public int UpperThreshold { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="DataFormatter" originaltag="see">DataFormatter</a> class.
        /// </summary>
        /// <param name="covid19Data">The data to format.</param>
        public DataFormatter(Covid19DataCollection covid19Data)
        {
            this.Covid19Data = covid19Data;
            //this.LowerThreshold = 1000;
            //this.UpperThreshold = 2500;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Format the overall data from the selected state to be displayed
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public string FormatStateData(StateAbbreviations state)
        {
            var stateDataCalculations = new StateDataCalculations(this.Covid19Data);
            var stateData = state + ": " + Environment.NewLine;
            stateData +=
                $"First Positive Test occurred on {stateDataCalculations.DateOfFirstPositive().ToShortDateString()}\n";
            var highestPos = stateDataCalculations.DataWithHighestNumberOfPositives();
            stateData +=
                $"Highest Number of Positive Tests: {highestPos.PositiveIncrease:n0} occurred on {highestPos.DataDate.ToShortDateString()}\n";
            var highestNeg = stateDataCalculations.DataWithHighestNumberOfNegatives();
            stateData +=
                $"Highest Number of Negative Tests: {highestNeg.NegativeIncrease:n0} occurred on {highestNeg.DataDate.ToShortDateString()}\n";
            var highestTests = stateDataCalculations.DataWithHighestNumberTests();
            stateData +=
                $"Highest Number of Tests: {highestTests.TotalTest:n0} occurred on {highestTests.DataDate.ToShortDateString()}\n";
            var highestDeaths = stateDataCalculations.DataWithHighestNumberDeaths();
            stateData +=
                $"Highest Number of Deaths: {highestDeaths.DeathIncrease:n0} occurred on {highestDeaths.DataDate.ToShortDateString()}\n";
            var highestHospital = stateDataCalculations.DataWithHighestHospitalization();
            stateData +=
                $"Highest Number of Hospitalizations: {highestHospital.HospitalizedIncrease:n0} occurred on {highestHospital.DataDate.ToShortDateString()}\n";
            var highestPercent = stateDataCalculations.DataWithHighestPercentPos();
            stateData +=
                $"Highest Percentage Positive Tests: {highestPercent.FindPercentPositive()}% occurred on {highestPercent.DataDate.ToShortDateString()}\n";

            stateData +=
                $"Average number of Positive Tests: {stateDataCalculations.AverageNumberPositiveSinceFirst():n2}\n";
            stateData += $"Positivity Rate of all Tests:  {stateDataCalculations.FindPositivityRate():n2}% \n";
            stateData +=
                $"Number of Days with positives tests greater than {this.UpperThreshold}: {stateDataCalculations.NumberOfDaysWithPositivesGreaterThan(this.UpperThreshold):n0}\n";
            stateData +=
                $"Number of Days with positives tests less than {this.LowerThreshold}: {stateDataCalculations.NumberOfDaysWithPositiveLessThan(this.LowerThreshold):n0}\n";
            stateData += "BreakDown of Positive Cases: " + Environment.NewLine +
                         formatPositiveCaseBreakdown(stateDataCalculations);
            return stateData;
        }

        private static string formatPositiveCaseBreakdown(StateDataCalculations stateDataCalculations)
        {
            var caseBreakDown = stateDataCalculations.PositiveCaseBreakdown();

            return caseBreakDown.Aggregate(string.Empty, (current, currBucket) => current + $"{currBucket.Key} : {currBucket.Value,5:N0}" + Environment.NewLine);
        }

        /// <summary>
        ///     Format the monthly data to display for each month in the selected state's data
        /// </summary>
        /// <returns></returns>
        public string FormatMonthlyData()
        {
            var stateData = new StateDataCalculations(this.Covid19Data);
            var firstMonth = stateData.DateOfFirstPositive().Month;
            var lastMonth = stateData.DateOfLastTest().Month;

            var monthData = "MONTHLY DATA: \n";
            for (var month = firstMonth; month <= lastMonth; month++)
            {
                monthData += this.monthData(month);

                monthData += Environment.NewLine;
            }

            return monthData;
        }

        private string monthData(int month)
        {
            var monthlyDataCalculations = new MonthlyDataCalculations(month, this.Covid19Data);
            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            var year = monthlyDataCalculations.DateOfFirstTest().Year.ToString();
            var numDays = monthlyDataCalculations.MonthlyData.Covid19Data.Count.ToString();
            var monthData = $"{monthName} {year} ({numDays} days of data)" + Environment.NewLine;

            if (!this.containsMonth(month))
            {
                return monthData;
            }

            var highestPosData = monthlyDataCalculations.DataOfHightestPositive();
            var highestPosValue = highestPosData[0].PositiveIncrease;
            monthData +=
                $"Highest Number of Positive Tests: {highestPosValue:n0} occurred on {getAllDates(highestPosData)}" +
                Environment.NewLine;
            var lowestPosData = monthlyDataCalculations.DataOfLowestPositive();
            var lowestPosValue = lowestPosData[0].PositiveIncrease;
            monthData +=
                $"Lowest Number of Positive Tests: {lowestPosValue:n0} occurred on {getAllDates(lowestPosData)}" +
                Environment.NewLine;
            var highestTestsData = monthlyDataCalculations.DataOfHightestTests();
            var highestTestsValue = highestTestsData[0].TotalTest;
            monthData +=
                $"Highest Number of Total Tests: {highestTestsValue:n0} occurred on {getAllDates(highestTestsData)}" +
                Environment.NewLine;
            var lowestTestData = monthlyDataCalculations.DataOfLowestTests();
            var lowestTestValue = lowestTestData[0].TotalTest;
            monthData +=
                $"Lowest Number of Total Tests: {lowestTestValue:n0} occurred on {getAllDates(lowestTestData)}" +
                Environment.NewLine;

            monthData +=
                $"Average Number of Positive Tests: {monthlyDataCalculations.AverageNumberPositive():n2}" +
                Environment.NewLine;
            monthData += $"Average Number of Total Tests: {monthlyDataCalculations.AverageNumberTests():n2}" +
                         Environment.NewLine;

            return monthData;
        }

        private bool containsMonth(int month)
        {
            var containsMonthQuery = this.Covid19Data.Covid19Data.Any(data => data.DataDate.Month == month);
            return containsMonthQuery;
        }

        private static string getAllDates(IEnumerable<Covid19DailyData> data)
        {
            var formattedDates = string.Empty;
            foreach (var currData in data)
            {
                var day = currData.DataDate.Day;
                formattedDates += convertOrdinalDate(day) + ", ";
            }

            return formattedDates;
        }

        private static string convertOrdinalDate(int date)
        {
            string suffix;
            switch (date)
            {
                case 1:
                case 21:
                case 31:
                    suffix = "st";
                    break;
                case 2:
                case 22:
                    suffix = "nd";
                    break;
                case 3:
                case 23:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return date + suffix;
        }

        #endregion
    }
}