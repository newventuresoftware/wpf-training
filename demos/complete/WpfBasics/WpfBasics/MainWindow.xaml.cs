using System;
using System.Windows;

namespace WpfBasics
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
            if (!decimal.TryParse(this.billBox.Text, out decimal bill))
                return;

            decimal tip = Convert.ToDecimal(this.tipSlider.Value) / 100;
            decimal total = bill * (1 + tip);

            this.totalBox.Text = $"Total: {total:C2}";

            short split = (short)this.comboBox.SelectedItem;
            if (split > 1)
            {
                decimal perPerson = total / split;
                this.totalBox.Text += Environment.NewLine + $"{perPerson:C2} per person";
            }
        }
    }
}
