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
    public class NewsCategoryController : BaseController<NewsCategoryController>
    {

        private NewsCategoryService _newsCategoryService;
        public NewsCategoryController(LogService logService, ILogger<NewsCategoryController> logger, AuthenUtils authenUtils, NewsCategoryService newsCategoryService) : base(logger, authenUtils, logService)
        {
            _newsCategoryService = newsCategoryService;
        }
        #region 1.List
        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_DELETE);
                ViewBag.Permission = permission;
                var list = _newsCategoryService.GetAll().ToList();
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
        public ActionResult Create(NewsCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _newsCategoryService.Add(model);
                    base.SuccessNotification("Thêm danh mục tin tức thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                ViewBag.NewCategories = GetListNewCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_ADD))
            {
                ViewBag.NewCategories = GetListNewCategory();
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

            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_EDIT))
            {
                var companyinfor = _newsCategoryService.GetById(id);

                if (companyinfor != null)
                {
                    ViewBag.NewCategories = GetListNewCategory(companyinfor.Id);
                    return View(companyinfor.ToNewsCategoryModel());
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
        public ActionResult Edit(NewsCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _newsCategoryService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật đơn vị thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.NewCategories = GetListNewCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.NewCategories = GetListNewCategory();
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
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_DELETE))
            {
                message = new MessageResult();
                message = _newsCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công danh mục tin tức";
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
        private List<SelectListItem> GetListNewCategory(int? newCategoryId = null)
        {
            Expression<Func<NewsCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (newCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != newCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _newsCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreateNewCategory(item, newCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateNewCategory(NewsCategory newsCategory, int? newsCategoryId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = newsCategory.Id.ToString(), Text = prefix + newsCategory.Name });
            foreach (var item in newsCategory.Childrens)
            {
                if (newsCategoryId.HasValue && !item.Id.Equals(newsCategoryId.Value))
                {
                    lstComany.AddRange(CreateNewCategory(item, newsCategoryId, prefix + "---"));
                }
                else if (!newsCategoryId.HasValue)
                {
                    lstComany.AddRange(CreateNewCategory(item, newsCategoryId, prefix + "---"));
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
