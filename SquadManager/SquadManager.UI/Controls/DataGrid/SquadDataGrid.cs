using SquadManager.UI.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SquadManager.UI.Controls.DataGrid
{
    public class SquadDataGrid : System.Windows.Controls.DataGrid
    {
        public SquadDataGrid()
        {
            AutoGenerateColumns = false;
            CanUserAddRows = false;
            CanUserDeleteRows = false;
            CanUserReorderColumns = false;
            CanUserResizeColumns = false;
            CanUserResizeRows = false;
            CanUserSortColumns = false;

            AddHandler(GotFocusEvent, new RoutedEventHandler(GetFocusOnClick));
        }

        private void GetFocusOnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                SquadDataGrid grd = (SquadDataGrid)sender;
                grd.BeginEdit(e);

                Control control = GetFirstChildByType<Control>(e.OriginalSource as DataGridCell);
                if (control != null)
                {
                    control.Focus();
                }
            }
        }
        private T GetFirstChildByType<T>(DependencyObject prop) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(prop); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild((prop), i) as DependencyObject;
                if (child == null)
                    continue;

                T castedProp = child as T;
                if (castedProp != null)
                    return castedProp;

                castedProp = GetFirstChildByType<T>(child);

                if (castedProp != null)
                    return castedProp;
            }
            return null;
        }

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
                    Header = column.Header,
                    HeaderTemplate = (DataTemplate)grid.FindResource(column.HeaderTemplate),
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
