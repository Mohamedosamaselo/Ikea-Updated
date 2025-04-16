using Ikea.BLL.Models.Employees;
using Ikea.BLL.Services.Interfaces;
using Ikea.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ikea.PL.Controllers.Employees
{
    [Authorize]
    public class EmployeeController : Controller
    {


        #region Service

        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _enviroment;
        private readonly ILogger<EmployeeController> _Logger;

        public EmployeeController(IEmployeeService employeeService,
                                    IWebHostEnvironment enviroment,
                                    ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _enviroment = enviroment;
            _Logger = logger;
        }
        #endregion


        #region Index

        [HttpGet] //Get : Employee/Index
        public async Task<IActionResult> Index(string Search)
        {
            var employees =await _employeeService.GetAllEmployeesAsync(Search);

            return View(employees);
        }

        #endregion


        #region Details 

        [HttpGet] // Get : Employee/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            
            var employee =await _employeeService.GetEmployeeByIdAsync(id.Value);
           
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        #endregion


        #region Create 

        [HttpGet] // Get : Employee/Create
        public IActionResult Create()
        {
            // ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost] // post : Employee/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync( /*CreatedEmployeeDto*/ EmployeeEditViewModel employeeToCreate)
        {
            // 1. validate on Model State 
            if (!ModelState.IsValid)
                return View(employeeToCreate);
            var message = string.Empty;
            try
            {// manual mappong From EmployeeEditViewModel to CreatedEmployeeDto
                var createdEmployee = new CreatedEmployeeDto()
                {
                    Name = employeeToCreate.Name,
                    Address = employeeToCreate.Address,
                    EmployeeType = employeeToCreate.EmployeeType,
                    Gender = employeeToCreate.Gender,
                    Email = employeeToCreate.Email,
                    Age = employeeToCreate.Age,
                    HiringDate = employeeToCreate.HiringDate,
                    IsActive = employeeToCreate.IsActive,
                    PhoneNumber = employeeToCreate.PhoneNumber,
                    Salary = employeeToCreate.Salary,
                    DepartmentId = employeeToCreate.DepartmentId
                };
                
                var IsCreated =await _employeeService.CreateEmployeeAsync(createdEmployee);

                if (IsCreated > 0)//if created 
                    return RedirectToAction(nameof(Index));

                else //if not created 
                    message = "Employee is Not Created  ";
            }
            catch (Exception ex)
            {
                // 1. log exception 
                _Logger.LogError(ex, ex.Message);

                // 2. Set Friendly message
                message = _enviroment.IsDevelopment() ? ex.Message : "an error has been occured during employee creation";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employeeToCreate);
        }

        #endregion


        #region Update   
        [HttpGet] // Get : Employee/Edit
        public async Task<IActionResult> EditAsync(int? id)
        {   // 1. check on id if it's null 
            if (id is null)
                return BadRequest();

            // 2. Get employee
            var employee =await _employeeService.GetEmployeeByIdAsync(id.Value);

            // 3. check on employee 
            if (employee is null)
                return NotFound();
            // 4. mapping from EmployeeDetailsDto to EmployeeEditViewModel  
            else
            {

                var employeEditViewModel = new EmployeeEditViewModel()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                };
                return View(employeEditViewModel);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost] // post : Employee/Edit EmloyeeEditViewModel
        public async Task<IActionResult> EditAsync([FromRoute] int id, EmployeeEditViewModel employeeVm)
        {
            //1. check on ModelState validation
            if (!ModelState.IsValid)
                return View(employeeVm);
            var message = "";

            try
            {
                // 2. Mapping from EmployeeEditViewModel to updatedEmployeeDto
                var Employee = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = employeeVm.Name,
                    Age = employeeVm.Age,
                    Salary = employeeVm.Salary,
                    Address = employeeVm.Address,
                    IsActive = employeeVm.IsActive,
                    Email = employeeVm.Email,
                    PhoneNumber = employeeVm.PhoneNumber,
                    HiringDate = employeeVm.HiringDate,
                    Gender = employeeVm.Gender,
                    EmployeeType = employeeVm.EmployeeType,
                };

                var updatedEmployee =await _employeeService.UpdateEmployeeAsync(Employee);

                if (updatedEmployee > 0)
                    return RedirectToAction(nameof(Index));

                message = "An Error has been occured during Update the employee";


            }
            catch (Exception ex)
            {
                // 1. log Exception 
                _Logger.LogError(string.Empty, ex.Message);

                // 2. Set Error message 
                message = _enviroment.IsDevelopment() ? "An Error has been occured during Update the employee" : ex.Message;
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employeeVm);

        }

        #endregion


        #region Delete

        [HttpPost]// post: employee/Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Message = string.Empty;
            try
            {
                var deletedEmployee =await _employeeService.DeleteEmployeeAsync(id);

                if (deletedEmployee)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "an Error has been occured during deleting employee";
            }
            catch (Exception ex)
            {
                // 1. log Exception 
                _Logger.LogError(ex, ex.Message);

                // 2. Set Error message 
                Message = _enviroment.IsDevelopment() ? "An Error has been occured during Update the employee" : ex.Message;

            }
            // ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction("Index");
        }

        #endregion

    }

}

