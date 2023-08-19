using Microsoft.EntityFrameworkCore;
using NodaTime;
using RoyexTechApplication.Data;
using RoyexTechApplication.Data.Entity;
using RoyexTechApplication.Models;

namespace RoyexTechApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext _context) 
        {
            this._context = _context;
        }

        public async Task<EmployeeDetails> GetAllEmployee(int? EmployeeId)
        {
            try
            {

                var empManagerId = _context.Employees.Where(x => x.IntEmployeeId == EmployeeId).Select(a => a.IntManagerId).FirstOrDefault();
                if (empManagerId == null)
                {
                    empManagerId = 0;
                }
                var mainManagerJoinYear = DateTime.Now.Year - 4;
                var isLeapYear = DateTime.IsLeapYear(DateTime.Now.Year);

                List<EmployeeSalaryDetails> Data = await (from e in _context.Employees
                                                        join m in _context.Employees on e.IntManagerId equals m.IntEmployeeId into employeeMgr
                                                        from manager in employeeMgr.DefaultIfEmpty()
                                                        where (e.IntManagerId == empManagerId || e.IntEmployeeId == empManagerId || empManagerId == 0)
                                                        select new EmployeeSalaryDetails
                                                        {
                                                            intEmployeeId = e.IntEmployeeId,
                                                            strEmployeeName = e.StrEmployeeName,
                                                            strDesignation = e.StrDesignation,
                                                            numSalary = e.NumSalary,
                                                            dteJoiningDate = e.DteJoiningDate.ToString("dd/MM/yyyy"),
                                                            BonusAmount =
                                                                        (manager.IntManagerId == null && manager.DteJoiningDate.Year <= mainManagerJoinYear && isLeapYear && e.IsBonusAdded)
                                                                        ? e.NumSalary + 10000
                                                                        :
                                                                        (manager.IntManagerId == null && manager.DteJoiningDate.Year <= mainManagerJoinYear && !isLeapYear && e.IsBonusAdded)
                                                                        ? e.NumSalary + 8000
                                                                        :
                                                                        (manager.IntManagerId == null && manager.DteJoiningDate.Year > mainManagerJoinYear && isLeapYear && e.IsBonusAdded)
                                                                        ? e.NumSalary + 5000
                                                                        :
                                                                        (manager.IntManagerId == null && manager.DteJoiningDate.Year > mainManagerJoinYear && !isLeapYear && e.IsBonusAdded)
                                                                        ? e.NumSalary + 3000
                                                                        :
                                                                        (manager.IntManagerId != null && e.DteJoiningDate < manager.DteJoiningDate && isLeapYear && e.IsBonusAdded)
                                                                        ? e.NumSalary + 12000
                                                                        :
                                                                        (manager.IntManagerId != null && e.DteJoiningDate <= manager.DteJoiningDate && !isLeapYear)
                                                                        ? e.NumSalary + 8000
                                                                        :
                                                                        e.NumSalary,
                                                            ExtraAmount =
                                                                        (e.IntManagerId != null && manager.DteJoiningDate.Year <= mainManagerJoinYear && e.DteJoiningDate.Date < manager.DteJoiningDate.Date && isLeapYear == true && e.IsBonusAdded == true)
                                                                        ? 2000
                                                                        :
                                                                        (e.IntManagerId != null && manager.DteJoiningDate.Year <= mainManagerJoinYear && e.DteJoiningDate.Date < manager.DteJoiningDate.Date && isLeapYear == false)
                                                                        ? 1000
                                                                        :
                                                                        (e.IntManagerId != null && manager.DteJoiningDate.Year < mainManagerJoinYear && e.DteJoiningDate.Date < manager.DteJoiningDate.Date && isLeapYear == true && e.IsBonusAdded == true)
                                                                        ? 1000
                                                                        :
                                                                        (e.IntManagerId != null && manager.DteJoiningDate.Year < mainManagerJoinYear && e.DteJoiningDate.Date < manager.DteJoiningDate.Date && isLeapYear == false && e.IsBonusAdded == true)
                                                                        ? 500
                                                                        :
                                                                        0
                                                        }).ToListAsync();
                EmployeeDetails obj = new EmployeeDetails();
                obj.EmployeeSalaryDetails = Data;
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
