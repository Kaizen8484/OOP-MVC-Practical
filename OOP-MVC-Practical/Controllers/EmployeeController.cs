using OOP_MVC_Practical.Data;
using OOP_MVC_Practical.Models;
using OOP_MVC_Practical.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace OOP_MVC_Practical.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly mvcDbContext mvcDbContext;

        public EmployeeController(mvcDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        // Manually generate next employee id
        private int NextEmployeeId()
        {
            int max = mvcDbContext.Employees.Max(e => (int?)e.Id) ?? 0;

            return ++max;
        }

        // Manually generate next employee id
        private int NextDepartmentId()
        {
            int max = mvcDbContext.Departments.Max(d => (int?)d.Id) ?? 0;                                                                                                                                                                                                   

            return ++max;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await mvcDbContext.Employees.ToListAsync();

            return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = NextEmployeeId(),
                    Name = model.Name,
                    Age = model.Age,
                };

                await mvcDbContext.Employees.AddAsync(employee);
                await mvcDbContext.SaveChangesAsync();

                TempData["Correct_Message"] = "You have added new employee.";
                return RedirectToAction("Index");
            }

            TempData["Error_Message"] = "Please fill all the fields.";
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await mvcDbContext.Employees.FirstOrDefaultAsync(s => s.Id == id);

            if (employee != null)
            {
                var updateVM = new UpdateEmployeeViewModel()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                };

                return await Task.Run(() => View("View", updateVM));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var staff = await mvcDbContext.Employees.FindAsync(model.Id);

            if (staff != null)
            {
                staff.Name = model.Name;
                staff.Age = model.Age;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var staff = await mvcDbContext.Employees.FindAsync(model.Id);

            if (staff != null)
            {
                mvcDbContext.Employees.Remove(staff);
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewAge()
        {
            // Find the youngest employee
            var youngest = mvcDbContext.Employees.OrderBy(e => e.Age).FirstOrDefault();

            // Find the eldest employee
            var eldest = mvcDbContext.Employees.OrderByDescending(e => e.Age).FirstOrDefault();

            return View(new FindAgeEmployeeViewModel
            {
                Youngest = youngest,
                Eldest = eldest
            });
        }

    }
}
