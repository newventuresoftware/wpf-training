using System;
using System.Threading;
using System.Threading.Tasks;
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

            progress = new Progress<int>(p => this.progressBar.Value = p);
        }

        private CancellationTokenSource cancelToken;
        private IProgress<int> progress;

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (cancelToken != null)
            {
                cancelToken.Cancel();
                return;
            }

            cancelToken = new CancellationTokenSource();
            this.button.Content = "Cancel";

            bool success = await DoBackgroundJob(progress, cancelToken.Token);
            if (success)
            {
                MessageBox.Show("Success!");
            }

            this.button.Content = "Run";
            cancelToken = null;
            this.progressBar.Value = 0;
        }

        private Task<bool> DoBackgroundJob(IProgress<int> progress, CancellationToken cancellation)
        {
            return Task.Run<bool>(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (cancellation.IsCancellationRequested)
                        return false;

                    Thread.Sleep(100);
                    progress.Report(i + 1);
                }

                return true;
            });
        }
    }
}
