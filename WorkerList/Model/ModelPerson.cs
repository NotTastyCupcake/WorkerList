using System;

namespace NotTastyCupcake.WorkerList.UserInterface.DesktopUI.Model
{
    public class ModelPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public DateTime? DateOfDismissal { get; set; }
    }

}
