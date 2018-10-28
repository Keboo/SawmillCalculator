using System;
using Xamarin.Forms;

namespace SawmillCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CutList_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CutListPage(), true);
        }

        private async void BoardFeet_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoardFeetPage(), true);
        }

        private async void Settings_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        private async void About_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), true);
        }
    }
}
