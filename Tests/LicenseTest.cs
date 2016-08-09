using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace SeattleHealthClinic
{
  public class LicenseTest : IDisposable
  {
    public LicenseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";

    }
    public void Dispose()
    {
      License.DeleteAll();
      Employee.DeleteAll();
    }
    [Fact]
    public void Test_EmptyDataTable_DataTableIsEmpty()
    {
      // Arrange
      List<License> allLicenses = new List<License>{};
      allLicenses = License.GetAll();
      // Act
      int countActual = allLicenses.Count;
      // Assert
      Assert.Equal(0, countActual);
    }
    [Fact]
    public void Test_AddLicense_AddsLicenseToDataTable()
    {
      // Arrange
      License newLicense = new License("WA017", "MD");
      // Act
      newLicense.Save();
      List<License> expectedLicenses = License.GetAll();
      int countActual = expectedLicenses.Count;
      // Assert
      Assert.Equal(1, countActual);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      string firstExpected = "CA30891";
      string lastExpected = "MA";
      License newLicense = new License(firstExpected, lastExpected);
      newLicense.Save();
      int expectedId = newLicense.GetId();
      //Act
      License savedLicense = License.GetAll()[0];
      int actualId = savedLicense.GetId();
      //Assert
      Assert.Equal(expectedId, actualId);
    }

  }
}
