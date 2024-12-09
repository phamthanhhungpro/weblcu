using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CompanyService : AbstractService<Company, CompanyModel>
    {
        public CompanyService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<Company> AppendChildData(params string[] includes)
        {
            var result = Context.Companies.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(Company entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Companies.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(CompanyModel entity)
        {
            var result = new MessageResult();
            try
            {
                var _company = new Company();
                if (entity.ParentId != null)
                {
                    var parent = Context.Companies.FirstOrDefault(x => x.Id == entity.ParentId);
                    if (parent != null)
                    {
                        _company.Parent = parent;
                    }
                }
                _company.SetNewData(entity);
                Context.Companies.Add(_company);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Company entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Companies.FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    updateData.UpdateDate = DateTime.Now;
                    Context.ChangeTracker.AutoDetectChangesEnabled = false;
                    Context.SaveChanges();
                    Context.ChangeTracker.AutoDetectChangesEnabled = true;
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
        public override MessageResult Update(CompanyModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Companies.Include(o => o.Parent).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId != null)
                    {
                        var parent = Context.Companies.FirstOrDefault(x => x.Id == entity.ParentId);
                        if (parent != null)
                        {
                            updateData.Parent = parent;
                        }
                        else
                        {
                            updateData.Parent = null;
                        }
                    }
                    else
                    {
                        updateData.Parent = null;
                    }
                    updateData.UpdateDate = DateTime.Now;
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
                var deleteData = Context.Companies.Include(e => e.Childrens).Include(e => e.CompanyUsers).FirstOrDefault(x => x.Id == id);
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
        public override Company GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
