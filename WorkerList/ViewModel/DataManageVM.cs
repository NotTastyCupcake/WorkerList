﻿using System;
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
                    if (FirstName == null || FirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstNameBlock");
                    }
                    if (LastName == null || LastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "LastNameBlock");
                    }
                    if (MiddleName == null || MiddleName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "MiddleNameBlock");
                    }
                    if (Position == null || Position.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PositionBlock");
                    }
                    if (Salary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (EmploymentDate == DateTime.Now)
                    {
                        MessageBox.Show("Укажите дату");
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
                    string resultStr = "Ничего не выбрано";
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
                    string resultStr = "Ничего не выбрано";
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
            AllPeople = DataWorker.GetAllPerson();
            MainWindow.AllPersonsView.ItemsSource = null;
            MainWindow.AllPersonsView.Items.Clear();
            MainWindow.AllPersonsView.ItemsSource = AllPeople;
            MainWindow.AllPersonsView.Items.Refresh();
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
