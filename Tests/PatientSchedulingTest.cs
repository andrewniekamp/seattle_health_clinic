using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class PatientSchedulingTest : IDisposable
  {
    public PatientSchedulingTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
      Condition.DeleteAll();
      PatientScheduling.DeleteAll();
      PatientScheduling.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = PatientScheduling.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfPatientSchedulingIsSame()
    {
      DateTime appointmentDate = new DateTime(2016,08,04);

      PatientScheduling firstPatientScheduling = new PatientScheduling(1, 2, "The patient reports general discomfort in the abdomen.", appointmentDate);
      PatientScheduling secondPatientScheduling = new PatientScheduling(1, 2, "The patient reports general discomfort in the abdomen.", appointmentDate);

      Assert.Equal(firstPatientScheduling, secondPatientScheduling);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      DateTime appointmentDate = new DateTime(2016,08,04);

      PatientScheduling testPatientScheduling = new PatientScheduling(testPatient.GetId(), 1, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling.Save();

      List<PatientScheduling> result = PatientScheduling.GetAll();
      List<PatientScheduling> testList = new List<PatientScheduling>{testPatientScheduling};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToPatientScheduling()
    {
      DateTime appointmentDate = new DateTime(2016,08,04);

      PatientScheduling testPatientScheduling = new PatientScheduling(1, 2, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling.Save();

      PatientScheduling savedPatientScheduling = PatientScheduling.GetAll()[0];
      int result = savedPatientScheduling.GetId();
      int testId = testPatientScheduling.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsPatientSchedulingInDB()
    {
      DateTime appointmentDate = new DateTime(2016,08,04);

      PatientScheduling testPatientScheduling = new PatientScheduling(1, 2, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling.Save();

      PatientScheduling foundPatientScheduling = PatientScheduling.Find(testPatientScheduling.GetId());

      Assert.Equal(testPatientScheduling, foundPatientScheduling);
    }


    [Fact]
    public void T6_Delete_DeleteRelationshipsInOtherTables()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      Patient testPatient2 = new Patient("And", "1234");
      testPatient2.Save();
      DateTime appointmentDate = new DateTime(2016,08,04);

      PatientScheduling testPatientScheduling = new PatientScheduling(testPatient.GetId(),  1, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling.Save();

      PatientScheduling testPatientScheduling2 = new PatientScheduling(testPatient2.GetId(),  1, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling2.Save();

      testPatientScheduling.Delete();

      List<PatientScheduling> result = PatientScheduling.GetAll();
      List<PatientScheduling> testList = new List<PatientScheduling>{testPatientScheduling2};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T7_Update_UpdatesPatientInDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      DateTime appointmentDate = new DateTime(2016,09,02);
      PatientScheduling testPatientScheduling = new PatientScheduling(testPatient.GetId(),  1, "The patient reports general discomfort in the abdomen.", appointmentDate);
      testPatientScheduling.Save();

      // string apptDate=testPatientScheduling.GetPatientSchedulingDate();

      DateTime newAppointmentDate = new DateTime(2016,08,20);
      string newNote = "The patient has a rash.";

      testPatientScheduling.Update(newNote, newAppointmentDate);
      string result = testPatientScheduling.GetNote();
      string result2 = testPatientScheduling.GetPatientSchedulingDate();
      string apptDate = newAppointmentDate.ToString("MM/dd/yyyy");
      Assert.Equal(newNote, result);
      Assert.Equal(apptDate, result2);
    }

  }
}
