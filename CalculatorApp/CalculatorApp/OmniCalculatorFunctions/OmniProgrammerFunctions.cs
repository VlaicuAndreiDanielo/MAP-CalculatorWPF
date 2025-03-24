using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorApp.OmniCalculatorFunctions
{
    static class OmniProgrammerFunctions
    {
        public static int selectedBase = 10;
        public static bool IsDigitGrouping { get; set; } = false;

        #region Conversions, Displays Update, Grouping Apply and User Interface

        public static long OmniProgrammerConvertBaseToDec(string input, int fromBase)
        {

            if (string.IsNullOrWhiteSpace(input))
                return 0;
            try
            {
                if (fromBase == 10 && input.Contains("."))
                {
                    double d = double.Parse(input, CultureInfo.CurrentCulture);
                    return Convert.ToInt64(d); 
                }
                else
                {
                    return Convert.ToInt64(input, fromBase);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string OmniProgrammerConvertDecToBase(long decValue, int toBase)
        {
            try
            {
                return Convert.ToString(decValue, toBase).ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conversia din zecimal în baza " + toBase + " a eșuat: " + ex.Message,
                                "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return "0";
            }
        }

        public static string OmniProgrammerUpdateProgrammerDisplays(string currentValue, int currentBase)
        {
            return OmniProgrammerApplyGrouping(currentValue, currentBase);
        }

        public static string OmniProgrammerUpdateProgrammerDisplaysHEX(long decVal)
        {
            return OmniProgrammerApplyGrouping(OmniProgrammerConvertDecToBase(decVal, 16), 16);
        }
        public static string OmniProgrammerUpdateProgrammerDisplaysDEC(long decVal)
        {
            return OmniProgrammerApplyGrouping(decVal.ToString(), 10);
        }
        public static string OmniProgrammerUpdateProgrammerDisplaysOCT(long decVal)
        {
            return OmniProgrammerApplyGrouping(OmniProgrammerConvertDecToBase(decVal, 8), 8);
        }
        public static string OmniProgrammerUpdateProgrammerDisplaysBIN(long decVal)
        {
            return OmniProgrammerApplyGrouping(OmniProgrammerConvertDecToBase(decVal, 2), 2);
        }

        private static string OmniProgrammerApplyGrouping(string numberStr, int baseVal)
        {
            if (!IsDigitGrouping || numberStr.Contains("(") || numberStr.Contains(")"))
                return numberStr;

            bool isNegative = false;
            if (numberStr.StartsWith("-"))
            {
                isNegative = true;
                numberStr = numberStr.Substring(1);
            }

            string[] parts = numberStr.Split('.');
            string intPart = parts[0];
            string fracPart = parts.Length > 1 ? parts[1] : "";

            int groupSize;
            string separator;
            if (baseVal == 10)
            {
                groupSize = 3;
                separator = ",";
            }
            else if (baseVal == 8)
            {
                groupSize = 3;
                separator = " ";
            }
            else if (baseVal == 16 || baseVal == 2)
            {
                groupSize = 4;
                separator = " ";
            }
            else
            {
                groupSize = 3;
                separator = ",";
            }

            string groupedInt = "";
            int count = 0;
            for (int i = intPart.Length - 1; i >= 0; i--)
            {
                groupedInt = intPart[i] + groupedInt;
                count++;
                if (count % groupSize == 0 && i > 0)
                {
                    groupedInt = separator + groupedInt;
                }
            }

            string result = groupedInt;
            if (!string.IsNullOrEmpty(fracPart))
            {
                result += "." + fracPart;
            }
            if (isNegative)
                result = "-" + result;

            return result;
        }

        #endregion

        #region KeyBoard Manipulation
        //Varianta anterioara de manipulare a tastaturii!
        public static (string newValue, long newLastDecValue, string newOperator, bool newIsNewEntry, long displayDecVal) OmniProgrammerWindow_KeyDownFirstVariant
            (object sender, KeyEventArgs e, string currentValue, int currentBase, bool isNewEntry, long lastDecValue, string currentOperator)
        {
            string updatedValue = currentValue;
            long updatedLastDecValue = lastDecValue;
            string updatedOperator = currentOperator;
            bool updatedIsNewEntry = isNewEntry;
            long displayDecVal = 0;

            if (e.Key == Key.D9 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                updatedValue = OmniProgrammerParen_Click(new Button() { Content = "(" }, null, updatedValue, currentBase);
                e.Handled = true;
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.D0 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                updatedValue = OmniProgrammerParen_Click(new Button() { Content = ")" }, null, updatedValue, currentBase);
                e.Handled = true;
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                string digit = (e.Key >= Key.D0 && e.Key <= Key.D9) ?
                               (e.Key - Key.D0).ToString() : (e.Key - Key.NumPad0).ToString();
                if (updatedValue == "0" || updatedIsNewEntry)
                {
                    updatedValue = digit;
                    updatedIsNewEntry = false;
                }
                else
                {
                    updatedValue += digit;
                }
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            if (currentBase == 16 && (e.Key >= Key.A && e.Key <= Key.F))
            {
                string letter = e.Key.ToString();
                if (updatedValue == "0" || updatedIsNewEntry)
                {
                    updatedValue = letter;
                    updatedIsNewEntry = false;
                }
                else
                {
                    updatedValue += letter;
                }
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            if (e.Key == Key.Add || (e.Key == Key.OemPlus && (Keyboard.Modifiers & ModifierKeys.Shift) == 0))
            {
                updatedOperator = "+";
                updatedLastDecValue = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                updatedValue = "0";
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                updatedOperator = "-";
                updatedLastDecValue = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                updatedValue = "0";
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Multiply)
            {
                updatedOperator = "*";
                updatedLastDecValue = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                updatedValue = "0";
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Divide)
            {
                updatedOperator = "/";
                updatedLastDecValue = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                updatedValue = "0";
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Enter)
            {
                var equalResult = OmniProgrammerEqual_Click(null, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedValue = equalResult.ValCur;
                updatedOperator = equalResult.Oper;
                updatedLastDecValue = equalResult.lastVal;
                updatedIsNewEntry = false;
                displayDecVal = equalResult.Var;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Back)
            {
                long backResult = OmniProgrammerBackspace_Click(null, null, updatedValue, currentBase);
                updatedValue = OmniProgrammerConvertDecToBase(backResult, currentBase);
                updatedIsNewEntry = false;
                displayDecVal = backResult;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Escape)
            {
                updatedValue = OmniProgrammerC_Click(null, null);
                updatedOperator = "";
                updatedLastDecValue = 0;
                updatedIsNewEntry = true;
                displayDecVal = 0;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
            return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
        }

        public static (string newValue, long newLastDecValue, string newOperator, bool newIsNewEntry, long displayDecVal) OmniProgrammerWindow_KeyDown
            (object sender, KeyEventArgs e, string currentValue, int currentBase, bool isNewEntry, long lastDecValue, string currentOperator)
        {
            string updatedValue = currentValue;
            long updatedLastDecValue = lastDecValue;
            string updatedOperator = currentOperator;
            bool updatedIsNewEntry = isNewEntry;
            long displayDecVal = 0;

            if (e.Key == Key.D9 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                updatedValue = OmniProgrammerParen_Click(new Button() { Content = "(" }, null, updatedValue, currentBase);
                e.Handled = true;
                updatedIsNewEntry = false;
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.D0 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                updatedValue = OmniProgrammerParen_Click(new Button() { Content = ")" }, null, updatedValue, currentBase);
                e.Handled = true;
                updatedIsNewEntry = false;
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                string digit = (e.Key >= Key.D0 && e.Key <= Key.D9)
                                ? (e.Key - Key.D0).ToString()
                                : (e.Key - Key.NumPad0).ToString();

                if (updatedValue == "0" || updatedIsNewEntry)
                {
                    updatedValue = digit;
                    updatedIsNewEntry = false;
                }
                else
                {
                    updatedValue += digit;
                }
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            if (currentBase == 16 && (e.Key >= Key.A && e.Key <= Key.F))
            {
                string letter = e.Key.ToString();
                if (updatedValue == "0" || updatedIsNewEntry)
                {
                    updatedValue = letter;
                    updatedIsNewEntry = false;
                }
                else
                {
                    updatedValue += letter;
                }
                displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            if (e.Key == Key.Add || (e.Key == Key.OemPlus && (Keyboard.Modifiers & ModifierKeys.Shift) == 0))
            {
                var arithResult = OmniProgrammerArithmetic_Click(new Button() { Content = "+" }, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedOperator = arithResult.op;
                updatedLastDecValue = arithResult.lastVal;
                updatedValue = arithResult.newCurrentValue;
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                var arithResult = OmniProgrammerArithmetic_Click(new Button() { Content = "-" }, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedOperator = arithResult.op;
                updatedLastDecValue = arithResult.lastVal;
                updatedValue = arithResult.newCurrentValue;
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Multiply)
            {
                var arithResult = OmniProgrammerArithmetic_Click(new Button() { Content = "*" }, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedOperator = arithResult.op;
                updatedLastDecValue = arithResult.lastVal;
                updatedValue = arithResult.newCurrentValue;
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Divide)
            {
                var arithResult = OmniProgrammerArithmetic_Click(new Button() { Content = "/" }, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedOperator = arithResult.op;
                updatedLastDecValue = arithResult.lastVal;
                updatedValue = arithResult.newCurrentValue;
                updatedIsNewEntry = true;
                displayDecVal = updatedLastDecValue;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            else if (e.Key == Key.Enter)
            {
                var equalResult = OmniProgrammerEqual_Click(null, null, updatedValue, currentBase, updatedLastDecValue, updatedOperator);
                updatedValue = equalResult.ValCur;
                updatedOperator = equalResult.Oper;
                updatedLastDecValue = equalResult.lastVal;
                updatedIsNewEntry = false;
                displayDecVal = equalResult.Var;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Back)
            {
                long backResult = OmniProgrammerBackspace_Click(null, null, updatedValue, currentBase);
                updatedValue = OmniProgrammerConvertDecToBase(backResult, currentBase);
                updatedIsNewEntry = false;
                displayDecVal = backResult;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }
            else if (e.Key == Key.Escape)
            {
                updatedValue = OmniProgrammerC_Click(null, null);
                updatedOperator = "";
                updatedLastDecValue = 0;
                updatedIsNewEntry = true;
                displayDecVal = 0;
                e.Handled = true;
                return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
            }

            displayDecVal = OmniProgrammerConvertBaseToDec(updatedValue, currentBase);
            return (updatedValue, updatedLastDecValue, updatedOperator, updatedIsNewEntry, displayDecVal);
        }

        public static (string newHistoryChain, bool enterPressed) OmniProgrammerHistoryKeyManipulation(KeyEventArgs e, string currentHistoryChain, int currentBase)
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
            else if (currentBase == 16 && e.Key >= Key.A && e.Key <= Key.F)
            {
                updatedChain += e.Key.ToString(); 
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
            else if (currentBase == 10 && e.Key == Key.OemPeriod)
            {
                updatedChain += ".";
            }
            else if (e.Key == Key.Enter)
            {
                isEnter = true;
            }

            return (updatedChain, isEnter);
        }

        #endregion

        #region Introduction Numbers 

        public static (bool entry, string currentValue, long decVal, bool isAcceptable) OmniProgrammerHexDigit_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase, bool isNewEntry)
        {
            string digit = (sender as Button).Content.ToString().ToUpper();

            if (!OmniProgrammerIsDigitAllowed(digit, currentBase))
            {
                long currentDecVal = 0;
                try
                {
                    currentDecVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                }
                catch
                {
                    MessageBox.Show("HEX Digit invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return (isNewEntry, currentValue, currentDecVal, false);
            }

            if (currentValue == "0" || isNewEntry)
            {
                currentValue = digit;
                isNewEntry = false;
            }
            else
            {
                currentValue += digit;
            }

            try
            {
                long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                return (isNewEntry, currentValue, decVal, true);
            }
            catch
            {
                MessageBox.Show("HEX Digit invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return (isNewEntry, currentValue, 0, false);
        }

        private static bool OmniProgrammerIsDigitAllowed(string digit, int currentBase)
        {
            if (digit == ".")
            {
                return currentBase == 10;
            }

            if (char.IsDigit(digit[0]))
            {
                int d = int.Parse(digit);
                if (currentBase == 2)
                    return d >= 0 && d <= 1;
                else if (currentBase == 8)
                    return d >= 0 && d <= 7;
                else if (currentBase == 10)
                    return d >= 0 && d <= 9;
                else if (currentBase == 16)
                    return true;
            }
            else
            {
                if (currentBase == 16)
                {
                    char c = char.ToUpper(digit[0]);
                    return c >= 'A' && c <= 'F';
                }
            }
            return false;
        }
        #endregion

        #region Arithmetic Operations, Negate and Equal

        public static (string op, long decVal, long lastVal, string newCurrentValue) OmniProgrammerArithmetic_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase, long lastDecValue, string currentOperator)
        {
            string op = (sender as Button).Content.ToString().ToUpper();
            long decVal = 0;
            try
            {
                decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
            }
            catch
            {
                MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (op == "AND" || op == "OR" || op == "XOR" || op == "NAND" || op == "NOR")
            {
                lastDecValue = decVal;
                return (op, decVal, lastDecValue, "0");
            }
            else
            {
                if (!string.IsNullOrEmpty(currentOperator))
                {
                    lastDecValue = OmniProgrammerApplyOperator(lastDecValue, decVal, currentOperator);
                }
                else
                {
                    lastDecValue = decVal;
                }
                return (op, decVal, lastDecValue, currentValue);
            }
        }

        public static long OmniProgrammerApplyOperator(long left, long right, string op)
        {
            switch (op)
            {
                case "+": return left + right;
                case "-": return left - right;
                case "*": return left * right;
                case "/":
                    if (right == 0)
                    {
                        MessageBox.Show("Împărțire la zero!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return left;
                    }
                    return left / right;
                case "%":
                    if (right == 0)
                    {
                        MessageBox.Show("Împărțire la zero!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                        return left;
                    }
                    return left % right;
                default:
                    return right;
            }
        }

        public static (long lastVal, string ValCur, string Oper, long Var) OmniProgrammerEqual_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase, long lastDecValue, string currentOperator)
        {
            if (currentOperator == "AND" || currentOperator == "OR" || currentOperator == "XOR" ||
                    currentOperator == "NAND" || currentOperator == "NOR")
            {
                if (currentBase != 2)
                {
                    MessageBox.Show("Operațiile bitwise sunt valabile doar în baza 2!",
                                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                string result = OmniProgrammerDoBitwiseOperation(
                                    OmniProgrammerLongToBinaryString(lastDecValue), currentValue, currentOperator);

                currentValue = result;
                long decVal = OmniProgrammerBinaryStringToLong(currentValue);
                lastDecValue = decVal;
                currentOperator = "";
                return (lastDecValue, currentValue, currentOperator, decVal);
            }
            else
            {
                long decVal = 0;
                try
                {
                    decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                }
                catch
                {
                    MessageBox.Show("Valoare invalidă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (!string.IsNullOrEmpty(currentOperator))
                {
                    lastDecValue = OmniProgrammerApplyOperator(lastDecValue, decVal, currentOperator);
                    currentOperator = "";
                }
                else
                {
                    lastDecValue = decVal;
                }
                currentValue = OmniProgrammerConvertDecToBase(lastDecValue, currentBase);
                return (lastDecValue, currentValue, currentOperator, decVal);
            }
        }

        public static long OmniProgrammerNegate_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            try
            {
                long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                decVal = -decVal;
                currentValue = OmniProgrammerConvertDecToBase(decVal, currentBase);
                return decVal;
            }
            catch
            {
                MessageBox.Show("Eroare la negare!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }
        #endregion

        #region Parentheses

        public static string OmniProgrammerParen_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            string par = (sender as Button).Content.ToString();
            currentValue += par;

            if (par == ")")
            {
                int openIndex = currentValue.LastIndexOf('(');
                if (openIndex >= 0)
                {
                    int closeIndex = currentValue.Length - 1; 
                    string inside = currentValue.Substring(openIndex + 1, closeIndex - openIndex - 1);

                    string outsideLeft = currentValue.Substring(0, openIndex);

                    currentValue = outsideLeft + inside;
                }
            }

            return currentValue;
        }


        #endregion

        #region Operations Bitwise and Shift

        public static (string op, long decVal, long lastVal, string newCurrentValue) OmniProgrammerBitwise_Click(
            object sender, RoutedEventArgs e, string currentValue, int currentBase, long lastDecValue, string currentOperator)
        {

            if (currentBase != 2)
            {
                MessageBox.Show("Operațiile bitwise sunt valabile doar în baza 2!",
                                "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return (currentOperator, 0, lastDecValue, currentValue);
            }

            string op = (sender as Button).Content.ToString().ToUpper();

            long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);

            if (op == "NOT")
            {
                string invertedBinary = OmniProgrammerBitwiseNot(currentValue);
                long invertedDec = OmniProgrammerBinaryStringToLong(invertedBinary);

                return ("", invertedDec, invertedDec, invertedBinary);
            }
            else if (op == "AND" || op == "OR" || op == "XOR" || op == "NAND" || op == "NOR")
            {
                if (string.IsNullOrEmpty(currentOperator))
                {
                    lastDecValue = decVal;
                }

                return (op, decVal, lastDecValue, "0");
            }
            else
            {
                return (currentOperator, decVal, lastDecValue, currentValue);
            }
        }

        private static string OmniProgrammerBitwiseNot(string bin)
        {
            char[] arr = bin.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (arr[i] == '0') ? '1' : '0';
            }
            return new string(arr);
        }

        private static string OmniProgrammerDoBitwiseOperation(string left, string right, string op)
        {

            int maxLen = Math.Max(left.Length, right.Length);
            left = left.PadLeft(maxLen, '0');
            right = right.PadLeft(maxLen, '0');

            char[] result = new char[maxLen];
            for (int i = 0; i < maxLen; i++)
            {
                char a = left[i];
                char b = right[i];
                switch (op)
                {
                    case "AND":
                        result[i] = (a == '1' && b == '1') ? '1' : '0';
                        break;
                    case "OR":
                        result[i] = (a == '1' || b == '1') ? '1' : '0';
                        break;
                    case "XOR":
                        result[i] = (a == b) ? '0' : '1';
                        break;
                    case "NAND":
                        result[i] = (a == '1' && b == '1') ? '0' : '1';
                        break;
                    case "NOR":
                        result[i] = (a == '0' && b == '0') ? '1' : '0';
                        break;
                    default:
                        result[i] = '0';
                        break;
                }
            }
            string s = new string(result).TrimStart('0');
            if (string.IsNullOrEmpty(s)) s = "0";
            return s;
        }

        public static long OmniProgrammerBinaryStringToLong(string bin)
        {
            return OmniProgrammerConvertBaseToDec(bin, 2);
        }

        private static string OmniProgrammerLongToBinaryString(long decVal)
        {
            return OmniProgrammerConvertDecToBase(decVal, 2);
        }

        public static long OmniProgrammerShift_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            string op = (sender as Button).Content.ToString();
            long decVal = 0;
            try
            {
                decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
            }
            catch
            {
                MessageBox.Show("Valoare invalidă pentru operația de shift!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (op == "<<")
                decVal = decVal << 1;
            else if (op == ">>")
                decVal = decVal >> 1;

            return decVal;
        }
        #endregion

        #region Cut, Copy, Paste, Digit Grouping and About

        public static string clipboardBuffer = string.Empty;

        public static string OmniProgrammerMenuCut_Click(object sender, RoutedEventArgs e, string textProgrammer)
        {
            clipboardBuffer = textProgrammer;
            Clipboard.SetText(clipboardBuffer);
            return "0";
        }

        public static void OmniProgrammerMenuCopy_Click(object sender, RoutedEventArgs e, string textProgrammer)
        {
            clipboardBuffer = textProgrammer;
            Clipboard.SetText(clipboardBuffer);
        }

        public static string OmniProgrammerMenuPaste_Click(object sender, RoutedEventArgs e, int currentBase)
        {
            if(Clipboard.GetText() != null)
            {
                clipboardBuffer = Clipboard.GetText();
            }
            if (!string.IsNullOrEmpty(clipboardBuffer))
            {
                string currentValue = OmniProgrammerRemoveCommas(clipboardBuffer);
                try
                {

                    long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                    return decVal.ToString();
                }
                catch
                {
                    MessageBox.Show("Input invalid pentru baza curentă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return currentValue;
            }
            return "0";
        }
        private static string OmniProgrammerRemoveCommas(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return input.Replace(",", "");
        }
        public static long OmniProgrammerMenuDigitGrouping_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            IsDigitGrouping = !IsDigitGrouping;
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                menuItem.IsChecked = IsDigitGrouping;
            }

            OmniProgrammerSaveSettings();

            try
            {
                long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                return decVal;
            }
            catch
            {
                MessageBox.Show("Digit Grouping invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }

        public static void OmniProgrammerMenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Calculator Programmer realizat de Vlaicu Andrei Danielo – Grupa 10LF234",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region C, CE, Backspace, Dot and Exit

        public static string OmniProgrammerC_Click(object sender, RoutedEventArgs e)
        {
            return "0";
        }

        public static string OmniProgrammerCE_Click(object sender, RoutedEventArgs e)
        {
            return "0";
        }

        public static void OmniProgrammerBtnExit_Click(object sender, RoutedEventArgs e)
        {
            // Se poate apela this.Close() în fereastra apelantă
        }

        public static long OmniProgrammerBackspace_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            if (!string.IsNullOrEmpty(currentValue))
            {
                currentValue = currentValue.Substring(0, currentValue.Length - 1);
                if (string.IsNullOrEmpty(currentValue))
                {
                    currentValue = "0";
                }
                try
                {
                    return OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                }
                catch
                {
                    MessageBox.Show("Valoare invalidă după backspace!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
            }
            return 0;
        }

        public static string OmniProgrammerDot_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            if (currentBase != 10)
            {
                MessageBox.Show("Punctul zecimal este valabil doar în baza 10!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (!currentValue.Contains("."))
            {
                currentValue += ".";
                return currentValue;
            }
            return currentValue;
        }
        #endregion

        #region Base Interchange

        public static (string newValue, int newBase) OmniProgrammerChangeBase_Click(object sender, RoutedEventArgs e, string currentValue, int currentBase)
        {
            try
            {
                long decVal = OmniProgrammerConvertBaseToDec(currentValue, currentBase);
                string baseText = (sender as Button).Content.ToString().ToUpper();

                switch (baseText)
                {
                    case "BIN":
                        currentBase = 2;
                        selectedBase = 2;
                        break;
                    case "OCT":
                        currentBase = 8;
                        selectedBase = 8;
                        break;
                    case "DEC":
                        currentBase = 10;
                        selectedBase = 10;
                        break;
                    case "HEX":
                        currentBase = 16;
                        selectedBase = 16;
                        break;
                    default:
                        MessageBox.Show("Bază necunoscută!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }

                currentValue = OmniProgrammerConvertDecToBase(decVal, currentBase);
                return (currentValue, currentBase);
            }
            catch
            {
                MessageBox.Show("Valoare invalidă pentru schimbarea bazei!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return ("0", currentBase);
        }
        #endregion

        #region Settings

        public static void OmniProgrammerLoadSettings()
        {
            IsDigitGrouping = Settings.Default.DigitGroupingProgrammer;
            selectedBase = Settings.Default.LastProgrammerBaseSelected;
        }

        public static void OmniProgrammerSaveSettings()
        {
            Settings.Default.DigitGroupingProgrammer = IsDigitGrouping;
            Settings.Default.LastProgrammerBaseSelected = selectedBase;
            Settings.Default.Save();
        }
        #endregion

        #region Calculator Mode

        public static void OmniProgrammerModeStandard_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Standard";
            Settings.Default.Save();
            Standard stdWin = new Standard();
            stdWin.Show();
        }

        public static void OmniProgrammerModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ești deja în modul Programmer!", "Info");
        }

        public static void OmniProgrammerModeScientific_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Scientific";
            Settings.Default.Save();
            Scientific scien = new Scientific();
            scien.Show();        
        }

        public static void OmniProgrammerModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Ecuation";
            Settings.Default.Save();
            Ecuation ec = new Ecuation();
            ec.Show();
        }
        #endregion

        #region Themes
        public static void OmniProgrammerThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        public static void OmniProgrammerThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        public static void OmniProgrammerThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        public static void OmniProgrammerThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        public static void OmniProgrammerThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        public static void OmniProgrammerThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        public static void OmniProgrammerThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        public static void OmniProgrammerThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        public static void OmniProgrammerThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        public static void OmniProgrammerThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
