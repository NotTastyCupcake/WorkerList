using System;
using System.Collections.Generic;
using System.Linq;
using WorkerList.Model;

namespace WorkerList.Data
{
    public static class DataWorker
    {
        #region Вывод
        /// <summary>
        /// Приводит данные из базы данных в List
        /// </summary>
        /// <returns> Лист сотрудников из базы данных </returns>
        public static List<ModelPerson> GetAllPerson()
        {
            using (Data.AppContext db = new Data.AppContext())
            {
                var result = db.Persons.ToList();
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
        public static string CreatePerson(string firstName, string lastName, string middleName, string position, decimal salary, DateTime employmentDate)
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
        /// Добавление даты увольнения сотрудника к строке
        /// </summary>
        /// <param name="oldPerson"> Модель сотрудника </param>
        /// <param name="NewDataOfDismissal"> Дата увольнения  </param>
        /// <returns>Строка состояние выполнения метода</returns>
        public static string EditPerson(ModelPerson oldPerson, DateTime? NewDataOfDismissal)
        {
            string result = "Такого сотрудника не существует";

            using (Data.AppContext db = new Data.AppContext())
            {
                ModelPerson person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.DateOfDismissal = NewDataOfDismissal;
                db.SaveChanges();
                result = $"Сотрудник {person.FirstName} {person.LastName} уволен!";
            }
            return result;
        }
        #endregion
    }
}
