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
    public class PeopleCategoryController : BaseController<PeopleCategoryController>
    {

        private PeopleCategoryService _peopleCategoryService;
        public PeopleCategoryController(LogService logService, ILogger<PeopleCategoryController> logger, AuthenUtils authenUtils, PeopleCategoryService peopleCategoryService) : base(logger, authenUtils, logService)
        {
            _peopleCategoryService = peopleCategoryService;
        }
        #region 1.List
        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_DELETE);
                ViewBag.Permission = permission;
                var list = _peopleCategoryService.GetAll().ToList();
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
        public ActionResult Create(PeopleCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _peopleCategoryService.Add(model);
                    base.SuccessNotification("Thêm loại dân tộc thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                ViewBag.PeopleCategories = GetListPeopleCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_ADD))
            {
                ViewBag.PeopleCategories = GetListPeopleCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_EDIT))
            {
                var data = _peopleCategoryService.GetById(id);

                if (data != null)
                {
                    ViewBag.PeopleCategories = GetListPeopleCategory(data.Id);
                    return View(data.ToPeopleCategoryModel());
                }
                base.ErrorNotification("Không tồn tại loại dân tộc !");
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
        public ActionResult Edit(PeopleCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _peopleCategoryService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật loại dân tộc thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.PeopleCategories = GetListPeopleCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.PeopleCategories = GetListPeopleCategory();
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
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_CATEGORY_DELETE))
            {
                message = new MessageResult();
                message = _peopleCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công loại dân tộc";
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
        private List<SelectListItem> GetListPeopleCategory(int? peopleCategoryId = null)
        {
            Expression<Func<PeopleCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (peopleCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != peopleCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _peopleCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreatePeopleCategory(item, peopleCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreatePeopleCategory(PeopleCategory peopleCategory, int? newsCategoryId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = peopleCategory.Id.ToString(), Text = prefix + peopleCategory.Name });
            foreach (var item in peopleCategory.Childrens)
            {
                if (newsCategoryId.HasValue && !item.Id.Equals(newsCategoryId.Value))
                {
                    lstComany.AddRange(CreatePeopleCategory(item, newsCategoryId, prefix + "---"));
                }
                else if (!newsCategoryId.HasValue)
                {
                    lstComany.AddRange(CreatePeopleCategory(item, newsCategoryId, prefix + "---"));
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
