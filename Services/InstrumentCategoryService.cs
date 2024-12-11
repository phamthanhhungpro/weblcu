using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class InstrumentCategoryService : AbstractService<InstrumentCategory, InstrumentCategoryModel>
    {
        public InstrumentCategoryService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<InstrumentCategory> AppendChildData(params string[] includes)
        {
            var result = Context.InstrumentCategories.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal)
                .AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return result;
        }

        private InstrumentCategory SetUrl(InstrumentCategory entity)
        {
            entity.ReNewUrl();
            var checkUrl = this.GetAll(o => o.Url.Contains(entity.Url) && o.Id != entity.Id).ToList();
            bool isCheck = false;
            var url = entity.Url;
            int count = 0;
            do
            {
                if (checkUrl.Any(o => o.Url.Equals(url)))
                {
                    isCheck = true;
                    count++;
                    url = entity.Url + "-" + count;
                }
                else
                    isCheck = false;
            } while (isCheck);

            entity.Url = url;
            return entity;
        }

        public override MessageResult Add(InstrumentCategory entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.InstrumentCategories.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override MessageResult Add(InstrumentCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new InstrumentCategory();
                if (entity.ParentId != null)
                {
                    var parent = Context.InstrumentCategories.FirstOrDefault(x => x.Id == entity.ParentId);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }

                data.SetNewData(entity);
                SetUrl(data);
                Context.InstrumentCategories.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override MessageResult Update(InstrumentCategory entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.InstrumentCategories.FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    SetUrl(updateData);
                    updateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override MessageResult Update(InstrumentCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.InstrumentCategories.Include(o => o.Parent)
                    .FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId.HasValue)
                    {
                        var parent = Context.InstrumentCategories.FirstOrDefault(x => x.Id == entity.ParentId);
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

                    SetUrl(updateData);
                    updateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
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
                var deleteData = Context.InstrumentCategories.Include(e => e.Childrens).Include(e => e.Childrens)
                    .FirstOrDefault(x => x.Id == id);
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
                        result.Code = Common.Enums.ErrorCode.OtherExitsAnother;
                    }
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override InstrumentCategory GetById(int id, params string[] includes) =>
            AppendChildData(includes).FirstOrDefault(x => x.Id == id);
    }
}