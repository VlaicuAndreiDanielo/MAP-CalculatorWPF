using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class Scientific : Window
    {
        private double lastValue = 0;
        private string currentOperator = string.Empty;
        private bool isNewEntry = true;
        private double radicalBase = 0;
        private bool isRadicalPending = false;
        private string expressionChain = "";

        public bool IsDigitGrouping { get; set; } = false;

        public Scientific()
        {
            InitializeComponent();
            LoadSettings();
            menuDigitGrouping.IsChecked = IsDigitGrouping;
            UpdateDisplay("0");
        }

        #region Display Update
        private void UpdateDisplay(string text)
        {
            txtDisplayScientific.Text = OmniScientificFunctions.OmniScientificUpdateDisplay(text);
        }
        #endregion

        #region Keyboard Manipulation
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var (newChain, enterPressed) = OmniScientificFunctions.OmniScientificHistoryKeyManipulation(e, expressionChain);
            expressionChain = newChain;

            if (enterPressed)
            {
                var resultEqual = OmniScientificFunctions.OmniScientificWindow_KeyDown(e, txtDisplayScientific.Text,
                    isNewEntry, isRadicalPending, radicalBase, currentOperator, lastValue);

                txtDisplayScientific.Text = resultEqual.newExpression;
                isNewEntry = resultEqual.newEntry;
                isRadicalPending = resultEqual.newRadicalPending;
                radicalBase = resultEqual.newRadicalBase;
                currentOperator = resultEqual.newOperator;
                lastValue = resultEqual.newLastValue;
                UpdateDisplay(txtDisplayScientific.Text);

                string operation = expressionChain + " = " + lastValue;
                HistoryManager.AddHistory(operation);

                expressionChain = lastValue.ToString();
                return;
            }
            var result = OmniScientificFunctions.OmniScientificWindow_KeyDown(e, txtDisplayScientific.Text,
                isNewEntry, isRadicalPending, radicalBase, currentOperator, lastValue);

            txtDisplayScientific.Text = result.newExpression;
            isNewEntry = result.newEntry;
            isRadicalPending = result.newRadicalPending;
            radicalBase = result.newRadicalBase;
            currentOperator = result.newOperator;
            lastValue = result.newLastValue;
            UpdateDisplay(txtDisplayScientific.Text);
        }

        #endregion

        #region Operators and Numbers Buttons

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            string digitPressed = (sender as System.Windows.Controls.Button).Content.ToString();
            expressionChain += digitPressed;
            txtDisplayScientific.Text = OmniScientificFunctions.OmniScientificDigit_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            isNewEntry = false;
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificOperator_Click(sender, e, txtDisplayScientific.Text, isNewEntry, isRadicalPending, radicalBase, currentOperator, lastValue);
            string operatorPressed = (sender as System.Windows.Controls.Button).Content.ToString();
            expressionChain += " " + operatorPressed + " ";
            currentOperator = result.op;
            isNewEntry = result.newEntry;
            lastValue = result.lastVal;
            UpdateDisplay(lastValue.ToString());
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificEqual_Click(sender, e, txtDisplayScientific.Text, isNewEntry, isRadicalPending, radicalBase, currentOperator, lastValue);

            isNewEntry = result.isNewEntry;
            isRadicalPending = result.isRadicalPending;
            currentOperator = result.currentOperator;
            lastValue = result.lastValue;
            if (radicalBase != 0)
            {
                string operation = radicalBase.ToString() + "√" + txtDisplayScientific.Text + "=" + txtDisplayScientific.Text;
                HistoryManager.AddHistory(operation);
            }
            else
            {
                string operation = expressionChain + " = " + lastValue;
                HistoryManager.AddHistory(operation);
            }

            UpdateDisplay(lastValue.ToString());
            expressionChain = lastValue.ToString();
        }
        #endregion

        #region Trigonometric, Exponential, Logarithmic, Factorial

        private void Trig_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificTrig_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string op = (sender as Button).Content.ToString().ToLower();
            string operation = op.ToUpper() + "(" + txtDisplayScientific.Text + ")" + "=" + result.result.ToString();
            HistoryManager.AddHistory(operation);

            UpdateDisplay(result.result.ToString());
            isNewEntry = result.newEntry;
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificExp_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string op = (sender as Button).Content.ToString().ToLower();
            string operation = "Exp(" + txtDisplayScientific.Text + ") =" + result.res.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.res.ToString());
            isNewEntry = result.newEntry;
        }

        private void Ln_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificLn_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string op = (sender as Button).Content.ToString().ToLower();
            string operation = "Ln(" + txtDisplayScientific.Text + ") =" + result.res.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.res.ToString());
            isNewEntry = result.newEntry;
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificLog_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string op = (sender as Button).Content.ToString().ToLower();
            string operation = "Log(" + txtDisplayScientific.Text + ") =" + result.res.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.res.ToString());
            isNewEntry = result.newEntry;
        }

        private void Factorial_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificFactorial_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string op = (sender as Button).Content.ToString().ToLower();
            string operation = txtDisplayScientific.Text + "! =" + result.fact.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.fact.ToString());
            isNewEntry = result.newEntry;
        }
        #endregion

        #region Special Functions: Backspace, C, CE, Negate, Reciprocal, Square, RadicalXY, Exit

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificBackspace_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            if (!string.IsNullOrEmpty(expressionChain))
            {
                expressionChain = expressionChain.Substring(0, expressionChain.Length - 1);
            }
            UpdateDisplay(result.exp);
            isNewEntry = result.newEntry;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            lastValue = 0;
            currentOperator = string.Empty;
            isNewEntry = true;
            expressionChain = "";
            UpdateDisplay(OmniScientificFunctions.OmniScientificC_Click(sender, e));
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            isNewEntry = OmniStandardFunctions.OmniStandardCE_Click(sender, e);
            expressionChain = "";
            UpdateDisplay("0");
            
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            double result = OmniScientificFunctions.OmniScientificNegate_Click(sender, e, txtDisplayScientific.Text);
            UpdateDisplay(result.ToString());
        }

        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificReciprocal_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string operation = "1/" + txtDisplayScientific.Text + "=" + result.res.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.res.ToString());
            isNewEntry = result.newEntry;
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificSquare_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            string operation = txtDisplayScientific.Text + "^2 " + "=" + result.res.ToString();
            HistoryManager.AddHistory(operation);
            UpdateDisplay(result.res.ToString());
            isNewEntry = result.newEntry;
        }

        private void RadicalXY_Click(object sender, RoutedEventArgs e)
        {
                var result = OmniScientificFunctions.OmniScientificRadicalXY_Click(sender, e, txtDisplayScientific.Text, isNewEntry, radicalBase, isRadicalPending);

                txtDisplayScientific.Text = result.expression;
                isNewEntry = result.isNewEntry;
                lastValue = result.radicalBase;  
                isRadicalPending = result.isRadicalPending;
                radicalBase = result.radicalBase;
                UpdateDisplay(txtDisplayScientific.Text);
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Memory Functions and History

        private void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniScientificFunctions.OmniScientificMemoryButton_Click(sender, e, txtDisplayScientific.Text, isNewEntry);
            txtDisplayScientific.Text = result.Val;
            isNewEntry = result.Entry;
            UpdateDisplay(txtDisplayScientific.Text);
            
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }
        #endregion

        #region Menu Functions: Cut, Copy, Paste, Digit Grouping, About

        private void MenuCut_Click(object sender, RoutedEventArgs e)
        {
            txtDisplayScientific.Text = OmniScientificFunctions.OmniScientificMenuCut_Click(sender, e, txtDisplayScientific.Text);
            UpdateDisplay("0");
            isNewEntry = true;
        }

        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificMenuCopy_Click(sender, e, txtDisplayScientific.Text);
        }

        private void MenuPaste_Click(object sender, RoutedEventArgs e)
        {
            txtDisplayScientific.Text = OmniScientificFunctions.OmniScientificMenuPaste_Click(sender, e, isNewEntry);
            UpdateDisplay(txtDisplayScientific.Text);
            isNewEntry = false;
        }

        private void MenuDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificMenuDigitGrouping_Click(sender, e);
            IsDigitGrouping = OmniScientificFunctions.IsDigitGrouping;
            menuDigitGrouping.IsChecked = IsDigitGrouping;
            UpdateDisplay(txtDisplayScientific.Text);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificMenuAbout_Click(sender, e);
        }
        #endregion

        #region Parentheses

        private void Paren_Click(object sender, RoutedEventArgs e)
        {
            txtDisplayScientific.Text = OmniScientificFunctions.OmniScientificParen_Click(sender, e, txtDisplayScientific.Text);
            UpdateDisplay(txtDisplayScientific.Text);
        }
        #endregion

        #region Settings 

        private void LoadSettings()
        {
            OmniScientificFunctions.OmniScientificLoadSettings();
            IsDigitGrouping = OmniScientificFunctions.IsDigitGrouping;
        }

        private void SaveSettings()
        {
            OmniScientificFunctions.OmniScientificSaveSettings();
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
            OmniScientificFunctions.OmniScientificModeStandard_Click(sender, e);
            this.Close();
        }

        private void ModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificModeProgrammer_Click(sender, e);
            this.Close();
        }

        private void ModeScientific_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificModeScientific_Click(sender, e);
        }

        private void ModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            OmniScientificFunctions.OmniScientificModeEcuation_Click(sender, e);
            this.Close();
        }
        #endregion

        #region Themes
        private void ThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeHeavenLight_Click(sender, e);
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        private void ThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeTotalDarkness_Click(sender, e);
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        private void ThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeForestGreen_Click(sender, e);
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        private void ThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeQuantumRed_Click(sender, e);
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        private void ThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeDeepBlue_Click(sender, e);
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        private void ThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemePaleLavender_Click(sender, e);
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        private void ThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeSunnyYellow_Click(sender, e);
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        private void ThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeDeepOrange_Click(sender, e);
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        private void ThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeIntenseViolet_Click(sender, e);
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        private void ThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            //OmniScientificFunctions.OmniScientificThemeBabyPink_Click(sender, e);
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
