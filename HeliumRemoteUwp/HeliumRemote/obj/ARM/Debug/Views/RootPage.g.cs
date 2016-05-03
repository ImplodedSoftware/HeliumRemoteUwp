﻿#pragma checksum "C:\dev\HeliumRemoteUwp\HeliumRemoteUwp\HeliumRemote\Views\RootPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6CDCADF5B0BE41887178DFAE5584B1E4"
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
    partial class RootPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
        };

        private class RootPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IRootPage_Bindings
        {
            private global::HeliumRemote.Views.RootPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.AutoSuggestBox obj7;
            private global::Windows.UI.Xaml.Controls.Button obj11;
            private global::Windows.UI.Xaml.Controls.TextBox obj15;

            public RootPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7:
                        this.obj7 = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)target;
                        ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)target).QuerySubmitted += (global::Windows.UI.Xaml.Controls.AutoSuggestBox param0, global::Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs param1) =>
                        {
                        this.dataRoot._vm.SearchContainer_OnQuerySubmitted(param0, param1);
                        };
                        break;
                    case 11:
                        this.obj11 = (global::Windows.UI.Xaml.Controls.Button)target;
                        ((global::Windows.UI.Xaml.Controls.Button)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.PlayPause_OnTapped(param0, param1);
                        };
                        break;
                    case 15:
                        this.obj15 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        ((global::Windows.UI.Xaml.Controls.TextBox)target).TextChanged += (global::System.Object param0, global::Windows.UI.Xaml.Controls.TextChangedEventArgs param1) =>
                        {
                        this.dataRoot._vm.FilterTb_OnTextChanged(param0, param1);
                        };
                        break;
                    default:
                        break;
                }
            }

            // IRootPage_Bindings

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

            // RootPage_obj1_Bindings

            public void SetDataRoot(global::HeliumRemote.Views.RootPage newDataRoot)
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
                    #line 8 "..\..\..\Views\RootPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.RootPage_OnLoaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.Root = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.Normal = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4:
                {
                    this.Mobile = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.SplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                    #line 284 "..\..\..\Views\RootPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.SplitView)this.SplitView).PaneClosed += this.SplitView_PaneClosed;
                    #line default
                }
                break;
            case 6:
                {
                    this.SplitViewPanePanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 7:
                {
                    this.SearchContainer = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 8:
                {
                    this.SearchButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 293 "..\..\..\Views\RootPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SearchButton).Click += this.SearchButton_OnClick;
                    #line default
                }
                break;
            case 9:
                {
                    this.MyFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.Grid element10 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 410 "..\..\..\Views\RootPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)element10).Tapped += this.MiniPlayer_OnTapped;
                    #line default
                }
                break;
            case 11:
                {
                    global::Windows.UI.Xaml.Controls.Button element11 = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 12:
                {
                    this.PlayerTimePane = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 13:
                {
                    this.MenuButton1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 265 "..\..\..\Views\RootPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.MenuButton1).Click += this.HamburgerRadioButton_OnClick;
                    #line default
                }
                break;
            case 14:
                {
                    this.PageTitle = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 15:
                {
                    this.FilterTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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
                    RootPage_obj1_Bindings bindings = new RootPage_obj1_Bindings();
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

