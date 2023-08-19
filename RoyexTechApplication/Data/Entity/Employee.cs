using System;
using System.Collections.Generic;

namespace RoyexTechApplication.Data.Entity;

public partial class Employee
{
    public int IntEmployeeId { get; set; }

    public string StrEmployeeName { get; set; } = null!;

    public int? IntManagerId { get; set; }

    public string StrDesignation { get; set; } = null!;

    public decimal? NumSalary { get; set; }

    public DateTime DteJoiningDate { get; set; }

    public bool IsBonusAdded { get; set; }
}
