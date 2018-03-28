using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace WpfRoutedApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button::Click");
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Grid::Click");
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Grid::Preview MouseLeftButtonDown");
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Button::Preview MouseLeftButtonDown");
        }
    }
}
