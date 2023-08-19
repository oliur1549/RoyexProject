using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyexTechApplication.Data;
using RoyexTechApplication.Data.Entity;
using RoyexTechApplication.Models;
using RoyexTechApplication.Services;
using System.Diagnostics;

namespace RoyexTechApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private IEmployeeService _service;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context, IEmployeeService _service)
        {
            _logger = logger;
            this._context = _context;
            this._service = _service;
        }
        public async Task<IActionResult> Index(int? EmployeeId)
        {
            try
            {
                EmployeeDetails result;
                int? empId = 0;
                if (EmployeeId > 0)
                {
                    empId = _context.Employees.Where(x => x.IntEmployeeId == EmployeeId).Select(a=> a.IntEmployeeId).FirstOrDefault();

                    if(empId == 0)
                    {
                        ViewBag.Message = "Please provide valid employee id.";
                        return View("_PopupMessage");
                    }
                }


                result = await _service.GetAllEmployee(empId);
                if(result.EmployeeSalaryDetails.Count > 0)
                {
                    return View(result);

                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async Task<IActionResult> GetEmployees(int? EmployeeId)
        //{
        //    try
        //    {
        //        List<EmployeeSalaryDetails> result;

        //        result = await _service.GetAllEmployee(EmployeeId);
        //        if (result.Count > 0)
        //        {
        //            return Json(result);

        //        }
        //        else
        //        {
        //            return RedirectToAction("Index");
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}