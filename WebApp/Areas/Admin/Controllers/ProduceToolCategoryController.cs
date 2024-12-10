using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class ProduceToolCategoryController : BaseController<ProduceToolCategoryController>
    {
        private readonly ProduceToolCategoryService _produceToolCategoryService;
        public ProduceToolCategoryController(ILogger<ProduceToolCategoryController> logger,
            AuthenUtils authenUtils,
            LogService logService,
            ProduceToolCategoryService produceToolCategoryService) : base(logger, authenUtils, logService)
        {
            _produceToolCategoryService = produceToolCategoryService;
        }

        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_DELETE);
                ViewBag.Permission = permission;
                var list = _produceToolCategoryService.GetAll().ToList();
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
        public ActionResult Create(ProduceToolCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _produceToolCategoryService.Add(model);
                    base.SuccessNotification("Thêm danh mục công cụ sản xuất thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Thêm danh mục công cụ sản xuất thất bại !");
                return View(model);
            }
            base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD))
            {
                ViewBag.ProduceToolCategories = GetData();
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
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_EDIT))
            {
                var model = _produceToolCategoryService.GetById(id);
                if (model == null)
                {
                    base.ErrorNotification("Danh mục công cụ sản xuất không tồn tại !");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ProduceToolCategories = GetData(id);
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
        public ActionResult Edit(ProduceToolCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    _produceToolCategoryService.Update(model);
                    base.SuccessNotification("Cập nhật danh mục công cụ sản xuất thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Cập nhật danh mục công cụ sản xuất thất bại !");
                ViewBag.ProduceToolCategories = GetData(model.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_DELETE))
            {
                message = _produceToolCategoryService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công danh mục công cụ sản xuất";
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
            Expression<Func<ProduceToolCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (id.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != id.Value);
            }
            var data = new List<SelectListItem>();

            foreach (var item in _produceToolCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(CreateData(item, id));
            }

            return data;
        }

        private List<SelectListItem> CreateData(ProduceToolCategory model, int? id, string prefix = "")
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