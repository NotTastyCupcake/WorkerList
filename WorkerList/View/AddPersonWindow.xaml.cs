using System.Text.RegularExpressions;
using System.Windows;
using WorkerList.ViewModel;

namespace WorkerList.View
{
    /// <summary>
    /// Логика взаимодействия для AddPerson.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
        private void PreviewTextBoxInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[a-zA-Z0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PreviewNumInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[a-zA-Zа-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PreviewDateInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
