﻿#pragma checksum "..\..\..\..\Calculators\Ecuation.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5757348D066DE54A98C25AB81E358A1037C0E886"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CalculatorApp {
    
    
    /// <summary>
    /// Ecuation
    /// </summary>
    public partial class Ecuation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 106 "..\..\..\..\Calculators\Ecuation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEquation;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CalculatorApp;component/calculators/ecuation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Calculators\Ecuation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Calculators\Ecuation.xaml"
            ((CalculatorApp.Ecuation)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 69 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuCut_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 70 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuCopy_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 71 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuPaste_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 74 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeStandard_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 75 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeProgrammer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 76 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeScientific_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 77 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeEcuation_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 81 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnHistory_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 82 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuAbout_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 85 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeHeavenLight_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 86 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeTotalDarkness_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 87 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeForestGreen_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 88 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeQuantumRed_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 89 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeDeepBlue_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 90 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemePaleLavender_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 91 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeSunnyYellow_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 92 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeDeepOrange_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 93 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeIntenseViolet_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 94 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ThemeBabyPink_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 96 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnExit_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.txtEquation = ((System.Windows.Controls.TextBox)(target));
            return;
            case 23:
            
            #line 118 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 119 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 120 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 121 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 123 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 124 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 125 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 126 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 128 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 129 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 130 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 131 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 133 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 134 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 135 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 136 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 138 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnClear_Click);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 139 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Btn_Click);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 140 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnEqual_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 141 "..\..\..\..\Calculators\Ecuation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Backspace_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

