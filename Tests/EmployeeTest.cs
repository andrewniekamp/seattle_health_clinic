using System;
using System.Collections.Generic;
using Xunit;

namespace SeattleHealthClinic
{
  public class EmployeeTest : IDisposable
  {
    public EmployeeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";

    }
    public void Dispose()
    {
      // delete all
    }
    [Fact]
    public void Test_EmptyDatabase_DatabaseIsEmpty()
    {
      // Arrange
      List<Employee> allEmployees = new List<Employee>{};
      allEmployees = Employee.GetAll();
      // Act
      int countActual = allEmployees.Count;
      // Assert
      Assert.Equal(0, countActual);
    }
  }
}
