using Common;
using Datas.Entity;
using Datas.Models.DomainModels;
using Datas;
using Services;
using Common.Entity;
using Newtonsoft.Json;
namespace WebApp.Common
{
    public class AuthenUtils(GroupFunctionService groupFunctionService, UserService userService, CompanyService companyService, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        private readonly ILogger _log;
        private readonly GroupFunctionService _groupFunctionService = groupFunctionService;
        private readonly UserService _userService = userService;
        private readonly CompanyService _companyService = companyService;
        private readonly DataContext _dataContext = dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public UserInfo? GetSessionUserInfo()
        {
            try
            {
                var userInfo = _httpContextAccessor.HttpContext?.Session.GetString(Constants.SESSION_USER_INFO);
                if (!string.IsNullOrEmpty(userInfo))
                {
                    return JsonConvert.DeserializeObject<UserInfo>(userInfo);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                return null;
            }
        }

        public User GetUserById(int Id)
        {
            var user = _userService.GetById(Id);
            return user;
        }

        public int GetUserCompanyId(int userId)
        {
            var user = _userService.GetFirstOrDefault(x => x.Id.Equals(userId));
            var companyid = 0;
            if (user != null)
            {
                if (user.UserCompanyId != null)
                {
                    companyid = user.UserCompanyId.Value;
                    return companyid;
                }
            }
            companyid = 0;
            return companyid;
        }
        public Company GetUserCompany(int userId)
        {
            var company = new Company();
            var user = _userService.GetFirstOrDefault(x => x.Id == userId, "UserCompany");
            var companyid = 0;
            if (user != null)
            {
                if (user.UserCompanyId != null)
                {
                    companyid = user.UserCompanyId.Value;
                    company = _companyService.GetFirstOrDefault(x => x.Id == companyid, "Childrens");
                }
            }
            return company;
        }
        public List<int> GetAllCompanyId()
        {
            return _companyService.GetAll().Select(x => x.Id).ToList();
        }
        public IEnumerable<GroupFunction> GetListGroupFunction()
        {
            return _groupFunctionService.GetAll(o => o.Status == Enums.ActiveStatus.Active);

        }
        public MessageResult<List<Function>> GetFunctionsByUserId(int userId)
        {
            var result = new MessageResult<List<Function>>();
            var user = _userService.GetById(userId, "UserRoles.RoleFunctions");
            if (user != null && user.Status == Enums.ActiveStatus.Active)
            {
                var lstFunction = new List<Function>();
                foreach (var role in user.UserRoles.Where(o => o.Status == Enums.ActiveStatus.Active))
                {
                    foreach (var function in role.RoleFunctions.Where(o => o.Status == Enums.ActiveStatus.Active))
                    {
                        if (!lstFunction.Any(o => o.Id == function.Id))
                        {
                            lstFunction.Add(function);
                        }
                    }
                }
                result.Value = lstFunction;
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }
        public Company GetParentCompany(int userId)
        {
            var company = new Company();
            var user = _userService.GetFirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                if (user.UserCompany != null)
                {
                    company = LoopGetCompay(user.UserCompany);
                }
            }
            return company;
        }
        private Company LoopGetCompay(Company company)
        {
            if (company.Parent != null)
            {
                return LoopGetCompay(company.Parent);
            }
            else
            {
                return company;
            }
        }

    }
}
