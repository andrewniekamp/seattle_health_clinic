using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class Patient
  {
    private int _id;
    private string _name;
    private string _address;

    public Patient(string name, string Address, int id = 0)
    {
      _id = id;
      _name = name;
      _address=Address;
    }



    public override bool Equals(System.Object otherPatient)
    {
      if (!(otherPatient is Patient))
      {
        return false;
      }
      else
      {
        Patient newPatient = (Patient) otherPatient;
        bool idEquality = this.GetId() == newPatient.GetId();
        bool nameEquality = this.GetName() == newPatient.GetName();
        bool addressEquality = this.GetAddress() == newPatient.GetAddress();

        return (idEquality && nameEquality&&addressEquality);
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

    public string GetAddress()
    {
      return _address;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public static List<Patient> GetAll()
    {
      List<Patient> allPatients = new List<Patient>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        string patientAddress = rdr.GetString(2);

        Patient newPatient = new Patient(patientName,patientAddress, patientId);
        allPatients.Add(newPatient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allPatients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients (name,address) OUTPUT INSERTED.id VALUES (@Name,@Address);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

      SqlParameter addressParameter = new SqlParameter();
      addressParameter.ParameterName = "@Address";
      addressParameter.Value = this.GetAddress();

      cmd.Parameters.Add(addressParameter);

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

    public static Patient Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients WHERE id = @PatientId;", conn);
      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = id.ToString();

      cmd.Parameters.Add(patientIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundPatientId = 0;
      string foundPatientName = null;
      string foundPatientAddress = null;

      while (rdr.Read())
      {
        foundPatientId = rdr.GetInt32(0);
        foundPatientName = rdr.GetString(1);
        foundPatientAddress = rdr.GetString(2);
      }
      Patient foundPatient = new Patient(foundPatientName, foundPatientAddress,foundPatientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPatient;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand( "DELETE FROM patients WHERE id = @PatientId;DELETE FROM patients_conditions WHERE patient_id = @PatientId;DELETE FROM critical_patients WHERE patient_id = @PatientId;DELETE FROM diagnosis WHERE patient_id = @PatientId;DELETE FROM patients_scheduling WHERE patient_id = @PatientId", conn);

      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = this.GetId();

      cmd.Parameters.Add(patientIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

  public void Update(string newName, string newAddress)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("UPDATE patients SET name = @NewName, address = @Address OUTPUT INSERTED.name, INSERTED.address WHERE id = @PatientId;", conn);

       SqlParameter newNameParameter = new SqlParameter();
       newNameParameter.ParameterName = "@NewName";
       newNameParameter.Value = newName;

       SqlParameter newAddressParameter = new SqlParameter();
       newAddressParameter.ParameterName = "@Address";
       newAddressParameter.Value = newAddress;

       SqlParameter patientIdParameter = new SqlParameter();
       patientIdParameter.ParameterName = "@PatientId";
       patientIdParameter.Value = this.GetId();

       cmd.Parameters.Add(newNameParameter);
       cmd.Parameters.Add(newAddressParameter);
       cmd.Parameters.Add(patientIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while (rdr.Read())
       {
         this._name = rdr.GetString(0);
         this._address = rdr.GetString(1);

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
