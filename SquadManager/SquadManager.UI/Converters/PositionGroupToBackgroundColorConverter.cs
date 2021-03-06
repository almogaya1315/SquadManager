﻿using SquadManager.UI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SquadManager.UI.Converters
{
    public class PositionGroupToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var positionRole = (PositionRole)value;

                switch (positionRole)
                {
                    case PositionRole.GK: return (SolidColorBrush)new BrushConverter().ConvertFrom("#ffcccc");
                    case PositionRole.RB:
                    case PositionRole.RWB:
                    case PositionRole.CB:
                    case PositionRole.LWB:
                    case PositionRole.LB: return (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffb3");
                    case PositionRole.RDM:
                    case PositionRole.CDM:
                    case PositionRole.LDM:
                    case PositionRole.LM:
                    case PositionRole.LWM:
                    case PositionRole.CM:
                    case PositionRole.RWM:
                    case PositionRole.RM:
                    case PositionRole.RCAM:
                    case PositionRole.CAM:
                    case PositionRole.LCAM: return (SolidColorBrush)new BrushConverter().ConvertFrom("#9fdfbf");
                    case PositionRole.RW:
                    case PositionRole.LW:
                    case PositionRole.RF:
                    case PositionRole.CF:
                    case PositionRole.LF:
                    case PositionRole.RS:
                    case PositionRole.ST:
                    case PositionRole.LS: return (SolidColorBrush)new BrushConverter().ConvertFrom("#99b3ff");
                }
            }

            return (SolidColorBrush)new BrushConverter().ConvertFrom("#e7f5f7"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
