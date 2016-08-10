using System;
using System.Collections.Generic;

namespace SeattleHealthClinic
{
  public class Physician : Employee
  {
    // properties
    internal List<string> _boardCertifications = new List<string>{};
    internal List<string> _stateLicenses = new List<string>{};
    // constructors, getters, setters
    public Physician(string firstName, string lastName, string SSN, string employeeType, string salaryType, string email, string password, DateTime hireDate, int Id = 0) : base(firstName, lastName, SSN, employeeType, salaryType, email, password, hireDate, Id)
    {
      // TODO
    }
    public void SetBoardCertification(string certification)
    {
      _boardCertifications.Add(certification);
    }
    public void SetStateLicenses(string license)
    {
      _stateLicenses.Add(license);
    }
    // other methods
  }
}
