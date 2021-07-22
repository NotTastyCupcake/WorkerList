using System.Windows;
using WorkerList.ViewModel;
using WorkerList.Model;
using System.Text.RegularExpressions;
using System;

namespace WorkerList.View
{
    /// <summary>
    /// Логика взаимодействия для DismissalPersonWindow.xaml
    /// </summary>
    public partial class DismissalPersonWindow : Window
    {
        public DismissalPersonWindow(ModelPerson personToDismissal)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            
            DataManageVM.SelectedPerson = personToDismissal;
            DataManageVM.DateOfDismissal = Convert.ToDateTime(personToDismissal.DateOfDismissal);
        }
        private void PreviewDateInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
