using System.Windows;
using System.Windows.Input;
using StaffAIS.ViewModel;

namespace StaffAIS.View.Windows;

public partial class EditWorkshopDialog : Window
{
    public EditWorkshopDialog()
    {
        InitializeComponent();
    }
    public EditWorkshopDialog(int id)
    {
        InitializeComponent();
        (this.DataContext as EditWorkshopVm).LoadData(id);
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
    }
}