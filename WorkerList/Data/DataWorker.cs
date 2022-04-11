using System;
using System.Linq;
using System.Xml.Linq;
using NotTastyCupcake.WorkerList.WorkerList.DesktopUI.Model;

namespace NotTastyCupcake.WorkerList.WorkerList.DesktopUI.Data
{
    public static class DataWorker
    {
        #region Вывод
        /// <summary>
        /// Приводит данные из базы данных в массив данных
        /// </summary>
        /// <returns> Массив сотрудников из базы данных </returns>
        public static ModelPerson[] GetAllPerson()
        {

            using (Data.AppContext db = new Data.AppContext())
            {
                var result = db.Persons.ToArray();
                return result;
            }

        }
        #endregion

        #region Добавление
        /// <summary>
        /// Добавление сотрудника в базу данных, если в ней нету схожых данных
        /// </summary>
        /// <param name="firstName"> Имя </param>
        /// <param name="lastName"> Фамилия </param>
        /// <param name="middleName"> Отчество </param>
        /// <param name="position"> Должность </param>
        /// <param name="salary"> Оклад </param>
        /// <param name="employmentDate"> Дата приема на работу </param>
        /// <returns> Строка состаяния выполениея метода </returns>
        public static string CreatePerson(string firstName, string lastName, string middleName, string position, decimal salary, DateTime? employmentDate)
        {
            string result = "Уже сушествует";

            using (Data.AppContext db = new Data.AppContext())
            {
                bool checkIsExist = db.Persons.Any(el => el.FirstName == firstName && el.LastName == lastName && el.MiddleName == middleName);
                //Проверка на наличие в БД сходств
                if (!checkIsExist)
                {
                    ModelPerson person = new ModelPerson
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        MiddleName = middleName,
                        Position = position,
                        Salary = salary,
                        EmploymentDate = employmentDate
                    };
                    db.Persons.Add(person);
                    db.SaveChanges();
                    result = "Готово!";
                }
                return result;
            }
        }
        #endregion

        #region Редактирование
        /// <summary>
        /// Добавление/изминение даты увольнения сотрудника к строке
        /// </summary>
        /// <param name="oldPerson"> Модель сотрудника </param>
        /// <param name="NewDataOfDismissal"> Дата увольнения  </param>
        /// <returns>Строка состояние выполнения метода</returns>
        public static string DismissalPerson(ModelPerson oldPerson, DateTime? newDataOfDismissal)
        {
            string result = "Такого сотрудника не существует";

            using (AppContext db = new AppContext())
            {
                ModelPerson person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.DateOfDismissal = newDataOfDismissal;
                db.SaveChanges();
                result = $"Сотрудник {person.FirstName} уволен с {newDataOfDismissal}!";
            }
            return result;
        }

        /// <summary>
        /// Изменение данных о сотруднике
        /// </summary>
        /// <param name="oldPerson">Старые данные</param>
        /// <param name="newFirstName">Новое имя</param>
        /// <param name="newLastName">Новая фамилия</param>
        /// <param name="newMiddleName">Новое отчество</param>
        /// <param name="newPosition">Новая должность</param>
        /// <param name="newSalary">Новый оклад</param>
        /// <returns>Строка - результат работы программы</returns>
        public static string EditPerson(ModelPerson oldPerson, string newFirstName, string newLastName, string newMiddleName, string newPosition, decimal newSalary)
        {
            string result = "Такого сотрудника не существует";

            using (AppContext db = new AppContext())
            {
                ModelPerson person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.FirstName = newFirstName;
                person.LastName = newLastName;
                person.MiddleName = newMiddleName;
                person.Position = newPosition;
                person.Salary = newSalary;
                db.SaveChanges();
                result = $"Сотрудник {person.FirstName} изменен!";
            }
            return result;
        }
        #endregion

        #region Выгрузку

        /// <summary>
        /// Выгрузка из базы данных
        /// </summary>
        /// <param name="addressToSave">Путь для сохранения</param>
        /// <returns>Результат работы программы</returns>
        public static string CreatFileUnloadingData(string addressToSave)
        {
            //Результат работы
            string result;
            try
            {
                XDocument xdoc = new XDocument();
                // создаем корневой элемент
                XElement persons = new XElement("persons");

                foreach (var pers in GetAllPerson())
                {
                    // создаем первый элемент
                    XElement person = new XElement("person");
                    //создаем отрибут
                    XAttribute personFirstName = new XAttribute("firstName", pers.FirstName);
                    XElement personLastName = new XElement("lastName", pers.LastName);
                    XElement personMiddleName = new XElement("middleName", pers.MiddleName);
                    XElement personPosition = new XElement("position", pers.Position);
                    XElement personSalary = new XElement("salary", pers.Salary);
                    XElement personEmploymentDate = new XElement("employmentDate", pers.EmploymentDate);
                    XElement personDateOfDismissal = new XElement("dateOfDismissal", pers.DateOfDismissal);

                    // добавляем атрибут и элементы в первый элемент
                    person.Add(personFirstName, personLastName, personMiddleName, personPosition, personEmploymentDate, personDateOfDismissal);
                    // добавляем в корневой элемент
                    persons.Add(person);
                }

                // добавляем корневой элемент в документ
                xdoc.Add(persons);
                //сохраняем документ
                xdoc.Save($"{addressToSave}");

                result = $"Файл создан, выгрузка завершина. Расположение: {addressToSave}";

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
