﻿using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class NationalCostumeCategoryService : AbstractService<NationalCostumeCategory, NationalCostumeCategoryModel>
    {
        public NationalCostumeCategoryService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<NationalCostumeCategory> AppendChildData(params string[] includes)
        {
            var result = Context.NationalCostumeCategories.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private NationalCostumeCategory SetUrl(NationalCostumeCategory entity)
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
            }
            while (isCheck);
            entity.Url = url;
            return entity;
        }

        public override MessageResult Add(NationalCostumeCategory entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.NationalCostumeCategories.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(NationalCostumeCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new NationalCostumeCategory();
                if (entity.ParentId.HasValue)
                {
                    var parent = Context.NationalCostumeCategories.FirstOrDefault(x => x.Id == entity.ParentId);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }
                data.SetNewData(entity);
                SetUrl(data);
                Context.NationalCostumeCategories.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(NationalCostumeCategory entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.NationalCostumeCategories.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(NationalCostumeCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.NationalCostumeCategories.Include(o => o.Parent).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId.HasValue)
                    {
                        var parent = Context.NationalCostumeCategories.FirstOrDefault(x => x.Id == entity.ParentId);
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
                var deleteData = Context.NationalCostumeCategories.Include(e => e.Childrens).Include(e => e.Childrens).FirstOrDefault(x => x.Id == id);
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
        public override NationalCostumeCategory GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
