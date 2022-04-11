using System.Windows;

namespace NotTastyCupcake.WorkerList.UserInterface.DesktopUI.View
{
    /// <summary>
    /// Логика взаимодействия для MessangeWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string text)
        {
            InitializeComponent();
            MessageText.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
