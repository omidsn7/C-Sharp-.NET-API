using DemoLibrary;
using System;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for SunInfo.xaml
    /// </summary>
    public partial class SunInfo : Window
    {
        public SunInfo()
        {
            InitializeComponent();
        }

        private async void loadSunInfo_Click(object sender, RoutedEventArgs e)
        {
            sunriseText.Text = $"Sunrise is at : { SunProcessor.LoadSunResults().Result.Sunrise.ToLocalTime().ToShortDateString()}";
            sunsetText.Text = $"Sunset is at : { SunProcessor.LoadSunResults().Result.Sunset.ToLocalTime().ToShortDateString()}";
        }
    }
}
