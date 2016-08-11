using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class Condition
  {
    private int _id;
    private string _status;

    public Condition(string status, int id = 0)
    {
      _id = id;
      _status = status;
    }

    public override bool Equals(System.Object otherCondition)
    {
      if (!(otherCondition is Condition))
      {
        return false;
      }
      else
      {
        Condition newCondition = (Condition) otherCondition;
        bool idEquality = this.GetId() == newCondition.GetId();
        bool statusEquality = this.GetCondition() == newCondition.GetCondition();

        return (idEquality && statusEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetCondition()
    {
      return _status;
    }

    public void SetCondition(string newCondition)
    {
      _status = newCondition;
    }

    public static List<Condition> GetAll()
    {
      List<Condition> allConditions = new List<Condition>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM conditions;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientCondition = rdr.GetString(1);

        Condition newCondition = new Condition(patientCondition, patientId);
        allConditions.Add(newCondition);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allConditions;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO conditions (status) OUTPUT INSERTED.id VALUES (@Condition);", conn);

      SqlParameter statusParameter = new SqlParameter();
      statusParameter.ParameterName = "@Condition";
      statusParameter.Value = this.GetCondition();

      cmd.Parameters.Add(statusParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Condition Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM conditions WHERE id = @ConditionId;", conn);
      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@ConditionId";
      patientIdParameter.Value = id.ToString();

      cmd.Parameters.Add(patientIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundConditionId = 0;
      string foundConditionCondition = null;

      while (rdr.Read())
      {
        foundConditionId = rdr.GetInt32(0);
        foundConditionCondition = rdr.GetString(1);
      }
      Condition foundCondition = new Condition(foundConditionCondition, foundConditionId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCondition;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM conditions;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


  }
}
