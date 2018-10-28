using System.Collections.Generic;
using Xamarin.Forms;

namespace SawmillCalculator
{
    public static class Settings
    {
        private const string BladeEdgeKey = "BladeEdge";
        private const string KerfKey = "Kerf";
        private const string FlitchKey = "Flitch";
        private const string TotalKey = "Total";
        private const string ThicknessKey = "Thickness";

        private const string BoardFeetWidthKey = "BfWidth";
        private const string BoardFeetThicknessKey = "BfThickness";
        private const string BoardFeetLengthKey = "BfLength";
        private const string BoardFeetQuantityKey = "BfQuantity";

        public static BladeEdge Edge
        {
            get => GetValue<BladeEdge>(BladeEdgeKey);
            set => Application.Current.Properties[BladeEdgeKey] = value;
        }

        public static int? Kerf
        {
            get => GetValue<int?>(KerfKey);
            set => Application.Current.Properties[KerfKey] = value;
        }

        public static int? Flitch
        {
            get => GetValue<int?>(FlitchKey);
            set => Application.Current.Properties[FlitchKey] = value;
        }

        public static int? Total
        {
            get => GetValue<int?>(TotalKey);
            set => Application.Current.Properties[TotalKey] = value;
        }

        public static int? Thickness
        {
            get => GetValue<int?>(ThicknessKey);
            set => Application.Current.Properties[ThicknessKey] = value;
        }

        public static int? BoardFeetWidth
        {
            get => GetValue<int?>(BoardFeetWidthKey);
            set => Application.Current.Properties[BoardFeetWidthKey] = value;
        }

        public static int? BoardFeetThickness
        {
            get => GetValue<int?>(BoardFeetThicknessKey);
            set => Application.Current.Properties[BoardFeetThicknessKey] = value;
        }

        public static int? BoardFeetLength
        {
            get => GetValue<int?>(BoardFeetLengthKey);
            set => Application.Current.Properties[BoardFeetLengthKey] = value;
        }

        public static int? BoardFeetQuantity
        {
            get => GetValue<int?>(BoardFeetQuantityKey);
            set => Application.Current.Properties[BoardFeetQuantityKey] = value;
        }

        private static T GetValue<T>(string key, T defaultValue = default(T))
        {
            IDictionary<string, object> properties = Application.Current.Properties;
            if (properties.TryGetValue(key, out object objectValue) &&
                objectValue is T value)
            {
                return value;
            }
            return defaultValue;
        }
    }

    public enum BladeEdge
    {
        BottomOrRight,
        TopOrLeft
    }
}