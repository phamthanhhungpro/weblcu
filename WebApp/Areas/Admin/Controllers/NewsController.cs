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
    public class NewsController : BaseController<NewsController>
    {
        private NewsCategoryService _newsCategoryService;
        private NewsService _newsService;
        public NewsController(LogService logService, ILogger<NewsController> logger, AuthenUtils authenUtils, NewsService newsService, NewsCategoryService newsCategoryService) : base(logger, authenUtils, logService)
        {
            _newsService = newsService;
            _newsCategoryService = newsCategoryService;
        }

        // GET: Admin/News
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new NewsPermission();
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
            IEnumerable<News> list = null;
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
            var total = _newsService.GetAll().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_VIEW))
            {
                if (!string.IsNullOrEmpty(term))
                {
                    list = _newsService.GetAll(e => e.Content.ToLower().Contains(term) || e.Title.ToLower().Contains(term) || e.Author.ToLower().Contains(term));
                }
                else
                {
                    list = _newsService.GetAll();
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

        private Object BindData(IEnumerable<News> list, int start, int length)
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
                    Title = item.Title,
                    Details = item.Details,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        // GET: Admin/News/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_ADD))
            {
                ViewBag.NewsCategories = GetListNewsCategory();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        private List<SelectListItem> GetListNewsCategory()
        {
            Expression<Func<NewsCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            var data = new List<SelectListItem>();
            foreach (var item in _newsCategoryService.GetAll().Where(query.Compile()))
            {
                data.AddRange(NewsCategory(item));
            }

            return data;
        }

        private List<SelectListItem> NewsCategory(NewsCategory newsCategory, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = newsCategory.Id.ToString(), Text = prefix + newsCategory.Name });
            foreach (var item in newsCategory.Childrens)
            {
                lstData.AddRange(NewsCategory(item, prefix + "---"));
            }
            return lstData;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NewsModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_ADD))
            {
                if (ModelState.IsValid)
                {
                    _newsService.Add(model);
                    base.SuccessNotification("Thêm mới tin tức thành công !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.NewsCategories = GetListNewsCategory();
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
                var data = _newsService.GetById(id);

                if (data != null)
                {
                    ViewBag.NewsCategories = GetListNewsCategory();
                    return View(data.ToNewsModel());
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
        public ActionResult Edit(NewsModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_NEWS_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _newsService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật tin tức thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        ViewBag.NewsCategories = GetListNewsCategory();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    ViewBag.NewsCategories = GetListNewsCategory();
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
                message = _newsService.Delete(id);
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
