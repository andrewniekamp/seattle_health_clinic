using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class ConditionEvaluationTest : IDisposable
  {
    public ConditionEvaluationTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
      Condition.DeleteAll();
      ConditionEval.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = ConditionEval.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfConditionEvalIsSame()
    {
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval firstConditionEval = new ConditionEval(1, 2, 1, statusDate);
      ConditionEval secondConditionEval = new ConditionEval(1, 2, 1, statusDate);

      Assert.Equal(firstConditionEval, secondConditionEval);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      Condition testCondition = new Condition("Stable");
      testCondition.Save();
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(testPatient.GetId(), testCondition.GetId(), 1, statusDate);
      testConditionEval.Save();

      List<ConditionEval> result = ConditionEval.GetAll();
      List<ConditionEval> testList = new List<ConditionEval>{testConditionEval};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToConditionEval()
    {
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(1, 2, 1, statusDate);
      testConditionEval.Save();

      ConditionEval savedConditionEval = ConditionEval.GetAll()[0];
      int result = savedConditionEval.GetId();
      int testId = testConditionEval.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsConditionEvalInDB()
    {
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(1, 2, 1, statusDate);
      testConditionEval.Save();

      ConditionEval foundConditionEval = ConditionEval.Find(testConditionEval.GetId());

      Assert.Equal(testConditionEval, foundConditionEval);
    }

    [Fact]
    public void T6_GetPatientName_GetsConditionEvalPatientName()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(testPatient.GetId(), 1, 2, statusDate);
      testConditionEval.Save();

      string result = testConditionEval.GetPatientName();

      Assert.Equal("Anderson", result);
    }

    [Fact]
    public void T7_GetConditionName_GetsConditionEvalDoctorName()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();


      Condition testCondition = new Condition("Stable");
      testCondition.Save();
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(testPatient.GetId(),testCondition.GetId(),1, statusDate);
      testConditionEval.Save();

      string result = testConditionEval.GetConditionName();

      Assert.Equal("Stable", result);
    }

    [Fact]
    public void T8_Delete_DeleteRelationshipsInOtherTables()
    {
      Patient testPatient = new Patient("Anderson", "1234 Main Street");
      testPatient.Save();

      Patient testPatient2 = new Patient("And", "1234");
      testPatient2.Save();
      Condition testCondition = new Condition("Stable");
      testCondition.Save();
      DateTime statusDate = new DateTime(2016,08,04);

      ConditionEval testConditionEval = new ConditionEval(testPatient.GetId(), testCondition.GetId(), 1, statusDate);
      testConditionEval.Save();

      ConditionEval testConditionEval2 = new ConditionEval(testPatient2.GetId(), testCondition.GetId(), 1, statusDate);
      testConditionEval2.Save();

      testPatient.Delete();

      List<ConditionEval> result = ConditionEval.GetAll();
      List<ConditionEval> testList = new List<ConditionEval>{testConditionEval2};

      Assert.Equal(testList, result);
    }

  }
}
