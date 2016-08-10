using System;
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
    internal string  _employee_salary_type;
    internal string  _employee_email;
    internal string  _employee_password;
    internal DateTime  _employee_date_hire = new DateTime();
    // constructors, getters, setters
    public Employee(string firstName, string lastName, string SSN, string employeeType, string salaryType, string email, string password, DateTime hireDate, int Id = 0)
    {
      _employee_name_first = firstName;
      _employee_name_last = lastName;
      _employee_ssn = SSN;
      _employee_type = employeeType;
      _employee_salary_type = salaryType;
      _employee_email = email;
      _employee_password = password;
      _employee_date_hire = hireDate;
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
    public string GetFullName()
    {
      return _employee_name_first + " " + _employee_name_last;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetSSN(string ssn)
    {
      _employee_ssn = ssn;
    }
    public string GetSSN()
    {
      return _employee_ssn;
    }
    public void SetHireDate(DateTime hireDate)
    {
      _employee_date_hire = hireDate;
    }
    public DateTime GetHireDate()
    {
      return _employee_date_hire;
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
    public string GetEmployeeType()
    {
      return  _employee_type;
    }
    public DateTime GetDateHire()
    {
      return _employee_date_hire;
    }
    public string GetSalaryType()
    {
      return _employee_salary_type;
    }
    // other methods
    // a method to save an employee to the database
    public void Save(string tableName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string sql = "INSERT INTO " + tableName + " (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_salary_type, employee_email, employee_password, employee_date_hire) OUTPUT INSERTED.id VALUES (@EmployeeNameFirst, @EmployeeNameLast, @SSN, @EmployeeType, @SalaryType, @Email, @Password, @HireDate);";
      SqlCommand cmd = new SqlCommand(sql, conn);
      cmd.Parameters.Add("EmployeeNameFirst", SqlDbType.VarChar).Value = this.GetFirstName();
      cmd.Parameters.Add("EmployeeNameLast", SqlDbType.VarChar).Value = this.GetLastName();
      cmd.Parameters.Add("SSN", SqlDbType.VarChar).Value = this.GetSSN();
      cmd.Parameters.Add("EmployeeType", SqlDbType.VarChar).Value = this.GetEmployeeType();
      cmd.Parameters.Add("SalaryType", SqlDbType.VarChar).Value = this.GetSalaryType();
      cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = this.GetEmail();
      cmd.Parameters.Add("Password", SqlDbType.VarChar).Value = this.GetPassword();
      cmd.Parameters.Add("HireDate", SqlDbType.DateTime).Value = this.GetHireDate();

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
      int id = 0;
      string firstName = null;
      string lastName = null;
      string ssn = null;
      string employeeType = null;
      string salaryType = null;
      string email = null;
      string password = null;
      DateTime  hireDate = new DateTime();
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        firstName = rdr.GetString(1);
        lastName = rdr.GetString(2);
        ssn = rdr.GetString(3);
        employeeType = rdr.GetString(4);
        hireDate = rdr.GetDateTime(5);
        salaryType = rdr.GetString(6);
        email = rdr.GetString(9);
        password = rdr.GetString(10);
      }
      Employee foundEmployee = new Employee(firstName, lastName, ssn, employeeType, salaryType, email, password, hireDate, id);

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
    public static List<Employee> GetAll(string tableName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string sql = "Select * from " + tableName + ";";
      SqlCommand cmd = new SqlCommand(sql, conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Employee> allEmployees = new List<Employee>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string firstName = rdr.GetString(1);
        string lastName = rdr.GetString(2);
        string ssn = rdr.GetString(3);
        string employeeType = rdr.GetString(4);
        DateTime hireDate = rdr.GetDateTime(5);
        string salaryType = rdr.GetString(6);
        string email = rdr.GetString(7);
        string password = rdr.GetString(8);
        Employee newEmployee = new Employee(firstName, lastName, ssn, employeeType, salaryType, email, password,  hireDate, id);
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
    public static void DeleteAll(string tableName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string sql = "DELETE from " + tableName + ";";
      SqlCommand cmd = new SqlCommand(sql, conn);
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
      SqlCommand cmd = new SqlCommand("SELECT employee_email FROM employees WHERE employees.employee_email = @EmployeeEmail AND employees.employee_password = @EmployeePassword;", conn);
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
    public static Employee FindEmail(string email)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE employees.employee_email = @EmployeeEmail;", conn);
      SqlParameter employeeEmailParameter = new SqlParameter();
      employeeEmailParameter.ParameterName = "@EmployeeEmail";
      employeeEmailParameter.Value = email;
      cmd.Parameters.Add(employeeEmailParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      int id = 0;
      string firstName = null;
      string lastName = null;
      string ssn = null;
      string employeeType = null;
      string salaryType = null;
      string email2 = null;
      string password = null;
      DateTime  hireDate = new DateTime();
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        firstName = rdr.GetString(1);
        lastName = rdr.GetString(2);
        ssn = rdr.GetString(3);
        employeeType = rdr.GetString(4);
        hireDate = rdr.GetDateTime(5);
        salaryType = rdr.GetString(6);
        email2 = rdr.GetString(7);
        password = rdr.GetString(8);
      }
      Employee foundEmployee = new Employee(firstName, lastName, ssn, employeeType, salaryType, email2, password, hireDate, id);

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
