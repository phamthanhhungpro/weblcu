using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Linq.Expressions;
using WebApp.Common;
using Common.Entity.Permission;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("[area]/[controller]")]
    public class CompanyController(LogService logService, CompanyService companyService, ILogger<CompanyController> logger, AuthenUtils authenUtils) : BaseController<CompanyController>(logger, authenUtils,logService)
    {
        private readonly ILogger<CompanyController> _logger = logger;
        private readonly CompanyService _companyService = companyService;

        #region 1.List
        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_COMPANY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_COMPANY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_COMPANY_DELETE);
                ViewBag.Permission = permission;
                var list = _companyService.GetAll().ToList();
                return View(list);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
        #endregion
        #region 2.Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _companyService.Add(model);
                    base.SuccessNotification("Thêm đơn vị thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Companies = GetListCompany();
                return View(model);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {

            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_ADD))
            {
                ViewBag.Companies = GetListCompany();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        #endregion
        #region 3. Edit
        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit(int id)
        {

            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_EDIT))
            {
                var companyinfor = _companyService.GetById(id);

                if (companyinfor != null)
                {
                    ViewBag.Companies = GetListCompany(companyinfor.Id);
                    return View(companyinfor.ToCompanyModel());
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
        public ActionResult Edit(CompanyModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _companyService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật đơn vị thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.Companies = GetListCompany();
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Companies = GetListCompany();
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
        #region 5. Delete
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_COMPANY_DELETE))
            {
                message = new MessageResult();
                message = _companyService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công đơn vị";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.CompanyNotPermission;
            }
            return Json(message);

        }
        #endregion
        #region 4.GetList
        private List<SelectListItem> GetListCompany(int? companyId = null)
        {
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
        #endregion
    }
}
