﻿#pragma checksum "C:\dev\HeliumRemoteUwp\HeliumRemoteUwp\HeliumRemote\UserControls\ImStar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "294B6F2C298F1B555528149C807D1958"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeliumRemote.UserControls
{
    partial class ImStar : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
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
                    this.gdStar = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.starForeground = (global::Windows.UI.Xaml.Shapes.Path)(target);
                }
                break;
            case 3:
                {
                    this.halfStar = (global::Windows.UI.Xaml.Shapes.Path)(target);
                }
                break;
            case 4:
                {
                    this.mask = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 5:
                {
                    this.starOutline = (global::Windows.UI.Xaml.Shapes.Path)(target);
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
            return returnValue;
        }
    }
}

