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
}