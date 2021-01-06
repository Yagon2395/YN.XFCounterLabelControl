using System.Globalization;
using Xamarin.Forms;
using YN.XFCounterLabelControl.Converters;

namespace YN.XFCounterLabelControl
{   
    public class CounterLabelControl : Label
    {
        public CounterLabelControl() { }

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
            typeof(int?),
            typeof(CounterLabelControl),
            null,
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
                
                
                if(ctrl.StartValue == ctrl.TargetValue)
                {
                    ctrl.HandleTargetEqualsToStart();
                }
                else
                {
                    ctrl.HandleAnimation();
                }                
            }
        } 
        
        private void HandleTargetEqualsToStart()
        {
            if (this.CounterType == CounterTypeEnum.Double)
            {
                this.Text = ((double)this.TargetValue).ToString();
            }
            else if (this.CounterType == CounterTypeEnum.Currency)
            {
                CurrencyConverter currencyConverter = new CurrencyConverter();
                this.Text = currencyConverter.Convert(this.TargetValue, typeof(double), null, CultureInfo.CurrentCulture).ToString();
            }
            else
            {
                this.Text = this.TargetValue.ToString();
            }
        }
        
        private void HandleAnimation()
        {
            Animation animation = null;

            if (this.CounterType == CounterTypeEnum.Double)
            {
                animation = new Animation(v => this.Text = v.ToString(), this.StartValue, this.TargetValue);
                animation.Commit(this, "CounterAnimation", 16, this.AnimationDuration, Easing.Linear, (v, c) => this.Text = this.TargetValue.ToString(), () => false);
            }
            else if (this.CounterType == CounterTypeEnum.Currency)
            {
                CurrencyConverter currencyConverter = new CurrencyConverter();
                animation = new Animation(v => this.Text = currencyConverter.Convert(v, typeof(double), null, CultureInfo.CurrentCulture).ToString(), this.StartValue, this.TargetValue);
                animation.Commit(this, "CounterAnimation", 16, this.AnimationDuration, Easing.Linear, (v, c) => this.Text = currencyConverter.Convert(this.TargetValue, typeof(double), null, CultureInfo.CurrentCulture).ToString(), () => false);
            }
            else
            {
                animation = new Animation(v => this.Text = ((int)v).ToString(), this.StartValue, this.TargetValue);
                animation.Commit(this, "CounterAnimation", 16, this.AnimationDuration, Easing.Linear, (v, c) => this.Text = this.TargetValue.ToString(), () => false);
            }
        }        
    }
}
