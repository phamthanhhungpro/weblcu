using Common;
using Common.Entity;
using Common.Entity.Permission;
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
    public class ProduceToolController : BaseController<ProduceToolController>
    {
        private readonly ProduceToolCategoryService _ProduceToolCategoryService;
        private readonly ProduceToolService _ProduceToolService;
        public ProduceToolController(LogService logService,
            ILogger<ProduceToolController> logger,
            AuthenUtils authenUtils, ProduceToolService ProduceToolService,
            ProduceToolCategoryService ProduceToolCategoryService) : base(logger, authenUtils, logService)
        {
            _ProduceToolService = ProduceToolService;
            _ProduceToolCategoryService = ProduceToolCategoryService;
        }

        // GET: Admin/ProduceTool
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new ProduceToolPermission();
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_DELETE);
                ViewBag.Permission = permission;
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("LoadAjaxData")]
        public JsonResult LoadAjaxData(string type)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            IEnumerable<ProduceTool> list = null;
            if (term != null)
            {
                term = term.ToLower();
            }
            try
            {
                start = Int32.Parse(startParam);
                length = Int32.Parse(lengthParam);

            }
            catch (Exception)
            {
            }
            var totalRecord = 0;
            var total = _ProduceToolService.GetAll().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_VIEW))
            {
                if (!string.IsNullOrEmpty(term))
                {
                    list = _ProduceToolService.GetAll(e => e.Name.ToLower().Contains(term));
                }
                else
                {
                    list = _ProduceToolService.GetAll();
                }
                totalRecord = list.Count();
            }
            var result = new
            {
                recordsTotal = total,
                recordsFiltered = totalRecord,
                aaData = list != null ? BindData(list, start, length) : null
            };
            var jsonResult = Json(result);
            return jsonResult;
        }

        private Object BindData(IEnumerable<ProduceTool> list, int start, int length)
        {
            var result = new List<object>();
            var index = start + 1;

            foreach (var item in list.Skip(start).Take(length))
            {
                result.Add(new
                {
                    item.Id,
                    Index = index,
                    Name = item.Name,
                    Details = item.Details,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        // GET: Admin/ProduceTool/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_ADD))
            {
                ViewBag.ProduceToolCategories = GetListProduceToolCategory();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        private List<SelectListItem> GetListProduceToolCategory()
        {
            Expression<Func<ProduceToolCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            var data = new List<SelectListItem>();
            foreach (var item in _ProduceToolCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(ProduceToolCategory(item));
            }

            return data;
        }

        private List<SelectListItem> ProduceToolCategory(ProduceToolCategory category, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = category.Id.ToString(), Text = prefix + category.Name });
            foreach (var item in category.Childrens)
            {
                lstData.AddRange(ProduceToolCategory(item, prefix + "---"));
            }
            return lstData;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(ProduceToolModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_ADD))
            {
                if (ModelState.IsValid)
                {
                    _ProduceToolService.Add(model);
                    base.SuccessNotification("Thêm mới tin tức thành công !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.ProduceToolCategories = GetListProduceToolCategory();
                    return View(model);
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [Route("Edit")]
        public ActionResult Edit(int id)
        {

            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_EDIT))
            {
                var data = _ProduceToolService.GetById(id);

                if (data != null)
                {
                    ViewBag.ProduceToolCategories = GetListProduceToolCategory();
                    return View(data.ToModel());
                }
                base.ErrorNotification("Không tồn tại tin tức!");
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
        public ActionResult Edit(ProduceToolModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _ProduceToolService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật tin tức thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.ProduceToolCategories = GetListProduceToolCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.ProduceToolCategories = GetListProduceToolCategory();
                    return View(model);
                }

            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PRODUCETOOL_DELETE))
            {
                message = new MessageResult();
                message = _ProduceToolService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công  tin tức";
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);

        }

    }
}
