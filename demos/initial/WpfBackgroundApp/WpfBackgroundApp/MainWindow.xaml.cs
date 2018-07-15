using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace WpfBackgroundApp
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                progressBar.Value = i + 1;
            }
            MessageBox.Show("Success!");
        }
    }
}
