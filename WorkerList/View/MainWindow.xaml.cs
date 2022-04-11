using System.Windows;
using System.Windows.Controls;
using NotTastyCupcake.WorkerList.UserInterface.DesktopUI.ViewModel;

namespace NotTastyCupcake.WorkerList.UserInterface.DesktopUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllPersonsView;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new DataManageVM();
            AllPersonsView = ViewAllPerson;
        }
    }
}
