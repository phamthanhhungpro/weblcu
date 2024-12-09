using Common.Entity;
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
    public class DepartmentService : AbstractService<Department, DepartmentModel>
    {
        public DepartmentService(DataContext dataContext) : base(dataContext)
        {
        }
        protected override IQueryable<Department> AppendChildData(params string[] includes)
        {
            var result = Context.Departments.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(Department entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Departments.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(DepartmentModel entity)
        {
            var result = new MessageResult();
            try
            {
                var _department = new Department();
                _department.SetNewData(entity);
                if (entity.ParentId != null)
                {
                    var parent = Context.Departments.FirstOrDefault(x => x.Id == entity.Id);
                    if (parent != null)
                    {
                        _department.Parent = parent;
                    }
                }
                if (entity.CompanyId != null)
                {
                    var company = Context.Companies.FirstOrDefault(x => x.Id == entity.CompanyId.Value);
                    if (company != null)
                    {
                        _department.DepartmentCompany = company;
                    }
                }
                Context.Departments.Add(_department);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Department entity)
        {
            var result = new MessageResult();
            try
            {
                var UpdateData = Context.Departments.Include(x => x.Parent).FirstOrDefault(x => x.Id == entity.Id);
                if (UpdateData != null)
                {
                    UpdateData.SetNewData(entity);
                    UpdateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.DepartmentNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(DepartmentModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Departments.Include(x => x.Parent).Include(x => x.DepartmentCompany).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId != null)
                    {
                        var parent = Context.Departments.FirstOrDefault(x => x.Id == entity.ParentId);
                        if (parent != null)
                        {
                            updateData.Parent = parent;
                        }
                    }
                    else
                    {
                        updateData.Parent = null;
                    }
                    if (entity.CompanyId != null)
                    {
                        var company = Context.Companies.FirstOrDefault(x => x.Id == entity.CompanyId.Value);
                        if (company != null)
                        {
                            updateData.DepartmentCompany = company;
                        }
                    }
                    else
                    {
                        updateData.DepartmentCompany = null;
                    }
                    updateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.DepartmentNotExit;
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
                var deleteData = Context.Departments.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    if (!deleteData.IsExistAnother())
                    {
                        deleteData.DeleteStatus = Common.Enums.DeleteStatus.IsDelete;
                        deleteData.UpdateDate = DateTime.Now;
                        Context.SaveChanges();
                    }
                    else
                    {
                        result.Code = Common.Enums.ErrorCode.DepartmentExitsAnother;
                    }
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.DepartmentNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public override Department GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
