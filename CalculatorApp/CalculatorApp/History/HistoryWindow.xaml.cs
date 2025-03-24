using System.Windows;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            DataContext = HistoryManager.HistoryItems;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            HistoryManager.ClearHistory();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
