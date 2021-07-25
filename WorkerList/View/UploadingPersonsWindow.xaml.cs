using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkerList.ViewModel;

namespace WorkerList.View
{
    /// <summary>
    /// Логика взаимодействия для UploadingPersonsWindow.xaml
    /// </summary>
    public partial class UploadingPersonsWindow : Window
    {
        public UploadingPersonsWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            UnloadingAddresTextBox.Text = DataManageVM.UnloadingAddres;
        }
    }
}
