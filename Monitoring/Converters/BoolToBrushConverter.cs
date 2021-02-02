using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Monitoring.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush PositiveBrush { get; set; } = new SolidColorBrush(Color.FromRgb(0, 180, 0));
        public Brush NegativeBrush { get; set; } = new SolidColorBrush(Color.FromRgb(180, 0, 0));
        private Brush bugBrush = Brushes.Black;
        // Appelée lorsque la donnée métier est  requise par l'IHM = lorsque le binding Source->Target se réalise
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush result = bugBrush;
            try
            {
                bool isPositive = (bool)value;
                result = isPositive ? PositiveBrush : NegativeBrush;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Wrong binding");
            }
            return result;
        }

        // Très rarement utile
        // Appelée lorsque l'IHM est  éditée et met à jour le métier = lorsque le binding Target->Source se réalise
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
