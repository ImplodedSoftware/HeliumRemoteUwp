﻿#pragma checksum "C:\dev\HeliumRemoteUwp\HeliumRemoteUwp\HeliumRemote\Views\ArtistDetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "92457794AF789BB41654583269283C09"
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
    partial class ArtistDetailPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
        };

        private class ArtistDetailPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IArtistDetailPage_Bindings
        {
            private global::HeliumRemote.Views.ArtistDetailPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListBox obj7;
            private global::Windows.UI.Xaml.Controls.GridView obj9;

            public ArtistDetailPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7:
                        this.obj7 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        ((global::Windows.UI.Xaml.Controls.ListBox)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.OnTapped(param0, param1);
                        };
                        break;
                    case 9:
                        this.obj9 = (global::Windows.UI.Xaml.Controls.GridView)target;
                        ((global::Windows.UI.Xaml.Controls.GridView)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.OnTapped(param0, param1);
                        };
                        break;
                    default:
                        break;
                }
            }

            // IArtistDetailPage_Bindings

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

            // ArtistDetailPage_obj1_Bindings

            public void SetDataRoot(global::HeliumRemote.Views.ArtistDetailPage newDataRoot)
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
                    #line 8 "..\..\..\Views\ArtistDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.ArtistDetailPage_OnLoaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.GroupedReleasesSource = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Image element3 = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 71 "..\..\..\Views\ArtistDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)element3).ImageOpened += this.Image_OnImageOpened;
                    #line 72 "..\..\..\Views\ArtistDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)element3).ImageFailed += this.Image_OnImageFailed;
                    #line default
                }
                break;
            case 4:
                {
                    this.RootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5:
                {
                    this.Normal = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 6:
                {
                    this.Mobile = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 7:
                {
                    this.ListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 8:
                {
                    this.TabletGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 9:
                {
                    this.GridView = (global::Windows.UI.Xaml.Controls.GridView)(target);
                }
                break;
            case 10:
                {
                    global::Windows.UI.Xaml.Controls.Image element10 = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 144 "..\..\..\Views\ArtistDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)element10).ImageFailed += this.Image_OnImageFailed;
                    #line default
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
                    ArtistDetailPage_obj1_Bindings bindings = new ArtistDetailPage_obj1_Bindings();
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

