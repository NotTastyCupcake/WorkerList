using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorkerList.Data;
using WorkerList.Model;
using WorkerList.View;

namespace WorkerList.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {

        #region Все данные
        private List<ModelPerson> allPeople = DataWorker.GetAllPerson();

        public List<ModelPerson> AllPeople
        {
            get { return allPeople; }
            set { allPeople = value; NotifyPropertyChanged("GetPeople"); }
        }

        //Свойстра персоны
        public static string SFirstName { get; set; }
        public static string SLastName { get; set; }
        public static string SMiddleName { get; set; }
        public static string SPosition { get; set; }
        public static decimal SSalary { get; set; }
        public static DateTime SEmploymentDate { get; set; }
        public static DateTime? SDateOfDismissal { get; set; }
        public static ModelPerson Person { get; set; }

        //Свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static ModelPerson SelectedPerson { get; set; }

        #endregion

        #region COMMANDS TO ADD
        private RelayCommand addNewPerson;
        public RelayCommand AddNewPerson
        {
            get
            {
                return addNewPerson ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (SFirstName == null || SFirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstNameBlock");
                    }
                    if (SLastName == null || SLastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "LastNameBlock");
                    }
                    if (SMiddleName == null || SMiddleName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "MiddleNameBlock");
                    }
                    if (SPosition == null || SPosition.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PositionBlock");
                    }
                    if (SSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (SEmploymentDate == DateTime.Now)
                    {
                        MessageBox.Show("Укажите дату");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePerson(SFirstName, SLastName, SMiddleName, SPosition, SSalary, SEmploymentDate);
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

        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if (SelectedTabItem.Name == "PersonsList" && SelectedPerson != null)
                    {
                        OpenEditPersonWindowMethod(SelectedPerson);
                    }
                }
                    );
            }
        }

        #endregion


        #region Команды изминения
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

                        resultStr = DataWorker.EditPerson(SelectedPerson, SDateOfDismissal);

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
        /// Открытие окна редактирования сотрудника
        /// </summary>
        private void OpenEditPersonWindowMethod(ModelPerson person)
        {
            EditPersonWindow editPersonWindow = new EditPersonWindow(person);
            SetCenterPositionAndOpen(editPersonWindow);
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

        private void SetNullValuesToProperties()
        {
            //для пользователя
            SFirstName = null;
            SLastName = null;
            SMiddleName = null;
            SPosition = null;
            SSalary = 0;
            SEmploymentDate = DateTime.Now;
        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            SetCenterPositionAndOpen(messageView);
        }

        private void UpdateAllDataView()
        {
            AllPeople = DataWorker.GetAllPerson();
            MainWindow.AllPersonsView.ItemsSource = null;
        }

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
