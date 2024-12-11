using System.Linq.Expressions;
using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class InstrumentCategoryController : BaseController<InstrumentCategoryController>
    
    {

        private InstrumentCategoryService _instrumentCategoryService;
        public InstrumentCategoryController(LogService logService, ILogger<InstrumentCategoryController> logger, AuthenUtils authenUtils, InstrumentCategoryService instrumentCategoryService) : base(logger, authenUtils, logService)
        {
            _instrumentCategoryService = instrumentCategoryService;
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
                var list = _instrumentCategoryService.GetAll().ToList();
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
        public ActionResult Create(InstrumentCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _instrumentCategoryService.Add(model);
                    base.SuccessNotification("Thêm danh mục nhạc cụ thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                ViewBag.InstrumentCategories = GetListInstrumentCategory();
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
                ViewBag.InstrumentCategories = GetListInstrumentCategory();
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
                var companyinfor = _instrumentCategoryService.GetById(id);

                if (companyinfor != null)
                {
                    ViewBag.InstrumentCategories = GetListInstrumentCategory(companyinfor.Id);
                    return View(companyinfor.ToInstrumentCategoryModel());
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
        public ActionResult Edit(InstrumentCategoryModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _instrumentCategoryService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật đơn vị thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.InstrumentCategories = GetListInstrumentCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
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
                message = _instrumentCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công danh mục nhạc cụ";
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
        private List<SelectListItem> GetListInstrumentCategory(int? newCategoryId = null)
        {
            Expression<Func<InstrumentCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (newCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != newCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _instrumentCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreateInstrumentCategory(item, newCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateInstrumentCategory(InstrumentCategory instrumentCategory, int? instrumentCategoryId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = instrumentCategory.Id.ToString(), Text = prefix + instrumentCategory.Name });
            foreach (var item in instrumentCategory.Childrens)
            {
                if (instrumentCategoryId.HasValue && !item.Id.Equals(instrumentCategoryId.Value))
                {
                    lstComany.AddRange(CreateInstrumentCategory(item, instrumentCategoryId, prefix + "---"));
                }
                else if (!instrumentCategoryId.HasValue)
                {
                    lstComany.AddRange(CreateInstrumentCategory(item, instrumentCategoryId, prefix + "---"));
                }
            }
            return lstComany;
        }
        #endregion

    }
}
