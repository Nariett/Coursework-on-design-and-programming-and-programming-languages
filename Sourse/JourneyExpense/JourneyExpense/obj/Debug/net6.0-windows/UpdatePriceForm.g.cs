﻿#pragma checksum "..\..\..\UpdatePriceForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "18F988B1EFDB3174E14C16C9A6B4001FEB4C3096"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using JourneyExpense;
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


namespace JourneyExpense {
    
    
    /// <summary>
    /// UpdatePriceForm
    /// </summary>
    public partial class UpdatePriceForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\UpdatePriceForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxFuelType;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\UpdatePriceForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxFuelOctan;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UpdatePriceForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxPrice;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UpdatePriceForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpdatePriceButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/JourneyExpense;component/updatepriceform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UpdatePriceForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.comboBoxFuelType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\UpdatePriceForm.xaml"
            this.comboBoxFuelType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBoxFuelType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.comboBoxFuelOctan = ((System.Windows.Controls.ComboBox)(target));
            
            #line 27 "..\..\..\UpdatePriceForm.xaml"
            this.comboBoxFuelOctan.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBoxFuelOctan_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textBoxPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.UpdatePriceButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\UpdatePriceForm.xaml"
            this.UpdatePriceButton.Click += new System.Windows.RoutedEventHandler(this.UpdatePriceButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

