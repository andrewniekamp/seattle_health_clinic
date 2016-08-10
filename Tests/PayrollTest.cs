// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Data.SqlClient;
// using Xunit;
//
// namespace SeattleHealthClinic
// {
//   public class PayrollTest : IDisposable
//   {
//     public PayrollTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
//     }
//     public void Dispose()
//     {
//       // Payroll.DeleteAll();
//       // Employee.DeleteAll();
//       // License.DeleteAll();
//       // Physician.DeleteAll();
//     }
//     [Fact]
//     public void Test_EmptyDataTable_DataTableIsEmpty()
//     {
//       // Arrange
//       List<Payroll> allPayrolls = new List<Payroll>{};
//       allPayrolls = Payroll.GetAll();
//       // Act
//       int countActual = allPayrolls.Count;
//       // Assert
//       Assert.Equal(0, countActual);
//     }
//     [Fact]
//     public void Test_AddPayroll_AddsPayrollToDataTable()
//     {
//       // Arrange
//       Payroll newPayroll = new Payroll("Bi-Weekly","Annual", "$225,000");
//       newPayroll.SetEmployeeId("000-00-0000");
//       // Act"
//       newPayroll.Save();
//       List<Payroll> expectedPayrolls = Payroll.GetAll();
//       int countActual = expectedPayrolls.Count;
//       // Assert
//       Assert.Equal(1, countActual);
//     }
//     [Fact]
//     public void Test_Save_AssignsIdToObject()
//     {
//       //Arrange
//       Payroll newPayroll = new Payroll("Bi-Weekly","Annual", "$225,000");
//       newPayroll.SetEmployeeId("000-00-0000");
//       newPayroll.Save();
//       int expectedId = newPayroll.GetId();
//       //Act
//       Payroll savedPayroll = Payroll.GetAll()[0];
//       int actualId = savedPayroll.GetId();
//       //Assert
//       Assert.Equal(expectedId, actualId);
//     }
//     [Fact]
//     public void Test_UpdateSalary_UpdatesSalary()
//     {
//       //Arrange
//       Payroll newPayroll = new Payroll("Bi-Weekly","Annual", "$225,000");
//       newPayroll.SetEmployeeId("000-00-0000");
//       newPayroll.Save();
//       string expectedSalary = "$250,000";
//       //Act
//       newPayroll.UpdateSalary(expectedSalary);
//       string actualSalary = newPayroll.GetSalaryAmount();
//       //Assert
//       Assert.Equal(expectedSalary, actualSalary);
//     }
//   }
// }
