using System;
using System.Globalization;
using Xamarin.Forms;
using YN.XFCounterLabelControl.Converters;

namespace YN.XFCounterLabelControl
{
    public class CounterLabelControl : Label
    {
        public CounterLabelControl()
        {
            this.Text = GetFormatedText(this.StartValue, this.CounterType);
        }

        public static readonly BindableProperty AnimationDurationProperty = BindableProperty.Create(
            nameof(AnimationDuration),
            typeof(uint),
            typeof(CounterLabelControl),
            (uint)2000);

        /// <summary>
        /// The time duration for the animation.
        /// </summary>
        public uint AnimationDuration
        {
            get { return (uint)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public static readonly BindableProperty CounterTypeProperty = BindableProperty.Create(
            nameof(CounterType),
            typeof(CounterTypeEnum),
            typeof(CounterLabelControl),
            CounterTypeEnum.Integer,
            propertyChanged: OnCounterTypePropertyChanged);

        private static void OnCounterTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CounterLabelControl)
            {
                var ctrl = (CounterLabelControl)bindable;

                ctrl.Text = ctrl.GetFormatedText(ctrl.StartValue, ctrl.CounterType);
            }
        }        

        /// <summary>
        /// Type of the data.
        /// </summary>
        public CounterTypeEnum CounterType
        {
            get { return (CounterTypeEnum)GetValue(CounterTypeProperty); }
            set { SetValue(CounterTypeProperty, value); }
        }

        public static readonly BindableProperty StartValueProperty = BindableProperty.Create(
            nameof(StartValue),
            typeof(double),
            typeof(CounterLabelControl),
            default(double));

        /// <summary>
        /// The start value for the animation.
        /// </summary>
        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly BindableProperty TargetValueProperty = BindableProperty.Create(
            nameof(TargetValue),
            typeof(double),
            typeof(CounterLabelControl),
            default(double),
            propertyChanged: OnTargetValuePropertyChanged);

        /// <summary>
        /// The target value for the animation.
        /// </summary>
        public double TargetValue
        {
            get { return (double)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        private static void OnTargetValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CounterLabelControl)
            {
                var ctrl = (CounterLabelControl)bindable;


                if (ctrl.StartValue == ctrl.TargetValue)
                {
                    ctrl.Text = ctrl.GetFormatedText(ctrl.TargetValue, ctrl.CounterType);
                }
                else
                {
                    ctrl.HandleAnimation();
                }
            }
        }

        /// <summary>
        /// Format string based on CounterType.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetFormatedText(double value, CounterTypeEnum type)
        {
            string text = string.Empty;
            if (type == CounterTypeEnum.Double)
            {
                text = value.ToString("F2");
            }
            else if (type == CounterTypeEnum.Currency)
            {
                CurrencyConverter currencyConverter = new CurrencyConverter();
                text = currencyConverter.Convert(value, typeof(double), null, CultureInfo.CurrentCulture).ToString();
            }
            else
            {
                text = ((int)value).ToString();
            }

            return text;
        }

        /// <summary>
        /// Here's where the magic happens.
        /// </summary>
        private void HandleAnimation()
        {
            try
            {
                Action<double> callbackAction = delegate (double v)
                {
                    this.Text = this.GetFormatedText(v, this.CounterType);
                };

                Action<double, bool> finishedAction = delegate (double value, bool canceled)
                {
                    this.Text = this.GetFormatedText(this.TargetValue, this.CounterType);
                };

                Animation animation = new Animation(callbackAction, this.StartValue, this.TargetValue);
                animation.Commit(this, "CounterAnimation", 16, this.AnimationDuration, Easing.Linear, finishedAction, () => false);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
