using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class CriticalPatientTest : IDisposable
  {
    public CriticalPatientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
    }
    //
    // [Fact]
    // public void T1_DBEmptyAtFirst()
    // {
    //   int result = Patient.GetAll().Count;
    //   Assert.Equal(0, result);
    // }

    [Fact]
    public void T2_Equal_ReturnsTrueIfPatientIsSame()
    {
      CriticalPatient firstCriticalPatient = new CriticalPatient("Anderson", "1234 Main Street",2);
      CriticalPatient secondCriticalPatient = new CriticalPatient("Anderson", "1234 Main Street",2);

      Assert.Equal(firstCriticalPatient, secondCriticalPatient);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      CriticalPatient testCriticalPatient = new CriticalPatient("Anderson", "1234 Main Street",2);

      Assert.Equal(2, testCriticalPatient.GetFrequency());
    }



        [Fact]
        public void T4_Save_SavesToDB()
        {
          CriticalPatient result = new CriticalPatient("Anderson", "1234 Main Street",2);

          result.CriticalSave();
          Console.WriteLine(result.GetId());
          Console.WriteLine(result.GetCriticalPatientId());
          Assert.Equal(result.GetId(),Patient.GetAll()[0].GetId());
        }


    // [Fact]
    // public void T4_Save_AssignsIdToPatient()
    // {
    //   Patient testPatient = new Patient("Anderson", "1234 Main Street");
    //   testPatient.Save();
    //
    //   Patient savedPatient = Patient.GetAll()[0];
    //   int result = savedPatient.GetId();
    //   int testId = testPatient.GetId();
    //
    //   Assert.Equal(testId, result);
    // }
    //
    // [Fact]
    // public void T5_Find_FindsPatientInDB()
    // {
    //   Patient testPatient = new Patient("Anderson", "1234 Main Street");
    //   testPatient.Save();
    //
    //   Patient foundPatient = Patient.Find(testPatient.GetId());
    //
    //   Assert.Equal(testPatient, foundPatient);
    // }
  }
}
