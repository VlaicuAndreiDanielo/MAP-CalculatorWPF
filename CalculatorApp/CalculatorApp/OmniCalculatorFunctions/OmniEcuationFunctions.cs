using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace CalculatorApp.OmniCalculatorFunctions
{
    static class OmniEcuationFunctions
    {

        #region Buttons Creation and Operation Assignment
        public static string OmniEcuationBtn_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            string content = (sender as Button).Content.ToString();
            return txtEquation += content;
        }

        public static string OmniEcuationBtnClear_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            return "";
        }

        public static string OmniEcuationBtnEqual_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            string expr = txtEquation;
            try
            {
                double result = OmniEcuationEvaluateExpression(expr);
                txtEquation = result.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la evaluarea expresiei: " + ex.Message,
                                "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return txtEquation;
        }
        public static string OmniEcuationBackspace_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            if (!string.IsNullOrEmpty(txtEquation))
            {
                txtEquation = txtEquation.Substring(0, txtEquation.Length - 1);
            }
            return txtEquation;
        }
        public static void OmniEcuationBtnExit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }
        #endregion

        #region PostFix Transformation And Expression Evaluation
        private static List<string> OmniEcuationInfixToPostfix(string infix)
        {
            List<string> output = new List<string>();
            Stack<string> stack = new Stack<string>();

            List<string> tokens = OmniEcuationTokenize(infix);

            foreach (string token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    output.Add(token);
                }
                else if (OmniEcuationIsOperator(token))
                {
                    while (stack.Count > 0 && OmniEcuationIsOperator(stack.Peek()) &&
                           OmniEcuationComparePrecedence(stack.Peek(), token) >= 0)
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        output.Add(stack.Pop());
                    }
                    if (stack.Count == 0)
                        throw new Exception("Paranteze neechilibrate!");
                    stack.Pop();
                }
                else
                {
                    throw new Exception("Token necunoscut: " + token);
                }
            }

            while (stack.Count > 0)
            {
                string top = stack.Pop();
                if (top == "(" || top == ")")
                    throw new Exception("Paranteze neechilibrate!");
                output.Add(top);
            }

            return output;
        }

        private static double OmniEcuationEvaluatePostfix(List<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in postfix)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                {
                    stack.Push(val);
                }
                else if (OmniEcuationIsOperator(token))
                {
                    if (stack.Count < 2)
                        throw new Exception("Expresie postfix invalidă (operanzi insuficienți).");

                    double b = stack.Pop();
                    double a = stack.Pop();

                    double r = 0;
                    switch (token)
                    {
                        case "+": r = a + b; break;
                        case "-": r = a - b; break;
                        case "*": r = a * b; break;
                        case "/":
                            if (b == 0) throw new DivideByZeroException();
                            r = a / b;
                            break;
                    }
                    stack.Push(r);
                }
                else
                {
                    throw new Exception("Token necunoscut în postfix: " + token);
                }
            }

            if (stack.Count != 1)
                throw new Exception("Expresie postfix invalidă (stack final != 1).");

            return stack.Pop();
        }
        public static double OmniEcuationEvaluateExpression(string expr)
        {
            List<string> postfix = OmniEcuationInfixToPostfix(expr);
            return OmniEcuationEvaluatePostfix(postfix);
        }
        #endregion

        #region Tokenizer and Operator Verification
        public static List<string> OmniEcuationTokenize(string expr)
        {
            List<string> tokens = new List<string>();
            int i = 0;
            while (i < expr.Length)
            {
                char c = expr[i];
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }
                if (char.IsDigit(c) || c == '.')
                {
                    int start = i;
                    while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.'))
                        i++;
                    tokens.Add(expr.Substring(start, i - start));
                }
                else if (OmniEcuationIsOperator(c.ToString()) || c == '(' || c == ')')
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                else
                {
                    throw new Exception($"Caractere nepermise: {c}");
                }
            }
            return tokens;
        }

        public static bool OmniEcuationIsOperator(string token)
        {
            return (token == "+" || token == "-" || token == "*" || token == "/");
        }

        public static int OmniEcuationComparePrecedence(string op1, string op2)
        {
            int p1 = (op1 == "+" || op1 == "-") ? 1 : 2;
            int p2 = (op2 == "+" || op2 == "-") ? 1 : 2;
            return p1 - p2;
        }
        #endregion

        #region Copy, Cut, Paste and About

        private static string clipboardBuffer = string.Empty;

        public static string OmniEcuationMenuCut_Click(object sender, RoutedEventArgs e, string txtEquation)
        {

            clipboardBuffer = txtEquation;
            Clipboard.SetText(clipboardBuffer);
            return txtEquation = "";
        }

        public static void OmniEcuationMenuCopy_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            clipboardBuffer = txtEquation;
            Clipboard.SetText(clipboardBuffer);
        }

        public static string OmniEcuationMenuPaste_Click(object sender, RoutedEventArgs e, string txtEquation)
        {
            if (Clipboard.GetText() != null)
            {
                clipboardBuffer = Clipboard.GetText();
            }
            if (!string.IsNullOrEmpty(clipboardBuffer))
            {
                txtEquation += clipboardBuffer;
            }
            return txtEquation;
        }

        public static void OmniEcuationMenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Calculator Ecuation realizat de Vlaicu Andrei Danielo – Grupa 10LF234",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region KeyBoard Manipulation
        public static string OmniEcuationWindow_KeyDown(object sender, KeyEventArgs e, string txtEquation)
        {
            if (e.Key == Key.D9 && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = "(" }, null, txtEquation);
            }
            else if (e.Key == Key.D0 && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = ")" }, null, txtEquation);
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = (e.Key - Key.D0).ToString();
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = digit }, null, txtEquation);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = digit }, null, txtEquation);
            }
            else if (e.Key == Key.Add || (e.Key == Key.OemPlus && Keyboard.Modifiers != ModifierKeys.Shift))
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = "+" }, null, txtEquation);
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = "-" }, null, txtEquation);
            }
            else if (e.Key == Key.Multiply)
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = "*" }, null, txtEquation);
            }
            else if (e.Key == Key.Divide)
            {
                return OmniEcuationBtn_Click(new System.Windows.Controls.Button() { Content = "/" }, null, txtEquation);
            }
            else if (e.Key == Key.Enter)
            {
                return OmniEcuationBtnEqual_Click(new System.Windows.Controls.Button() { Content = "=" }, null, txtEquation);
            }
            else if (e.Key == Key.Back)
            {
                return OmniEcuationBackspace_Click(null, null, txtEquation);
            }
            else if (e.Key == Key.Escape)
            {
                return OmniEcuationBtnClear_Click(null, null, txtEquation);
            }

            return txtEquation;
        }

        public static bool OmniEcuationHasEnterBeenPressed(object sender, KeyEventArgs e)
        {
            return e.Key == Key.Enter;
        }
        #endregion

        #region Calculator Mode
        public static void OmniEcuationModeStandard_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Standard";
            Settings.Default.Save();
            Standard stdWin = new Standard();
            stdWin.Show();
        }

        public static void OmniEcuationModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Programmer";
            Settings.Default.Save();
            Programmer prog = new Programmer();
            prog.Show();
        }

        public static void OmniEcuationModeScientific_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = "Scientific";
            Settings.Default.Save();
            Scientific scien = new Scientific();
            scien.Show();
        }

        public static void OmniEcuationModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.LastCalculatorMode = " Ecuation";
            Settings.Default.Save();
            MessageBox.Show("Ești deja în modul Ecuation!", "Info");
        }
        #endregion

        #region Themes
        public static void OmniEcuationThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        public static void OmniEcuationThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        public static void OmniEcuationThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        public static void OmniEcuationThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        public static void OmniEcuationThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        public static void OmniEcuationThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        public static void OmniEcuationThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        public static void OmniEcuationThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        public static void OmniEcuationThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        public static void OmniEcuationThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
