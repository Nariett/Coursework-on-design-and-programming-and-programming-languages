﻿#pragma checksum "..\..\..\GraphForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54A5411E7530C4454DB94F247869F65231BC52D2"
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
    /// GraphForm
    /// </summary>
    public partial class GraphForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvasGraph;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DataPickerFirstData;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DataPickerSecondData;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonShow;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalPrice;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCreateView;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClear;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\GraphForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonShowAll;
        
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
            System.Uri resourceLocater = new System.Uri("/JourneyExpense;component/graphform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GraphForm.xaml"
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
            this.canvasGraph = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.DataPickerFirstData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.DataPickerSecondData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.ButtonShow = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\GraphForm.xaml"
            this.ButtonShow.Click += new System.Windows.RoutedEventHandler(this.ButtonShow_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TotalPrice = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ButtonCreateView = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.ButtonClear = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\GraphForm.xaml"
            this.ButtonClear.Click += new System.Windows.RoutedEventHandler(this.ButtonClear_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonShowAll = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\GraphForm.xaml"
            this.ButtonShowAll.Click += new System.Windows.RoutedEventHandler(this.ButtonShowAll_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

