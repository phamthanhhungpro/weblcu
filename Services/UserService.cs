using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : AbstractService<User, UserModel>
    {
        public UserService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<User> AppendChildData(params string[] includes)
        {
            var result = Context.Users.AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(User entity)
        {
            throw new NotImplementedException();
        }

        public override MessageResult Delete(int id)
        {
            var result = new MessageResult();
            var user = Context.Users.FirstOrDefault(o => o.Id.Equals(id));
            if (user != null)
            {
                if (!user.IsExistAnother())
                {
                    Context.Users.Remove(user);
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Enums.ErrorCode.UserExistAnother;
                }
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }

        public override User GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        public override MessageResult Update(User entity)
        {
            throw new NotImplementedException();
        }
        public override MessageResult Update(UserModel entity)
        {
            var result = new MessageResult();
            var updateUser = GetFirstOrDefault(e => e.Id.Equals(entity.Id), "UserCompany", "DepartmentCompany", "UserPosition");
            if (updateUser != null)
            {
                updateUser.SetNewData(entity);
                updateUser.UpdateDate = DateTime.Now;
                SetCompany(ref updateUser, entity.CompanyId);
                SetDepartment(ref updateUser, entity.DepartmentId);
                SetPosition(ref updateUser, entity.PositionId);
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }

        public MessageResult Update(UserInfoModel user)
        {
            var result = new MessageResult();
            var updateUser = Context.Users.FirstOrDefault(o => o.Id.Equals(user.Id));
            if (updateUser != null)
            {
                updateUser.SetNewData(user);
                updateUser.UpdateDate = DateTime.Now;
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }

        public override MessageResult Add(UserModel entity)
        {
            var result = new MessageResult();
            try
            {
                var _user = entity.ToUserCreate();
                if (entity.CompanyId.HasValue)
                {
                    _user.UserCompany = Context.Companies.FirstOrDefault(o => o.Id.Equals(entity.CompanyId.Value));

                }
                if (entity.DepartmentId.HasValue)
                {
                    _user.DepartmentCompany = Context.Departments.FirstOrDefault(o => o.Id.Equals(entity.DepartmentId.Value));

                }
                if (entity.PositionId.HasValue)
                {
                    _user.UserPosition = Context.Positions.FirstOrDefault(o => o.Id.Equals(entity.PositionId.Value));
                }
                Context.Users.Add(_user);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        private void SetCompany(ref User updateUser, int? id)
        {
            if (id.HasValue)
            {
                if (updateUser.UserCompany == null || !updateUser.UserCompany.Id.Equals(id.Value))
                {
                    updateUser.UserCompany = null;
                    updateUser.UserCompany = Context.Companies.FirstOrDefault(o => o.Id.Equals(id.Value));
                }
            }
            else
            {
                updateUser.UserCompany = null;
            }
        }



        private void SetDepartment(ref User updateUser, int? id)
        {
            if (id.HasValue)
            {
                if (updateUser.DepartmentCompany == null || !updateUser.DepartmentCompany.Id.Equals(id.Value))
                {
                    updateUser.DepartmentCompany = null;
                    updateUser.DepartmentCompany = Context.Departments.FirstOrDefault(o => o.Id.Equals(id.Value));
                }
            }
            else
            {
                updateUser.DepartmentCompany = null;
            }
        }
        private void SetPosition(ref User updateUser, int? id)
        {
            if (id.HasValue)
            {
                if (updateUser.UserPosition == null || !updateUser.UserPosition.Id.Equals(id.Value))
                {
                    updateUser.UserPosition = null;
                    updateUser.UserPosition = Context.Positions.FirstOrDefault(o => o.Id.Equals(id.Value));
                }
            }
            else
            {
                updateUser.UserPosition = null;
            }
        }
        public MessageResult LockUser(int id)
        {
            var result = new MessageResult();
            var user = GetFirstOrDefault(e => e.Id.Equals(id));
            if (user != null)
            {
                user.Status = Enums.ActiveStatus.Inactive;
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }

        //public MessageResult Update(UserInfoModel user)
        //{
        //    var result = new MessageResult();
        //    var updateUser = Context.Users.FirstOrDefault(o => o.Id.Equals(user.Id));
        //    if (updateUser != null)
        //    {
        //        updateUser.SetNewData(user);
        //        Context.Configuration.ValidateOnSaveEnabled = false;
        //        Context.SaveChanges();
        //        Context.Configuration.ValidateOnSaveEnabled = true;
        //    }
        //    else
        //    {
        //        result.Code = Enums.ErrorCode.UserNotExist;
        //    }
        //    return result;
        //}
        public MessageResult<User> Login(string userName, string password)
        {
            var result = new MessageResult<User>();
            var user = Context.Users.FirstOrDefault(p => p.UserName.Equals(userName) && p.Password.Equals(Utilities.CalculateMD5Hash(password)));
            if (user != null)
            {
                if (user.Status == Enums.ActiveStatus.Active)
                {
                    result.Value = user;
                }
                else
                {
                    result.Code = Enums.ErrorCode.UserNotActive;
                }
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
                result.Message = "Tài khoản hoặc mật khẩu không chính xác.";
            }
            return result;
        }

        public MessageResult<User> Login(LoginModel model)
        {
            var result = new MessageResult<User>();
            var user = Context.Users.FirstOrDefault(p => p.UserName.Equals(model.UserName) && p.Password.Equals(model.EncodePassWord));
            if (user != null)
            {
                if (user.Status == Enums.ActiveStatus.Active)
                {
                    result.Value = user;
                }
                else
                {
                    result.Code = Enums.ErrorCode.UserNotActive;
                }
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
                result.Message = "Tài khoản hoặc mật khẩu không chính xác.";
            }
            return result;
        }

        public MessageResult ChangePassword(PasswordModel password)
        {
            var result = new MessageResult();
            var user = Context.Users.FirstOrDefault(o => o.Id.Equals(password.Id) && o.Password.Equals(password.EncodeOldPassword));
            if (user != null)
            {
                user.Password = password.EncodeNewPassword;
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
                result.Message = "Mật khẩu cũ không chính xác.";
            }
            return result;
        }

        public MessageResult ResetPassword(int userId, string md5Password)
        {
            var result = new MessageResult();
            var user = Context.Users.FirstOrDefault(o => o.Id.Equals(userId));
            if (user != null)
            {
                user.Password = md5Password;
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.UserNotExist;
            }
            return result;
        }

        //private bool CheckLocal(UserData user)
        //{
        //    var id = user.CompanyId;
        //    var company = Context.Companies.FirstOrDefault(x => x.GlobalId == id);
        //    if (company != null && company.SourceType == Enums.SourceType.Local)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}
