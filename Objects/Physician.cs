using System;
using System.Collections.Generic;

namespace SeattleHealthClinic
{
  public class Physician : Employee
  {
    // properties
    internal List<string> _boardCertifications;
    internal List<string> _stateLicenses;
    // constructors, getters, setters
    public Physician(string firstName, string lastName, string SSN, string employeeType, string salaryType, string email, string password, DateTime hireDate, int Id = 0) : base(firstName, lastName, SSN, employeeType, salaryType, email, password, hireDate, Id)
    {
      // TODO
    }
    // other methods
  }
}
