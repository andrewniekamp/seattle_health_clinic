using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class CriticalPatient :Patient
  {
    private int _criticalPatientId;
    // private string _name;
    // private string _address;
    private int _frequency;

    public  CriticalPatient(string name, string Address, int frequency,int criticalPatientId = 0) : base(name,Address)
    {
      _criticalPatientId=criticalPatientId;
      _frequency=frequency;
    }

    public override bool Equals(System.Object otherPatient)
    {
      if (!(otherPatient is CriticalPatient))
      {
        return false;
      }
      else
      {
        CriticalPatient newPatient = (CriticalPatient) otherPatient;
        bool idEquality = this.GetId() == newPatient.GetId();
        bool nameEquality = this.GetName() == newPatient.GetName();
        bool addressEquality = this.GetAddress() == newPatient.GetAddress();
        bool frequencyEquality = this.GetFrequency() == newPatient.GetFrequency();

        return (idEquality && nameEquality&&addressEquality&&frequencyEquality);
      }
    }

    public int GetFrequency()
    {
      return _frequency ;
    }

    public int GetCriticalPatientId()
    {
      return _criticalPatientId;
    }

    // public string GetName()
    // {
    //   return _name;
    // }
    //
    // public string GetAddress()
    // {
    //   return _address;
    // }
    // public void SetName(string newName)
    // {
    //   _name = newName;
    // }
    public void CriticalSave()
    {
      this.Save();

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO critical_patients (patient_id,frequency) OUTPUT INSERTED.id VALUES (@PatientsId,@Frequency);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatientsId";
      nameParameter.Value = this.GetId();

      cmd.Parameters.Add(nameParameter);

      SqlParameter addressParameter = new SqlParameter();
      addressParameter.ParameterName = "@Frequency";
      addressParameter.Value = this.GetFrequency();

      cmd.Parameters.Add(addressParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._criticalPatientId=rdr.GetInt32(0);

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
