using ASPNETCRUDO.Data;
using ASPNETCRUDO.Migrations;
using ASPNETCRUDO.Models;
using ASPNETCRUDO.Models.Domain;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCRUDO.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;


        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequiest)
        {
            var Employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequiest.Name,
                Email = addEmployeeRequiest.Email,
                Salary = addEmployeeRequiest.Salary,
                DateOfBirth = addEmployeeRequiest.DateOfBirth,
                Department = addEmployeeRequiest.Department,


            };

            await mvcDemoDbContext.Employees.AddAsync(Employee);
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task< IActionResult> View(Guid id)
        {
            var employee= await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                var viewModel = new UpadateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };
                      return await Task.Run(()=> View("View",viewModel));
            }
           
            return RedirectToAction("Index");
        }

        [HttpPost ]
        public async Task<IActionResult> View(UpadateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpadateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
