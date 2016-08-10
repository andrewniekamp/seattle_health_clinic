using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace SeattleHealthClinic
{
  public class PhysicianTest  : IDisposable
  {
    public PhysicianTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Physician.DeleteAll();
      License.DeleteAll();
      Payroll.DeleteAll();
    }
    [Fact]
    public void Test_EmptyDataTable_DataTableIsEmpty()
    {
      // Arrange
      List<Employee> allPhysicians = new List<Employee>{}; // must use employee, not physician...
      allPhysicians = Physician.GetAll();
      // Act
      int countActual = allPhysicians.Count;
      // Assert
      Assert.Equal(0, countActual);
    }
    [Fact]
    public void Test_Constructor_ConstructorCreatesObject()
    {
      // Arrange
      string firstExpected = "Doc";
      string lastExpected = "Gonzo";
      string ssnExpected = "000-00-0000";
      string typeExpected = "Medical";
      string salaryTypeExpected = "Annual";
      string emailExpected = "docgonzo@outlook.com";
      string passwordExpected = "password";
      DateTime hireDateExpected = new DateTime(2016, 1, 1);
      // Act
      Physician newPhysician = new Physician(firstExpected, lastExpected, ssnExpected, typeExpected, salaryTypeExpected, emailExpected, passwordExpected, hireDateExpected);
      newPhysician.Save();
      // Assert
      Assert.Equal(firstExpected, newPhysician.GetFirstName());
    }
  }
}
