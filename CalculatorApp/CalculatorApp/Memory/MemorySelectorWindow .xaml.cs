using System;
using System.Collections.Generic;
using System.Windows;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class MemorySelectorWindow : Window
    {
        public double SelectedValue { get; private set; } = 0;
        public int SelectedIndex { get { return lstMemory.SelectedIndex; } }

        public MemorySelectorWindow()
        {
            InitializeComponent();
            LoadMemory();
        }

        private void LoadMemory()
        {
            if (Settings.Default.LastCalculatorMode == "Standard")
            {
                List<double> memoryItems = OmniStandardFunctions.GetMemoryStack();
                lstMemory.ItemsSource = memoryItems;
            }
            else if(Settings.Default.LastCalculatorMode == "Scientific")
            {
                List<double> memoryItems = OmniScientificFunctions.GetMemoryStack();
                lstMemory.ItemsSource = memoryItems;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (lstMemory.SelectedItem != null)
            {
                SelectedValue = (double)lstMemory.SelectedItem;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Te rog selectează un element din memorie.", "Selectează", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstMemory.SelectedIndex >= 0)
            {
                int indexToRemove = lstMemory.SelectedIndex;

                if (Settings.Default.LastCalculatorMode == "Standard")
                {
                    OmniStandardFunctions.OmniStandardRemoveMemoryAt(indexToRemove);
                }
                else if (Settings.Default.LastCalculatorMode == "Scientific")
                {
                    OmniScientificFunctions.OmniScientificRemoveMemoryAt(indexToRemove);
                }
                LoadMemory();
            }
            else
            {
                MessageBox.Show("Te rog selectează un element din memorie pentru a fi eliminat.",
                                "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
