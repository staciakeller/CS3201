using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Covid19Analysis.FileHandler;
using Covid19Analysis.Model;
using Covid19Analysis.OutputBuilder;
using Covid19Analysis.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Covid19Analysis
{
    //TODO set up repository and GitHub
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 355;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 850;

        /// <summary>
        ///     The data being analyzed by app
        /// </summary>
        public Covid19DataCollection LoadedData = new Covid19DataCollection();

        private bool loadButtonClicked;

        private ContentDialogResult futureDecision;

        #endregion

        #region Properties

        /// <summary>
        ///     the DataFormatter object
        /// </summary>
        public DataFormatter DataFormatter { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
            //TODO update title bar
            this.DataFormatter = new DataFormatter(this.LoadedData);
            this.DataFormatter.UpperThreshold = Convert.ToInt32(this.upperThresholdTextBox.Text);
            this.DataFormatter.LowerThreshold = Convert.ToInt32(this.lowerThresholdTextBox.Text);
        }

        #endregion

        #region Methods
        //TODO should auto update with threshold is changed
        private void upperThresholdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.DataFormatter.UpperThreshold = Convert.ToInt32(this.upperThresholdTextBox.Text);
        }

        private void lowerThresholdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.DataFormatter.LowerThreshold = Convert.ToInt32(this.lowerThresholdTextBox.Text);
        }

        private async void loadFile_Click(object sender, RoutedEventArgs e)
        {
            if (this.loadButtonClicked)
            {
                this.existingDataDialog();
                //TODO not displayed if clicked a second time
            }
            else
            {
                await this.loadNewFile();
            }
        }

        private async void existingDataDialog()
        {
            var existingContentDialog = new ContentDialog {
                Title = "Existing Data",
                Content =
                    "You have already loaded data. Would you like to override the existing data or merge new data?",
                PrimaryButtonText = "Override",
                SecondaryButtonText = "Merge"
            };

            var result = await existingContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await this.overrideData();
            }
            else if (result == ContentDialogResult.Secondary)
            {
                await this.mergeData();
            }
        }

        private async Task overrideData()
        {
            this.loadButtonClicked = false;
            this.LoadedData.Covid19Data.Clear();
            await this.loadNewFile();
        }

        private async Task mergeData()
        {
            var file = await generatePicker();

            if (file == null)
            {
                this.summaryTextBox.Text = "File selected was null.";
            }

            var newData = await readData(file);
            await this.handleMergingNewData(newData);

            this.writeData(this.LoadedData);
            this.loadButtonClicked = true;
        }

        private async Task handleMergingNewData(Covid19DataCollection newData)
        {
            foreach (var currData in newData.Covid19Data)
            {
                if (this.LoadedData.ContainsSimilarData(currData))
                {
                    await this.similarDataDialog(currData);
                }
                else
                {
                    this.LoadedData.AddDailyData(currData);
                }
            }
        }

        private async Task similarDataDialog(Covid19DailyData newDataItem)
        {
            var similarDataDialog = new SimilarDataFoundDialog(this.LoadedData, newDataItem);
            if (this.futureDecision == ContentDialogResult.Primary)
            {
                this.LoadedData.Covid19Data.Remove(similarDataDialog.ExistingData);
                this.LoadedData.Covid19Data.Add(newDataItem);
            }
            else if (this.futureDecision == ContentDialogResult.None)
            {
                var result = await similarDataDialog.ShowAsync();
                if (similarDataDialog.FutureDecisionChecked)
                {
                    this.futureDecision = result;
                }
            }
        }

        private static async Task<StorageFile> generatePicker()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".csv");
            openPicker.FileTypeFilter.Add(".txt");

            var file = await openPicker.PickSingleFileAsync();
            return file;
        }

        private async Task loadNewFile()
        {
            var file = await generatePicker();

            if (file == null)
            {
                this.summaryTextBox.Text = "File selected was null.";
            }

            this.LoadedData = await readData(file);
            this.writeData(this.LoadedData);

            this.loadButtonClicked = true;
        }

        private static async Task<Covid19DataCollection> readData(StorageFile file)
        {
            var fileReader = new Covid19DataFileReader();
            await fileReader.ReadFile(file);
            if (fileReader.ErrorLines.Count == 0)
            {
                return fileReader.LoadedData;
            }

            var errorContentDialog = new ContentDialog {
                Title = "Error on lines",
                Content = fileReader.ErrorLinesToString(),
                CloseButtonText = "Ok"
            };
            await errorContentDialog.ShowAsync();

            return fileReader.LoadedData;
        }

        private void writeData(Covid19DataCollection loadedData)
        {
            var dataFromSelectedState =
                new Covid19DataCollection(loadedData.GetDataFromState(StateAbbreviations.GA));
            this.DataFormatter.Covid19Data = dataFromSelectedState;
            this.summaryTextBox.Text = this.DataFormatter.FormatStateData(StateAbbreviations.GA) + Environment.NewLine;
            this.summaryTextBox.Text += this.DataFormatter.FormatMonthlyData();
        }

        #endregion
    }
}