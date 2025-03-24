using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Linq.Expressions;

namespace CalculatorApp.OmniCalculatorFunctions
{
    static class OmniScientificFunctions
    {
        private static List<double> memoryStack = new List<double>();

        public static bool IsDigitGrouping { get; set; } = false;
        public static int? SelectedMemoryIndex { get; set; } = null;

        #region Display Update and Update Display after Grouping

        private static string OmniScientificGroupIntegerPartPreserveFraction(string text)
        {
            bool isNegative = false;
            if (text.StartsWith("-"))
            {
                isNegative = true;
                text = text.Substring(1);
            }

            int dotIndex = text.IndexOf('.');
            if (dotIndex < 0)
            {
                if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double intVal))
                {
                    text = intVal.ToString("#,##0", CultureInfo.CurrentCulture);
                }
            }
            else
            {
                string intPart = text.Substring(0, dotIndex);
                string fracPart = text.Substring(dotIndex + 1);
                if (double.TryParse(intPart, NumberStyles.Any, CultureInfo.CurrentCulture, out double intVal))
                {
                    intPart = intVal.ToString("#,##0", CultureInfo.CurrentCulture);
                    text = intPart + "." + fracPart;
                }
            }

            if (isNegative)
                text = "-" + text;
            return text;
        }

        public static string OmniScientificUpdateDisplay(string text)
        {
            string expression;
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double number))
            {
                if (IsDigitGrouping)
                {
                    expression = OmniScientificGroupIntegerPartPreserveFraction(number.ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    expression = number.ToString(CultureInfo.CurrentCulture);
                }
            }
            else
            {
                expression = text;
            }
            return expression;
        }

        #endregion

        #region KeyBoard Manipulation

        public static (string newExpression, bool newEntry, bool newRadicalPending, double newRadicalBase, string newOperator, double newLastValue) OmniScientificWindow_KeyDown(
            KeyEventArgs e, string expression, bool isNewEntry, bool isRadicalPending, double radicalBase, string currentOperator, double lastValue)
        {
            string newExpression = expression;
            bool newEntry = isNewEntry;
            bool newRadicalPending = isRadicalPending;
            double newRadicalBase = radicalBase;
            string newOperator = currentOperator;
            double newLastValue = lastValue;

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    if (e.Key == Key.D9)
                    {
                        newExpression = OmniScientificParen_Click(new Button() { Content = "(" }, null, newExpression);
                        newEntry = false;
                        return (newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                    }
                    else if (e.Key == Key.D0)
                    {
                        newExpression = OmniScientificParen_Click(new Button() { Content = ")" }, null, newExpression);
                        newEntry = false;
                        return (newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                    }
                }
                string digit = (e.Key - Key.D0).ToString();
                newExpression = OmniScientificDigit_Click(new Button() { Content = digit }, null, newExpression, newEntry);
                newEntry = false;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                newExpression = OmniScientificDigit_Click(new Button() { Content = digit }, null, newExpression, newEntry);
                newEntry = false;
            }
            else if (e.Key == Key.Add || (e.Key == Key.OemPlus && Keyboard.Modifiers != ModifierKeys.Shift))
            {
                var opResult = OmniScientificOperator_Click(new Button() { Content = "+" }, null, newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                newOperator = opResult.op;
                newEntry = opResult.newEntry;
                newLastValue = opResult.lastVal;
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                var opResult = OmniScientificOperator_Click(new Button() { Content = "-" }, null, newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                newOperator = opResult.op;
                newEntry = opResult.newEntry;
                newLastValue = opResult.lastVal;
            }
            else if (e.Key == Key.Multiply)
            {
                var opResult = OmniScientificOperator_Click(new Button() { Content = "*" }, null, newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                newOperator = opResult.op;
                newEntry = opResult.newEntry;
                newLastValue = opResult.lastVal;
            }
            else if (e.Key == Key.Divide)
            {
                var opResult = OmniScientificOperator_Click(new Button() { Content = "/" }, null, newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                newOperator = opResult.op;
                newEntry = opResult.newEntry;
                newLastValue = opResult.lastVal;
            }
            else if (e.Key == Key.Enter)
            {
                var eqResult = OmniScientificEqual_Click(null, null, newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
                newEntry = eqResult.isNewEntry;
                newRadicalPending = eqResult.isRadicalPending;
                newOperator = eqResult.currentOperator;
                newLastValue = eqResult.lastValue;
                newExpression = newLastValue.ToString();
            }
            else if (e.Key == Key.Back)
            {
                var bsResult = OmniScientificBackspace_Click(null, null, newExpression, newEntry);
                newExpression = bsResult.exp;
                newEntry = bsResult.newEntry;
            }
            else if (e.Key == Key.Escape)
            {
                newExpression = OmniScientificC_Click(null, null);
                newLastValue = 0;
                newOperator = "";
                newEntry = true;
            }
            return (newExpression, newEntry, newRadicalPending, newRadicalBase, newOperator, newLastValue);
        }

        public static (string newHistoryChain, bool enterPressed) OmniScientificHistoryKeyManipulation(KeyEventArgs e, string currentHistoryChain)
        {
            bool isEnter = false;
            string updatedChain = currentHistoryChain;

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                updatedChain += (e.Key - Key.D0).ToString();
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                updatedChain += (e.Key - Key.NumPad0).ToString();
            }
            else if (e.Key == Key.Add || (e.Key == Key.OemPlus && Keyboard.Modifiers != ModifierKeys.Shift))
            {
                updatedChain += " + ";
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                updatedChain += " - ";
            }
            else if (e.Key == Key.Multiply)
            {
                updatedChain += " * ";
            }
            else if (e.Key == Key.Divide)
            {
                updatedChain += " / ";
            }
            else if (e.Key == Key.Enter)
            {
                isEnter = true;
            }

            return (updatedChain, isEnter);
        }

        #endregion

        #region Operators and Numbers Buttons
        public static string OmniScientificDigit_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            string digit = (sender as Button).Content.ToString();
            if (isNewEntry)
            {
                expression = (digit == ".") ? "0." : digit;
                isNewEntry = false;
            }
            else
            {
                if (digit == "." && expression.Contains("."))
                    return expression;
                expression += digit;
            }

            if (IsDigitGrouping)
            {
                expression = OmniScientificGroupIntegerPartPreserveFraction(expression);
            }
            return expression;
        }

        public static (string op, bool newEntry, double lastVal) OmniScientificOperator_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry, bool isRadicalPending, double radicalBase, string currentOperator, double lastValue)
        {
            lastValue = OmniScientificPerformCalculation(expression, isNewEntry, isRadicalPending, radicalBase, currentOperator, lastValue);
            currentOperator = (sender as Button).Content.ToString();
            isNewEntry = true;
            return (currentOperator, isNewEntry, lastValue);
        }

        public static (bool isNewEntry, bool isRadicalPending, string currentOperator, double lastValue) OmniScientificEqual_Click
            (object sender, RoutedEventArgs e, string expression, bool isNewEntry, bool isRadicalPending, double radicalBase, string currentOperator, double lastValue)
        {

            if (isRadicalPending)
            {
                if (double.TryParse(expression, out double radicand))
                {

                    if (radicalBase == 0)
                    {
                        MessageBox.Show("Ordinul radicalului nu poate fi zero!",
                                        "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        double result = Math.Pow(radicand, 1.0 / radicalBase);
                    }
                }
                else
                {
                    MessageBox.Show("Valoare invalidă pentru radicand!",
                                    "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                isRadicalPending = false;
                isNewEntry = true;
            }
            else
            if (expression.Contains("(") || expression.Contains(")"))
            {
                try
                {
                    var dt = new System.Data.DataTable();
                    var result = dt.Compute(expression, "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la evaluarea expresiei: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                lastValue = OmniScientificPerformCalculation(expression,isNewEntry,isRadicalPending, radicalBase, currentOperator, lastValue);
                currentOperator = string.Empty;
                isNewEntry = true;
            }

            return (isNewEntry, isRadicalPending, currentOperator, lastValue);
        }

        private static double OmniScientificPerformCalculation(string expression, bool isNewEntry, bool isRadicalPending, double radicalBase, string currentOperator, double lastValue)
        {
            if (!double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double input))
            {
                MessageBox.Show("Format numeric invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!string.IsNullOrEmpty(currentOperator))
            {
                switch (currentOperator)
                {
                    case "+":
                        lastValue += input;
                        break;
                    case "-":
                        lastValue -= input;
                        break;
                    case "*":
                        lastValue *= input;
                        break;
                    case "/":
                        if (input == 0)
                        {
                            MessageBox.Show("Împărțire la 0!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        lastValue /= input;
                        break;
                    case "%":
                        lastValue %= input;
                        break;
                }
            }
            else
            {
                lastValue = input;
            }
            return lastValue;
        }

        #endregion

        #region Trigonometric Functions

        public static (double result, bool newEntry) OmniScientificTrig_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string op = (sender as Button).Content.ToString().ToLower();
            double result = 0;

            switch (op)
            {
                case "sin":
                    result = Math.Sin(val);
                    isNewEntry = false;
                    break;
                case "cos":
                    result = Math.Cos(val);
                    isNewEntry = false;
                    break;
                case "tan":
                    result = Math.Tan(val);
                    isNewEntry = false;
                    break;
                case "ctg":
                    if (Math.Tan(val) == 0)
                    {
                        MessageBox.Show("Eroare la ctg (tan = 0)!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    result = 1 / Math.Tan(val);
                    isNewEntry = false;
                    break;
            }
            return (result, isNewEntry);
        }

        #endregion

        #region Exponential, Logarithmic, Factorial Functions

        public static (double res, bool newEntry) OmniScientificExp_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double result = Math.Exp(val);
            isNewEntry = true;
            return (result, isNewEntry);
        }

        public static (double res, bool newEntry) OmniScientificLn_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (val <= 0)
            {
                MessageBox.Show("ln(x) definit pentru x>0!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double result = Math.Log(val);

            isNewEntry = true;
            return (result, isNewEntry);
        }
       
        public static (double res, bool newEntry) OmniScientificLog_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (val <= 0)
            {
                MessageBox.Show("log10(x) definit pentru x>0!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double result = Math.Log10(val);

            isNewEntry = true;
            return(result, isNewEntry);
        }

        public static (long fact, bool newEntry) OmniScientificFactorial_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (val < 0 || val != Math.Floor(val))
            {
                MessageBox.Show("Factorial definit pentru numere întregi >= 0!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            long n = (long)val;
            long fact = 1;
            for (long i = 1; i <= n; i++)
                fact *= i;

            isNewEntry = true;
            return (fact, isNewEntry);
        }

        #endregion

        #region Special Functions: BackSpace, C, CE, Negate, RadicalXY, Square, Reciprocal, Exit
        public static (string exp, bool newEntry) OmniScientificBackspace_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!isNewEntry && expression.Length > 1)
            {
                expression = expression.Substring(0, expression.Length - 1);
            }
            else
            {
                expression = OmniScientificUpdateDisplay("0");
                isNewEntry = true;
            }
            return (expression, isNewEntry);
        }


        public static void OmniScientificBtnExit_Click(object sender, RoutedEventArgs e)
        {
            // Se poate apela this.Close() în fereastra apelantă!
        }

        public static bool OmniScientificCE_Click(object sender, RoutedEventArgs e)
        {
            return true;
        }

        public static string OmniScientificC_Click(object sender, RoutedEventArgs e)
        {
            return "0";
        }

        public static double OmniScientificNegate_Click(object sender, RoutedEventArgs e, string expression)
        {
            if (double.TryParse(expression, out double val))
            {
                val = -val;
            }
            return val;
        }


        public static (double res, bool newEntry) OmniScientificReciprocal_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (val == 0)
            {
                MessageBox.Show("Împărțirea la 0 nu este permisă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double result = 1 / val;
            isNewEntry = true;
            return (result, isNewEntry);
        }

        public static (double res,bool newEntry) OmniScientificSquare_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!double.TryParse(expression, out double val))
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            double result = val * val;
            isNewEntry = true;
            return (result, isNewEntry);
        }

        public static (string expression, bool isNewEntry, double radicalBase, bool isRadicalPending) OmniScientificRadicalXY_Click(object sender, RoutedEventArgs exprex, string expression, bool isNewEntry, double radicalBase, bool isRadicalPending)
        {
            if (double.TryParse(expression, out double index))
            {
                radicalBase = index;
                isRadicalPending = true;
                expression = "0";
                isNewEntry = true;
            }
            else
            {
                MessageBox.Show("Valoare invalidă pentru ordinul radicalului!",
                                "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return (expression, isNewEntry, radicalBase, true);
        }
        #endregion

        #region Memory Buttons

        public static (string Val, bool Entry) OmniScientificMemoryButton_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            string op = (sender as Button).Content.ToString();
            if (!double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double currentVal))
                return (expression, isNewEntry);

            switch (op)
            {
                case "MC":
                    memoryStack.Clear();
                    isNewEntry = true;
                    SelectedMemoryIndex = null;
                    break;
                case "M+":
                    if (SelectedMemoryIndex.HasValue)
                    {
                        memoryStack[SelectedMemoryIndex.Value] += currentVal;
                    }
                    else
                    {
                        if (memoryStack.Count > 0)
                            memoryStack[memoryStack.Count - 1] += currentVal;
                        else
                            memoryStack.Add(currentVal);
                    }
                    isNewEntry = true;
                    break;
                case "M-":
                    if (SelectedMemoryIndex.HasValue)
                    {
                        memoryStack[SelectedMemoryIndex.Value] -= currentVal;
                    }
                    else
                    {
                        if (memoryStack.Count > 0)
                            memoryStack[memoryStack.Count - 1] -= currentVal;
                        else
                            memoryStack.Add(-currentVal);
                    }
                    isNewEntry = true;
                    break;
                case "MS":
                    memoryStack.Add(currentVal);
                    isNewEntry = false;
                    break;
                case "MR":
                    if (memoryStack.Count > 0)
                    {
                        if (SelectedMemoryIndex.HasValue)
                        {
                            isNewEntry = false;
                            return (memoryStack[SelectedMemoryIndex.Value].ToString(), isNewEntry);
                        }
                        else
                        {
                            isNewEntry = false;
                            return (memoryStack[memoryStack.Count - 1].ToString(), isNewEntry);
                        }
                    }
                    break;
                case "M🠷":
                    if (memoryStack.Count > 0)
                    {
                        MemorySelectorWindow memWindow = new MemorySelectorWindow();
                        if (memWindow.ShowDialog() == true)
                        {
                            SelectedMemoryIndex = memWindow.SelectedIndex;

                        }
                    }

                    isNewEntry = false;
                    return (expression, isNewEntry);

            }
            return (currentVal.ToString(), isNewEntry);
        }
        public static void OmniScientificRemoveMemoryAt(int index)
        {
            if (index >= 0 && index < memoryStack.Count)
            {
                memoryStack.RemoveAt(index);
            }
        }

        #endregion

        #region Cut, Copy, Paste, Digit Grouping and About

        private static string clipboardBuffer = string.Empty;

        public static string OmniScientificMenuCut_Click(object sender, RoutedEventArgs e, string expression)
        {
            clipboardBuffer = expression;
            Clipboard.SetText(clipboardBuffer);
            return "0";
        }

        public static void OmniScientificMenuCopy_Click(object sender, RoutedEventArgs e, string expresion)
        {
            clipboardBuffer = expresion; 
            Clipboard.SetText(clipboardBuffer);
        }

        public static string OmniScientificMenuPaste_Click(object sender, RoutedEventArgs e, bool isNewEntry)
        {
            if(Clipboard.GetText() != null)
            {
                clipboardBuffer = Clipboard.GetText();
            }
            if (!string.IsNullOrEmpty(clipboardBuffer))
                return clipboardBuffer;
            return "0";
        }

        public static void OmniScientificMenuDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            IsDigitGrouping = !IsDigitGrouping;
        }

        public static void OmniScientificMenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Calculator Scientific realizat de Vlaicu Andrei Danielo – Grupa 10LF234",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Parentheses

        public static string OmniScientificParen_Click(object sender, RoutedEventArgs e, string expression)
        {
            string par = (sender as Button).Content.ToString();

            string express = expression;

            express += par;

            if (par == ")")
            {

                int lastOpenIndex = express.LastIndexOf('(');
                if (lastOpenIndex >= 0)
                {
                    string inside = express.Substring(lastOpenIndex + 1, express.Length - lastOpenIndex - 2);
                    express = express.Substring(0, lastOpenIndex) + inside;
                }
            }

           return express;
        }

        #endregion

        #region Settings
        public static void OmniScientificLoadSettings()
        {
            IsDigitGrouping = Settings.Default.DigitGroupingScientific;
        }

        public static void OmniScientificSaveSettings()
        {
            Settings.Default.DigitGroupingScientific = IsDigitGrouping;
            Settings.Default.Save();
        }
        public static List<double> GetMemoryStack()
        {
            return new List<double>(memoryStack);
        }
        #endregion

        #region Calculator Mode

        public static void OmniScientificModeStandard_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Standard";
            Settings.Default.Save();
            Standard stdWin = new Standard();
            stdWin.Show();
        }

        public static void OmniScientificModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Programmer";
            Settings.Default.Save();
            Programmer prog = new Programmer();
            prog.Show();
        }

        public static void OmniScientificModeScientific_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ești deja în modul Scientific!", "Info");
        }

        public static void OmniScientificModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Ecuation";
            Settings.Default.Save();
            Ecuation ec = new Ecuation();
            ec.Show();
        }

        #endregion

        #region Themes
        public static void OmniScientificThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        public static void OmniScientificThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        public static void OmniScientificThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        public static void OmniScientificThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        public static void OmniScientificThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        public static void OmniScientificThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        public static void OmniScientificThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        public static void OmniScientificThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        public static void OmniScientificThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        public static void OmniScientificThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
