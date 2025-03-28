using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class Standard : Window
    {
        private double lastValue = 0;
        private string currentOperator = string.Empty;
        private bool isNewEntry = true;
        private string expressionChain = "";

        public bool IsDigitGrouping { get; set; } = false;

        public Standard()
        {
            InitializeComponent();
            LoadSettings();
            menuDigitGrouping.IsChecked = IsDigitGrouping;
            UpdateDisplay("0");
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string iconPath = System.IO.Path.Combine(exeFolder, "Calculator.png");
            this.Icon = new BitmapImage(new Uri(iconPath, UriKind.Absolute));
        }

        #region Buttons Digit and Operation Assignment

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardDigit_Click(sender, e, isNewEntry, txtDisplay.Text);
            string digitPressed = (sender as System.Windows.Controls.Button).Content.ToString();
            expressionChain += digitPressed;
            isNewEntry = result.NewEntry;
            txtDisplay.Text = result.NewExpression;
            UpdateDisplay(txtDisplay.Text);
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardOperator_Click(sender, e, lastValue, currentOperator, txtDisplay.Text);
            string operatorPressed = (sender as System.Windows.Controls.Button).Content.ToString();
            expressionChain += " " + operatorPressed + " ";
            lastValue = result.lastValue;
            currentOperator = result.currentOperator;
            isNewEntry = result.isNewEntry;
            txtDisplay.Text = result.newExpression;
            UpdateDisplay(txtDisplay.Text);
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            lastValue = OmniStandardFunctions.OmniStandardEqual_Click(sender, e, currentOperator, txtDisplay.Text, lastValue);
            txtDisplay.Text = lastValue.ToString();
            currentOperator = string.Empty;
            isNewEntry = false;
            UpdateDisplay(txtDisplay.Text);
            string operation = expressionChain + " = " + lastValue;
            HistoryManager.AddHistory(operation);
            expressionChain = lastValue.ToString();
        }

        #endregion

        #region Special Functions: BackSpace, C, CE, Negate, Sqrt, Square, Reciprocal, Exit

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardBackspace_Click(sender, e, txtDisplay.Text, isNewEntry);
            if(!string.IsNullOrEmpty(expressionChain))
            {
                expressionChain = expressionChain.Substring(0, expressionChain.Length - 1);
            }
            txtDisplay.Text = result.newExpression;
            isNewEntry = result.newEntry;
            UpdateDisplay(txtDisplay.Text);
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            isNewEntry = OmniStandardFunctions.OmniStandardCE_Click(sender, e);
            txtDisplay.Text = "0";
            expressionChain = "";
            UpdateDisplay(txtDisplay.Text);
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = OmniStandardFunctions.OmniStandardC_Click(sender, e);
            lastValue = 0;
            currentOperator = string.Empty;
            isNewEntry = true;
            expressionChain = "";
            UpdateDisplay(txtDisplay.Text);
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = OmniStandardFunctions.OmniStandardNegate_Click(sender, e, txtDisplay.Text);
            UpdateDisplay(txtDisplay.Text);
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardSqrt_Click(sender, e, txtDisplay.Text, isNewEntry);
            txtDisplay.Text = result.newExpression;
            isNewEntry = result.newEntry;
            UpdateDisplay(txtDisplay.Text);
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardSquare_Click(sender, e, txtDisplay.Text, isNewEntry);
            txtDisplay.Text = result.newExpression;
            isNewEntry = result.newEntry;
            UpdateDisplay(txtDisplay.Text);
        }

        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardReciprocal_Click(sender, e, txtDisplay.Text, isNewEntry);
            txtDisplay.Text = result.newExpression;
            isNewEntry = result.newEntry;
            UpdateDisplay(txtDisplay.Text);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Memory Buttons and History

        private void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniStandardFunctions.OmniStandardMemoryButton_Click(sender, e, txtDisplay.Text, isNewEntry);
            txtDisplay.Text = result.Val;
            isNewEntry = result.Entry;
            UpdateDisplay(txtDisplay.Text);
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }

        #endregion

        #region Cut, Copy, Paste, About, Digit Grouping

        private void MenuCut_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = OmniStandardFunctions.OmniStandardMenuCut_Click(sender, e, txtDisplay.Text);
            isNewEntry = true;
            UpdateDisplay(txtDisplay.Text);
        }

        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardMenuCopy_Click(sender, e, txtDisplay.Text);
        }

        private void MenuPaste_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = OmniStandardFunctions.OmniStandardMenuPaste_Click(sender, e);
            isNewEntry = false;
            UpdateDisplay(txtDisplay.Text);
        }

        private void MenuDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardMenuDigitGrouping_Click(sender, e);
            IsDigitGrouping = OmniStandardFunctions.IsDigitGrouping;
            menuDigitGrouping.IsChecked = IsDigitGrouping;
            UpdateDisplay(txtDisplay.Text);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardMenuAbout_Click(sender, e);
        }

        #endregion

        #region Update Display 
        private void UpdateDisplay(string text)
        {
            txtDisplay.Text = OmniStandardFunctions.OmniStandardUpdateDisplay(text, txtDisplay.Text);
        }
        #endregion

        #region KeyBoard Manipulation

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var (newChain, enterPressed) = OmniStandardFunctions.OmniStandardHistoryKeyManipulation(e, expressionChain);
            expressionChain = newChain;

            if (enterPressed)
            {
                var resultEqual = OmniStandardFunctions.OmniStandardWindow_KeyDown(e, isNewEntry, txtDisplay.Text, currentOperator, lastValue);
                txtDisplay.Text = resultEqual.NewExpression;
                lastValue = resultEqual.LastValue;
                currentOperator = resultEqual.CurrentOperator;
                isNewEntry = resultEqual.IsNewEntry;
                UpdateDisplay(txtDisplay.Text);

                string operation = expressionChain + " = " + lastValue;
                HistoryManager.AddHistory(operation);

                expressionChain = lastValue.ToString();
                return;
            }

            var result = OmniStandardFunctions.OmniStandardWindow_KeyDown(e, isNewEntry, txtDisplay.Text, currentOperator, lastValue);
            txtDisplay.Text = result.NewExpression;
            lastValue = result.LastValue;
            currentOperator = result.CurrentOperator;
            isNewEntry = result.IsNewEntry;
            UpdateDisplay(txtDisplay.Text);
        }


        #endregion

        #region Settings

        private void LoadSettings()
        {
            OmniStandardFunctions.OmniStandardLoadSettings();
            IsDigitGrouping = OmniStandardFunctions.IsDigitGrouping;
            menuDigitGrouping.IsChecked = IsDigitGrouping;
            UpdateDisplay(txtDisplay.Text);
        }

        private void SaveSettings()
        {
            OmniStandardFunctions.OmniStandardSaveSettings();
        }

        protected override void OnClosed(EventArgs e)
        {
            SaveSettings();
            base.OnClosed(e);
        }

        #endregion

        #region Calculator Mode

        private void ModeStandard_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardModeStandard_Click(sender, e);
        }

        private void ModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardModeProgrammer_Click(sender, e);
            this.Close();
        }

        private void ModeScientific_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardModeScientific_Click(sender, e);
            this.Close();
        }

        private void ModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            OmniStandardFunctions.OmniStandardModeEcuation_Click(sender, e);
            this.Close();
        }

        #endregion

        #region Themes
        private void ThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeHeavenLight_Click(sender, e);
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        private void ThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeTotalDarkness_Click(sender, e);
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        private void ThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeForestGreen_Click(sender, e);
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        private void ThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeQuantumRed_Click(sender, e);
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        private void ThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeDeepBlue_Click(sender, e);
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        private void ThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemePaleLavender_Click(sender, e);
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        private void ThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeSunnyYellow_Click(sender, e);
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        private void ThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeDeepOrange_Click(sender, e);
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        private void ThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeIntenseViolet_Click(sender, e);
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        private void ThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            //OmniStandardFunctions.OmniStandardThemeBabyPink_Click(sender, e);
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
