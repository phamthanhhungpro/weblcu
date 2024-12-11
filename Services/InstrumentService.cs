using Common.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class InstrumentService : AbstractService<Instrument, InstrumentModel>
    {
        public InstrumentService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<Instrument> AppendChildData(params string[] includes)
        {
            var result = Context.Instruments.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private Instrument SetUrl(Instrument entity)
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

        public override MessageResult Add(Instrument entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.Instruments.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(InstrumentModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new Instrument();
                if (entity.CategoryId.HasValue)
                {
                    var category = Context.InstrumentCategories.FirstOrDefault(x => x.Id == entity.CategoryId);
                    if (category != null)
                    {
                        data.Category = category;
                    }
                }
                data.SetNewData(entity);
                //data.PostDate = data.PostDate.AddHours(data.InsertDate.Hour);
                //data.PostDate = data.PostDate.AddMinutes(data.InsertDate.Minute);
                SetUrl(data);
                Context.Instruments.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Instrument entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Instruments.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(InstrumentModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Instruments.Include(o => o.Category).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.CategoryId.HasValue)
                    {
                        var parent = Context.InstrumentCategories.FirstOrDefault(x => x.Id == entity.CategoryId);
                        if (parent != null)
                        {
                            updateData.Category = parent;
                        }
                        else
                        {
                            updateData.Category = null;
                        }
                    }
                    else
                    {
                        updateData.Category = null;
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
                var deleteData = Context.Instruments.Include(e => e.Category).FirstOrDefault(x => x.Id == id);
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

        public MessageResult AddView(int id)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Instruments.FirstOrDefault(x => x.Id == id);
                if (updateData != null)
                {
                    updateData.View = updateData.View + 1;
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


        public override Instrument GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}