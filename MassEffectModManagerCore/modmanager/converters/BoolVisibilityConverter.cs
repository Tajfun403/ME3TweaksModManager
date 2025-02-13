﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MassEffectModManagerCore.modmanager.converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string str && (str == "Inverse" || str == "Not"))
            {
                return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
            }
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {    // Don't need any convert back
            return null;
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToHiddenVisibleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {    // Don't need any convert back
            return null;
        }
    }

}
