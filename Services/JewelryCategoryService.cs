using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class JewelryCategoryService : AbstractService<JewelryCategory, JewelryCategoryModel>
    {
        public JewelryCategoryService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<JewelryCategory> AppendChildData(params string[] includes)
        {
            var result = Context.JewelryCategories.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private JewelryCategory SetUrl(JewelryCategory entity)
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

        public override MessageResult Add(JewelryCategory entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.JewelryCategories.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(JewelryCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new JewelryCategory();
                if (entity.ParentId != null)
                {
                    var parent = Context.JewelryCategories.FirstOrDefault(x => x.Id == entity.ParentId);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }
                data.SetNewData(entity);
                SetUrl(data);
                Context.JewelryCategories.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(JewelryCategory entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.JewelryCategories.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(JewelryCategoryModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.JewelryCategories.Include(o => o.Parent).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.ParentId.HasValue)
                    {
                        var parent = Context.JewelryCategories.FirstOrDefault(x => x.Id == entity.ParentId);
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
                var deleteData = Context.JewelryCategories.Include(e => e.Childrens).Include(e => e.Childrens).FirstOrDefault(x => x.Id == id);
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
        public override JewelryCategory GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
