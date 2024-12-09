using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class LoginController(AuthenUtils authenUtils, UserService userService, SettingUtils settingUtils, MailUtils mailUtils, LogService logService, AdminSettingUtils adminSettingUtils) : Controller
    {
        //private readonly ILogger _logger = logger;
        private readonly UserService _userService = userService;
        private readonly SettingUtils _settingUtils = settingUtils;
        private readonly MailUtils _mailUtils = mailUtils;
        private readonly LogService _logService = logService;
        private readonly AdminSettingUtils _adminSettingUtils = adminSettingUtils;

        public AuthenUtils _authen { get; private set; } = authenUtils;

        [Route("")]
        public IActionResult Index()
        {
#if DEBUG
            var result = _userService.Login(new LoginModel { UserName = "admin", Password = "Admin@123" });
            //Response.SetCookie(_authen.CreateAuthenCookie(result.Value.Id, result.Value.UserName));
            HttpContext?.Session.Clear();
            HttpContext?.Session.SetString(Constants.SESSION_USER_INFO, JsonConvert.SerializeObject(result.Value.ToUserInfo()));
            return RedirectToAction("Default", "Admin");
#else
            if (_authen.GetSessionUserInfo() != null)
            {
                return RedirectToAction("Default", "Admin");
            }
            return View("Index1",new LoginModel());
#endif
        }

        private bool CheckLock()
        {
            var permission = true;

            var ip = HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            if(!string.IsNullOrEmpty(ip))
            {
                if (_adminSettingUtils.Setting.BlockIps.Any(o => ip.Contains(o)))
                {
                    permission = false;
                }
            }    

            if (_adminSettingUtils.Setting.LockStartTime.HasValue && _adminSettingUtils.Setting.LockEndTime.HasValue)
            {
                if(_adminSettingUtils.Setting.LockTimeType.HasValue)
                {
                    switch(_adminSettingUtils.Setting.LockTimeType)
                    {
                        case 1:
                            if (_adminSettingUtils.Setting.LockStartTime.Value.TimeOfDay < DateTime.Now.TimeOfDay && _adminSettingUtils.Setting.LockEndTime.Value.TimeOfDay > DateTime.Now.TimeOfDay)
                                permission = false;
                            break;
                        case 2:
                            if (_adminSettingUtils.Setting.LockStartTime < DateTime.Now && _adminSettingUtils.Setting.LockEndTime > DateTime.Now)
                                permission = false;
                            break;
                    }    

            
                }    

            }
            return permission;
        }

        [HttpPost]
        [Route("")]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Login(model);
                if (result.IsSuccess())
                {
                    var permission = true;
                    if(!result.Value.SuperAdmin)
                    {
                        permission = CheckLock();
                    }    
                    if(permission)
                    {
                        var ip = HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
                        if (string.IsNullOrEmpty(ip))
                        {
                            ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();
                        }
                        //Response.SetCookie(_authen.CreateAuthenCookie(result.Value.Id, result.Value.UserName));
                        HttpContext?.Session.Clear();
                        HttpContext?.Session.SetString(Constants.SESSION_USER_INFO, JsonConvert.SerializeObject(result.Value.ToUserInfo()));
                        _logService.Add(new Datas.Models.DomainModels.LogData
                        {
                            UserId = result.Value.Id,
                            UserName = result.Value.UserName,
                            FullName = result.Value.FullName,
                            Method = Request.Method,
                            Controller = "Login",
                            Action = "Index",
                            Query = "{UserName : \" " + model.UserName + "\", Password: \"*****\"}",
                            Ip = ip
                        });
                        return RedirectToAction("Default", "Admin");
                    }    
                    else
                    {
                        ModelState.AddModelError("", "Bạn không được phép truy cập hệ thống");
                    }    
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View("Index1", model);
        }

        [Route("ForgotPass")]
        public IActionResult ForgotPass()
        {
            return View();
        }

        [Route("ForgotPass")]
        [HttpPost]
        public IActionResult ForgotPass(string email)
        {
            if (CheckLock())
            {
                if (!string.IsNullOrEmpty(email))
                {

                    var user = _userService.GetFirstOrDefault(o => !string.IsNullOrEmpty(o.Email) && o.Email.Equals(email));
                    if (user != null)
                    {
                        _settingUtils.Read();
                        if (_settingUtils.IsSendMail())
                        {
                            var id = Guid.NewGuid().ToString();
                            var link = Request.Scheme + "://" + Request.Host + "/Admin/Login/Confirm?key=" + id;
                            _mailUtils.Ids.Add(new MailForgot { Id = user.Id, Key = id, Email = email, Date = DateTime.Now.AddMinutes(30) });
                            var result = MailUtils.SendEmail(_settingUtils.Setting, new MailRequest
                            {
                                Body = "Bạn vừa có yêu cầu khôi phục mật khẩu.<br/>Nếu đó là yêu cầu của bạn. Vui lòng xác nhận đường dẫn sau để khôi phục mật khẩu: <a href='" + link + "'>" + link + "</a>",
                                Subject = "Khôi phục mật khẩu",
                                ToEmail = email
                            });
                            if (result.Contains("2.0.0 OK"))
                            {
                                ViewBag.Message = "Gửi yêu cầu khôi phục mật khẩu thành công. Vui lòng kiểm tra hòm thư để xác nhận yêu cầu khôi phục mật khẩu";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Không gửi được mail yêu cầu khôi phục mật khẩu. Vui lòng liên hệ ban quản trị.";
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Không tồn tại email. Vui lòng nhập lại.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng nhập email.");
                }
            }
             else
                ModelState.AddModelError("", "Bạn không được phép truy cập hệ thống");


            return View();
        }

        [Route("Confirm")]
        public IActionResult Confirm(string key)
        {
            if (CheckLock())
            {
                var id = _mailUtils.Ids.FirstOrDefault(o => o.Key.Equals(key));
                if (id != null)
                {
                    var ip = HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();
                    }
                    _logService.Add(new Datas.Models.DomainModels.LogData
                    {
                        Method = Request.Method,
                        Controller = "Login",
                        Action = "Confirm",
                        Query = "{key : \" " + key + "\", id: \"" + id.Id + "\"}",
                        Ip = ip
                    });
                    if (id.Date >= DateTime.Now)
                    {
                        _settingUtils.Read();
                        if (_settingUtils.IsSendMail())
                        {
                            var newPass = Utilities.GenerateRandomPassword();
                            var result = _userService.ResetPassword(id.Id, Utilities.CalculateMD5Hash(newPass));
                            if (result.IsSuccess())
                            {
                                var resultRS = MailUtils.SendEmail(_settingUtils.Setting, new MailRequest
                                {
                                    Body = "Khôi phục mật khẩu thành công.</br>Mật khẩu mới của bạn là: " + newPass,
                                    Subject = "Khôi phục mật khẩu",
                                    ToEmail = id.Email
                                });
                                if (resultRS.Contains("2.0.0 OK"))
                                {
                                    ViewBag.Message = "Khôi phục mật khẩu thành công. Vui lòng kiểm tra hòm thư để lấy mật khẩu mới";
                                }
                                else
                                {
                                    ViewBag.Message = "Không gửi được mật khẩu mới vào email. Vui lòng thực hiện lại";
                                }
                            }
                            else
                            {
                                ViewBag.Message = "Khôi phục mật khẩu thất bại. Vui lòng thực hiện lại";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Không gửi được mật khẩu mới vào email. Vui lòng liên hệ ban quản trị.";
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Đã hết thời gian khôi phục mật khẩu. Vui lòng thực hiện lại";
                    }
                    _mailUtils.Ids.Remove(id);
                }
                else
                {
                    ViewBag.Message = "Không tồn tại yêu cầu khôi phục mật khẩu. Vui lòng thực hiện lại";
                }
            }
            else
                ViewBag.Message = "Bạn không được phép truy cập hệ thống";

            return View();
        }
    }
}
