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
    public class DailyItemCategoryService : AbstractService<DailyItemCategory, DailyItemCategoryModel>
    {
        public DailyItemCategoryService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<DailyItemCategory> AppendChildData(params string[] includes)
        {
            var result = Context.DailyItemCategories.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal)
                .AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return result;
        }

        private DailyItemCategory SetUrl(DailyItemCategory entity)
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

        public override MessageResult Add(DailyItemCategory entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.DailyItemCategories.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override MessageResult Add(DailyItemCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new DailyItemCategory();
                if (entity.ParentId != null)
                {
                    var parent = Context.DailyItemCategories.FirstOrDefault(x => x.Id == entity.ParentId);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }

                data.SetNewData(entity);
                SetUrl(data);
                Context.DailyItemCategories.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public override MessageResult Update(DailyItemCategory entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.DailyItemCategories.FirstOrDefault(x => x.Id == entity.Id);
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

        public override MessageResult Update(DailyItemCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.DailyItemCategories.Include(o => o.Parent)
                    .FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId.HasValue)
                    {
                        var parent = Context.DailyItemCategories.FirstOrDefault(x => x.Id == entity.ParentId);
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
                var deleteData = Context.DailyItemCategories.Include(e => e.Childrens).Include(e => e.Childrens)
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

        public override DailyItemCategory GetById(int id, params string[] includes) =>
            AppendChildData(includes).FirstOrDefault(x => x.Id == id);
    }
}