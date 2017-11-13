using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SquadManager.UI.Controls.DataGrid
{
    public class SquadDataGrid : System.Windows.Controls.DataGrid
    {
        public List<ColumnViewModel> ColumnBindings
        {
            get { return (List<ColumnViewModel>)GetValue(ColumnBindingsProperty); }
            set { SetValue(ColumnBindingsProperty, value); }
        }

        public static readonly DependencyProperty ColumnBindingsProperty =
            DependencyProperty.Register("ColumnBindings",
                typeof(List<ColumnViewModel>), typeof(SquadDataGrid),
                new PropertyMetadata(null, ColumnBindingsPropertyChanged));

        private static void ColumnBindingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = (SquadDataGrid)d;
            if (grid == null) return;

            var columns = (List<ColumnViewModel>)grid.GetValue(ColumnBindingsProperty);
            if (columns == null || columns.Count == 0) return;

            foreach (var column in columns)
            {
                var gridColumn = new SquadDataGridColumn()
                {
                    CellTemplate = (DataTemplate)grid.FindResource(column.Template),
                    DataContext = column.DataContextPath,
                };

                if (column.EditingTemplate != null)
                {
                    gridColumn.CellEditingTemplate = (DataTemplate)grid.FindResource(column.EditingTemplate);
                }

                grid.Columns.Add(gridColumn);
            }
        }
    }
}
