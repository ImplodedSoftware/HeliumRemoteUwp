﻿#pragma checksum "C:\dev\HeliumRemoteUwp\Uwp.SharedResources\Views\SearchResultsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "204147A8E51A5CD87BB0A3546D72042F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uwp.SharedResources.Views
{
    partial class SearchResultsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
        };

        private class SearchResultsPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ISearchResultsPage_Bindings
        {
            private global::Uwp.SharedResources.Views.SearchResultsPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListBox obj3;
            private global::Windows.UI.Xaml.Controls.ListBox obj4;
            private global::Windows.UI.Xaml.Controls.ListBox obj5;

            public SearchResultsPage_obj1_Bindings()
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
                        this.dataRoot._vm.GridView_OnTapped(param0, param1);
                        };
                        break;
                    case 4:
                        this.obj4 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        ((global::Windows.UI.Xaml.Controls.ListBox)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.GridView_OnTapped(param0, param1);
                        };
                        break;
                    case 5:
                        this.obj5 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        ((global::Windows.UI.Xaml.Controls.ListBox)target).Tapped += (global::System.Object param0, global::Windows.UI.Xaml.Input.TappedRoutedEventArgs param1) =>
                        {
                        this.dataRoot._vm.GridView_OnTapped(param0, param1);
                        };
                        break;
                    default:
                        break;
                }
            }

            // ISearchResultsPage_Bindings

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

            // SearchResultsPage_obj1_Bindings

            public void SetDataRoot(global::Uwp.SharedResources.Views.SearchResultsPage newDataRoot)
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
                    #line 9 "..\..\..\Views\SearchResultsPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.SearchResultsPage_OnLoaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.TopGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.ArtistsLb = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 4:
                {
                    this.AlbumsLb = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 5:
                {
                    this.TracksLb = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 6:
                {
                    this.tbArtists = (global::Uwp.SharedResources.UserControls.TabButton)(target);
                }
                break;
            case 7:
                {
                    this.tbAlbums = (global::Uwp.SharedResources.UserControls.TabButton)(target);
                }
                break;
            case 8:
                {
                    this.tbTracks = (global::Uwp.SharedResources.UserControls.TabButton)(target);
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
                    SearchResultsPage_obj1_Bindings bindings = new SearchResultsPage_obj1_Bindings();
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

