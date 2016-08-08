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
  }
}
