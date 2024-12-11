using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProduceToolService : AbstractService<ProduceTool, ProduceToolModel>
    {
        public ProduceToolService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<ProduceTool> AppendChildData(params string[] includes)
        {
            var result = Context.ProduceTools.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private ProduceTool SetUrl(ProduceTool entity)
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

        public override MessageResult Add(ProduceTool entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.ProduceTools.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(ProduceToolModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new ProduceTool();
                data.SetNewData(entity);
                SetUrl(data);
                Context.ProduceTools.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(ProduceTool entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.ProduceTools.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(ProduceToolModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.ProduceTools.Include(o => o.Category).FirstOrDefault(x => x.Id == entity.Id);
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

        public override MessageResult Delete(int id)
        {
            var result = new MessageResult();
            try
            {
                var deleteData = Context.ProduceTools.FirstOrDefault(x => x.Id == id);
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
        public override ProduceTool GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
