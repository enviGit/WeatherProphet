using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WeatherProphet.ViewModels;

namespace WeatherProphet
{
    public partial class MainWindow : Window
    {
        private bool _isDarkTheme = true;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ToggleTheme(object sender, RoutedEventArgs e)
        {
            if (Resources.MergedDictionaries.Count > 0)
            {
                Resources.MergedDictionaries.Clear();

                if (_isDarkTheme)
                {
                    Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative) });
                    _isDarkTheme = false;
                }
                else
                {
                    Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative) });
                    _isDarkTheme = true;
                }
            }
        }
    }
}