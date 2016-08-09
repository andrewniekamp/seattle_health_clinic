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
      ConditionEval.DeleteAll();
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

    [Fact]
    public void T6_Delete_FindsPatientInDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      Patient testPatient2 = new Patient("Ander", "1234 Main Str");
      testPatient2.Save();
      testPatient.Delete();

      List<Patient> allPatients = Patient.GetAll();
      List<Patient> resultPatients = new List<Patient> {testPatient2};
      Assert.Equal(resultPatients, allPatients);
    }

    [Fact]
    public void T7_Update_UpdatesPatientInDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      string newName = "Nosredna";
      string newAddress = "4321 Side Street";

      testPatient.Update(newName, newAddress);
      string result = testPatient.GetName();
      string result2 = testPatient.GetAddress();

      Assert.Equal(newName, result);
      Assert.Equal(newAddress, result2);
    }

    [Fact]
    public void T8_GetAllEval_GetsPatientEvals()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      DateTime evalDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(testPatient.GetId(), 3, 1, evalDate);
      testConditionEval.Save();
      ConditionEval testConditionEval2 = new ConditionEval(testPatient.GetId(), 6, 1, evalDate);
      testConditionEval2.Save();
      int testId = testPatient.GetId() + 1;
      ConditionEval testConditionEval3 = new ConditionEval(testId, 3, 3, evalDate);
      testConditionEval3.Save();

      List<ConditionEval> result = testPatient.GetAllEval();
      List<ConditionEval> testList = new List<ConditionEval>{testConditionEval, testConditionEval2};

      Assert.Equal(testList, result);
    }

  }
}
