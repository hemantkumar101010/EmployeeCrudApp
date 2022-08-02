using EmployeeMVC.Data;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }


        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var employee = await _employeeDbContext.Employees.ToListAsync();
               
            return  View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchEmployee)
        {
            ViewData["EmployeeInfo"] = searchEmployee;
            var empResult = from employee in _employeeDbContext.Employees select employee;
            if (!String.IsNullOrEmpty(searchEmployee))
            {
                empResult = empResult.Where(s => s.FirstName!.Contains(searchEmployee) ||s.City!.Contains(searchEmployee));
            }
            return View(await empResult.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                _employeeDbContext.Employees.Add(employeeViewModel);
                await _employeeDbContext.SaveChangesAsync();
                TempData["AlertMessage"] = $"{employeeViewModel.FirstName}'s record saved successfully...";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();   
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if(employee != null)    
                return View(employee);
            return View("Error");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                _employeeDbContext.Employees.Update(employeeViewModel);
                await _employeeDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if (employee != null)
                return View(employee);

            return View("Error");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeViewModel)
        {
                _employeeDbContext.Employees.Remove(employeeViewModel);
                await _employeeDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

    }
}
