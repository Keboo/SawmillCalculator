using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace SawmillCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void EmailTapped(object sender, EventArgs e)
        {
            await Email.ComposeAsync("Sawmill App", "I have found an issue", "grant@micapeak.net");
        }
    }
}