using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeliumRemote.UserControls
{
    public sealed partial class ImStar : UserControl
    {
        private const int STAR_SIZE = 32;

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor",
                typeof (SolidColorBrush), typeof (ImStar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnBackgroundColorChanged));

        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof (SolidColorBrush),
                typeof (ImStar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnStarForegroundColorChanged));

        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof (SolidColorBrush),
                typeof (ImStar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnStarOutlineColorChanged));


        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (double),
                typeof (ImStar),
                new PropertyMetadata(-1.0,
                    OnValueChanged));

        public SolidColorBrush EmptyColor;

        public ImStar()
        {
            DataContext = this;
            InitializeComponent();

            EmptyColor = new SolidColorBrush(Color.FromArgb(160, 128, 128, 128));

            gdStar.Width = STAR_SIZE;
            gdStar.Height = STAR_SIZE;
            gdStar.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, STAR_SIZE, STAR_SIZE)
            };

            mask.Width = STAR_SIZE;
            mask.Height = STAR_SIZE;
            mask.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, STAR_SIZE, STAR_SIZE)
            };

            SizeChanged += Star_SizeChanged;
        }

        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush) GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public SolidColorBrush StarForegroundColor
        {
            get { return (SolidColorBrush) GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        public SolidColorBrush StarOutlineColor
        {
            get { return (SolidColorBrush) GetValue(StarOutlineColorProperty); }
            set { SetValue(StarOutlineColorProperty, value); }
        }

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void Star_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private static void OnBackgroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImStar) d;
            control.gdStar.Background = (SolidColorBrush) e.NewValue;
            control.mask.Fill = (SolidColorBrush) e.NewValue;
        }

        private static void OnStarForegroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImStar) d;
            control.starForeground.Fill = (SolidColorBrush) e.NewValue;
        }

        private static void OnStarOutlineColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImStar) d;
            control.starOutline.Stroke = (SolidColorBrush) e.NewValue;
        }

        private static void OnValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var starControl = (ImStar) d;

            if (starControl.Value == 0.0)
            {
                starControl.starForeground.Fill = starControl.EmptyColor;
            }
            else
            {
                starControl.starForeground.Fill = starControl.StarForegroundColor;
            }

            var marginLeftOffset = (int) (1.0*STAR_SIZE);
            starControl.mask.Margin = new Thickness(marginLeftOffset, 0, 0, 0);

            if (starControl.Value == 0.5)
                starControl.halfStar.Fill = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
            else
                starControl.halfStar.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            starControl.InvalidateArrange();
            starControl.InvalidateMeasure();
        }
    }
}