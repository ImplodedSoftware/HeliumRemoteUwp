using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeliumRemote.UserControls
{
    public sealed partial class ImRating : UserControl
    {
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof (SolidColorBrush),
                typeof (ImRating),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnBackgroundColorChanged));

        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof (SolidColorBrush),
                typeof (ImRating),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnStarForegroundColorChanged));

        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof (SolidColorBrush),
                typeof (ImRating),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnStarOutlineColorChanged));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (double),
                typeof (ImRating),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnValueChanged));

        public static readonly DependencyProperty NumberOfStarsProperty =
            DependencyProperty.Register("NumberOfStars", typeof (int), typeof (ImRating),
                new PropertyMetadata(5,
                    OnNumberOfStarsChanged));

        public ImRating()
        {
            InitializeComponent();
        }

        public static Action<double> PropagateValueChanged { get; set; }

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
            get
            {
                try
                {
                    return (double) GetValue(ValueProperty);
                }
                catch
                {
                    return -1.0;
                }
            }
            set { SetValue(ValueProperty, value); }
        }

        public int NumberOfStars
        {
            get { return (int) GetValue(NumberOfStarsProperty); }
            set { SetValue(NumberOfStarsProperty, value); }
        }

        private static void OnBackgroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImRating) d;
            foreach (ImStar star in control.spStars.Children)
                star.BackgroundColor = (SolidColorBrush) e.NewValue;
        }

        private static void OnStarForegroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImRating) d;
            foreach (ImStar star in control.spStars.Children)
                star.StarForegroundColor = (SolidColorBrush) e.NewValue;
        }

        private static void OnStarOutlineColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (ImRating) d;
            foreach (ImStar star in control.spStars.Children)
                star.StarOutlineColor = (SolidColorBrush) e.NewValue;
        }

        private static void OnValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ratingsControl = (ImRating) d;
            SetupStars(ratingsControl);
            PropagateValueChanged?.Invoke(ratingsControl.Value);
        }

        private static void OnNumberOfStarsChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ratingsControl = (ImRating) d;
            SetupStars(ratingsControl);
        }

        private static void SetupStars(ImRating ratingsControl)
        {
            var localValue = ratingsControl.Value;

            ratingsControl.spStars.Children.Clear();
            for (var i = 0; i < ratingsControl.NumberOfStars; i++)
            {
                var star = new ImStar
                {
                    BackgroundColor = ratingsControl.BackgroundColor,
                    StarForegroundColor = ratingsControl.StarForegroundColor,
                    StarOutlineColor = ratingsControl.StarOutlineColor
                };
                if (localValue > 1)
                    star.Value = 1.0;
                else if (localValue > 0)
                {
                    star.Value = localValue;
                }
                else
                {
                    star.Value = 0.0;
                }

                localValue -= 1.0;
                ratingsControl.spStars.Children.Insert(i, star);
            }
        }

        public void Update()
        {
            SetupStars(this);
        }

        private void spStars_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
        }

        private void spStars_PointerMoved_1(object sender, PointerRoutedEventArgs e)
        {
            var isPressed = e.GetCurrentPoint((UIElement) sender).Properties.IsLeftButtonPressed;
            if (isPressed)
            {
                var p = e.GetCurrentPoint((UIElement) sender);
                var xp = (int) p.Position.X;
                var rv = xp/34.0;
                var roundedRating = Math.Ceiling(2*rv)/2;
                if (roundedRating < 0.0)
                    roundedRating = 0.0;
                if (roundedRating > 10.0)
                    roundedRating = 10.0;

                Value = roundedRating;
            }
        }

        private void spStars_Loaded_1(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}