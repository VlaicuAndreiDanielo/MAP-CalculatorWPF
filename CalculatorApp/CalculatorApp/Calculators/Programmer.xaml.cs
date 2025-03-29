using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class Programmer : Window
    {
        private int currentBase = 10;
        private string currentValue = "0";
        private long lastDecValue = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;
        private string expressionChain = "";
        private bool isHEX = false, isBIN = false, isDEC = false, isOCT = false;
        public bool IsDigitGrouping { get; set; } = false;

        public Programmer()
        {
            InitializeComponent();
            LoadSettings(); LoadBase(); ButtonsEnabler();
            menuDigitGroupingProgrammer.IsChecked = this.IsDigitGrouping;
            UpdateProgrammerDisplays(0);
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string iconPath = System.IO.Path.Combine(exeFolder, "Calculator.png");
            this.Icon = new BitmapImage(new Uri(iconPath, UriKind.Absolute));
        }

        #region Conversions, Displays Update, Grouping Apply and User Interface

        //Nu mai este necesara acesata functie! A fost implementata in OmniProgrammerFunctions
        /* private long ConvertBaseToDec(string input, int fromBase)
        {
            return OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(input, fromBase);
        } */

        //Nu mai este necesara acesata functie! A fost implementata in OmniProgrammerFunctions
        /*private string ConvertDecToBase(long decValue, int toBase)
        {
            return OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(decValue, toBase).ToUpper();
        }*/

        private void UpdateProgrammerDisplays(long decVal)
        {
            txtDisplayProgrammer.Text = OmniProgrammerFunctions.OmniProgrammerUpdateProgrammerDisplays(currentValue, currentBase);
            txtHexValue.Text = OmniProgrammerFunctions.OmniProgrammerUpdateProgrammerDisplaysHEX(decVal);
            txtDecValue.Text = OmniProgrammerFunctions.OmniProgrammerUpdateProgrammerDisplaysDEC(decVal);
            txtOctValue.Text = OmniProgrammerFunctions.OmniProgrammerUpdateProgrammerDisplaysOCT(decVal);
            txtBinValue.Text = OmniProgrammerFunctions.OmniProgrammerUpdateProgrammerDisplaysBIN(decVal);
        }

        #endregion

        #region KeyBoard Manipulation

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var (newChain, enterPressed) = OmniProgrammerFunctions.OmniProgrammerHistoryKeyManipulation(e, expressionChain, currentBase);
            expressionChain = newChain;

            if (enterPressed)
            {
                var resultEqual = OmniProgrammerFunctions.OmniProgrammerWindow_KeyDown(sender, e, currentValue, currentBase, isNewEntry, lastDecValue, currentOperator);
                currentValue = resultEqual.newValue;
                lastDecValue = resultEqual.newLastDecValue;
                currentOperator = resultEqual.newOperator;
                isNewEntry = resultEqual.newIsNewEntry;
                UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
                string operation = expressionChain + " = " + currentValue;
                HistoryManager.AddHistory(operation);
                expressionChain = currentValue;
                return;
            }

            var result = OmniProgrammerFunctions.OmniProgrammerWindow_KeyDown(sender, e, currentValue, currentBase, isNewEntry, lastDecValue, currentOperator);
            currentValue = result.newValue;
            lastDecValue = result.newLastDecValue;
            currentOperator = result.newOperator;
            isNewEntry = result.newIsNewEntry;
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
        }


        #endregion

        #region Introduction Numbers 

        private void HexDigit_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniProgrammerFunctions.OmniProgrammerHexDigit_Click(sender, e, currentValue, currentBase, isNewEntry);
            string digitPressed = (sender as System.Windows.Controls.Button).Content.ToString();
            if (result.isAcceptable) { expressionChain += digitPressed; }
            isNewEntry = result.entry;
            currentValue = result.currentValue;
            UpdateProgrammerDisplays(result.decVal);
        }
        #endregion

        #region Arithmetic Operations, Negate and Equal

        private void Arithmetic_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniProgrammerFunctions.OmniProgrammerArithmetic_Click(
                sender, e, currentValue, currentBase, lastDecValue, currentOperator);
            string operatorAritmatical = (sender as Button).Content.ToString().ToUpper();
            expressionChain += " " + operatorAritmatical + " ";
            currentOperator = result.op;
            lastDecValue = result.lastVal;
            isNewEntry = true;

            currentValue = OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(lastDecValue, currentBase);
            UpdateProgrammerDisplays(result.lastVal);
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniProgrammerFunctions.OmniProgrammerEqual_Click(
                sender, e, currentValue, currentBase, lastDecValue, currentOperator);

            currentValue = result.ValCur;
            currentOperator = result.Oper;
            lastDecValue = result.lastVal;
            isNewEntry = false;
            string operation = expressionChain + " = " + currentValue;
            expressionChain = OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(lastDecValue, currentBase);
            HistoryManager.AddHistory(operation); 
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
            expressionChain = currentValue;
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            var negResult = OmniProgrammerFunctions.OmniProgrammerNegate_Click(sender, e, currentValue, currentBase);
            currentValue = OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(negResult, currentBase);
            UpdateProgrammerDisplays(negResult);
        }
        #endregion

        #region Parentheses

        private void Paren_Click(object sender, RoutedEventArgs e)
        {
            currentValue = OmniProgrammerFunctions.OmniProgrammerParen_Click(sender, e, currentValue, currentBase);
            long decVal = OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase);
            txtDisplayProgrammer.Text = currentValue;
            UpdateProgrammerDisplays(decVal);
        }

        #endregion

        #region History
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }
        #endregion

        #region Operations Bitwise and Shift

        private void Bitwise_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniProgrammerFunctions.OmniProgrammerBitwise_Click(
                sender, e, currentValue, currentBase, lastDecValue, currentOperator);
            string operatorLogical = (sender as Button).Content.ToString().ToUpper();
            expressionChain += " " + operatorLogical + " ";
            currentOperator = result.op;      
            lastDecValue = result.lastVal;     
            currentValue = result.newCurrentValue; 
            UpdateProgrammerDisplays(result.decVal);
        }


        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            var shiftResult = OmniProgrammerFunctions.OmniProgrammerShift_Click(sender, e, currentValue, currentBase);
            currentValue = OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(shiftResult, currentBase);
            UpdateProgrammerDisplays(shiftResult);
        }
        #endregion

        #region Cut, Copy, Paste, Digit Grouping and About

        private void MenuCut_Click(object sender, RoutedEventArgs e)
        {
            currentValue = OmniProgrammerFunctions.OmniProgrammerMenuCut_Click(sender, e, txtDisplayProgrammer.Text);
            lastDecValue = 0;
            isNewEntry = true;
            currentOperator = "";
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
        }

        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            OmniProgrammerFunctions.OmniProgrammerMenuCopy_Click(sender, e, txtDisplayProgrammer.Text);
        }

        private void MenuPaste_Click(object sender, RoutedEventArgs e)
        {
            string pasteResult = OmniProgrammerFunctions.OmniProgrammerMenuPaste_Click(sender, e, currentBase);
            isNewEntry = false; currentValue = pasteResult.ToString();
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
        }

        private void MenuDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
            var groupingResult = OmniProgrammerFunctions.OmniProgrammerMenuDigitGrouping_Click(sender, e, currentValue, currentBase);
            UpdateProgrammerDisplays(groupingResult);
            IsDigitGrouping = OmniProgrammerFunctions.IsDigitGrouping;
            menuDigitGroupingProgrammer.IsChecked = IsDigitGrouping;
        }


        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            OmniProgrammerFunctions.OmniProgrammerMenuAbout_Click(sender, e);
        }
        #endregion

        #region C, CE, Backspace, Dot and Exit

        private void C_Click(object sender, RoutedEventArgs e)
        {
            currentValue = OmniProgrammerFunctions.OmniProgrammerC_Click(sender, e);
            lastDecValue = 0;
            isNewEntry = true;
            currentOperator = "";
            expressionChain = "";
            UpdateProgrammerDisplays(0);
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            currentValue = OmniProgrammerFunctions.OmniProgrammerCE_Click(sender, e);
            isNewEntry = true;
            expressionChain = "";
            UpdateProgrammerDisplays(0);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            var backResult = OmniProgrammerFunctions.OmniProgrammerBackspace_Click(sender, e, currentValue, currentBase);
            if (!string.IsNullOrEmpty(expressionChain))
            {
                expressionChain = expressionChain.Substring(0, expressionChain.Length - 1);
            }
            currentValue = OmniProgrammerFunctions.OmniProgrammerConvertDecToBase(backResult, currentBase);
            UpdateProgrammerDisplays(backResult);
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            var dotResult = OmniProgrammerFunctions.OmniProgrammerDot_Click(sender, e, currentValue, currentBase);
            currentValue = dotResult;
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(dotResult, currentBase));
        }
        #endregion

        #region Base Interchange

        private void ChangeBase_Click(object sender, RoutedEventArgs e)
        {
            var result = OmniProgrammerFunctions.OmniProgrammerChangeBase_Click(sender, e, currentValue, currentBase);
            currentValue = result.newValue;
            currentBase = result.newBase;
            isNewEntry = false;
            UpdateProgrammerDisplays(OmniProgrammerFunctions.OmniProgrammerConvertBaseToDec(currentValue, currentBase));
            BaseEnable(); LoadBase(); ButtonsEnabler();
        }

        private void BaseEnable()
        {
            OmniProgrammerFunctions.OmniProgrammerBaseEnable(currentBase);
        }

        private void LoadBase()
        {
            isBIN = Settings.Default.IsBaseBIN;
            isDEC = Settings.Default.IsBaseDEC;
            isHEX = Settings.Default.IsBaseHEX;
            isOCT = Settings.Default.IsBaseOCT;
        }

        private void ButtonsEnabler()
        {
            Btn0.IsEnabled = isBIN;
            Btn1.IsEnabled = isBIN;
            Btn2.IsEnabled = isOCT;
            Btn3.IsEnabled = isOCT;
            Btn4.IsEnabled = isOCT;
            Btn5.IsEnabled = isOCT;
            Btn6.IsEnabled = isOCT;
            Btn7.IsEnabled = isOCT;
            Btn8.IsEnabled = isDEC;
            Btn9.IsEnabled = isDEC;
            BtnA.IsEnabled = isHEX;
            BtnB.IsEnabled = isHEX;
            BtnC.IsEnabled = isHEX;
            BtnD.IsEnabled = isHEX;
            BtnE.IsEnabled = isHEX;
            BtnF.IsEnabled = isHEX;
        }
        #endregion

        #region Settings

        private void LoadSettings()
        {
            OmniProgrammerFunctions.OmniProgrammerLoadSettings();
            this.IsDigitGrouping = OmniProgrammerFunctions.IsDigitGrouping;
            currentBase = OmniProgrammerFunctions.selectedBase;
        }

        private void SaveSettings()
        {
            OmniProgrammerFunctions.OmniProgrammerSaveSettings();
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
            OmniProgrammerFunctions.OmniProgrammerModeStandard_Click(sender, e);
            this.Close();
        }

        private void ModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            OmniProgrammerFunctions.OmniProgrammerModeProgrammer_Click(sender, e);
        }

        private void ModeScientific_Click(object sender, RoutedEventArgs e)
        {
            OmniProgrammerFunctions.OmniProgrammerModeScientific_Click(sender, e);
            this.Close();
        }

        private void ModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            OmniProgrammerFunctions.OmniProgrammerModeEcuation_Click(sender, e);
            this.Close();
        }
        #endregion

        #region Themes
        private void ThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeHeavenLight_Click(sender, e);
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        private void ThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeTotalDarkness_Click(sender, e);
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        private void ThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeForestGreen_Click(sender, e);
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        private void ThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeQuantumRed_Click(sender, e);
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        private void ThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeDeepBlue_Click(sender, e);
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        private void ThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemePaleLavender_Click(sender, e);
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        private void ThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeSunnyYellow_Click(sender, e);
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        private void ThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeDeepOrange_Click(sender, e);
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        private void ThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeIntenseViolet_Click(sender, e);
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        private void ThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            //OmniProgrammerFunctions.OmniProgrammerThemeBabyPink_Click(sender, e);
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
