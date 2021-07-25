using System.Text.RegularExpressions;
using System.Windows;
using WorkerList.Model;
using WorkerList.ViewModel;

namespace WorkerList.View
{
    /// <summary>
    /// Логика взаимодействия для EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public EditPersonWindow(ModelPerson personToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();

            DataManageVM.SelectedPerson = personToEdit;
            DataManageVM.FirstName = personToEdit.FirstName;
            DataManageVM.LastName = personToEdit.LastName;
            DataManageVM.MiddleName = personToEdit.MiddleName;
            DataManageVM.Position = personToEdit.Position;
            DataManageVM.Salary = personToEdit.Salary;
        }

        #region Коррекция ввода
        /// <summary>
        /// Запрет на ввод англоязычных букв
        /// </summary>
        private void PreviewTextBoxInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[a-zA-Z0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Запрет на ввод любых букв
        /// </summary>
        private void PreviewNumInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[a-zA-Zа-яА-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
