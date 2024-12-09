using Common.Entity;
using Common;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;
using Common.Entity.Permission;
using Newtonsoft.Json;
using Datas.Models.DomainModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("[area]/[controller]")]
    public class RoleController : BaseController<RoleController>
    {
        private RoleService _roleService;
        private FunctionService _functionService;
        private UserService _userService;
        private CompanyService _companyService;
        private DepartmentService _departmentService;
        public RoleController(LogService logService, ILogger<RoleController> logger, AuthenUtils authenUtils, RoleService roleService, FunctionService functionService, UserService userService, CompanyService companyService, DepartmentService departmentService) : base(logger, authenUtils, logService)
        {
            _roleService = roleService;
            _functionService = functionService;
            _userService = userService;
            _companyService = companyService;
            _departmentService = departmentService;
        }
        #region 1. Danh sách quyền
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new RolePermission();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_VIEW))
            {
                permission.IsAdd = CheckFunctionPermission(Constants.PERMISSION_ROLE_ADD);
                permission.IsEdit = CheckFunctionPermission(Constants.PERMISSION_ROLE_EDIT);
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_ROLE_DELETE);
                permission.isAssign = CheckFunctionPermission(Constants.PERMISSION_ROLE_ASSIGN);
                ViewBag.permission = permission;

                var ListRole = _roleService.GetAll().OrderByDescending(o => o.Id);
                return View(ListRole);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        #endregion
        #region 2. Tạo quyền mới
        [Route("Create")]
        public ActionResult Create()
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_ADD))
            {
                ViewBag.ListFunction = _functionService.GetAll(o => o.Status == Enums.ActiveStatus.Active).ToList();
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
        public ActionResult Create(RoleModel roleModel)
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_ADD))
            {
                if (ModelState.IsValid)
                {
                    var name = roleModel.Name;
                    var checkName = _roleService.GetFirstOrDefault(o => o.Name == name);
                    if (checkName == null)
                    {
                        _roleService.Add(roleModel);
                        base.SuccessNotification("Thêm mới nhóm quyền thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ListFunction = _functionService.GetAll(o => o.Status == Enums.ActiveStatus.Active).ToList();
                        base.ErrorNotification("Tên nhóm quyền đã tồn tại");
                        return View();
                    }
                }
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        #endregion

        #region 3. Xóa quyền
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int? id)
        {
            MessageResult message = new MessageResult();
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_DELETE))
            {
                if (id.HasValue && id > 0)
                {
                    message = _roleService.Delete(id.Value);
                    if (message.IsSuccess())
                    {
                        message.Message = "Xóa thành công nhóm quyền";
                    }
                }
                else
                {
                    message.Code = Enums.ErrorCode.RoleNotExist;
                }
            }
            else
            {
                message.Code = Enums.ErrorCode.RoleNotPermission;
            }
            return Json(message);
        }

        #endregion
        #region 4.Chỉnh sửa quyền
        [Route("Edit")]
        public ActionResult Edit(int? id)
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_EDIT))
            {
                if (id.HasValue)
                {
                    var role = _roleService.GetById(id.Value);
                    ViewBag.ListFunction = _functionService.GetAll(o => o.Status == Enums.ActiveStatus.Active).ToList();
                    return View(role.ToRoleModel());
                }
                else
                {
                    base.ErrorNotification("Nhóm quyền không tồn tại");
                }
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(RoleModel model)
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_EDIT))
            {
                if (ModelState.IsValid)
                {
                    MessageResult a = _roleService.Update(model);
                    if (a.IsSuccess())
                    {
                        base.SuccessNotification("Chỉnh sửa nhóm quyền thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(a.Message);
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        #endregion
        #region 5. Phân quyền cho người dùng

        [HttpPost]
        [Route("AssignUser")]
        public ActionResult AssignUser(AssignUserModel model)
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_ASSIGN))
            {
                if (ModelState.IsValid)
                {
                    MessageResult result = _roleService.AssignUser(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Phân quyền thành công");
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                    }
                }
                else
                {
                    base.ErrorNotification("Vui lòng nhập đầy đủ thông tin");
                }
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        [Route("AssignUser")]
        public ActionResult AssignUser(int id)
        {
            var UserInfor = Authen.GetSessionUserInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_ROLE_ASSIGN))
            {
                var model = _roleService.GetById(id).ToAssignUserModel();
                //ViewBag.ListUser = _userService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.UserCompany == null && o.DepartmentCompany == null).ToList();
                //ViewBag.ListCompany = _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null).ToList();
                //ViewBag.ListDepartment = _departmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.Parent == null && o.DepartmentCompany == null).ToList();
                CreateListSelectUser();
                return View(model);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        private void CreateListSelectUser(bool isView = false)
        {
            var lstShareUser = new List<PermissionMenu>();
            var lstCompany = _companyService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue, "Childrens", "CompanyUsers").ToList();
            foreach (var item in lstCompany)
            {
                lstShareUser.Add(CreateSelectDataByCompany(item, isView));
            }
            var lstDepartment= _departmentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue && !o.DepartmentCompanyId.HasValue, "Childrens").ToList();
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
                    type =2,
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
                type =1,
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
                    type =2,
                    key = user.Id,
                    title = user.FullName + " (" + user.UserName + ")",
                    checkbox = true,
                    unselectable = isView
                });
            }
            shareCompany.children = lstShare;
            return shareCompany;
        }

        #endregion


    }
}
