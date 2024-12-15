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
    public class DailyItemCategoryController : BaseController<DailyItemCategoryController>
    
    {

        private readonly DailyItemCategoryService _DailyItemCategoryService;
        public DailyItemCategoryController(ILogger<DailyItemCategoryController> logger,
            AuthenUtils authenUtils,
            LogService logService,
            DailyItemCategoryService DailyItemCategoryService) : base(logger, authenUtils, logService)
        {
            _DailyItemCategoryService = DailyItemCategoryService;
        }

        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_DELETE);
                ViewBag.Permission = permission;
                var list = _DailyItemCategoryService.GetAll().ToList();
                return View(list);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        // CREATE
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(DailyItemCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _DailyItemCategoryService.Add(model);
                    base.SuccessNotification("Thêm danh mục vật dụng hàng ngày thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Thêm danh mục vật dụng hàng ngày thất bại !");
                return View(model);
            }
            base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_ADD))
            {
                ViewBag.DailyItemCategories = GetData();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        // EDIT
        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_EDIT))
            {
                var model = _DailyItemCategoryService.GetById(id);
                if (model == null)
                {
                    base.ErrorNotification("Danh mục vật dụng hàng ngày không tồn tại !");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.DailyItemCategories = GetData(id);
                return View(model.ToModel());
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit(DailyItemCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    _DailyItemCategoryService.Update(model);
                    base.SuccessNotification("Cập nhật danh mục vật dụng hàng ngày thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Cập nhật danh mục vật dụng hàng ngày thất bại !");
                ViewBag.DailyItemCategories = GetData(model.Id);
                return View(model);
            }
            base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Default");
        }

        // DELETE
        [HttpPost]
        [Route("Delete/{id}")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_DAILYITEM_CATEGORY_DELETE))
            {
                message = _DailyItemCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công danh mục vật dụng hàng ngày";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.CompanyNotPermission;
            }
            return Json(message);
        }

        // CRUD method
        private List<SelectListItem> GetData(int? id = null)
        {
            Expression<Func<DailyItemCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (id.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != id.Value);
            }
            var data = new List<SelectListItem>();

            foreach (var item in _DailyItemCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(CreateData(item, id));
            }

            return data;
        }

        private List<SelectListItem> CreateData(DailyItemCategory model, int? id, string prefix = "")
        {
            var data = new List<SelectListItem>();
            data.Add(new SelectListItem { Value = model.Id.ToString(), Text = prefix + model.Name });
            foreach (var item in model.Childrens)
            {
                if (id.HasValue && !item.Id.Equals(id.Value))
                {
                    data.AddRange(CreateData(item, id, prefix + "---"));
                }
                else if (!id.HasValue)
                {
                    data.AddRange(CreateData(item, id, prefix + "---"));
                }
            }
            return data;
        }
    }
}
