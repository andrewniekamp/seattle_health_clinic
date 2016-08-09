using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
      Employee.DeleteAll();
      License.DeleteAll();
    }
    [Fact]
    public void Test_EmptyDataTable_DataTableIsEmpty()
    {
      // Arrange
      List<Employee> allEmployees = new List<Employee>{};
      allEmployees = Employee.GetAll();
      // Act
      int countActual = allEmployees.Count;
      // Assert
      Assert.Equal(0, countActual);
    }
    [Fact]
    public void Test_AddEmployee_AddsEmployeeToDatabase()
    {
      // Arrange
      Employee newEmployee = new Employee("Doc", "Gonzo");
      // Act
      newEmployee.Save();
      List<Employee> expectedEmployees = Employee.GetAll();
      int countActual = expectedEmployees.Count;
      // Assert
      Assert.Equal(1, countActual);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      string firstExpected = "Doc";
      string lastExpected = "Gonzo";
      Employee newEmployee = new Employee(firstExpected, lastExpected);
      newEmployee.Save();
      int expectedId = newEmployee.GetId();
      //Act
      Employee savedEmployee = Employee.GetAll()[0];
      int actualId = savedEmployee.GetId();
      //Assert
      Assert.Equal(expectedId, actualId);
    }
    [Fact]
    public void Test_Find_FindsEmployeeById()
    {
      // Arrange
      string firstExpected = "Doc";
      string lastExpected = "Gonzo";
      Employee newEmployee = new Employee(firstExpected, lastExpected);
      newEmployee.Save();
      int findId = newEmployee.GetId();
      // Act
      Employee employeeActual = newEmployee.Find(findId);
      string firstActual = newEmployee.GetFirstName();
      string lastActual = newEmployee.GetLastName();
      // Assert
      Assert.Equal(firstExpected, firstActual);
      Assert.Equal(lastExpected, lastActual);
    }
    [Fact]
    public void Test_UpdateName_UpdatesEmployeeNameInDatabase()
    {
      // Arrange
      Employee newEmployee = new Employee("Doc", "Gonzo");
      newEmployee.Save();
      string newFirst = "Dr.";
      string newLast = "Aharchi";
      string newName = "Dr." + "Aharchi";
      // Act
      newEmployee.UpdateName(newFirst, newLast);
      // Assert
      Assert.Equal(newName, newEmployee.GetFirstName() + newEmployee.GetLastName());
    }
    [Fact]
    public void Test_AddLicense_AddsLicenseToEmployee()
    {
      //Arrange
      Employee testEmployee = new Employee("Doc", "Gonzo");
      testEmployee.Save();
      License testLicense1 = new License("WA9999", "MD");
      testLicense1.Save();
      License testLicense2 = new License("WA0000", "RN");
      testLicense2.Save();
      //Act
      testEmployee.AddLicense(testLicense1);
      testEmployee.AddLicense(testLicense2);
      List<License> result = testEmployee.GetLicenses();
      int actualCount = result.Count;
      List<License> testList = new List<License>{testLicense1, testLicense2};
      int expectedCount = testList.Count;
      //Assert
      Assert.Equal(expectedCount, actualCount);
    }
    [Fact]
    public void Test_GetLicenses_ReturnsAllEmployeeLicenses()
    {
      //Arrange
      Employee testEmployee = new Employee("Doc", "Gonzo");
      testEmployee.Save();
      License testLicense1 = new License("WA9999", "MD");
      testLicense1.Save();
      License testLicense2 = new License("WA0000", "RN");
      testLicense2.Save();
      List<License> testList = new List<License> {testLicense1};
      //Act
      testEmployee.AddLicense(testLicense1);
      List<License> savedLicenses = testEmployee.GetLicenses();
      int expectedCount = testList.Count;
      int actualCount = savedLicenses.Count;
      //Assert
      Assert.Equal(expectedCount, actualCount);
    }
    [Fact]
    public void Test_VerifyLogin_ChecksLoginInfoAgainstDatabase()
    {
      //Arrange
      Employee testEmployee = new Employee ("Doc", "Gonzo");
      testEmployee.SetLogin("doc@doc.com", "password");
      testEmployee.Save();
      testEmployee.SaveLogin();

      //Act
      bool expected = true;
      bool result = Employee.VerifyLogin("doc@doc.com", "password");

      //Assert
      Assert.Equal(expected, result);
    }
  }
}
