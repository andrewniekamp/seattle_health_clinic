using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class Symptom
  {
    private int _id;
    private string _name;
    private string _classification;
    private bool _contagious;

    public Symptom(string name, string Classification, bool contagious, int id = 0)
    {
      _id = id;
      _name = name;
      _classification=Classification;
      _contagious = contagious;
    }

    public override bool Equals(System.Object otherSymptom)
    {
      if (!(otherSymptom is Symptom))
      {
        return false;
      }
      else
      {
        Symptom newSymptom = (Symptom) otherSymptom;
        bool idEquality = this.GetId() == newSymptom.GetId();
        bool nameEquality = this.GetName() == newSymptom.GetName();
        bool classificationEquality = this.GetClassification() == newSymptom.GetClassification();
        bool contagiousEquality = this.IsContagious() == newSymptom.IsContagious();

        return (idEquality && nameEquality&&classificationEquality && contagiousEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetClassification()
    {
      return _classification;
    }

    public bool IsContagious()
    {
      return _contagious;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public void SetContagious(bool newContagious)
    {
      _contagious = newContagious;
    }
    public static List<Symptom> GetAll()
    {
      List<Symptom> allSymptoms = new List<Symptom>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM symptoms;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        string patientClassification = rdr.GetString(2);
        bool patientContagious = rdr.GetBoolean(3);

        Symptom newSymptom = new Symptom(patientName,patientClassification, patientContagious, patientId);
        allSymptoms.Add(newSymptom);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allSymptoms;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO symptoms (name,classification,contagious) OUTPUT INSERTED.id VALUES (@Name,@Classification,@Contagious);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter classificationParameter = new SqlParameter();
      classificationParameter.ParameterName = "@Classification";
      classificationParameter.Value = this.GetClassification();
      cmd.Parameters.Add(classificationParameter);

      SqlParameter contagiousParameter = new SqlParameter();
      contagiousParameter.ParameterName = "@Contagious";
      contagiousParameter.Value = this.IsContagious();
      cmd.Parameters.Add(contagiousParameter);

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

    public static Symptom Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM symptoms WHERE id = @SymptomId;", conn);
      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@SymptomId";
      patientIdParameter.Value = id.ToString();

      cmd.Parameters.Add(patientIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundSymptomId = 0;
      string foundSymptomName = null;
      string foundSymptomClassification = null;
      bool foundSymptomContagious=false;
      while (rdr.Read())
      {
        foundSymptomId = rdr.GetInt32(0);
        foundSymptomName = rdr.GetString(1);
        foundSymptomClassification = rdr.GetString(2);
        foundSymptomContagious = rdr.GetBoolean(3);

      }
      Symptom foundSymptom = new Symptom(foundSymptomName, foundSymptomClassification,foundSymptomContagious,foundSymptomId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundSymptom;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM symptoms;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


    public void Delete()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand( "DELETE FROM symptoms WHERE id = @SymptomId;DELETE FROM diagnosis WHERE id= @SymptomId;", conn);

    SqlParameter patientIdParameter = new SqlParameter();
    patientIdParameter.ParameterName = "@SymptomId";
    patientIdParameter.Value = this.GetId();

    cmd.Parameters.Add(patientIdParameter);

    cmd.ExecuteNonQuery();

    if (conn != null)
    {
      conn.Close();
    }
  }

  public void Update(string newName, string newClassification)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("UPDATE symptoms SET name = @NewName, classification = @Classification, contagious=@Contagious OUTPUT INSERTED.name, INSERTED.classification WHERE id = @SymptomId;", conn);

       SqlParameter newNameParameter = new SqlParameter();
       newNameParameter.ParameterName = "@NewName";
       newNameParameter.Value = newName;

       SqlParameter newClassificationParameter = new SqlParameter();
       newClassificationParameter.ParameterName = "@Classification";
       newClassificationParameter.Value = newClassification;

       SqlParameter newContagiousParameter = new SqlParameter();
       newContagiousParameter.ParameterName = "@Contagious";
       newContagiousParameter.Value = newClassification;


       SqlParameter patientIdParameter = new SqlParameter();
       patientIdParameter.ParameterName = "@SymptomId";
       patientIdParameter.Value = this.GetId();

       cmd.Parameters.Add(newNameParameter);
       cmd.Parameters.Add(newClassificationParameter);
       cmd.Parameters.Add(newContagiousParameter);
       cmd.Parameters.Add(patientIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while (rdr.Read())
       {
         this._name = rdr.GetString(0);
         this._classification = rdr.GetString(1);
         this._contagious = rdr.GetBoolean(2);

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

  }
}
