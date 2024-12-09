using Azure;
using Common.Entity.Permission;
using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Linq.Expressions;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class DepartmentController : BaseController<DepartmentController>
    {
        private DepartmentService _departmentService;
        private CompanyService _companyService;
        public DepartmentController(LogService logService, ILogger<DepartmentController> logger, AuthenUtils authenUtils, DepartmentService deparmentService, CompanyService companyService) : base(logger, authenUtils, logService)
        {
            _departmentService = deparmentService;
            _companyService = companyService;
        }
        #region 1. List
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_ADD);
                ViewBag.Permission = permisson;
                var listcompany = new List<Company>();
                var list = _departmentService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete, "DepartmentCompany").ToList();
                return View(list);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_ADD))
            {
                ViewBag.ListCompany = GetListCompany();
                ViewBag.Departments = GetListDepartment();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        #endregion
        #region 2. Create
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(DepartmentModel department)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_ADD))
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(department);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(department);
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("GetDepartments")]
        public ActionResult GetDepartments(int? id)
        {
            var message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_EDIT))
            {
                if (id.HasValue && id.Value > 0)
                {
                    ViewBag.Departments = GetListDepartment(id.Value);
                }
                else
                {
                    ViewBag.Departments = GetListDepartment();
                }
                return PartialView("_DisplayDepartment");
            }
            else
            {
                message.Code = Enums.ErrorCode.DepartmentNotPermission;
                Response.StatusCode = 500;
            }
            return Json(message);
        }
        #endregion
        #region 3. Edit
        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_EDIT))
            {
                var department = _departmentService.GetById(id,"Childrens");

                if (department != null)
                {
                    ViewBag.ListCompany = GetListCompany();
                    if (department.DepartmentCompany != null)
                    {
                        ViewBag.Departments = GetListDepartment(department.DepartmentCompany.Id, department.Id);
                    }
                    else
                    {
                        ViewBag.Departments = GetListDepartment();
                    }
                    return View(department.ToDepartmentModel());
                }
                base.ErrorNotification("Không tồn tại đơn vị !");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(DepartmentModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _departmentService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật đơn vị thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ViewBag.ListCompany = GetListCompany();
                    ViewBag.Departments = GetListDepartment(model.CompanyId, model.Id);
                    return View(model);
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        #endregion
        #region 4. Delete
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_DEPARTMENT_DELETE))
            {
                message = _departmentService.Delete(id);
            }
            else
            {
                message.Code = Enums.ErrorCode.DepartmentNotPermission;
            }
            return Json(message);
        }
        #endregion
        #region 5. Get List
        private List<SelectListItem> GetListCompany(int? companyId = null)
        {
            var companycurent = CheckCompanyUser(null);
            Expression<Func<Company, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (companyId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != companyId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _companyService.GetAll("Childrens").Where(query.Compile()))
            {
                companyData.AddRange(CreateCompany(item, companyId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateCompany(Company company, int? companyId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = company.Id.ToString(), Text = prefix + company.Name });
            foreach (var item in company.Childrens)
            {
                if (companyId.HasValue && !item.Id.Equals(companyId.Value))
                {
                    lstComany.AddRange(CreateCompany(item, companyId, prefix + "---"));
                }
                else if (!companyId.HasValue)
                {
                    lstComany.AddRange(CreateCompany(item, companyId, prefix + "---"));
                }
            }
            return lstComany;
        }
        private List<SelectListItem> GetListDepartment(int? companyId = null, int? departmentId = null)
        {
            var departmentData = new List<SelectListItem>();
            var lstDeparment = new List<Department>();
            if (companyId.HasValue)
            {
                if (departmentId.HasValue)
                {
                    companyId = CheckCompanyUser(companyId);
                    lstDeparment = _departmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null && o.DepartmentCompany.Id == companyId.Value && o.Id != departmentId.Value, "Childrens").ToList();
                }
                else
                {
                    companyId = CheckCompanyUser(companyId);
                    lstDeparment = _departmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null && o.DepartmentCompany.Id == companyId.Value, "Childrens").ToList();
                }
            }
            foreach (var item in lstDeparment)
            {
                departmentData.AddRange(CreateDepartment(item));
            }
            if (departmentId.HasValue)
            {
                var check = departmentData.FirstOrDefault(x => x.Value == departmentId.Value.ToString());
                if (check != null)
                {
                    departmentData.Remove(check);
                }
            }
            return departmentData;
        }
        private List<SelectListItem> CreateDepartment(Department department, string prefix = "")
        {
            var lstDepartment = new List<SelectListItem>();
            lstDepartment.Add(new SelectListItem { Value = department.Id.ToString(), Text = prefix + department.Name });
            if(department.Childrens !=null)
            {

            }    
            foreach (var item in department.Childrens)
            {
                lstDepartment.AddRange(CreateDepartment(item, prefix + "---"));
            }
            return lstDepartment;
        }


        #endregion
    }
}
