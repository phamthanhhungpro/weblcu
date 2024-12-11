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
    public class InstrumentController : BaseController<InstrumentController>
    {
        private InstrumentCategoryService _instrumentCategoryService;
        private InstrumentService _instrumentService;
        public InstrumentController(LogService logService, ILogger<InstrumentController> logger, AuthenUtils authenUtils, InstrumentService instrumentService, InstrumentCategoryService instrumentCategoryService) : base(logger, authenUtils, logService)
        {
            _instrumentService = instrumentService;
            _instrumentCategoryService = instrumentCategoryService;
        }

        // GET: Admin/Instrument
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new InstrumentPermission();
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_NEWS_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_NEWS_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_NEWS_DELETE);
                ViewBag.Permission = permission;
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("LoadAjaxNew")]
        public JsonResult LoadAjaxNew(string type)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            IEnumerable<Instrument> list = null;
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
            var total = _instrumentService.GetAll().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_VIEW))
            {
                if (!string.IsNullOrEmpty(term))
                {
                    list = _instrumentService.GetAll(e => e.Content.ToLower().Contains(term) || e.Name.ToLower().Contains(term));
                }
                else
                {
                    list = _instrumentService.GetAll();
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

        private Object BindData(IEnumerable<Instrument> list, int start, int length)
        {
            var result = new List<object>();
            var index = start + 1;

            foreach (var item in list.Skip(start).Take(length))
            {
                result.Add(new
                {
                    item.Id,
                    Index = index,
                    PostDate = item.PostDate.ToString("dd/MM/yyyy"),
                    Name = item.Name,
                    Details = item.Details,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        // GET: Admin/Instrument/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_ADD))
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

        private List<SelectListItem> GetListInstrumentCategory()
        {
            Expression<Func<InstrumentCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            var data = new List<SelectListItem>();
            foreach (var item in _instrumentCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(InstrumentCategory(item));
            }

            return data;
        }

        private List<SelectListItem> InstrumentCategory(InstrumentCategory instrumentCategory, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = instrumentCategory.Id.ToString(), Text = prefix + instrumentCategory.Name });
            foreach (var item in instrumentCategory.Childrens)
            {
                lstData.AddRange(InstrumentCategory(item, prefix + "---"));
            }
            return lstData;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(InstrumentModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_ADD))
            {
                if (ModelState.IsValid)
                {
                    _instrumentService.Add(model);
                    base.SuccessNotification("Thêm mới nhạc cụ thành công !");
                    return RedirectToAction(nameof(Index));
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

        [Route("Edit")]
        public ActionResult Edit(int id)
        {

            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_EDIT))
            {
                var data = _instrumentService.GetById(id);

                if (data != null)
                {
                    ViewBag.InstrumentCategories = GetListInstrumentCategory();
                    return View(data.ToInstrumentModel());
                }
                base.ErrorNotification("Không tồn tại nhạc cụ!");
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
        public ActionResult Edit(InstrumentModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _instrumentService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật nhạc cụ thành công");
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

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_DELETE))
            {
                message = new MessageResult();
                message = _instrumentService.Delete(id);
                if (message.IsSuccess())
                {
                    message.Message = "Xóa thành công  nhạc cụ";
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
