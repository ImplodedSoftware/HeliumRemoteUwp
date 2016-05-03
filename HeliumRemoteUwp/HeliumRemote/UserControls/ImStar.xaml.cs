using System;
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
        public SolidColorBrush _emptyColor;

        public ImStar()
        {
            this.DataContext = this;
            InitializeComponent();

            _emptyColor = new SolidColorBrush(Color.FromArgb(160, 128, 128, 128));

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

            this.SizeChanged += new SizeChangedEventHandler(Star_SizeChanged);
        }

        void Star_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        /// <summary>
        /// BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor",
            typeof(SolidColorBrush), typeof(ImStar),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                new PropertyChangedCallback(OnBackgroundColorChanged)));

        /// <summary>
        /// Gets or sets the BackgroundColor property.  
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the BackgroundColor property.
        /// </summary>
        private static void OnBackgroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImStar control = (ImStar)d;
            control.gdStar.Background = (SolidColorBrush)e.NewValue;
            control.mask.Fill = (SolidColorBrush)e.NewValue;
        }

        /// <summary>
        /// StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(SolidColorBrush),
            typeof(ImStar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnStarForegroundColorChanged)));

        /// <summary>
        /// Gets or sets the StarForegroundColor property.  
        /// </summary>
        public SolidColorBrush StarForegroundColor
        {
            get { return (SolidColorBrush)GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StarForegroundColor property.
        /// </summary>
        private static void OnStarForegroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImStar control = (ImStar)d;
            control.starForeground.Fill = (SolidColorBrush)e.NewValue;
        }

        /// <summary>
        /// StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof(SolidColorBrush),
            typeof(ImStar),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                new PropertyChangedCallback(OnStarOutlineColorChanged)));

        /// <summary>
        /// Gets or sets the StarOutlineColor property.  
        /// </summary>
        public SolidColorBrush StarOutlineColor
        {
            get { return (SolidColorBrush)GetValue(StarOutlineColorProperty); }
            set { SetValue(StarOutlineColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StarOutlineColor property.
        /// </summary>
        private static void OnStarOutlineColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImStar control = (ImStar)d;
            control.starOutline.Stroke = (SolidColorBrush)e.NewValue;
        }


        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double),
            typeof(ImStar),
            new PropertyMetadata(-1.0,
                new PropertyChangedCallback(OnValueChanged)));

        /// <summary>
        /// Gets or sets the Value property.  
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        private static void OnValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImStar starControl = (ImStar)d;

            if (starControl.Value == 0.0)
            {
                starControl.starForeground.Fill = starControl._emptyColor;
            }
            /*
            else if (starControl.Value == 9.0)
            {
                starControl.starForeground.Fill = starControl._emptyColor;
            }
            */
            else
            {
                starControl.starForeground.Fill = starControl.StarForegroundColor;
            }

            /*
            Int32 marginLeftOffset = (Int32)(starControl.Value * (double)STAR_SIZE);
            starControl.mask.Margin = new Thickness(marginLeftOffset, 0, 0, 0);
            */
            Int32 marginLeftOffset = (Int32)(1.0 * (double)STAR_SIZE);
            starControl.mask.Margin = new Thickness(marginLeftOffset, 0, 0, 0);

            if (starControl.Value == 0.5)
                starControl.halfStar.Fill = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));//starControl._emptyColor;
            else
                starControl.halfStar.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            starControl.InvalidateArrange();
            starControl.InvalidateMeasure();
        }

    }

}
