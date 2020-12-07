using System;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using YN.XFCounterLabelControl.Converters;

namespace YN.XFCounterLabelControl
{
    public enum CounterTypeEnum
    {
        Integer,
        Double,
        Currency
    }

    public class CounterLabelControl : Label
    {
        public CounterLabelControl()
        {

        }

        public static readonly BindableProperty CounterTypeProperty = BindableProperty.Create(
            nameof(CounterType),
            typeof(CounterTypeEnum),
            typeof(CounterLabelControl),
            CounterTypeEnum.Integer);

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
            typeof(int),
            typeof(CounterLabelControl),
            0);

        /// <summary>
        /// The start value for the animation.
        /// </summary>
        public int StartValue
        {
            get { return (int)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly BindableProperty TargetValueProperty = BindableProperty.Create(
            nameof(TargetValue),
            typeof(int),
            typeof(CounterLabelControl),
            0,
            propertyChanged: OnTargetValuePropertyChanged);

        /// <summary>
        /// The target value for the animation.
        /// </summary>
        public int TargetValue
        {
            get { return (int)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }

        private static void OnTargetValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CounterLabelControl)
            {
                var ctrl = (CounterLabelControl)bindable;
                Animation animation = null;

                if (ctrl.CounterType == CounterTypeEnum.Double)
                {
                    animation = new Animation(v => ctrl.Text = v.ToString(), ctrl.StartValue, ctrl.TargetValue);
                    animation.Commit(ctrl, "CounterAnimation", 16, ctrl.AnimationDuration, Easing.Linear, (v, c) => ctrl.Text = ctrl.TargetValue.ToString(), () => false);
                }
                else if (ctrl.CounterType == CounterTypeEnum.Currency)
                {
                    CurrencyConverter currencyConverter = new CurrencyConverter();
                    animation = new Animation(v => ctrl.Text = currencyConverter.Convert(v, typeof(double), null, CultureInfo.CurrentCulture).ToString(), ctrl.StartValue, ctrl.TargetValue);
                    animation.Commit(ctrl, "CounterAnimation", 16, ctrl.AnimationDuration, Easing.Linear, (v, c) => ctrl.Text = currencyConverter.Convert(ctrl.TargetValue, typeof(double), null, CultureInfo.CurrentCulture).ToString(), () => false);
                }
                else
                {
                    animation = new Animation(v => ctrl.Text = ((int)v).ToString(), ctrl.StartValue, ctrl.TargetValue);
                    animation.Commit(ctrl, "CounterAnimation", 16, ctrl.AnimationDuration, Easing.Linear, (v, c) => ctrl.Text = ctrl.TargetValue.ToString(), () => false);
                }
            }
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
    }
}
