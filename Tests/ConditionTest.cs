using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class ConditionTest : IDisposable
  {
    public ConditionTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=seattle_health_clinic_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Condition.DeleteAll();
    }

    [Fact]
    public void T1_DBEmptyAtFirst()
    {
      int result = Condition.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueIfConditionIsSame()
    {
      Condition firstCondition = new Condition("Stable");
      Condition secondCondition = new Condition("Stable");

      Assert.Equal(firstCondition, secondCondition);
    }

    [Fact]
    public void T3_Save_SavesToDB()
    {
      Condition testCondition = new Condition("Stable");
      testCondition.Save();

      List<Condition> result = Condition.GetAll();
      List<Condition> testList = new List<Condition>{testCondition};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToCondition()
    {
      Condition testCondition = new Condition("Stable");
      testCondition.Save();

      Condition savedCondition = Condition.GetAll()[0];
      int result = savedCondition.GetId();
      int testId = testCondition.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsConditionInDB()
    {
      Condition testCondition = new Condition("Stable");
      testCondition.Save();

      Condition foundCondition = Condition.Find(testCondition.GetId());

      Assert.Equal(testCondition, foundCondition);
    }
  }
}
