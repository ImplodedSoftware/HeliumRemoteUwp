using System;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeliumRemote.UserControls
{
    public sealed partial class ImRating : UserControl
    {
        public static Action<double> PropagateValueChanged { get; set; }
        public ImRating()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush),
            typeof(ImRating),
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
            ImRating control = (ImRating)d;
            foreach (ImStar star in control.spStars.Children)
                star.BackgroundColor = (SolidColorBrush)e.NewValue;
        }
        /// <summary>
        /// StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(SolidColorBrush),
            typeof(ImRating),
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
            ImRating control = (ImRating)d;
            foreach (ImStar star in control.spStars.Children)
                star.StarForegroundColor = (SolidColorBrush)e.NewValue;

        }
        /// <summary>
        /// StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof(SolidColorBrush),
            typeof(ImRating),
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
            ImRating control = (ImRating)d;
            foreach (ImStar star in control.spStars.Children)
                star.StarOutlineColor = (SolidColorBrush)e.NewValue;

        }

        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double),
            typeof(ImRating),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnValueChanged)));

        /// <summary>
        /// Gets or sets the Value property.  
        /// </summary>
        public double Value
        {
            get
            {
                try
                {
                    return (double)GetValue(ValueProperty);
                }
                catch
                {
                    return -1.0;
                }
            }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        private static void OnValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImRating ratingsControl = (ImRating)d;
            SetupStars(ratingsControl);
            if (PropagateValueChanged != null)
                PropagateValueChanged(ratingsControl.Value);
        }
        /// <summary>
        /// NumberOfStars Dependency Property
        /// </summary>
        public static readonly DependencyProperty NumberOfStarsProperty =
            DependencyProperty.Register("NumberOfStars", typeof(Int32), typeof(ImRating),
                new PropertyMetadata((Int32)5,
                new PropertyChangedCallback(OnNumberOfStarsChanged)));

        /// <summary>
        /// Gets or sets the NumberOfStars property.  
        /// </summary>
        public Int32 NumberOfStars
        {
            get { return (Int32)GetValue(NumberOfStarsProperty); }
            set { SetValue(NumberOfStarsProperty, value); }
        }

        /// <summary>
        /// Handles changes to the NumberOfStars property.
        /// </summary>
        private static void OnNumberOfStarsChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ImRating ratingsControl = (ImRating)d;
            SetupStars(ratingsControl);
        }
        /// <summary>
        /// Sets up stars when Value or NumberOfStars properties change
        /// Will only show up to the number of stars requested (up to Maximum)
        /// so if Value > NumberOfStars * 1, then Value is clipped to maximum
        /// number of full stars
        /// </summary>
        /// <param name="ratingsControl"></param>
        private static void SetupStars(ImRating ratingsControl)
        {
            double localValue = ratingsControl.Value;

            ratingsControl.spStars.Children.Clear();
            for (int i = 0; i < ratingsControl.NumberOfStars; i++)
            {
                ImStar star = new ImStar();
                star.BackgroundColor = ratingsControl.BackgroundColor;
                star.StarForegroundColor = ratingsControl.StarForegroundColor;
                star.StarOutlineColor = ratingsControl.StarOutlineColor;
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
        private void spStars_PointerPressed_1(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            /*
            PointerPoint p = e.GetCurrentPoint((UIElement)sender);
            int xp = (int)p.Position.X;
            double rv = xp / 26.0; //26/2
            double roundedRating = (Math.Ceiling(2 * rv)) / 2;
            //roundedRating += 0.5;
            if (roundedRating < 0.0)
                roundedRating = 0.0;
            if (roundedRating > 10.0)
                roundedRating = 10.0;

            this.Value = roundedRating;
            //rctrl.Value = roundedRating;*/
        }

        private void spStars_PointerMoved_1(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            bool isPressed = e.GetCurrentPoint((UIElement)sender).Properties.IsLeftButtonPressed;
            if (isPressed)
            {

                PointerPoint p = e.GetCurrentPoint((UIElement)sender);
                int xp = (int)p.Position.X;
                double rv = xp / 34.0; //26/2
                double roundedRating = (Math.Ceiling(2 * rv)) / 2;
                //roundedRating += 0.5;
                if (roundedRating < 0.0)
                    roundedRating = 0.0;
                if (roundedRating > 10.0)
                    roundedRating = 10.0;

                this.Value = roundedRating;
            }
        }

        private void spStars_Loaded_1(object sender, RoutedEventArgs e)
        {
            Update();
        }

    }
}
