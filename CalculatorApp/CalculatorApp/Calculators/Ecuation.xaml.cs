using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorApp.OmniCalculatorFunctions;

namespace CalculatorApp
{
    public partial class Ecuation : Window
    {
        private string expressionChain = "";
        public Ecuation()
        {
            InitializeComponent();
            txtEquation.Text = "";
        }

        #region Buttons Creation and Operation Assignment
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationBtn_Click(sender, e, txtEquation.Text);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationBtnClear_Click(sender, e, txtEquation.Text);
            expressionChain = "";
        }

        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            expressionChain = txtEquation.Text;
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationBtnEqual_Click(sender, e, txtEquation.Text);
            expressionChain = expressionChain + " = " + txtEquation.Text;
            HistoryManager.AddHistory(expressionChain);
            expressionChain = txtEquation.Text;

        }
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationBackspace_Click(sender, e, txtEquation.Text);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region History
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.ShowDialog();
        }
        #endregion

        #region Copy, Cut, Paste and About

        private void MenuCut_Click(object sender, RoutedEventArgs e)
        {
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationMenuCut_Click(sender, e, txtEquation.Text);
        }

        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationMenuCopy_Click(sender, e, txtEquation.Text);
        }

        private void MenuPaste_Click(object sender, RoutedEventArgs e)
        {

           txtEquation.Text = OmniEcuationFunctions.OmniEcuationMenuPaste_Click(sender, e, txtEquation.Text);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationMenuAbout_Click(sender, e);
        }

        #endregion

        #region KeyBoard Manipulation
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            expressionChain = txtEquation.Text;
            txtEquation.Text = OmniEcuationFunctions.OmniEcuationWindow_KeyDown(sender, e, txtEquation.Text);
            if (OmniEcuationFunctions.OmniEcuationHasEnterBeenPressed(sender, e))
            {
                expressionChain = expressionChain + " = " + txtEquation.Text;
                HistoryManager.AddHistory(expressionChain);
                expressionChain = txtEquation.Text;
            }
        }

        #endregion

        #region Calculator Mode
        private void ModeStandard_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationModeStandard_Click(sender, e);
            this.Close();
        }

        private void ModeProgrammer_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationModeProgrammer_Click(sender, e);
            this.Close();
        }

        private void ModeScientific_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationModeScientific_Click(sender, e);
            this.Close();
        }

        private void ModeEcuation_Click(object sender, RoutedEventArgs e)
        {
            OmniEcuationFunctions.OmniEcuationModeEcuation_Click(sender, e);   
        }
        #endregion

        #region Themes
        private void ThemeHeavenLight_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeHeavenLight_Click(sender, e);
            ThemeManager.ApplyTheme("HeavenLight");
            Settings.Default.CalculatorTheme = "HeavenLight";
        }

        private void ThemeTotalDarkness_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeTotalDarkness_Click(sender, e);
            ThemeManager.ApplyTheme("TotalDarkness");
            Settings.Default.CalculatorTheme = "TotalDarkness";
        }

        private void ThemeForestGreen_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeForestGreen_Click(sender, e);
            ThemeManager.ApplyTheme("ForestGreen");
            Settings.Default.CalculatorTheme = "ForestGreen";
        }

        private void ThemeQuantumRed_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeQuantumRed_Click(sender, e);
            ThemeManager.ApplyTheme("QuantumRed");
            Settings.Default.CalculatorTheme = "QuantumRed";
        }

        private void ThemeDeepBlue_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeDeepBlue_Click(sender, e);
            ThemeManager.ApplyTheme("DeepBlue");
            Settings.Default.CalculatorTheme = "DeepBlue";
        }

        private void ThemePaleLavender_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemePaleLavender_Click(sender, e);
            ThemeManager.ApplyTheme("PaleLavender");
            Settings.Default.CalculatorTheme = "PaleLavender";
        }

        private void ThemeSunnyYellow_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeSunnyYellow_Click(sender, e);
            ThemeManager.ApplyTheme("SunnyYellow");
            Settings.Default.CalculatorTheme = "SunnyYellow";
        }

        private void ThemeDeepOrange_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeDeepOrange_Click(sender, e);
            ThemeManager.ApplyTheme("DeepOrange");
            Settings.Default.CalculatorTheme = "DeepOrange";
        }

        private void ThemeIntenseViolet_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeIntenseViolet_Click(sender, e);
            ThemeManager.ApplyTheme("IntenseViolet");
            Settings.Default.CalculatorTheme = "IntenseViolet";
        }

        private void ThemeBabyPink_Click(object sender, RoutedEventArgs e)
        {
            //OmniEcuationFunctions.OmniEcuationThemeBabyPink_Click(sender, e);
            ThemeManager.ApplyTheme("BabyPink");
            Settings.Default.CalculatorTheme = "BabyPink";
        }
        #endregion
    }
}
