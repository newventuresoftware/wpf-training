using System.Windows;
using System.Windows.Controls;

namespace WpfTemplateApp.Views
{
    /// <summary>
    /// Interaction logic for ItemsControlExample.xaml
    /// </summary>
    public partial class ItemsControlExample : UserControl
    {
        public ItemsControlExample()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"Clicked on {e.Parameter}");
        }
    }
}
