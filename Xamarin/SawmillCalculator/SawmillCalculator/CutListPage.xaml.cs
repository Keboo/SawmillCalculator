using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms.Xaml;

namespace SawmillCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CutListPage
    {
        public ObservableCollection<object> CutList { get; } = new ObservableCollection<object>();

        public CutListPage()
        {
            InitializeComponent();

            //CutListItemsControl.ItemsSource = CutList;

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

        private int Kerf => Settings.Kerf ?? 0;

        private int Flitch => Settings.Flitch ?? 0;

        private int Thickness => (Settings.Thickness ?? 9) + 4;

        private int Total => (Settings.Total ?? 5) + 1;

        private void Calculate()
        {
            CutList.Clear();

            var index = 1;

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

                CutList.Add(textFrom32nd(currentCut, index).Item1);
                if (Settings.Edge == BladeEdge.BottomOrRight)
                {
                    currentCut += Kerf;
                }

                index++;
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
                    CutList.Add(textFrom32nd(currentCut, index).Item1);
                    index++;
                }

                if (Settings.Edge == BladeEdge.BottomOrRight)
                {
                    currentCut += Kerf;
                }
            }

            //this.cutList.reverse();
        }

        private static (string, string, bool) textFrom32nd(int x, int index)
        {
            var inches = Math.Floor(x / 32m);
            var fraction = x - inches * 32;
            var ftext = "";
            if (fraction > 0)
            {
                ftext = GetFraction((int)fraction, 32);
            }

            string result;
            if (inches > 0 && ftext != "")
            {
                result = inches + "-" + ftext + '"';
            }
            else if (inches > 0 && ftext == "")
            {
                result = inches.ToString(CultureInfo.CurrentCulture) + '"';
            }
            else
            {
                result = ftext + '"';
            }

            return (measurement: result, id: "Index" + index, @checked: false);

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
    }
}