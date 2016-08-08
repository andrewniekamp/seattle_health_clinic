using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class PatientTest : IDisposable
  {
    public PatientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Patient.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfPatientIsSame()
    {
      Patient firstPatient = new Patient("Anderson", "1234 Main Street");
      Patient secondPatient = new Patient("Anderson", "1234 Main Street");

      Assert.Equal(firstPatient, secondPatient);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      List<Patient> result = Patient.GetAll();
      List<Patient> testList = new List<Patient>{testPatient};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToPatient()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      Patient savedPatient = Patient.GetAll()[0];
      int result = savedPatient.GetId();
      int testId = testPatient.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsPatientInDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      Patient foundPatient = Patient.Find(testPatient.GetId());

      Assert.Equal(testPatient, foundPatient);
    }
  }
}
