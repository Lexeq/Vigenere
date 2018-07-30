using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VigenereBreaker.Views
{
    public partial class NumericUpDown : UserControl
    {
        #region Properties

        #region Step

        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1.0));
        #endregion

        #region MinValue

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0.0, null, CoerceMinValue));

        private static object CoerceMinValue(DependencyObject d, object value)
        {
            var obj = (NumericUpDown)d;
            var min = (double)value;

            if (min > obj.MaxValue)
                return obj.MaxValue;

            else return value;
        }
        #endregion

        #region MaxValue

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(NumericUpDown), new PropertyMetadata(100.0, null, CoerceMaxValue));

        private static object CoerceMaxValue(DependencyObject d, object value)
        {
            var obj = (NumericUpDown)d;
            var max = (double)value;

            if (max < obj.MinValue)
                return obj.MinValue;
            else return value;
        }
        #endregion

        #region Value

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(NumericUpDown),
            new PropertyMetadata(0.0, ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericUpDown)?.SyncValueAndText(false);
        }

        #endregion

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NumericUpDown), new PropertyMetadata("0", OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericUpDown)?.SyncValueAndText(true);
        }

        #endregion
        #endregion

        #region Ctor

        public NumericUpDown()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Increase();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Decrease();
        }

        private void txtNum_LostFocus(object sender, RoutedEventArgs e)
        {
            SyncValueAndText(false);
        }

        #endregion

        #region Overrides

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            SyncValueAndText(false);
        }
        #endregion

        #region Methods

        private void Increase()
        {
            Value = CoerceMinMax(Value + Step);
        }

        private void Decrease()
        {
            Value = CoerceMinMax(Value - Step);
        }

        private double CoerceMinMax(double value)
        {
            return Math.Min(MaxValue, Math.Max(MinValue, value));
        }

        private void UpdateButtons()
        {
            btnUp.IsEnabled = Value < MaxValue;
            btnDown.IsEnabled = Value > MinValue;
        }

        private void SyncValueAndText(bool fromText)
        {
            if (Text == Value.ToString())
                return;

            if (fromText)
            {
                var parsed = double.TryParse(Text, out double newValue);
                if (parsed && ValidateValueRange(newValue))
                    Value = newValue;
            }

            else
            {
                Text = Value.ToString();
            }

            UpdateButtons();
        }

        private bool ValidateValueRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }
        #endregion
    }
}
