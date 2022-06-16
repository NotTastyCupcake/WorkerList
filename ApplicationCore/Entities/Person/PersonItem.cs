using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Position;
using System;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotTastyCupcake.WorkerList.ApplicationCore.Interfaces;

namespace NotTastyCupcake.WorkerList.ApplicationCore.Entities.Person
{
    public class PersonItem : BaseEntities, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public int PositionId { get; private set; }
        public PositionItem Position { get; private set; }
        public decimal Salary { get; private set; }
        public DateTime EmploymentDate { get; private set; }
        public DateTime? DateOfDismissal { get; private set; }

        public PersonItem(string firstName, string lastName, string middleName, int positionId, int salary, DateTime employmentDate)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PositionId = positionId;
            Salary = salary;
            EmploymentDate = employmentDate;
        }


        public void UpdateFullName(string firstName, string lastName, string middleName)
        {
            Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            Guard.Against.NullOrEmpty(middleName, nameof(middleName));
            Guard.Against.NullOrEmpty(lastName, nameof(lastName));

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public void UpdateSalary(decimal salary)
        {
            Guard.Against.NegativeOrZero(salary, nameof(salary));

            Salary = salary;
        }

        public void UpdatePosition(int positionId)
        {
            Guard.Against.Zero(positionId, nameof(positionId));
            PositionId = positionId;
        }

        public void DismissalPerson()
        {
            DateOfDismissal = DateTime.Now;
        }
    }
}
