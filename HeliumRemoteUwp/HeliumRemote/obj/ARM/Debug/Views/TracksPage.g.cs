﻿#pragma checksum "C:\dev\HeliumRemoteUwp\HeliumRemoteUwp\HeliumRemote\Views\TracksPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DE379DA675D529C4ABBF7E1FBD0CF2F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeliumRemote.Views
{
    partial class TracksPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
        };

        private class TracksPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ITracksPage_Bindings
        {
            private global::HeliumRemote.Views.TracksPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListBox obj3;

            public TracksPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3:
                        this.obj3 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        ((global::Windows.UI.Xaml.Controls.ListBox)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.OnTapped(param0, param1);
                        };
                        break;
                    default:
                        break;
                }
            }

            // ITracksPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // TracksPage_obj1_Bindings

            public void SetDataRoot(global::HeliumRemote.Views.TracksPage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 7 "..\..\..\Views\TracksPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.TracksPage_OnLoaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.RootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.ItemsLb = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    TracksPage_obj1_Bindings bindings = new TracksPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}
