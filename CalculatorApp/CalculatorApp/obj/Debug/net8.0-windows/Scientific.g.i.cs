﻿#pragma checksum "..\..\..\Scientific.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0A0E8DB54222C54535E7246ED04A4F688DAD6880"
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
    /// Scientific
    /// </summary>
    public partial class Scientific : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 69 "..\..\..\Scientific.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuDigitGrouping;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Scientific.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDisplayScientific;
        
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
            System.Uri resourceLocater = new System.Uri("/CalculatorApp;V1.0.0.0;component/scientific.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Scientific.xaml"
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
            
            #line 10 "..\..\..\Scientific.xaml"
            ((CalculatorApp.Scientific)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 65 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuCut_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 66 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuCopy_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 67 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuPaste_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.menuDigitGrouping = ((System.Windows.Controls.MenuItem)(target));
            
            #line 72 "..\..\..\Scientific.xaml"
            this.menuDigitGrouping.Click += new System.Windows.RoutedEventHandler(this.MenuDigitGrouping_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 75 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeStandard_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 76 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeProgrammer_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 77 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeScientific_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 78 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ModeEcuation_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 81 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuAbout_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 83 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnExit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.txtDisplayScientific = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            
            #line 117 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 118 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 119 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 120 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 121 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 122 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MemoryButton_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 126 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Trig_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 127 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Trig_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 128 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Trig_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 129 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Trig_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 133 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exp_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 134 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Ln_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 135 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Log_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 136 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Square_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 137 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Factorial_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 138 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RadicalXY_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 143 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CE_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 144 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.C_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 145 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Backspace_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 146 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Negate_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 148 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 149 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 150 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 151 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Operator_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 153 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 154 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 155 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 156 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Operator_Click);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 158 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 159 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 160 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 161 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Operator_Click);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 163 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 164 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Digit_Click);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 165 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Operator_Click);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 166 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Equal_Click);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 168 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Paren_Click);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 169 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Paren_Click);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 170 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Operator_Click);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 171 "..\..\..\Scientific.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Reciprocal_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

