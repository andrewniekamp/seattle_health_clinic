using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SeattleHealthClinic
{
  public class CriticalPatient :Patient
  {
    // private int _id;
    // private string _name;
    // private string _address;
    private int _frequency;

    public  CriticalPatient(string name, string Address, int frequency,int id = 0) : base(name,Address,id)
    {
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

    // public int GetId()
    // {
    //   return _id;
    // }
    //
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


  }
}
