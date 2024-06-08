using System.Windows.Controls;
using System.Windows.Input;
using StaffAIS.Global;

namespace StaffAIS.View.Controls;

public partial class RecordGrid : UserControl
{
    public RecordGrid()
    {
        InitializeComponent();
        this.PlGrid.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
    }
    private void dataGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                if (!dgr.IsMouseOver)
                {
                    (dgr as DataGridRow).IsSelected = false;
                }
            }
        }
    }
}