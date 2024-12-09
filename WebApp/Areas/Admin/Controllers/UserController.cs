using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Entity;
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
    public class UserController(LogService logService, ILogger<UserController> logger, AuthenUtils authen, UserService userService, CompanyService companyService, DepartmentService deparmentService, PositionService positisonService) : BaseController<UserController>(logger, authen,logService)
    {
        private UserService _userService = userService;
        private DepartmentService _deparmentService = deparmentService;
        private CompanyService _companyService = companyService;
        private PositionService _positionService = positisonService;

        #region Hiển thị thông tin người dùng 
        [Route("UserInfo")]
        public ActionResult UserInfo()
        {
            var userInfo = Authen.GetSessionUserInfo();
            if (userInfo != null)
            {
                var user = _userService.GetById(userInfo.Id);
                return View(user);
            }
            else
            {
                base.ErrorNotification("Không tồn tại người dùng này! ");
                return RedirectToAction(nameof(UserInfo));
            }
        }

        #endregion

        #region  Danh sách người dùng
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new UserPermission();
            if (CheckFunctionPermission(Constants.PERMISSION_USER_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_USER_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_USER_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_USER_ADD);
                permisson.IsLockUser = CheckFunctionPermission(Constants.PERMISSION_USER_LOCK);
                permisson.IsResetPass = CheckFunctionPermission(Constants.PERMISSION_USER_RESETPASSWORD);
                ViewBag.Permisson = permisson;
                var listUser = new List<User>();
                listUser = _userService.GetAll().ToList();
                return View(listUser);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
        #endregion

        #region Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_USER_ADD))
            {
                GetList();
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
        public ActionResult Create(UserModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_USER_ADD))
            {
                var checkUsername = _userService.GetFirstOrDefault(e => e.UserName.Equals(model.UserName));
                if (ModelState.IsValid)
                {
                    //var checkUsername = _userService.GetByUserName(model.UserName);
                    if (checkUsername == null)
                    {
                        model.Password = "B346E4FFF0B0403DAAA83AEE71F84390";
                        _userService.Add(model);
                        base.SuccessNotification("Thêm tài khoản thành công !");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification("Tài khoản đã tồn tại, Nhập tên tài khoản khác");
                        GetList();
                        GetListCompanySelect(model.CompanyId);
                        return View(model);
                    }
                }
                GetList();
                GetListCompanySelect(model.CompanyId);
                return View(model);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        #endregion

        #region Edit
        [Route("Edit")]
        public ActionResult Edit(int? id)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_USER_EDIT))
            {
                if (id.HasValue)
                {
                    var account = _userService.GetById(id.Value);
                    UserModel userModel = account.ToUserModel();
                    GetList();
                    GetListCompanySelect(userModel.CompanyId);
                    return View(userModel);
                }
                else
                {
                    base.ErrorNotification("Không tìm thấy người dùng");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(UserModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_USER_EDIT))
            {
                var user = _userService.GetFirstOrDefault(e => e.UserName.Equals(model.UserName));
                if (ModelState.IsValid)
                {
                    var result = _userService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật tài khoản thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    GetList();
                    GetListCompanySelect(model.CompanyId);
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

        #region Khóa người dùng
        [HttpPost]
        [Route("LockUser")]
        public JsonResult LockUser(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_USER_LOCK))
            {
                message = _userService.LockUser(id);
            }
            else
            {
                message.Code = Enums.ErrorCode.UserNotPermission;
            }
            return Json(message);
        }
        #endregion

        #region Xóa người dùng
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_USER_DELETE))
            {
                message = _userService.Delete(id);
            }
            else
            {
                message.Code = Enums.ErrorCode.UserNotPermission;
            }
            return Json(message);
        }
        #endregion

        #region Password
        [Route("ChangePassWord")]
        public ActionResult ChangePassWord()
        {
            var userInfo = Authen.GetSessionUserInfo();
            if (userInfo != null)
            {
                return View();
            }
            else
            {
                base.ErrorNotification("Không tồn tại người dùng này! ");
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        [Route("ChangePassWord")]
        public ActionResult ChangePassWord(PasswordModel model)
        {
            var userInfo = Authen.GetSessionUserInfo();
            model.Id = userInfo.Id;
            if (ModelState.IsValid)
            {
                MessageResult message = _userService.ChangePassword(model);
                if (message.IsSuccess())
                {
                    base.SuccessNotification("Thay mật khẩu thành công");
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    base.ErrorNotification(message.Message);
                }
                return View();

            }
            return View();
        }

        [HttpPost]
        [Route("ResetPass")]
        public JsonResult ResetPass(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_USER_RESETPASSWORD))
            {
                message = _userService.ResetPassword(id, "B346E4FFF0B0403DAAA83AEE71F84390");
            }
            else
            {
                message.Code = Enums.ErrorCode.UserNotPermission;
            }
            return Json(message);
        }

        #endregion

        #region Other
        private void GetList()
        {
            ViewBag.ListCompanies = GetListCompany();
            ViewBag.ListDepartment = GetListDepartment();
            ViewBag.ListPosition = GetListPosition();
        }
        [HttpPost]
        [Route("GetListCompanySelect")]
        public PartialViewResult GetListCompanySelect(int? id)
        {
            ViewBag.ListDepartment = GetListDepartment(id);
            return PartialView("_LoadSelect");
        }
        private List<SelectListItem> GetListCompanyByUser(int? companyId = null, int? userId = null)
        {
            var companyData = new List<SelectListItem>();
            if (companyId.HasValue)
            {
                //var companycurent = CheckCompanyUser(null);
                //foreach (var item in _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Id == companycurent && o.Id != companyId.Value).ToList())
                //{
                //    companyData.AddRange(CreateCompany(item, companyId));
                //}
            }
            else
            {
                var companycurent = _companyService.GetFirstOrDefault(o => o.Id.Equals(userId.Value));
                if (companycurent != null && companycurent.Parent == null)
                {
                    companyData.AddRange(CreateCompany(companycurent, companyId));
                }
                else
                {
                    var parent = Authen.GetParentCompany(userId.Value);
                    companyData.AddRange(CreateCompany(parent, companyId));
                }
            }
            return companyData;
        }

        private List<SelectListItem> GetListCompany(int? companyId = null)
        {
            var companyData = new List<SelectListItem>();
            if (companyId.HasValue)
            {
                //var companycurent = CheckCompanyUser(null);
                //foreach (var item in _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Id == companycurent && o.Id != companyId.Value).ToList())
                //{
                //    companyData.AddRange(CreateCompany(item, companyId));
                //}
            }
            else
            {
                foreach (var item in _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null, "Childrens").ToList())
                {
                    companyData.AddRange(CreateCompany(item, companyId));
                }
            }
            return companyData;
        }

        private List<SelectListItem> CreateCompany(Company company, int? companyId, string prefix = "")
        {
            var lstComany = new List<SelectListItem>();
            lstComany.Add(new SelectListItem { Value = company.Id.ToString(), Text = prefix + company.Name });
            if (company.Childrens != null)
            {
                foreach (var item in company.Childrens)
                {
                    var companyChild = _companyService.GetById(item.Id);

                    if (companyId.HasValue && !companyChild.Id.Equals(companyId.Value))
                    {
                        lstComany.AddRange(CreateCompany(companyChild, companyId, prefix + "---"));
                    }
                    else if (!companyId.HasValue)
                    {
                        lstComany.AddRange(CreateCompany(companyChild, companyId, prefix + "---"));
                    }
                }
            }
            return lstComany;
        }
        private List<SelectListItem> GetListDepartment(int? companyId = null, bool op = false)
        {
            var departmentData = new List<SelectListItem>();
            var lstDeparment = new List<Department>();
            if (companyId.HasValue)
            {
                lstDeparment = _deparmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null && o.DepartmentCompany.Id == companyId.Value, "Childrens").ToList();
            }
            else
            {
                lstDeparment = _deparmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null && o.DepartmentCompany == null, "Childrens").ToList();
            }
            foreach (var item in lstDeparment)
            {
                departmentData.AddRange(CreateDepartment(item));
            }
            return departmentData;
        }
        private List<SelectListItem> CreateDepartment(Department department, string prefix = "")
        {
            var lstDepartment = new List<SelectListItem>();
            lstDepartment.Add(new SelectListItem { Value = department.Id.ToString(), Text = prefix + department.Name });
            foreach (var item in department.Childrens)
            {
                lstDepartment.AddRange(CreateDepartment(item, prefix + "---"));
            }
            return lstDepartment;
        }

        private List<SelectListItem> GetListPosition()
        {
            var positionData = new List<SelectListItem>();
            foreach (var item in _positionService.GetAll(o => o.Status == Enums.ActiveStatus.Active))
            {
                positionData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            return positionData;
        }
        #endregion

        [HttpGet]
        [Route("EditUser")]
        public ActionResult EditUser()
        {
            var userInfo = Authen.GetSessionUserInfo();
            if (userInfo !=null)
            {
                var id = userInfo.Id;
                var user = _userService.GetById(id);
                return View(user.ToUserInfoModel());
            }
            else
            {
                base.ErrorNotification("Không tồn tại người dùng này! ");
                return RedirectToAction("UserInfo", "User");
            }

        }
        [HttpPost]
        [Route("EditUser")]
        public ActionResult EditUser(UserInfoModel user)
        {
            if (ModelState.IsValid)
            {
                var userInfo = Authen.GetSessionUserInfo();
                user.Id = userInfo.Id;
                var result = _userService.Update(user);
                if (result.IsSuccess())
                {
                    base.SuccessNotification("Chỉnh sửa thông tin thành công");
                    return RedirectToAction("UserInfo", "User");
                }
                else
                {
                    base.ErrorNotification(result.Message);
                    return RedirectToAction("UserInfo", "User");
                }

            }
            return View();
        }

        [Route("LogOut")]
        public ActionResult LogOut()
        {
            HttpContext?.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
