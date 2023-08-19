namespace RoyexTechApplication.Models
{
    public class EmployeeDetails
    {
        public string EmployeeID { get; set; }
        public List<EmployeeSalaryDetails> EmployeeSalaryDetails { get; set; }
    }
    public class EmployeeSalaryDetails
    {
        public int intEmployeeId { get; set; }
        public string strEmployeeName { get; set; }
        public string strDesignation { get; set; }
        public decimal? numSalary { get; set; }
        public decimal? BonusAmount { get; set; }
        public decimal? ExtraAmount { get; set; }
        public string dteJoiningDate { get; set; }

    }
}
