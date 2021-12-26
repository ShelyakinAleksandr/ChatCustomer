using System;
using System.Windows.Data;
using System.Globalization;
using ChatCustomer.Model;

namespace ChatCustomer.Comm
{
    class ConverterCommandParametrs : IMultiValueConverter
    {
        public object Convert(object[] value, Type targettype, object parametr, CultureInfo culture)
        {
            FilterMassege filterMassege = new FilterMassege();

            filterMassege.Filter = System.Convert.ToBoolean(value[0]);
            filterMassege.DateStart = System.Convert.ToDateTime(value[1]);
            filterMassege.DateEnd = System.Convert.ToDateTime(value[2]);
            return filterMassege;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
