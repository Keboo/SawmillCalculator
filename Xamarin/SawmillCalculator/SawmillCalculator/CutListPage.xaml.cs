using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SawmillCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CutListPage
    {
        private ObservableCollection<string> CutList { get; } = new ObservableCollection<string>();

        private int Kerf => Settings.Kerf ?? 0;

        private int Flitch => Settings.Flitch ?? 0;

        private int Thickness => (Settings.Thickness ?? 9) + 4;

        private int Total => (Settings.Total ?? 5) + 1;

        public CutListPage()
        {
            InitializeComponent();

            CutListItemsControl.ItemsSource = CutList;

            KerfPicker.SelectedIndexChanged += KerfPickerOnSelectedIndexChanged;
            FlitchPicker.SelectedIndexChanged += FlitchPickerOnSelectedIndexChanged;
            ThicknessPicker.SelectedIndexChanged += ThicknessPickerOnSelectedIndexChanged;
            TotalPicker.SelectedIndexChanged += TotalPickerOnSelectedIndexChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            KerfPicker.SelectedIndex = Settings.Kerf ?? 0;
            TotalPicker.SelectedIndex = Settings.Total ?? 5;
            FlitchPicker.SelectedIndex = Settings.Flitch ?? 0;
            ThicknessPicker.SelectedIndex = Settings.Thickness ?? 9;
        }

        private void TotalPickerOnSelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Total = TotalPicker.SelectedIndex;
            Calculate();
        }

        private void ThicknessPickerOnSelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Thickness = ThicknessPicker.SelectedIndex;
            Calculate();
        }

        private void FlitchPickerOnSelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Flitch = FlitchPicker.SelectedIndex;
            Calculate();
        }

        private void KerfPickerOnSelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Kerf = KerfPicker.SelectedIndex;
            Calculate();
        }

        private void Calculate()
        {
            CutList.Clear();

            // Calculate everything in 32nds of an inch.
            // Add the starting spot.
            var currentCut = Flitch * 16;
            // Add the flitch cut if necessary
            if (currentCut > 0)
            {
                if (Settings.Edge == BladeEdge.TopOrLeft)
                {
                    currentCut += Kerf;
                }

                CutList.Add(TextFrom32nd(currentCut));
                if (Settings.Edge == BladeEdge.BottomOrRight)
                {
                    currentCut += Kerf;
                }
            }

            // Loop and figure out all the cuts.
            while (currentCut <= Total * 32)
            {
                currentCut += Thickness * 4;
                if (Settings.Edge == BladeEdge.TopOrLeft)
                {
                    currentCut += Kerf;
                }

                if (currentCut > 0)
                {
                    CutList.Add(TextFrom32nd(currentCut));
                }

                if (Settings.Edge == BladeEdge.BottomOrRight)
                {
                    currentCut += Kerf;
                }
            }
        }

        private static string TextFrom32nd(int x)
        {
            decimal inches = Math.Floor(x / 32m);
            decimal fraction = x - inches * 32;
            
            var fractionText = "";
            if (fraction > 0)
            {
                fractionText = GetFraction((int)fraction, 32);
            }

            string result;
            if (inches > 0 && fractionText != "")
            {
                result = inches + "-" + fractionText + '"';
            }
            else if (inches > 0 && fractionText == "")
            {
                result = inches.ToString(CultureInfo.CurrentCulture) + '"';
            }
            else
            {
                result = fractionText + '"';
            }

            return result;

            string GetFraction(int numerator, int denominator)
            {
                int gcd = GCD(numerator, denominator);

                return $"{numerator / gcd}/{denominator / gcd}";

                int GCD(int a, int b)
                {
                    while (b > 0)
                    {
                        int rem = a % b;
                        a = b;
                        b = rem;
                    }
                    return a;
                }
            }
        }

        private void Cell_OnTapped(object sender, EventArgs e)
        {
            var switchCell = (SwitchCell) sender;
            switchCell.On = !switchCell.On;
        }
    }
}