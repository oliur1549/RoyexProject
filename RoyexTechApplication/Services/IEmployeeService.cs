using RoyexTechApplication.Data.Entity;
using RoyexTechApplication.Models;

namespace RoyexTechApplication.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDetails> GetAllEmployee(int? EmployeeId);
    }
}
