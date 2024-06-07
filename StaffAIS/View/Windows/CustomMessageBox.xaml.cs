using System.Windows;
using System.Windows.Input;

namespace StaffAIS.View.Windows;

public partial class CustomMessageBox : Window
{
    public CustomMessageBox()
    {
        InitializeComponent();
    }
    public static bool? Show(String message)
    {
        // NOTE: Message and Image are fields created in the XAML markup
        CustomMessageBox msgBox = new CustomMessageBox();
        msgBox.Message.Text = message;
        return msgBox.ShowDialog();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}