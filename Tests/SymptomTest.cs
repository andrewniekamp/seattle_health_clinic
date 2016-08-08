using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class SymptomTest : IDisposable
  {
    public SymptomTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Symptom.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Symptom.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfSymptomIsSame()
    {
      Symptom firstSymptom = new Symptom("Anderson", "1234 Main Street");
      Symptom secondSymptom = new Symptom("Anderson", "1234 Main Street");

      Assert.Equal(firstSymptom, secondSymptom);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Symptom testSymptom = new Symptom("Anderson", "1234 Main Street");
      testSymptom.Save();

      List<Symptom> result = Symptom.GetAll();
      List<Symptom> testList = new List<Symptom>{testSymptom};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToSymptom()
    {
      Symptom testSymptom = new Symptom("Anderson", "1234 Main Street");
      testSymptom.Save();

      Symptom savedSymptom = Symptom.GetAll()[0];
      int result = savedSymptom.GetId();
      int testId = testSymptom.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsSymptomInDB()
    {
      Symptom testSymptom = new Symptom("Anderson", "1234 Main Street");
      testSymptom.Save();

      Symptom foundSymptom = Symptom.Find(testSymptom.GetId());

      Assert.Equal(testSymptom, foundSymptom);
    }

    [Fact]
    public void T6_Delete_FindsSymptomInDB()
    {
      Symptom testSymptom = new Symptom("Anderson", "1234 Main Street");
      testSymptom.Save();
      Symptom testSymptom2 = new Symptom("Ander", "1234 Main Str");
      testSymptom2.Save();
      testSymptom.Delete();

      List<Symptom> allSymptoms = Symptom.GetAll();
      List<Symptom> resultSymptoms = new List<Symptom> {testSymptom2};
      Assert.Equal(resultSymptoms, allSymptoms);
    }

    [Fact]
    public void T7_Update_UpdatesSymptomInDB()
    {
      Symptom testSymptom = new Symptom("Anderson", "1234 Main Street");
      testSymptom.Save();
      string newName = "Nosredna";
      string newAddress = "4321 Side Street";

      testSymptom.Update(newName, newAddress,false);
      string result = testSymptom.GetName();
      string result2 = testSymptom.GetClassification();

      Assert.Equal(newName, result);
      Assert.Equal(newAddress, result2);
    }

  }
}
