using AutoMapper;
using Ikea.BLL.Models.Departments;
using Ikea.BLL.Services.Interfaces;
using Ikea.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ikea.PL.Controllers.Departments
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services

        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _Logger;
        private readonly IWebHostEnvironment _Enviroment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,
                                    ILogger<DepartmentController> Logger,
                                    IWebHostEnvironment Env,
                                    IMapper mapper
                                    )
        {
            _departmentService = departmentService;
            _Logger = Logger;
            _Enviroment = Env;
            _mapper = mapper;
        }
        #endregion


        #region Index

        // pass Data in one Way only but with [HttpPost] we can pass View in two ways [ action => view || view => action  ]
        [HttpGet] // Get : department/Index
        public async Task<IActionResult> Index()
        {
            // View's Dictionary : PAss Data From Controller [Action] to View (from view => [partialView , Layout ])

            /// 1. ViewData [ Require Type casting ]: is a Property of type Dictionary introduced in (Asp.Net FrameWork 3.5)
            ///            => it helps us to transfere data from [ action ] to  [ View ]
            ViewData["Message"] = "Hello Viewdata";

            /// 2. ViewBag : is a property of type Dynamic introduced in (Asp.Net Framework 4.5 based on Dunamic Keyword )
            ///            => it helps us to transfere data from [ action ] to  [ View ]

            var departments = await _departmentService.GetAllDepartmentsAsync();

            return View(departments);
        }


        #endregion

        #region Details

        [HttpGet] //Get: Department/Details/id
        public IActionResult Details(int? id)
        {
            // 1. check on id
            if (id is null)
                return BadRequest(); // BuiltIn View 
            // 2. Get Department
            var department = _departmentService.GetDepartmentByIdAsync(id.Value);
            // 3. if deparrment is null return notfound else return View department 
            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Action Filter 
        public async Task<IActionResult> CreateAsync(DepartmentEditViewModel DepartmentToCreate)
        {
            // 1 . validation on ModelState   [ Server Side Validation ]
            if (!ModelState.IsValid)
                return View(DepartmentToCreate);

            var Message = string.Empty;

            try
            {       // Manual Mapping 
                /// var CreatedDepartment = new CreatedDepartmentDto()
                /// {
                ///     Code = DepartmentToCreate.Code,
                ///     Name = DepartmentToCreate.Name,
                ///     Description = DepartmentToCreate.Description,
                ///     CreationDate = DepartmentToCreate.CreationDate,
                /// };
                // Mapping Using AutoMapper
                var CreatedDepartment = _mapper.Map<DepartmentEditViewModel, CreatedDepartmentDto>(DepartmentToCreate);

                var Created = await _departmentService.CreateDepartmentAsync(CreatedDepartment) > 0;

                /// TempData  : is a property of type Dictionary oject 
                ///         => used to  transfere data beween two consective requests 

                if (!Created)
                {
                    Message = "Department isn't Created ";
                    ModelState.AddModelError(string.Empty, Message);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _Logger.LogError(ex, ex.Message);

                //2. Friendly Messsage 
                Message = _Enviroment.IsDevelopment() ? ex.Message : "Department is not created ";
                TempData["Message"] = Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit 

        [HttpGet] //Get: Department/Edit/id
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id is null)
                return BadRequest();//400

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound(); // 404

            // mapping From DepartmentDetailsDto to DepartmentEditViewModel
            else
            {
                var model = _mapper.Map<DepartmentDetailsDto, DepartmentEditViewModel>(department);

                return View(model);
            }

        }


        [HttpPost] //Post : Department/Edit/id?
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync([FromRoute] int id, DepartmentEditViewModel departmentVm)
        {
            if (!ModelState.IsValid)
                return View(departmentVm);
            var Message = string.Empty;
            try
            { //Manual mapping from DepartmentEditViewModel to UpdatedDepartmentDto
                /// var UpdatedDepartment = new UpdatedDepartmentDto()
                /// {
                ///     Code = departmentVm.Code,
                ///     Name = departmentVm.Name,
                ///     Description = departmentVm.Description,
                ///     CreationDate = departmentVm.CreationDate
                /// };

                var UpdatedDepartment = _mapper.Map<DepartmentEditViewModel, UpdatedDepartmentDto>(departmentVm); // 

                var Updated = await _departmentService.UpdateDepartmentAsync(UpdatedDepartment) > 0;

                if (Updated)
                    return RedirectToAction("Index");

                Message = "An Error Has Been Occured During the Update";
            }
            catch (Exception ex)
            {
                // 1. log Exception 

                // 2 . Set Friendly error Messg
                Message = _Enviroment.IsDevelopment() ? ex.Message : "An Error has Been Occured During the Update ";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVm);
        }


        #endregion

        #region Delete 

        [HttpGet] // Get : Department/Delete/id?
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();


            return View(department);
        }

        [HttpPost] // post : Department/Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Message = string.Empty;

            try
            {
                var Department = await _departmentService.DeleteDepartmentAsync(id);

                if (Department)// if Deleted
                    return RedirectToAction("Index");
                else
                    Message = "An Error Has Been Occured During Deleting the Department ";
            }
            catch (Exception ex)
            {
                // 1. log Exception 
                _Logger.LogError(ex, ex.Message);

                // 2 . Set Friendly error Messg
                Message = _Enviroment.IsDevelopment() ? ex.Message : "An Error has Been Occured During the Deleting the Department  ";
            }

            // ModelState.AddModelError(String.Empty, Message); // dah l sec scenario of delete [DeleteModal]
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
