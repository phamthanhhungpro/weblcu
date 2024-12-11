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

        private readonly InstrumentCategoryService _instrumentCategoryService;
        public InstrumentCategoryController(ILogger<InstrumentCategoryController> logger,
            AuthenUtils authenUtils,
            LogService logService,
            InstrumentCategoryService instrumentCategoryService) : base(logger, authenUtils, logService)
        {
            _instrumentCategoryService = instrumentCategoryService;
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
                var list = _instrumentCategoryService.GetAll().ToList();
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
        public ActionResult Create(InstrumentCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD))
            {
                if (ModelState.IsValid)
                {
                    _instrumentCategoryService.Add(model);
                    base.SuccessNotification("Thêm danh mục nhạc cụ thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Thêm danh mục nhạc cụ thất bại !");
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
                ViewBag.InstrumentCategories = GetData();
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
                var model = _instrumentCategoryService.GetById(id);
                if (model == null)
                {
                    base.ErrorNotification("Danh mục nhạc cụ không tồn tại !");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.InstrumentCategories = GetData(id);
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
        public ActionResult Edit(InstrumentCategory model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_CATEGORY_EDIT))
            {
                if (ModelState.IsValid)
                {
                    _instrumentCategoryService.Update(model);
                    base.SuccessNotification("Cập nhật danh mục nhạc cụ thành công !");
                    return RedirectToAction(nameof(Index));
                }
                base.ErrorNotification("Cập nhật danh mục nhạc cụ thất bại !");
                ViewBag.InstrumentCategories = GetData(model.Id);
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

        // CRUD method
        private List<SelectListItem> GetData(int? id = null)
        {
            Expression<Func<InstrumentCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (id.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != id.Value);
            }
            var data = new List<SelectListItem>();

            foreach (var item in _instrumentCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(CreateData(item, id));
            }

            return data;
        }

        private List<SelectListItem> CreateData(InstrumentCategory model, int? id, string prefix = "")
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
