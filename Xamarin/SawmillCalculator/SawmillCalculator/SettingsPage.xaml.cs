using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SawmillCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BladeSideCell.On = Settings.Edge == BladeEdge.BottomOrRight;
            
        }

        private void BladeSideCell_OnOnChanged(object sender, ToggledEventArgs e)
        {
            Settings.Edge = BladeSideCell.On ? BladeEdge.BottomOrRight : BladeEdge.TopOrLeft;
        }
    }

    public class BladeEdgeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Bottom or Right" : "Top or Left";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}