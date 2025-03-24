using System.Configuration;
using System.Data;
using System.Windows;


namespace CalculatorApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string lastMode = Settings.Default.LastCalculatorMode;
            Window mainWindow;

            switch (lastMode)
            {
                case "Programmer":
                    mainWindow = new Programmer();
                    break;
                case "Scientific":
                    mainWindow = new Scientific();
                    break;
                case "Ecuation":
                    mainWindow = new Ecuation();
                    break;
                case "Standard":
                    mainWindow = new Standard();
                    break;
                default:
                    mainWindow = new Standard();
                    break;
            }
            mainWindow.Show();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string calculatorTheme = Settings.Default.CalculatorTheme;
            switch (calculatorTheme)
            {
                case "HeavenLight":
                    ThemeManager.ApplyTheme("HeavenLight");
                    break;
                case "TotalDarkness":
                    ThemeManager.ApplyTheme("TotalDarkness");
                    break;
                case "ForestGreen":
                    ThemeManager.ApplyTheme("ForestGreen");
                    break;
                case "QuantumRed":
                    ThemeManager.ApplyTheme("QuantumRed");
                    break;
                case "DeepBlue":
                    ThemeManager.ApplyTheme("DeepBlue");
                    break;
                case "PaleLavender":
                    ThemeManager.ApplyTheme("PaleLavender");
                    break;
                case "SunnyYellow":
                    ThemeManager.ApplyTheme("SunnyYellow");
                    break;
                case "DeepOrange":
                    ThemeManager.ApplyTheme("DeepOrange");
                    break;
                case "IntenseViolet":
                    ThemeManager.ApplyTheme("IntenseViolet");
                    break;
                case "BabyPink":
                    ThemeManager.ApplyTheme("BabyPink");
                    break;
                default:
                    ThemeManager.SetDefaultTheme();
                    break;
            }
       
        }
    }

}
