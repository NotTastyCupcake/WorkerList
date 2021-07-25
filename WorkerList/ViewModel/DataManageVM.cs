using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WorkerList.Data;
using WorkerList.Model;
using WorkerList.View;

namespace WorkerList.ViewModel
{
    public class DataManageVM : DependencyObject, INotifyPropertyChanged
    {
        #region Все данные

        private ICollectionView allPeople = CollectionViewSource.GetDefaultView(DataWorker.GetAllPerson());

        public ICollectionView AllPeople
        {
            get { return allPeople; }
            set { allPeople = value; NotifyPropertyChanged("GetPeople"); }
        }
        

        //Свойстра персоны
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string MiddleName { get; set; }
        public static string Position { get; set; }
        public static decimal Salary { get; set; }
        public static DateTime? EmploymentDate { get; set; }
        public static DateTime? DateOfDismissal { get; set; }

        //Свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static ModelPerson SelectedPerson { get; set; }


        #endregion

        #region Команды добавления
        private RelayCommand addNewPerson;
        public RelayCommand AddNewPerson
        {
            get
            {
                return addNewPerson ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool corFirstName = FirstName == null || FirstName.Replace(" ", "").Length == 0;
                    bool corLastName = LastName == null || LastName.Replace(" ", "").Length == 0;
                    bool corMiddleName = MiddleName == null || MiddleName.Replace(" ", "").Length == 0;
                    bool corPosition = Position == null || Position.Replace(" ", "").Length == 0;
                    bool corSalary = Salary == 0;
                    bool corEmploymentDate = EmploymentDate == null;

                    if (corFirstName || corLastName || corMiddleName || corPosition|| corSalary || corEmploymentDate)
                    {
                        if (corFirstName)
                        {
                            SetRedBlockControll(wnd, "FirstNameBlock");
                        }
                        if (corLastName)
                        {
                            SetRedBlockControll(wnd, "LastNameBlock");
                        }
                        if (corMiddleName)
                        {
                            SetRedBlockControll(wnd, "MiddleNameBlock");
                        }
                        if (corPosition)
                        {
                            SetRedBlockControll(wnd, "PositionBlock");
                        }
                        if (corSalary)
                        {
                            SetRedBlockControll(wnd, "SalaryBlock");
                        }
                        if (corEmploymentDate)
                        {
                            SetRedBlockControll(wnd, "EmploymentDateBlock");
                        }
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePerson(FirstName, LastName, MiddleName, Position, Salary, EmploymentDate);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        #endregion

        private RelayCommand uploadingBrowse;
        public RelayCommand UploadingBrowse
        {
            get
            {
                return uploadingBrowse ?? new RelayCommand(obj =>
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Расширяемый язык разметки(*.xml)| *.xml";
                    if (saveFile.ShowDialog() == true)
                    {
                        string result = DataWorker.CreatFileUnloadingData(saveFile.FileName);
                        ShowMessageToUser(result);
                        SetNullValuesToProperties();
                    }
                }
                );
            }
        }

        #region Команды открытия окон
        private RelayCommand openAddNewPersonWnd;
        public RelayCommand OpenAddNewPersonWnd
        {
            get
            {
                return openAddNewPersonWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPersonWindowMethod();
                }
                    );
            }
        }

        private RelayCommand openDismissalItemWnd;
        public RelayCommand OpenDismissalItemWnd
        {
            get
            {
                return openDismissalItemWnd ?? new RelayCommand(obj =>
                {
                    //если сотрудник
                    if (SelectedTabItem.Name == "PersonsTab" && SelectedPerson != null)
                    {
                        OpenDismissalPersonWindowMethod(SelectedPerson);
                    }
                }
                    );
            }
        }

        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    //если сотрудник
                    if (SelectedTabItem.Name == "PersonsTab" && SelectedPerson != null)
                    {
                        OpenEditPersonWindowMethod(SelectedPerson);
                    }
                }
                    );
            }
        }


        #endregion

        #region Команды изминения
        private RelayCommand dismissalPerson;
        public RelayCommand DismissalPerson
        {
            get
            {
                return dismissalPerson ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    if (SelectedPerson != null)
                    {

                        resultStr = DataWorker.DismissalPerson(SelectedPerson,DateOfDismissal);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    if (SelectedPerson != null)
                    {

                        resultStr = DataWorker.EditPerson(SelectedPerson, FirstName, LastName, MiddleName, Position,Salary);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        #endregion

        #region Метода открытия окна

        /// <summary>
        /// Открытие окна добавления сотрудника
        /// </summary>
        private void OpenAddPersonWindowMethod()
        {
            AddPersonWindow newPersonWindow = new AddPersonWindow();
            SetCenterPositionAndOpen(newPersonWindow);
        }

        /// <summary>
        /// Открытие окна добавления даты увольнения сотрудника
        /// </summary>
        private void OpenDismissalPersonWindowMethod(ModelPerson person)
        {
            DismissalPersonWindow dismissalPersonWindow = new DismissalPersonWindow(person);
            SetCenterPositionAndOpen(dismissalPersonWindow);
        }


        private void OpenEditPersonWindowMethod(ModelPerson person)
        {
            EditPersonWindow dismissalPersonWindow = new EditPersonWindow(person);
            SetCenterPositionAndOpen(dismissalPersonWindow);
        }


        /// <summary>
        /// Отображение модального окна поверх других, по центру 
        /// </summary>
        /// <param name="window">Любое окно</param>
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }



        #endregion

        #region Обновление

        private void SetNullValuesToProperties()
        {
            //для пользователя
            FirstName = null;
            LastName = null;
            MiddleName = null;
            Position = null;
            Salary = 0;
            DateOfDismissal = null;
            EmploymentDate = null;
        }

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void UpdateAllDataView()
        {
            UpdateAllPersonsView();
        }

        private void UpdateAllPersonsView()
        {
            AllPeople = CollectionViewSource.GetDefaultView(DataWorker.GetAllPerson());
            MainWindow.AllPersonsView.ItemsSource = null;
            MainWindow.AllPersonsView.Items.Clear();
            MainWindow.AllPersonsView.ItemsSource = AllPeople;
            MainWindow.AllPersonsView.Items.Refresh();
        }
        #endregion

        #region Статистика
        //Средняя зарплата
        public dynamic AverageSalary { get; } = DataWorker.GetAllPerson().Average(person => person.Salary);
        //Количество сотрудников
        public int CountPerson { get; } = Data.DataWorker.GetAllPerson().Count();
        #endregion

        #region Фильтрация
        //Поле для фильтрации
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(DataManageVM), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var currend = d as DataManageVM;
            if (currend != null)
            {
                currend.AllPeople.Filter = null;
                currend.AllPeople.Filter = currend.FilterPerson;
            }
        }

        public DataManageVM()
        {
            AllPeople.Filter = FilterPerson;
        }

        private bool FilterPerson(object obj)
        {
            bool result = true;
            ModelPerson person = obj as ModelPerson;
            if (!string.IsNullOrWhiteSpace(FilterText) && person != null && !person.FirstName.Contains(FilterText) && !person.LastName.Contains(FilterText))
            {
                result = false;
            }
            return result;
        } 
        #endregion

        #region Доп. методы
        private void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
