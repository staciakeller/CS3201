using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Covid19Analysis.Model;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Covid19Analysis.View
{
    /// <summary>
    ///     Content Dialog to be displayed if a new Covid19DailyData matches an existing record
    /// </summary>
    public sealed partial class SimilarDataFoundDialog
    {
        #region Properties

        /// <summary>
        ///     the data that already exists in the app
        /// </summary>
        public Covid19DataCollection ExistingDataCollection { get; }

        /// <summary>
        ///     The new data to be added
        /// </summary>
        public Covid19DailyData NewData { get; set; }

        /// <summary>
        ///     the data which already exists in the app and matches the new data
        /// </summary>
        public Covid19DailyData ExistingData { get; }

        /// <summary>
        ///     if the future decision has been set
        /// </summary>
        public bool FutureDecisionChecked { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     the content dialog to be displayed if a matched data has been found in merging data
        /// </summary>
        /// <param name="existingDataSet">precondition= existingDataSet != null</param>
        /// <param name="newData">precondition= newData != null</param>
        public SimilarDataFoundDialog(Covid19DataCollection existingDataSet, Covid19DailyData newData)
        {
            this.InitializeComponent();

            this.ExistingDataCollection = existingDataSet;
            this.NewData = newData;
            this.ExistingData = existingDataSet.FindDataWith(this.NewData.DataDate, this.NewData.State);

            this.existingDataTextBlock.Text += this.ExistingData.ToString();
            this.newDataTextBlock.Text += this.NewData.ToString();
        }

        #endregion

        #region Methods

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.ExistingDataCollection.Covid19Data.Remove(this.ExistingData);
            this.ExistingDataCollection.AddDailyData(this.NewData);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.NewData = this.ExistingData;
        }

        private void ContentDialog_FutureDecisionChecked(object sender, RoutedEventArgs e)
        {
            this.FutureDecisionChecked = true;
        }

        #endregion
    }
}