using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PhoneMediApp.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;

            return String.Format("{0:d}", date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // For "display only", I don't bother implementing ConvertBack
            // HOWEVER if your planning on two-way binding, then you must.
            throw new NotImplementedException();
        }
    }
}
