using Common.Entity.Permission;
using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using WebApp.Common;
using Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class NationalCostumeCategoryController : BaseController<NationalCostumeCategoryController>
    {

        private NationalCostumeCategoryService _nationalCostumeCategoryService;
        public NationalCostumeCategoryController(LogService logService, ILogger<NationalCostumeCategoryController> logger, AuthenUtils authenUtils, NationalCostumeCategoryService nationalCostumeCategoryService) : base(logger, authenUtils, logService)
        {
            _nationalCostumeCategoryService = nationalCostumeCategoryService;
        }
        #region 1.List
        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_DELETE);
                ViewBag.Permission = permission;
                var list = _nationalCostumeCategoryService.GetAll().ToList();
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
        public ActionResult Create(NationalCostumeCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _nationalCostumeCategoryService.Add(model);
                    base.SuccessNotification("Thêm loại trang phục dân tộc thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_ADD))
            {
                ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_EDIT))
            {
                var data = _nationalCostumeCategoryService.GetById(id);

                if (data != null)
                {
                    ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory(data.Id);
                    return View(data.ToNationalCostumeCategoryModel());
                }
                base.ErrorNotification("Không tồn tại loại trang phục dân tộc !");
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
        public ActionResult Edit(NationalCostumeCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _nationalCostumeCategoryService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật loại trang phục dân tộc thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory();
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
            if (CheckFunctionPermission(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_DELETE))
            {
                message = new MessageResult();
                message = _nationalCostumeCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công loại trang phục dân tộc";
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
        private List<SelectListItem> GetListNationalCostumeCategory(int? categoryId = null)
        {
            Expression<Func<NationalCostumeCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (categoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != categoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _nationalCostumeCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreateNationalCostumeCategory(item, categoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateNationalCostumeCategory(NationalCostumeCategory nationalCostumeCategory, int? categoryId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = nationalCostumeCategory.Id.ToString(), Text = prefix + nationalCostumeCategory.Name });
            foreach (var item in nationalCostumeCategory.Childrens)
            {
                if (categoryId.HasValue && !item.Id.Equals(categoryId.Value))
                {
                    lstComany.AddRange(CreateNationalCostumeCategory(item, categoryId, prefix + "---"));
                }
                else if (!categoryId.HasValue)
                {
                    lstComany.AddRange(CreateNationalCostumeCategory(item, categoryId, prefix + "---"));
                }
            }
            return lstComany;
        }
        #endregion
        //#region Load goi y 
        //[HttpGet]
        //public JsonResult GetSuggetNewCategory()
        //{
        //    var company = GetCurentCompany();
        //    var list = LoopGetCompany(company);
        //    var newList = new List<SelectListItem>();
        //    var prifix = "";
        //    for (int i = list.Count - 1; i >= 0; i--)
        //    {
        //        newList.Add(new SelectListItem { Value = prifix + list[i].Value, Text = list[i].Text });
        //        prifix += "--";
        //    }
        //    return Json(newList, JsonRequestBehavior.AllowGet);
        //}
        ////private List<SelectListItem> ListCompanySuggest(int id)
        ////{
        ////    var list = new List<SelectListItem>();

        ////    return list;
        ////}
        //private List<SelectListItem> LoopGetCompany(Company company)
        //{
        //    var list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = company.Name, Text = company.Name });
        //    if (company.Parent != null)
        //    {
        //        list.AddRange(LoopGetCompany(company.Parent));
        //    }
        //    return list;
        //}
        //#endregion


    }
}
