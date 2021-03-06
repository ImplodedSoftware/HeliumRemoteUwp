﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp.SharedResources.Views
{
    public sealed partial class ActionDialogTracks : ContentDialog
    {
        public ActionDialogTracks()
        {
            this.InitializeComponent();
        }
        public int ResultCode { get; set; }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var tag = (string)btn.Tag;
            ResultCode = int.Parse(tag);
            Hide();
        }
    }
}
