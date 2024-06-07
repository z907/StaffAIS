using System.Windows.Controls;
using System.Windows.Input;
using StaffAIS.Global;

namespace StaffAIS.Controls;

public partial class UserGrid : UserControl
{
    public UserGrid()
    {
        InitializeComponent();
        this.UsrGrid.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
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


