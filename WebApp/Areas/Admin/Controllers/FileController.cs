using Azure;
using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using elFinder.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Services;
using System;
using System.Linq.Expressions;
using System.Text;
using WebApp.Common;
using File = Datas.Models.DomainModels.File;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class FileController : BaseController<FileController>
    {
        private readonly FileService _fileService;
        private readonly CompanyService _companyService;
        private readonly DepartmentService _departmentService;
        private readonly UserService _userService;
        private IWebHostEnvironment _hostEnvironment;
        private readonly ViewFileUtils _viewFileUtils;
        public FileController(LogService logService, ILogger<FileController> logger, AuthenUtils authenUtils , FileService fileService,CompanyService companyService,DepartmentService departmentService, UserService userService, IWebHostEnvironment environment, ViewFileUtils viewFileUtils) : base(logger, authenUtils, logService)
        {
            _fileService = fileService;
            _companyService = companyService;
            _departmentService = departmentService;
            _userService = userService;
            _hostEnvironment = environment;
            _viewFileUtils = viewFileUtils;
        }

        private void CreateListSelectUser(bool isView = false)
        {
            var lstShareUser = new List<PermissionMenu>();
            var lstCompany = _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue, "Childrens", "CompanyUsers").ToList();
            foreach (var item in lstCompany)
            {
                lstShareUser.Add(CreateSelectDataByCompany(item, isView));
            }
            var lstDepartment = _departmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue && !o.DepartmentCompanyId.HasValue, "Childrens").ToList();
            foreach (var item in lstDepartment)
            {
                lstShareUser.Add(CreateSelectDataByDepartment(item, isView));
            }
            var lstUser = _userService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.UserCompanyId.HasValue && !o.DepartmentCompanyId.HasValue).ToList();
            if (lstUser.Count > 0)
            {
                var shareCompany = new PermissionMenu
                {
                    key = -1,
                    title = "Khác",
                    children = new List<PermissionMenu>()
                };
                foreach (var user in lstUser)
                {
                    shareCompany.children.Add(new PermissionMenu
                    {
                        type = 2,
                        key = user.Id,
                        title = user.FullName + " (" + user.UserName + ")",
                        checkbox = true,
                        unselectable = isView
                    });
                }
                lstShareUser.Add(shareCompany);
            }
            ViewBag.UserShare = JsonConvert.SerializeObject(lstShareUser);
        }

        private PermissionMenu CreateSelectDataByCompany(Company company, bool isView = false)
        {
            var lstShare = new List<PermissionMenu>();
            var shareCompany = new PermissionMenu
            {
                key = company.Id,
                title = company.Name
            };
            var lstCompany = company.Childrens.Where(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal);
            foreach (var item in lstCompany)
            {
                lstShare.Add(CreateSelectDataByCompany(item, isView));
            }
            var lstDepartment = company.Departments.Where(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal).ToList();
            foreach (var item in lstDepartment)
            {
                lstShare.Add(CreateSelectDataByDepartment(item, isView));
            }
            foreach (var user in company.CompanyUsers.Where(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal && !o.DepartmentCompanyId.HasValue))
            {
                lstShare.Add(new PermissionMenu
                {
                    type = 2,
                    key = user.Id,
                    title = user.FullName + " (" + user.UserName + ")",
                    checkbox = true,
                    unselectable = isView
                });
            }
            shareCompany.children = lstShare;
            return shareCompany;
        }

        private PermissionMenu CreateSelectDataByDepartment(Department department, bool isView = false)
        {
            var lstShare = new List<PermissionMenu>();
            var shareCompany = new PermissionMenu
            {
                type = 1,
                key = department.Id,
                title = department.Name
            };
            var lstDepartment = department.Childrens.Where(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal);
            foreach (var item in lstDepartment)
            {
                lstShare.Add(CreateSelectDataByDepartment(item, isView));
            }
            foreach (var user in department.DepartmentUsers.Where(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal))
            {
                lstShare.Add(new PermissionMenu
                {
                    type = 2,
                    key = user.Id,
                    title = user.FullName + " (" + user.UserName + ")",
                    checkbox = true,
                    unselectable = isView
                });
            }
            shareCompany.children = lstShare;
            return shareCompany;
        }

        private Object BindData(IEnumerable<File> list, int start, int length)
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
                    FilePath = item.FilePath,
                    FileName = string.IsNullOrEmpty(item.FilePath) ? "" : (item.FilePath.Contains("/") ? item.FilePath.Substring(item.FilePath.LastIndexOf("/") + 1) : (item.FilePath.Contains(@"\") ? item.FilePath.Substring(item.FilePath.LastIndexOf(@"\") + 1) : item.FilePath)),
                    Details = item.Details,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        private void LoadFile(string filePath, int id =0)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = _hostEnvironment.ContentRootPath + filePath;
                var baseFolder = "/AppData/files/" + UserData.Id;
                if (filePath.IndexOf(baseFolder) == 0)
                {
                    if (System.IO.File.Exists(path))
                    {
                        var fileExtension = filePath.Substring(filePath.LastIndexOf("."));
                        if((new List<string>{ ".jpg", ".jpeg", ".png", ".gif",".pdf" }).Contains(fileExtension))
                        {
                            var fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                            var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                            ViewBag.Mime = mine;
                            ViewBag.FileData = Convert.ToBase64String(System.IO.File.ReadAllBytes(path));
                        }  
                        else if ((new List<string> { ".doc", ".docx", ".xls", ".xlsx" }).Contains(fileExtension))
                        {
                            if(id> 0 || id==-1)
                            {
                                var newFile = new ViewFile { FilePath = path, Id = id, Key = Guid.NewGuid().ToString() };
                                _viewFileUtils.Add(newFile);
                                ViewBag.ViewExtent = true;
                                ViewBag.Host = Request.Scheme + "://" + Request.Host;
                                ViewBag.Action = "/Admin/File/ReadFileByKey/" + newFile.Key;
                            }    

                        }
                    }
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;
            if(!actionDescriptor.ActionName.Equals("ReadFileByKey") || !actionDescriptor.ActionName.Equals("ReadFileByPublic"))
            {
                base.OnActionExecuting(filterContext);
            }    
        }

        [Route("LoadAjaxNew")]
        public JsonResult LoadAjaxNew(string access)
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            IEnumerable<File> list = null;
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
            var a = 0;
            if (!string.IsNullOrEmpty(access))
                int.TryParse(access, out a);
            var totalRecord = 0;
            var total = _fileService.GetAll().Count();
            var currentUserId = UserData.Id.ToString();
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_VIEW))
            {
                Expression<Func<File, bool>> expression = o => (o.Owner == UserData.Id || o.Access == Enums.AccessStatus.Public || (o.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(o.AccessData) && o.AccessData.Contains(currentUserId)));
                if(a==1)
                {
                    expression = o => o.Owner == UserData.Id || (o.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(o.AccessData) && o.AccessData.Contains(currentUserId));
                }    
                else if(a>1)
                {
                    expression = o => o.Owner != UserData.Id && (o.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(o.AccessData) && o.AccessData.Contains(currentUserId));
                }    
                if (!CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT) || !CheckFunctionPermission(Constants.PERMISSION_FILE_DELETE))
                {
                    expression = expression.AndAlso1(e => e.Status == Enums.ActiveStatus.Active);
                }
                if (!string.IsNullOrEmpty(term))
                {
                    expression = expression.AndAlso1(e => e.Name.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Details) && e.Details.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Content) && e.Content.ToLower().Contains(term)));
                }
                list = _fileService.GetAll(expression);
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

        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new FilePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_VIEW))
            {
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_FILE_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_FILE_DELETE);
                ViewBag.Permission = permission;
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_ADD))
            {
                ViewBag.Host = Request.Scheme + "://" + Request.Host;
                ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                CreateListSelectUser();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(FileModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_ADD))
            {
                if (ModelState.IsValid)
                {
                    CreateListSelectUser();
                    model.Owner = UserData.Id;
                    _fileService.Add(model);
                    base.SuccessNotification("Thêm mới tài liệu thành công !");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Host = Request.Scheme + "://" + Request.Host;
                    ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                    CreateListSelectUser();
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                    return View(model);
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [Route("View")]
        public ActionResult View(int id)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT))
            {
                var permission = new FilePermission();
                permission.IsView = true;
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_FILE_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_FILE_DELETE);
                ViewBag.Permission = permission;
                var data = _fileService.GetById(id);

                if (data != null)
                {
                    if (data.Owner == UserData.Id || data.Access == Enums.AccessStatus.Public || (data.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(data.AccessData) && data.AccessData.Contains("|" + UserData.Id.ToString() + "|")))
                    {
                        ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                        LoadFile(data.FilePath, id);
                        CreateListSelectUser(true);
                        return View(data.ToFileModel());
                    }
                    else
                    {
                        base.ErrorNotification("Tài khoản không được cấp quyền này");
                        return RedirectToAction("Index", "Default");
                    }
                }
                base.ErrorNotification("Không tồn tại tài liệu!");
                return RedirectToAction(nameof(Index));
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
            ViewBag.Host = Request.Scheme + "://" + Request.Host;
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT))
            {
                var data = _fileService.GetById(id);

                if (data != null)
                {
                    if (data.Owner == UserData.Id  || data.Access == Enums.AccessStatus.Public || (data.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(data.AccessData) && data.AccessData.Contains("|" + UserData.Id.ToString() + "|")))
                    {
                        ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                        LoadFile(data.FilePath,id);
                        CreateListSelectUser();
                        return View(data.ToFileModel());
                    }
                    else
                    {
                        base.ErrorNotification("Tài khoản không được cấp quyền này");
                        return RedirectToAction("Index", "Default");
                    }
                }
                base.ErrorNotification("Không tồn tại tài liệu!");
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
        [AutoValidateAntiforgeryToken]
        public ActionResult Edit(FileModel model)
        {
            ViewBag.Host = Request.Scheme + "://" + Request.Host;
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_EDIT))
            {
                var data = _fileService.GetById(model.Id);
                if (data.Owner == UserData.Id || data.Access == Enums.AccessStatus.Public || (data.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(data.AccessData) && data.AccessData.Contains("|" + UserData.Id.ToString() + "|")))
                {

                    if (ModelState.IsValid)
                    {
                        var result = _fileService.Update(model);
                        if (result.IsSuccess())
                        {
                            base.SuccessNotification("Cập nhật tài liệu thành công");
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                            CreateListSelectUser();
                            LoadFile(model.FilePath);
                            base.ErrorNotification(result.Message);
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.BaseFolder = "/AppData/files/" + UserData.Id;
                        CreateListSelectUser();
                        ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
                        return View(model);
                    }
                }
                else
                {
                    base.ErrorNotification("Tài khoản không được cấp quyền này");
                    return RedirectToAction("Index", "Default");
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("ReadFileById")]
        public ActionResult ReadFileById(int id)
        {
            var data = _fileService.GetById(id);
            if (data != null)
            {
                if (data.Owner == UserData.Id || data.Access == Enums.AccessStatus.Public || (data.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(data.AccessData) && data.AccessData.Contains("|" + UserData.Id.ToString() + "|")))
                {
                    if(!string.IsNullOrEmpty(data.FilePath))
                    {
                        var path = _hostEnvironment.ContentRootPath + data.FilePath;
                        if (System.IO.File.Exists(path))
                        {
                            var fileExtension = data.FilePath.Substring(data.FilePath.LastIndexOf("."));
                            var fileName = data.FilePath.Substring(data.FilePath.LastIndexOf("/") + 1);
                            var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                            return new FileContentResult(System.IO.File.ReadAllBytes(path), mine) { FileDownloadName = fileName };
                        }    
                    }    
                }
            }
            base.ErrorNotification("Không tìm thấy tài liệu tương ứng");
            return View("ErrorFile");
        }

        [Route("ReadFileById1/{id}")]
        public ActionResult ReadFileById1(int id)
        {
            var data = _fileService.GetById(id);
            if (data != null)
            {
                if (data.Owner == UserData.Id || data.Access == Enums.AccessStatus.Public || (data.Access == Enums.AccessStatus.Private && !string.IsNullOrEmpty(data.AccessData) && data.AccessData.Contains("|" + UserData.Id.ToString() + "|")))
                {
                    if (!string.IsNullOrEmpty(data.FilePath))
                    {
                        var path = _hostEnvironment.ContentRootPath + data.FilePath;
                        if (System.IO.File.Exists(path))
                        {
                            var fileExtension = data.FilePath.Substring(data.FilePath.LastIndexOf("."));
                            var fileName = data.FilePath.Substring(data.FilePath.LastIndexOf("/") + 1);
                            var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                            MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path));
                            return new FileStreamResult(ms, mine);
                        }
                    }
                }
            }
            base.ErrorNotification("Không tìm thấy tài liệu tương ứng");
            return View("ErrorFile");
        }

        [Route("ReadFileByKey/{key}")]
        public ActionResult ReadFileByKey(string key)
        {
            var viewFile = _viewFileUtils.Remove(key);
            if (viewFile != null)
            {
                if(viewFile.Id >0)
                {
                    var data = _fileService.GetById(viewFile.Id);
                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.FilePath))
                        {
                            var path = _hostEnvironment.ContentRootPath + data.FilePath;
                            if (System.IO.File.Exists(path))
                            {
                                var fileExtension = data.FilePath.Substring(data.FilePath.LastIndexOf("."));
                                var fileName = data.FilePath.Substring(data.FilePath.LastIndexOf("/") + 1);
                                var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                                MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path));
                                return new FileStreamResult(ms, mine);
                            }
                        }
                    }
                }
                else
                {
                    string filePath = viewFile.FilePath;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileExtension = filePath.Substring(filePath.LastIndexOf("."));
                            if ((new List<string> { ".doc", ".docx", ".xls", ".xlsx" }).Contains(fileExtension))
                            {
                                var fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                                var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                                MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
                                return new FileStreamResult(ms, mine);
                            }
                        }
                    }
                }
                
            }
           
            base.ErrorNotification("Không tìm thấy tài liệu tương ứng");
            return View("ErrorFile");
        }

        [Route("ReadFileByPath")]
        [HttpPost]
        public ActionResult ReadFileByPath(string filePath)
        {
            LoadFile(filePath,-1);
            return PartialView("_ViewFile");
        }

        [Route("ReadFileByPublic")]
        [HttpPost]
        public ActionResult ReadFileByPublic(string filePath)
        {
            ViewBag.ViewExtent = true;
            ViewBag.Host = Request.Scheme + "://" + Request.Host;
            ViewBag.Action = filePath;
            return PartialView("_ViewFile");
        }

        [Route("ReadFileByPath1/{filePath}")]
        [HttpPost]
        public ActionResult ReadFileByPath1(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = _hostEnvironment.ContentRootPath + filePath;
                var baseFolder = "/AppData/files/" + UserData.Id;
                if (filePath.IndexOf(baseFolder) == 0)
                {
                    if (System.IO.File.Exists(path))
                    {
                        var fileExtension = filePath.Substring(filePath.LastIndexOf("."));
                        if ((new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".pdf" ,".doc" ,".docx" ,".xls" ,".xlsx" }).Contains(fileExtension))
                        {
                            var fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                            var mine = MimeTypeListUtils.Instance().GetType(fileExtension);
                            MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path));
                            return new FileStreamResult(ms, mine);
                        }
                    }
                }
            }
            base.ErrorNotification("Không tìm thấy tài liệu tương ứng");
            return View("ErrorFile");
        }

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_FILE_DELETE))
            {
                var data = _fileService.GetById(id);
                if (data != null)
                {
                    if (data.Owner == UserData.Id)
                    {
                        message = _fileService.Delete(id);
                    }
                    else
                    {
                        message.Code = Enums.ErrorCode.OtherNotPermisson;
                    }
                }
                else
                    message.Code = Enums.ErrorCode.OtherNotExit;
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);
        }
    }
}
