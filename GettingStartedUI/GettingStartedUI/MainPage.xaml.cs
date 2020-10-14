using System;
using System.Diagnostics;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GettingStartedUI.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GettingStartedUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.outputTextBlock.Text = "Greeting";

            
        }

        private void helloButton_Click(object sender, RoutedEventArgs e)
        {
            this.outputTextBlock.Text = "Hello";
        }

        private void goodbyeButton_Click(object sender, RoutedEventArgs e)
        {
            this.outputTextBlock.Text = "Good-Bye";
        }

        private async void getInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Get info invoked");

            var closeDialog = new ContentDialog()
            {
                Title = "Close",
                Content = "Do you want to close the app?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            var result = await closeDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Application.Current.Exit();
            }
        }

        private async void getDataButton_Click(object sender, RoutedEventArgs e)
        {
            var getNameDialog = new GetNameDialog();

            var result = await getNameDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.outputTextBlock.Text = getNameDialog.Name;
            }
        }
    }
}
 