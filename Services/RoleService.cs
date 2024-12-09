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
    public class RoleService(DataContext dataContext) : AbstractService<Role, RoleModel>(dataContext)
    {
        protected override IQueryable<Role> AppendChildData(params string[] includes)
        {
            var result = Context.Roles.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(Role entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Roles.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(RoleModel entity)
        {
            var result = new MessageResult();
            try
            {
                var role = new Role();
                role.SetNewData(entity);
                if (entity.Functions != null && entity.Functions.Count() > 0)
                {
                    var lstFunction = new List<Function>();
                    foreach (var item in entity.Functions.Where(x => x.IsSelect))
                    {
                        var isFunction = Context.Functions.FirstOrDefault(x => x.Id == item.FunctionId);
                        if (isFunction != null)
                        {
                            lstFunction.Add(isFunction);
                        }
                    }
                    role.RoleFunctions = lstFunction;
                }
                Context.Roles.Add(role);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Role entity)
        {
            var result = new MessageResult();
            try
            {
                var UpdateData = Context.Roles.FirstOrDefault(x => x.Id == entity.Id);
                if (UpdateData != null)
                {
                    UpdateData.SetNewData(entity);
                    UpdateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.CompanyNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(RoleModel entity)
        {
            var result = new MessageResult();
            try
            {
                var UpdateData = Context.Roles.Include(x => x.RoleFunctions).FirstOrDefault(x => x.Id == entity.Id);
                if (UpdateData != null)
                {
                    UpdateData.SetNewData(entity);
                    if (entity.Functions != null && entity.Functions.Count() > 0)
                    {
                        UpdateData.RoleFunctions.Clear();
                        var lstFunction = new List<Function>();
                        foreach (var item in entity.Functions.Where(x => x.IsSelect))
                        {
                            var isFunction = Context.Functions.FirstOrDefault(x => x.Id == item.FunctionId);
                            if (isFunction != null)
                            {
                                lstFunction.Add(isFunction);
                            }
                        }
                        UpdateData.RoleFunctions = lstFunction;
                    }
                    UpdateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.CompanyNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public override MessageResult Delete(int id)
        {
            var result = new MessageResult();
            try
            {
                var deleteData = Context.Roles.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    if (deleteData.IsExistAnother())
                    {
                        deleteData.DeleteStatus = Common.Enums.DeleteStatus.IsDelete;
                        deleteData.UpdateDate = DateTime.Now;
                        Context.SaveChanges();
                    }
                    else
                    {
                        result.Code = Common.Enums.ErrorCode.CompanyExitsAnother;
                    }
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.CompanyNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public override Role GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        public MessageResult AssignUser(AssignUserModel model)
        {
            var result = new MessageResult();
            var updateRole = Context.Roles.Include(o => o.RoleUsers).FirstOrDefault(o => o.Id.Equals(model.Id));
            if (updateRole != null)
            {
                updateRole.RoleUsers = new List<User>();
                var lstUser = new List<User>();
                foreach (var assign in model.Users.Where(o => o.IsSelect))
                {
                    var user = Context.Users.FirstOrDefault(o => o.Id == assign.UserId);
                    if (user != null)
                    {
                        lstUser.Add(user);
                    }
                }
                updateRole.RoleUsers = lstUser;
                Context.SaveChanges();
            }
            else
            {
                result.Code = Enums.ErrorCode.RoleNotExist;
            }
            return result;
        }
    }
}
