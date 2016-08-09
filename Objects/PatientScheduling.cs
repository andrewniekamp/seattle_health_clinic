using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class PatientScheduling
  {
    private int _id;
    private int _patientId;
    private int _doctorId;
    private string _note;
    private DateTime _appointmentDate;

    public PatientScheduling (int patientId, int doctorId, string Note, DateTime appointmentDate, int id = 0)
    {
      _id = id;
      _patientId = patientId;
      _doctorId=doctorId;
      _note=Note;
      _appointmentDate = appointmentDate;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetPatientId()
    {
      return _patientId;
    }

    public int GetDoctorId()
    {
      return _doctorId;
    }

    public void SetPatientId(int newPatientId)
    {
      _patientId = newPatientId;
    }

    public string GetNote()
    {
      return _note;
    }
    public void SetNote(string newNote)
    {
      _note = newNote;
    }

    public string GetPatientSchedulingDate()
    {
      return _appointmentDate.ToString("MM/dd/yyyy");
    }
    public void SetPatientId(DateTime newPatientSchedulingDate)
    {
      _appointmentDate = newPatientSchedulingDate;
    }

    public override bool Equals(System.Object otherPatientScheduling)
    {
      if (!(otherPatientScheduling is PatientScheduling))
      {
        return false;
      }
      else
      {
        PatientScheduling newPatientScheduling = (PatientScheduling) otherPatientScheduling;
        bool idEquality = this.GetId() == newPatientScheduling.GetId();
        bool patientIdEquality = this.GetPatientId() == newPatientScheduling.GetPatientId();
        bool noteEquality = this.GetNote() == newPatientScheduling.GetNote();
        bool doctorIdEquality = this.GetDoctorId() == newPatientScheduling.GetDoctorId();
        bool appointmentDateEquality = this.GetPatientSchedulingDate() == newPatientScheduling.GetPatientSchedulingDate();

        return (idEquality && patientIdEquality && noteEquality && appointmentDateEquality&&doctorIdEquality);
      }
    }

    public static List<PatientScheduling> GetAll()
    {
      List<PatientScheduling> allPatientSchedulings = new List<PatientScheduling>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients_scheduling ORDER BY appointment_date;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int patientSchedulingId = rdr.GetInt32(0);
        int patientId = rdr.GetInt32(1);
        int doctorId = rdr.GetInt32(2);
        string note = rdr.GetString(3);
        DateTime appointmentDate = rdr.GetDateTime(4);

        PatientScheduling newPatientScheduling = new PatientScheduling(patientId,doctorId,note, appointmentDate, patientSchedulingId);
        allPatientSchedulings.Add(newPatientScheduling);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allPatientSchedulings;
    }



    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients_scheduling (patient_id, doctor_id, note, appointment_date) OUTPUT INSERTED.id VALUES (@PatientId, @DoctorId,@Note, @PatientSchedulingDate);", conn);

      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = this.GetPatientId();
      cmd.Parameters.Add(patientIdParameter);

      SqlParameter noteParameter = new SqlParameter();
      noteParameter.ParameterName = "@Note";
      noteParameter.Value = this.GetNote();
      cmd.Parameters.Add(noteParameter);

      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = this.GetDoctorId();
      cmd.Parameters.Add(doctorIdParameter);

      SqlParameter appointmentDateParameter = new SqlParameter();
      appointmentDateParameter.ParameterName = "@PatientSchedulingDate";
      appointmentDateParameter.Value = this.GetPatientSchedulingDate();
      cmd.Parameters.Add(appointmentDateParameter);

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

    public static PatientScheduling Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients_scheduling WHERE id = @PatientSchedulingId;", conn);
      SqlParameter performanceIdParameter = new SqlParameter();
      performanceIdParameter.ParameterName = "@PatientSchedulingId";
      performanceIdParameter.Value = id.ToString();

      cmd.Parameters.Add(performanceIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundPatientSchedulingId = 0;
      int foundPatientId = 0;
      int founddoctorId = 0;
      string foundNote = null;

      DateTime foundPatientSchedulingDate = DateTime.MinValue;

      while (rdr.Read())
      {
        foundPatientSchedulingId = rdr.GetInt32(0);
        foundPatientId = rdr.GetInt32(1);
        founddoctorId = rdr.GetInt32(2);
        foundNote = rdr.GetString(3);
        foundPatientSchedulingDate = rdr.GetDateTime(4);
      }

      PatientScheduling foundPatientScheduling = new PatientScheduling(foundPatientId, founddoctorId,foundNote, foundPatientSchedulingDate, foundPatientSchedulingId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPatientScheduling;
    }

    public void Update(string newNote, DateTime newDate)
       {
         SqlConnection conn = DB.Connection();
         conn.Open();

         SqlCommand cmd = new SqlCommand("UPDATE patients_scheduling SET note = @NewNote, appointment_date = @NewDate OUTPUT INSERTED.note, INSERTED.appointment_date WHERE id = @PatientSchedulingId;", conn);

         SqlParameter newNoteParameter = new SqlParameter();
         newNoteParameter.ParameterName = "@NewNote";
         newNoteParameter.Value = newNote;

         SqlParameter newDateParameter = new SqlParameter();
         newDateParameter.ParameterName = "@NewDate";
         newDateParameter.Value = newDate;

         SqlParameter patientSchedulingIdParameter = new SqlParameter();
         patientSchedulingIdParameter.ParameterName = "@PatientSchedulingId";
         patientSchedulingIdParameter.Value = this.GetId();

         cmd.Parameters.Add(newNoteParameter);
         cmd.Parameters.Add(newDateParameter);
         cmd.Parameters.Add(patientSchedulingIdParameter);

         SqlDataReader rdr = cmd.ExecuteReader();

         while (rdr.Read())
         {
           this._note = rdr.GetString(0);
           this._appointmentDate = rdr.GetDateTime(1);

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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM patients_scheduling WHERE id = @PatientSchedulingId", conn);

      SqlParameter schedulingIdParameter = new SqlParameter();
      schedulingIdParameter.ParameterName = "@PatientSchedulingId";
      schedulingIdParameter.Value = this.GetId();

      cmd.Parameters.Add(schedulingIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patients_scheduling;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
