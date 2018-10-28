using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SawmillCalculator
{
    public class BoardFeetViewModel : INotifyPropertyChanged
    {
        private int _Thickness;
        public int Thickness
        {
            get => _Thickness;
            set
            {
                if (Set(ref _Thickness, value))
                {
                    Settings.BoardFeetThickness = value;
                    RaisePropertyChanged(nameof(Total));
                }
            }
        }

        private int _Width;
        public int Width
        {
            get => _Width;
            set
            {
                if (Set(ref _Width, value))
                {
                    Settings.BoardFeetWidth = value;
                    RaisePropertyChanged(nameof(Total));
                }
            }
        }

        private int _Length;
        public int Length
        {
            get => _Length;
            set
            {
                if (Set(ref _Length, value))
                {
                    Settings.BoardFeetLength = value;
                    RaisePropertyChanged(nameof(Total));
                }
            }
        }

        private int _Quantity;
        public int Quantity
        {
            get => _Quantity;
            set
            {
                if (Set(ref _Quantity, value))
                {
                    Settings.BoardFeetQuantity = value;
                    RaisePropertyChanged(nameof(Total));
                }
            }
        }

        public int Total => 
            (int)Math.Round(Width * Thickness * Length * Quantity / 12.0);

        public BoardFeetViewModel()
        {
            Thickness = Settings.BoardFeetThickness ?? 2; //Inches
            Width = Settings.BoardFeetWidth ?? 6; //Inches
            Length = Settings.BoardFeetLength ?? 16; //Feet
            Quantity = Settings.BoardFeetQuantity ?? 1;
        }

        private bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        private void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}