using System.Windows;
using System.Windows.Input;
using StaffAIS.ViewModel;

namespace StaffAIS.View.Windows;

public partial class EditWorkerDialog : Window
{
    public EditWorkerDialog()
    {
        InitializeComponent();
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
    public EditWorkerDialog(int id)
    {
        InitializeComponent();
        (this.DataContext as EditWorkerVm).LoadData(id);
    }
}