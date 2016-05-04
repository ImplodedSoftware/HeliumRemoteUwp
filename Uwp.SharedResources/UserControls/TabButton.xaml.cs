using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Command;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Uwp.SharedResources.UserControls
{
    public sealed partial class TabButton : UserControl
    {
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof (bool),
            typeof (TabButton),
            new PropertyMetadata(null)
            );

        public static readonly DependencyProperty CustomTextProperty = DependencyProperty.Register(
            "CustomText",
            typeof (string),
            typeof (TabButton),
            new PropertyMetadata(null)
            );

        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
            "ClickCommand",
            typeof (RelayCommand),
            typeof (TabButton),
            new PropertyMetadata(null)
            );

        public TabButton()
        {
            InitializeComponent();
        }

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set
            {
                SetValue(IsSelectedProperty, value);
                if (IsSelected)
                {
                    var b = Application.Current.Resources["NavLineBrush"] as SolidColorBrush;
                    Rectangle.Fill = b;
                }
                else
                {
                    var b = Application.Current.Resources["NavLineDisabledBrush"] as SolidColorBrush;
                    Rectangle.Fill = b;
                }
            }
        }

        public string CustomText
        {
            get { return (string) GetValue(CustomTextProperty); }
            set
            {
                SetValue(CustomTextProperty, value);
                TextBlock.Text = CustomText;
            }
        }

        public RelayCommand ClickCommand
        {
            get { return (RelayCommand) GetValue(ClickCommandProperty); }
            set
            {
                SetValue(ClickCommandProperty, value);
                ContainerButton.Command = ClickCommand;
            }
        }

        public void SetCustomWidth(int width)
        {
            ContentGrid.Width = width;
        }
    }
}