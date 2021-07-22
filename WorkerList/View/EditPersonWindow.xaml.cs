using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
