using System.Windows;
using System.Windows.Input;
using StaffAIS.ViewModel;

namespace StaffAIS.View.Windows;

public partial class EditPlanDialog : Window
{
    public EditPlanDialog()
    {
        InitializeComponent();
    }
    public EditPlanDialog(int id)
    {
        InitializeComponent();
        (this.DataContext as EditPlanVm).LoadData(id);
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