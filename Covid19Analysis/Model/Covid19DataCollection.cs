using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Covid19Analysis.Model
{
    /// <summary>
    ///     A Wrapper Class for the Covid19DailyData
    /// </summary>
    //TODO implement ICollection or IList
    public class Covid19DataCollection : IEnumerator
    {
        #region Data members

        /// <summary>The position</summary>
        private int position;

        #endregion

        #region Properties

        /// <summary>Gets or sets the covid19 data.</summary>
        /// <value>The covid19 data.</value>
        //TODO make Covid19Data private once interface is implemented
        public IList<Covid19DailyData
        > Covid19Data { get; set; }

        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        public object Current => this.Covid19Data[this.position];

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="Covid19DataCollection" originaltag="see">Covid19DataCollection</a> class.
        /// </summary>
        public Covid19DataCollection()
        {
            this.Covid19Data = new List<Covid19DailyData>();
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="Covid19DataCollection" originaltag="see">Covid19DataCollection</a> class.
        /// </summary>
        /// <param name="covid19Data">The covid19 data.</param>
        public Covid19DataCollection(IList<Covid19DailyData> covid19Data)
        {
            this.Covid19Data = covid19Data;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public bool MoveNext()
        {
            this.position++;
            return this.position < this.Covid19Data.Count;
        }

        /// <inheritdoc />
        public void Reset()
        {
            this.position = 0;
        }

        /// <summary>
        ///     Return a List of data from the specified state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<Covid19DailyData> GetDataFromState(StateAbbreviations state)
        {
            var aStateCollection = new List<Covid19DailyData>();
            foreach (var currDailyData in this.Covid19Data)
            {
                if (currDailyData.State == StateAbbreviations.GA)
                {
                    aStateCollection.Add(currDailyData);
                }
            }
            //TODO get get of hardcoded reference to GA
            return aStateCollection;
        }

        /// <summary>Adds the daily data.</summary>
        /// <param name="theData">The data.</param>
        /// <exception cref="ArgumentNullException">theData - must not be null</exception>
        public void AddDailyData(Covid19DailyData theData)
        {
            if (theData == null)
            {
                throw new ArgumentNullException(nameof(theData), "must not be null");
            }

            this.Covid19Data.Add(theData);
        }

        /// <summary>
        ///     Check if the Collection already contains an data object similar to the parameter
        /// </summary>
        /// <param name="theData"></param>
        /// precondition: theData != null
        /// <returns>true, if the collection has a member that matches the parameter</returns>
        public bool ContainsSimilarData(Covid19DailyData theData)
        {
            if (theData == null)
            {
                throw new ArgumentNullException(nameof(theData), "cannot be null");
            }

            var containsSimilar =
                this.Covid19Data.Any(data => data.DataDate == theData.DataDate && data.State == theData.State);
            return containsSimilar;
        }

        /// <summary>
        ///     Find a data object in the collection that meets the conditions
        /// </summary>
        /// <param name="date"></param>
        /// <param name="state"></param>
        /// precondition: data != null
        /// state != null
        /// <returns>Covid19DailyData that meets the conditions</returns>
        public Covid19DailyData FindDataWith(DateTime date, StateAbbreviations? state)
        {
            var existingData = this.Covid19Data.First(data => data.DataDate == date && data.State == state);
            return existingData;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Collection of {this.Covid19Data.Count} Covid19DailyData entries";
        }

        #endregion
    }
}