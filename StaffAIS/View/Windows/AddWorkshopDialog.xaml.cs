using System.Windows;
using System.Windows.Input;

namespace StaffAIS.View.Windows;

public partial class AddWorkshopDialog : Window
{
    public AddWorkshopDialog()
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
    
}