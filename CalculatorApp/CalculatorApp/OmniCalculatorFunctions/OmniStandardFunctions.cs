using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApp.OmniCalculatorFunctions
{
    static class OmniStandardFunctions
    {
        public static bool IsDigitGrouping { get; set; } = false;
        private static List<double> memoryStack = new List<double>();
        public static int? SelectedMemoryIndex { get; set; } = null;

        #region Buttons Digit and Operation Assignment

        public static (bool NewEntry, string NewExpression) OmniStandardDigit_Click(object sender, RoutedEventArgs e, bool isNewEntry, string expression)
        {

            string digit = (sender as System.Windows.Controls.Button).Content.ToString();
            string decSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (digit == ".")
            {
                digit = decSeparator;
            }

            if (isNewEntry)
            {
                expression = (digit == decSeparator) ? "0" + decSeparator : digit;
                isNewEntry = false;
            }
            else
            { 
                if (digit == decSeparator && expression.Contains(decSeparator))
                    return (false, expression);
                expression += digit;
            }

            if (IsDigitGrouping)
            {
                expression = OmniStandardGroupIntegerPartPreserveFraction(expression);
            }
            return (isNewEntry, expression);
        }

        private static string OmniStandardGroupIntegerPartPreserveFraction(string text)
        {
            string decSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            bool endsWithDecimal = text.EndsWith(decSeparator);

            bool isNegative = false;
            if (text.StartsWith("-"))
            {
                isNegative = true;
                text = text.Substring(1);
            }

            int decIndex = text.IndexOf(decSeparator);
            if (decIndex < 0)
            {
                if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double intVal))
                {
                    text = intVal.ToString("#,##0", CultureInfo.CurrentCulture);
                }
            }
            else
            {
                string intPart = text.Substring(0, decIndex);
                string fracPart = text.Substring(decIndex + 1);
                if (double.TryParse(intPart, NumberStyles.Any, CultureInfo.CurrentCulture, out double intVal))
                {
                    intPart = intVal.ToString("#,##0", CultureInfo.CurrentCulture);
                    text = intPart + decSeparator + fracPart;
                }
            }

            if (endsWithDecimal && !text.EndsWith(decSeparator))
            {
                text += decSeparator;
            }

            if (isNegative)
                text = "-" + text;

            return text;
        }

        public static (double lastValue, string currentOperator, bool isNewEntry, string newExpression) OmniStandardOperator_Click
            (object sender, RoutedEventArgs e, double lastValue, string currentOperator, string displayText)
        {
            if (!string.IsNullOrEmpty(currentOperator))
            {
                lastValue = OmniStandardEqual_Click(sender, e, currentOperator, displayText, lastValue);
                displayText = lastValue.ToString();
            }
            else
            {
                if (double.TryParse(displayText, NumberStyles.Any, CultureInfo.CurrentCulture, out double value))
                {
                    lastValue = value;
                }
            }
            currentOperator = (sender as System.Windows.Controls.Button)?.Content.ToString() ?? "";
            bool isNewEntry = true;
            return (lastValue, currentOperator, isNewEntry, displayText);
        }

        public static double OmniStandardEqual_Click(object sender, RoutedEventArgs e, string currentOperator, string expression, double lastValue)
        {
            double result = OmniStandardPerformCalculation(currentOperator, expression, lastValue);
            currentOperator = string.Empty;
            return result;
        }

        private static double OmniStandardPerformCalculation(string currentOperator, string expression, double lastValue)
        {
            if (!double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double input))
            {
                MessageBox.Show("Format numeric invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return lastValue;
            }

            if (!string.IsNullOrEmpty(currentOperator))
            {
                try
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
                                throw new DivideByZeroException();
                            lastValue /= input;
                            break;
                        case "%":
                            lastValue %= input;
                            break;
                    }
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("Împărțire la 0!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
            }
            else
            {
                lastValue = input;
            }
            return lastValue;
        }


        #endregion

        #region Special Functions: BackSpace, C, CE, Negate, Sqrt, Square, Reciprocal, Exit

        public static (string newExpression, bool newEntry) OmniStandardBackspace_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (!isNewEntry && expression.Length > 1)
            {
                expression = expression.Substring(0, expression.Length - 1);
                if (IsDigitGrouping)
                {
                    string oldText = expression;
                    if (double.TryParse(oldText, NumberStyles.Any, CultureInfo.CurrentCulture, out double number))
                    {
                        expression = OmniStandardGroupIntegerPartPreserveFraction(number.ToString(CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        expression = oldText;
                    }
                }
            }
            else
            {
                expression = "0";
                isNewEntry = true;
            }
            return (expression, isNewEntry);
        }

        public static bool OmniStandardCE_Click(object sender, RoutedEventArgs e)
        {
            return true;
        }
        public static string OmniStandardC_Click(object sender, RoutedEventArgs e)
        {
            return "0";
        }
        public static void OmniStandardBtnExit_Click(object sender, RoutedEventArgs e)
        {
            // Se poate apela this.Close() în fereastra apelantă!
        }

        public static string OmniStandardNegate_Click(object sender, RoutedEventArgs e, string expression)
        {
            if (double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                val = -val;
            }
            return val.ToString();
        }

        public static (string newExpression, bool newEntry) OmniStandardSqrt_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                if (val < 0)
                {
                    MessageBox.Show("Nu se poate calcula rădăcina unui număr negativ!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                val = Math.Sqrt(val);
                isNewEntry = true;
            }
            return (val.ToString(), isNewEntry);
        }

        public static (string newExpression, bool newEntry) OmniStandardSquare_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                val = val * val;
                isNewEntry = true;
            }
            return (val.ToString(), isNewEntry);
        }

        public static (string newExpression, bool newEntry) OmniStandardReciprocal_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            if (double.TryParse(expression, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                if (val == 0)
                {
                    MessageBox.Show("Împărțirea la 0 nu este permisă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                val = 1 / val;
                isNewEntry = true;
            }
            return (val.ToString(), isNewEntry);
        }

        #endregion

        #region Memory Buttons

        public static (string Val, bool Entry) OmniStandardMemoryButton_Click(object sender, RoutedEventArgs e, string expression, bool isNewEntry)
        {
            string op = (sender as System.Windows.Controls.Button).Content.ToString();
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
        public static void OmniStandardRemoveMemoryAt(int index)
        {
            if (index >= 0 && index < memoryStack.Count)
            {
                memoryStack.RemoveAt(index);
            }
        }
        #endregion

        #region Cut, Copy, Paste, About, Digit Grouping

        private static string clipboardBuffer = string.Empty;

        public static string OmniStandardMenuCut_Click(object sender, RoutedEventArgs e, string expression)
        {
            clipboardBuffer = expression;
            Clipboard.SetText(clipboardBuffer);
            return "0";
        }

        public static void OmniStandardMenuCopy_Click(object sender, RoutedEventArgs e, string expression)
        {
            clipboardBuffer = expression;
            Clipboard.SetText(clipboardBuffer);
        }

        public static string OmniStandardMenuPaste_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
                clipboardBuffer = Clipboard.GetText();
            }
            if (!string.IsNullOrEmpty(clipboardBuffer))
                return clipboardBuffer;
            return "0";
        }

        public static void OmniStandardMenuDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            IsDigitGrouping = !IsDigitGrouping;
        }

        public static void OmniStandardMenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Calculator Standard realizat de Vlaicu Andrei Danielo – Grupa 10LF234",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region Update Display 
        public static string OmniStandardUpdateDisplay(string text, string expression)
        {
            string decSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (text.EndsWith(decSeparator))
            {
                return text;
            }

            if (double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double number))
            {
                if (IsDigitGrouping)
                    expression = number.ToString("#,##0.##############", CultureInfo.CurrentCulture);
                else
                    expression = number.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                expression = text;
            }
            return expression;
        }

        #endregion

        #region KeyBoard Manipulation
       
        public static (string NewExpression, double LastValue, string CurrentOperator, bool IsNewEntry) OmniStandardWindow_KeyDown
            (KeyEventArgs e, bool isNewEntry, string expression, string currentOperator, double lastValue)
        {
            string newExpression = expression;
            double newLastValue = lastValue;
            string newCurrentOperator = currentOperator;
            bool newEntry = isNewEntry;

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = (e.Key - Key.D0).ToString();
                var result = OmniStandardDigit_Click(new System.Windows.Controls.Button() { Content = digit }, null, newEntry, expression);
                newEntry = result.NewEntry;
                newExpression = result.NewExpression;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                var result = OmniStandardDigit_Click(new System.Windows.Controls.Button() { Content = digit }, null, newEntry, expression);
                newEntry = result.NewEntry;
                newExpression = result.NewExpression;
            }
            else if (e.Key == Key.Add || (e.Key == Key.OemPlus && Keyboard.Modifiers != ModifierKeys.Shift))
            {
                var result = OmniStandardOperator_Click(new System.Windows.Controls.Button() { Content = "+" }, null, newLastValue, newCurrentOperator, expression);
                newLastValue = result.lastValue;
                newCurrentOperator = result.currentOperator;
                newEntry = result.isNewEntry;
                newExpression = result.newExpression;
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                var result = OmniStandardOperator_Click(new System.Windows.Controls.Button() { Content = "-" }, null, newLastValue, newCurrentOperator, expression);
                newLastValue = result.lastValue;
                newCurrentOperator = result.currentOperator;
                newEntry = result.isNewEntry;
                newExpression = result.newExpression;
            }
            else if (e.Key == Key.Multiply)
            {
                var result = OmniStandardOperator_Click(new System.Windows.Controls.Button() { Content = "*" }, null, newLastValue, newCurrentOperator, expression);
                newLastValue = result.lastValue;
                newCurrentOperator = result.currentOperator;
                newEntry = result.isNewEntry;
                newExpression = result.newExpression;
            }
            else if (e.Key == Key.Divide)
            {
                var result = OmniStandardOperator_Click(new System.Windows.Controls.Button() { Content = "/" }, null, newLastValue, newCurrentOperator, expression);
                newLastValue = result.lastValue;
                newCurrentOperator = result.currentOperator;
                newEntry = result.isNewEntry;
                newExpression = result.newExpression;
            }
            else if (e.Key == Key.Enter)
            {
                newLastValue = OmniStandardEqual_Click(null, null, newCurrentOperator, expression, newLastValue);
                newExpression = newLastValue.ToString();
                newCurrentOperator = string.Empty;
                newEntry = false;
            }
            else if (e.Key == Key.Back)
            {
                var result = OmniStandardBackspace_Click(null, null, expression, newEntry);
                newExpression = result.newExpression;
                newEntry = result.newEntry;
            }
            else if (e.Key == Key.Escape)
            {
                newExpression = OmniStandardC_Click(null, null);
                newLastValue = 0;
                newCurrentOperator = string.Empty;
                newEntry = true;
            }
            return (newExpression, newLastValue, newCurrentOperator, newEntry);
        }

        public static (string newHistoryChain, bool enterPressed) OmniStandardHistoryKeyManipulation(KeyEventArgs e, string currentHistoryChain)
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

        #region Settings
        public static void OmniStandardLoadSettings()
        {
            IsDigitGrouping = Settings.Default.DigitGroupingStandard;
        }

        public static void OmniStandardSaveSettings()
        {
            Settings.Default.DigitGroupingStandard = IsDigitGrouping;
            Settings.Default.Save();
        }

        public static List<double> GetMemoryStack()
        {
            return new List<double>(memoryStack);
        }

        #endregion

        #region Calculator Mode
        public static void OmniStandardModeStandard_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Standard";
            Settings.Default.Save();
            MessageBox.Show("Ești deja în modul Standard!", "Info");
        }

        public static void OmniStandardModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Programmer";
            Settings.Default.Save();
            Programmer progWin = new Programmer();
            progWin.Show();
        }

        public static void OmniStandardModeScientific_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Scientific";
            Settings.Default.Save();
            Scientific scien = new Scientific();
            scien.Show();
        }

        public static void OmniStandardModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Ecuation";
            Settings.Default.Save();
            Ecuation ec = new Ecuation();
            ec.Show();
        }
        #endregion

        #region Themes
        public static void OmniStandardThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        public static void OmniStandardThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        public static void OmniStandardThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        public static void OmniStandardThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        public static void OmniStandardThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        public static void OmniStandardThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        public static void OmniStandardThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        public static void OmniStandardThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        public static void OmniStandardThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        public static void OmniStandardThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
