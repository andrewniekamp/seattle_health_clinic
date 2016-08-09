using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SeattleHealthClinic
{
  public class Payroll
  {
    // properties
    internal int _id;
    internal string _payPeriod;
    internal string _salaryType;
    internal string _salaryAmount;
    internal string _employeeId;
    // constructors, getters, setters
    public Payroll(string payperiod, string salarytype, string salaryamount, int Id = 0)
    {
      _payPeriod = payperiod;
      _salaryType = salarytype;
      _salaryAmount = salaryamount;
      _id = Id;
    }
    public string GetPayPeriod()
    {
      return _payPeriod;
    }
    public string GetSalaryType()
    {
      return _salaryType;
    }
    public string GetSalaryAmount()
    {
      return _salaryAmount;
    }
    public int GetId()
    {
      return _id;
    }
    // other methods
    // a method to save payroll information to the database
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO payrolls (pay_period, salary_type, salary_amount) OUTPUT INSERTED.id VALUES (@PayPeriod, @SalaryType, @SalaryAmount);", conn);

      SqlParameter payPeriodParameter = new SqlParameter();
      payPeriodParameter.ParameterName = "@PayPeriod";
      payPeriodParameter.Value = this.GetPayPeriod();
      cmd.Parameters.Add(payPeriodParameter);

      SqlParameter salaryTypeParameter = new SqlParameter();
      salaryTypeParameter.ParameterName = "@SalaryType";
      salaryTypeParameter.Value = this.GetSalaryType();
      cmd.Parameters.Add(salaryTypeParameter);

      SqlParameter salaryAmountParameter = new SqlParameter();
      salaryAmountParameter.ParameterName = "@SalaryAmount";
      salaryAmountParameter.Value = this.GetSalaryAmount();
      cmd.Parameters.Add(salaryAmountParameter);

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
    // a method to return all payroll table records
    public static List<Payroll> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM payrolls ORDER BY salary_amount;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      List<Payroll> allPayrolls = new List<Payroll>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string payrollPayPeriod = rdr.GetString(1);
        string payrollSalaryType = rdr.GetString(2);
        string payrollSalaryAmount = rdr.GetString(3);
        Payroll newPayroll = new Payroll(payrollPayPeriod, payrollSalaryType, payrollSalaryAmount, id);
        allPayrolls.Add(newPayroll);
      }
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return allPayrolls;
    }
    // a method to find payroll information based on an employee ssn
    public Payroll Find(string Id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM payrolls WHERE employee_id = @EmployeeId;", conn);
      SqlParameter employeeIdParameter = new SqlParameter();
      employeeIdParameter.ParameterName = "@EmployeeId";
      employeeIdParameter.Value = Id.ToString();
      cmd.Parameters.Add(employeeIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      int foundPayrollId = 0;
      string foundPayrollPayPeriod = null;
      string foundPayrollSalaryType = null;
      string foundPayrollSalaryAmount = null;
      while(rdr.Read())
      {
        foundPayrollId = rdr.GetInt32(0);
        foundPayrollPayPeriod = rdr.GetString(1);
        foundPayrollSalaryType = rdr.GetString(2);
        foundPayrollSalaryAmount = rdr.GetString(3);
      }
      Payroll foundPayroll = new Payroll(foundPayrollPayPeriod, foundPayrollSalaryType, foundPayrollSalaryAmount, foundPayrollId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPayroll;
    }
    // a method to delete all payroll table records
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM payrolls;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
