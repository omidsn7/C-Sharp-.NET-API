using DemoLibrary;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MaxNumber = 0;
        private int CurrentNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializedClient();
            nextImageButton.IsEnabled = false;
        }

        private async Task LoadImage(int ImageNum = 0)
        {
            var comic = await ComicProcessor.LoadComic(ImageNum);

            if (ImageNum == 0)
            {
                MaxNumber = comic.Num;
            }

            CurrentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            comicImage.Source = new BitmapImage(uriSource);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImage();
        }

        private async void previousImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNumber > 1)
            {
                CurrentNumber -= 1;
                nextImageButton.IsEnabled = true;
                await LoadImage(CurrentNumber);
                
                if (CurrentNumber == 1)
                {
                    previousImageButton.IsEnabled = false;
                }
            }
        }

        private async void nextImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNumber < MaxNumber)
            {
                CurrentNumber += 1;
                previousImageButton.IsEnabled = true;
                await LoadImage(CurrentNumber);

                if (CurrentNumber == MaxNumber)
                {
                    nextImageButton.IsEnabled = false;
                }
            }
        }

        private void sunInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SunInfo sunInfo = new SunInfo();
            sunInfo.Show();
        }
    }
}