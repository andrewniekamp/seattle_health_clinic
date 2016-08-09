using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class Employee
  {
    // properties
    internal int _id;
    internal string  _employee_name_first;
    internal string  _employee_name_last;
    internal string  _employee_ssn;
    internal string  _employee_type;
    internal string  _employee_date_hire;
    internal string  _employee_salary_type;
    internal string  _employee_data_access;
    internal string  _employee_email;
    internal string  _employee_password;
    // constructors, getters, setters
    public Employee(string first_name, string last_name, int Id = 0)
    {
      _employee_name_first = first_name;
      _employee_name_last = last_name;
      _id = Id;
    }
    public string GetFirstName()
    {
      return _employee_name_first;
    }
    public string GetLastName()
    {
      return _employee_name_last;
    }
    public int GetId()
    {
      return _id;
    }
    //temporary methods, refactor if necessary
    public void SetLogin (string newEmail, string newPassword)
    {
      _employee_email = newEmail;
      _employee_password = newPassword;
    }
    public string GetEmail()
    {
      return _employee_email;
    }
    public string GetPassword()
    {
      return _employee_password;
    }
    // other methods
    // a method to save an employee to the database
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO employees (employee_name_first, employee_name_last) OUTPUT INSERTED.id VALUES (@EmployeeNameFirst, @EmployeeNameLast);", conn);
      SqlParameter firstNameParameter = new SqlParameter();
      firstNameParameter.ParameterName = "@EmployeeNameFirst";
      firstNameParameter.Value = this.GetFirstName();
      cmd.Parameters.Add(firstNameParameter);
      SqlParameter lastNameParameter = new SqlParameter();
      lastNameParameter.ParameterName = "@EmployeeNameLast";
      lastNameParameter.Value = this.GetLastName();
      cmd.Parameters.Add(lastNameParameter);
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
    // a method to find an employee using the employee id
    public static Employee Find(int Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE id = @EmployeeId;", conn);
      SqlParameter employeeIdParameter = new SqlParameter();
      employeeIdParameter.ParameterName = "@EmployeeId";
      employeeIdParameter.Value = Id.ToString();
      cmd.Parameters.Add(employeeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      int foundEmployeeId = 0;
      string foundEmployeeFirstName = null;
      string foundEmployeeLastName = null;
      while(rdr.Read())
      {
        foundEmployeeId = rdr.GetInt32(0);
        foundEmployeeFirstName = rdr.GetString(1);
        foundEmployeeLastName = rdr.GetString(2);
      }
      Employee foundEmployee = new Employee(foundEmployeeFirstName, foundEmployeeLastName, foundEmployeeId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundEmployee;
    }
    // a method to update the name of an employee
    public void UpdateName(string newFirst, string newLast)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("UPDATE employees SET employee_name_first = @NewFirst, employee_name_last = @NewLast OUTPUT INSERTED.employee_name_first, INSERTED.employee_name_last WHERE id = @EmployeeId;", conn);
      SqlParameter newFirstParameter = new SqlParameter();
      newFirstParameter.ParameterName = "@NewFirst";
      newFirstParameter.Value = newFirst;
      cmd.Parameters.Add(newFirstParameter);
      SqlParameter newLastParameter = new SqlParameter();
      newLastParameter.ParameterName = "@NewLast";
      newLastParameter.Value = newLast;
      cmd.Parameters.Add(newLastParameter);
      SqlParameter employeeIdParameter = new SqlParameter();
      employeeIdParameter.ParameterName = "@EmployeeId";
      employeeIdParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(employeeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._employee_name_first = rdr.GetString(0);
        this._employee_name_last = rdr.GetString(1);
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
    // a method to return a list of all employees table records
    public static List<Employee> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees ORDER BY employee_name_last;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Employee> allEmployees = new List<Employee>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string firstName = rdr.GetString(1);
        string lastName = rdr.GetString(2);
        Employee newEmployee = new Employee(firstName, lastName, id);
        allEmployees.Add(newEmployee);
      }
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return allEmployees;
    }
    // a method to delete all employees table records
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM employees;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    // methods which interact with the License class/datatable
    // a method to add create an association between an employee and a license
    public void AddLicense(License newLicense)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO certifications (employee_id, license_id) VALUES (@EmployeeId, @LicenseId);", conn);
      SqlParameter employeeIdParameter = new SqlParameter();
      employeeIdParameter.ParameterName = "@EmployeeId";
      employeeIdParameter.Value = this.GetId();
      cmd.Parameters.Add(employeeIdParameter);
      SqlParameter licenseIdParameter = new SqlParameter();
      licenseIdParameter.ParameterName = "@LicenseId";
      licenseIdParameter.Value = newLicense.GetId();
      cmd.Parameters.Add(licenseIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
    // a method to get all licenses associated with a particular employee
    public List<License> GetLicenses()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT licenses.* FROM employees JOIN certifications ON (employees.id = certifications.employee_id) JOIN licenses ON (certifications.license_id = licenses.id) WHERE employees.id = @EmployeeId;", conn);
      SqlParameter EmployeeIdParameter = new SqlParameter();
      EmployeeIdParameter.ParameterName = "@EmployeeId";
      EmployeeIdParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(EmployeeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<License> licenses = new List<License>{};
      while(rdr.Read())
      {
        int licenseId = rdr.GetInt32(0);
        string licenseNumber = rdr.GetString(1);
        string licenseType = rdr.GetString(2);
        License newLicense = new License(licenseNumber, licenseType, licenseId);
        licenses.Add(newLicense);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return licenses;
    }
    //a method to verify login information
    public static bool VerifyLogin(string email, string password)
    {
      bool result = false;

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT employee_email, employee_password FROM employees WHERE employees.employee_email = @EmployeeEmail AND employees.employee_password = @EmployeePassword;", conn);
      SqlParameter employeeEmailParameter = new SqlParameter();
      employeeEmailParameter.ParameterName = "@EmployeeEmail";
      employeeEmailParameter.Value = email;
      cmd.Parameters.Add(employeeEmailParameter);
      SqlParameter employeePasswordParameter = new SqlParameter();
      employeePasswordParameter.ParameterName = "@EmployeePassword";
      employeePasswordParameter.Value = password;
      cmd.Parameters.Add(employeePasswordParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      if (rdr.HasRows == true)
      {
        result = true;
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return result;
    }
    //a method to save login information, currently needed for verify login tests
    public void SaveLogin()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO employees (employee_email, employee_password) VALUES (@EmployeeEmail, @EmployeePassword);", conn);
      SqlParameter employeeEmailParameter = new SqlParameter();
      employeeEmailParameter.ParameterName = "@EmployeeEmail";
      employeeEmailParameter.Value = this.GetEmail();
      cmd.Parameters.Add(employeeEmailParameter);
      SqlParameter employeePasswordParameter = new SqlParameter();
      employeePasswordParameter.ParameterName = "@EmployeePassword";
      employeePasswordParameter.Value = this.GetPassword();
      cmd.Parameters.Add(employeePasswordParameter);
      //this is unnecessary, fix later
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
    //a method to find the employee based on email address, can be refactored later on
    public static Employee Find(string email)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE employee_email = @EmployeeEmail;", conn);
      SqlParameter employeeEmailParameter = new SqlParameter();
      employeeEmailParameter.ParameterName = "@EmployeeEmail";
      employeeEmailParameter.Value = email;
      cmd.Parameters.Add(employeeEmailParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      int foundEmployeeId = 0;
      string foundEmployeeFirstName = null;
      string foundEmployeeLastName = null;
      while(rdr.Read())
      {
        foundEmployeeId = rdr.GetInt32(0);
        foundEmployeeFirstName = rdr.GetString(1);
        foundEmployeeLastName = rdr.GetString(2);
      }
      Employee foundEmployee = new Employee(foundEmployeeFirstName, foundEmployeeLastName, foundEmployeeId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundEmployee;
    }
  }
}
