using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class License
  {
    // properties
    private int _id;
    private string _issueState;
    private DateTime _issueDate;
    private DateTime _expirationDate;
    private string _number;
    private string _type;
    // constructors, getters, setters
    public License(string number, string type, int Id=0)
    {
      _number = number;
      _type = type;
      _id = Id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetLicenseNumber()
    {
      return _number;
    }
    public string GetLicenseType()
    {
      return _type;
    }
    // other methods
    // a method to save a licencse
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO employees_medical (license_number, license_type) OUTPUT INSERTED.id VALUES (@LicenseNumber, @LicenseType);", conn);
      SqlParameter numberParameter = new SqlParameter();
      numberParameter.ParameterName = "@LicenseNumber";
      numberParameter.Value = this.GetLicenseNumber();
      cmd.Parameters.Add(numberParameter);
      SqlParameter typeParameter = new SqlParameter();
      typeParameter.ParameterName = "@LicenseType";
      typeParameter.Value = this.GetLicenseType();
      cmd.Parameters.Add(typeParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
    }
    // a method to get all licenses
    public static List<License> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees_medical ORDER BY license_type;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<License> allLicenses = new List<License>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string licenseNumber = rdr.GetString(3);
        string licenseType = rdr.GetString(4);
        License newLicense = new License(licenseNumber, licenseType, id);
        allLicenses.Add(newLicense);
      }
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return allLicenses;
    }
    // a method to get a license by license
    public License Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees_medical WHERE id = @LicenseId;", conn);
      SqlParameter licenseIdParameter = new SqlParameter();
      licenseIdParameter.ParameterName = "@LicenseId";
      licenseIdParameter.Value = Id.ToString();
      cmd.Parameters.Add(licenseIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      int foundLicenseId = 0;
      string foundLicenseNumber = null;
      string foundLicenseType = null;
      while(rdr.Read())
      {
        foundLicenseId = rdr.GetInt32(0);
        foundLicenseNumber = rdr.GetString(3);
        foundLicenseType = rdr.GetString(4);
      }
      License foundLicense = new License(foundLicenseNumber, foundLicenseType, foundLicenseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundLicense;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM employees_medical;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
