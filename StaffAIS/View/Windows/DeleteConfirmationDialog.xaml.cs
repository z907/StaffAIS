using System.Windows;
using System.Windows.Input;

namespace StaffAIS.View.Windows;

public partial class DeleteConfirmationDialog : Window
{
    public DeleteConfirmationDialog()
    {
        InitializeComponent();
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}