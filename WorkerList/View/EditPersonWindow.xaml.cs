
using System.Text.RegularExpressions;
using System.Windows;
using WorkerList.ViewModel;

namespace WorkerList.View
{
    /// <summary>
    /// Логика взаимодействия для EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public EditPersonWindow(Model.ModelPerson personToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPerson = personToEdit;
            DataManageVM.SDateOfDismissal = personToEdit.DateOfDismissal;
        }
    }
}
