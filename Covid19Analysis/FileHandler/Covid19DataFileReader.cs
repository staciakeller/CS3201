using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Covid19Analysis.Model;

namespace Covid19Analysis.FileHandler
{
    internal class Covid19DataFileReader
    {
        #region Data members

        private const string ValidLineExpression = @"\A\d{8},\w{2},\d+,\d+,\d+,\d+";

        #endregion

        #region Properties

        public Covid19DataCollection LoadedData { get; }

        public IDictionary<int, string> ErrorLines { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="Covid19DataFileReader" originaltag="see">Covid19DataFileReader</a> class.
        /// </summary>
        public Covid19DataFileReader()
        {
            this.LoadedData = new Covid19DataCollection();
            this.ErrorLines = new Dictionary<int, string>();
        }

        #endregion

        #region Methods

        /// <summary>Reads the file.</summary>
        /// <param name="file">The file.</param>
        public async Task ReadFile(StorageFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null");
            }

            var allText = await FileIO.ReadTextAsync(file);
            var modifiedText = allText.Replace("\r", "");

            var lines = modifiedText.Split("\n");
            var lineNum = 0;

            foreach (var aLine in lines)
            {
                lineNum++;

                if (isValidLine(aLine))
                {
                    //TODO header line is not error line
                    var aDataSet = aLine.Split(",");
                    var dailyData = parseDailyDataObject(aDataSet);
                    this.LoadedData.AddDailyData(dailyData);
                }
                else
                {
                    this.ErrorLines.Add(lineNum, aLine);
                }
            }
        }

        private static bool isValidLine(string line)
        {
            var m = Regex.Match(line, ValidLineExpression);
            return m.Success;
        }

        private bool lineStartsWithDate(string line)
        {
            var m = Regex.Match(line, @"\A\d{8}");
            return m.Success;
        }

        private static Covid19DailyData parseDailyDataObject(string[] aDataSet)
        {
            var date = DateTime.ParseExact(aDataSet.GetValue(DateField).ToString(), "yyyyMMdd",
                CultureInfo.InvariantCulture);
            var state = Enum.Parse<StateAbbreviations>(aDataSet.GetValue(StateField).ToString());
            var posInc = int.Parse(aDataSet.GetValue(PositivesField).ToString());
            var negInc = int.Parse(aDataSet.GetValue(NegativeField).ToString());
            var deaths = int.Parse(aDataSet.GetValue(DeathsField).ToString());
            var hospital = int.Parse(aDataSet.GetValue(HospitalField).ToString());

            var dailyData = new Covid19DailyData(date, state, posInc, negInc, deaths, hospital);
            return dailyData;
        }

        public string ErrorLinesToString()
        {
            var errorOutput = string.Empty;
            foreach (var (currKey, currValue) in this.ErrorLines)
            {
                errorOutput += $"Line {currKey}: {currValue}" + Environment.NewLine;
            }

            return errorOutput;
        }

        #endregion

        #region DataMembers

        private const int DateField = 0;
        private const int StateField = 1;
        private const int PositivesField = 2;
        private const int NegativeField = 3;
        private const int DeathsField = 4;
        private const int HospitalField = 5;

        #endregion
    }
}