using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class DiagnosisTest : IDisposable
  {
    public DiagnosisTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
      Symptom.DeleteAll();
      Diagnosis.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Diagnosis.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfDiagnosisIsSame()
    {
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis firstDiagnosis = new Diagnosis(1, 2, 1, diagnosisDate);
      Diagnosis secondDiagnosis = new Diagnosis(1, 2, 1, diagnosisDate);

      Assert.Equal(firstDiagnosis, secondDiagnosis);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      Symptom testSymptom = new Symptom("Stable","Heart");
      testSymptom.Save();
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis testDiagnosis = new Diagnosis(testPatient.GetId(), 1,testSymptom.GetId(), diagnosisDate);
      testDiagnosis.Save();

      List<Diagnosis> result = Diagnosis.GetAll();
      List<Diagnosis> testList = new List<Diagnosis>{testDiagnosis};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToDiagnosis()
    {
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis testDiagnosis = new Diagnosis(1, 2, 1, diagnosisDate);
      testDiagnosis.Save();

      Diagnosis savedDiagnosis = Diagnosis.GetAll()[0];
      int result = savedDiagnosis.GetId();
      int testId = testDiagnosis.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsDiagnosisInDB()
    {
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis testDiagnosis = new Diagnosis(1, 2, 1, diagnosisDate);
      testDiagnosis.Save();

      Diagnosis foundDiagnosis = Diagnosis.Find(testDiagnosis.GetId());

      Assert.Equal(testDiagnosis, foundDiagnosis);
    }

    [Fact]
    public void T6_GetPatientName_GetsDiagnosisPatientName()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis testDiagnosis = new Diagnosis(testPatient.GetId(), 1, 2, diagnosisDate);
      testDiagnosis.Save();

      Patient foundPatient = Patient.Find(testDiagnosis.GetPatientId());

      Assert.Equal("Anderson", foundPatient.GetName());
    }


    [Fact]
    public void T8_Delete_DeleteRelationshipsInOtherTables()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      Patient testPatient2 = new Patient("And", "1234");
      testPatient2.Save();
      Symptom testSymptom = new Symptom("Stable","Heart");
      testSymptom.Save();
      DateTime diagnosisDate = new DateTime(2016,08,04);

      Diagnosis testDiagnosis = new Diagnosis(testPatient.GetId(), 1,  testSymptom.GetId(),diagnosisDate);
      testDiagnosis.Save();

      Diagnosis testDiagnosis2 = new Diagnosis(testPatient2.GetId(), 1, testSymptom.GetId(), diagnosisDate);
      testDiagnosis2.Save();

      testPatient.Delete();

      List<Diagnosis> result = Diagnosis.GetAll();
      List<Diagnosis> testList = new List<Diagnosis>{testDiagnosis2};

      Assert.Equal(testList, result);
    }

  }
}
