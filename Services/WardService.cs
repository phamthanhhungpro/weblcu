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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class WardService : AbstractService<Ward, WardModel>
    {
        public WardService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<Ward> AppendChildData(params string[] includes)
        {
            var result = Context.Wards.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(Ward entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Wards.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(WardModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new Ward();
                data.SetNewData(entity);
                if (entity.DistrictId.HasValue)
                {
                    var people = Context.Districts.FirstOrDefault(x => x.Id == entity.DistrictId);
                    if (people != null)
                    {
                        data.District = people;
                    }
                }
                Context.Wards.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Ward entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Wards.FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
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
        public override MessageResult Update(WardModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Wards.Include("District").FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    if (entity.DistrictId.HasValue)
                    {
                        var district = Context.Districts.FirstOrDefault(x => x.Id == entity.DistrictId);
                        if (district != null)
                        {
                            updateData.District = district;
                        }
                        else
                        {
                            updateData.District = null;
                        }
                    }
                    else
                    {
                        updateData.District = null;
                    }
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
                //var deleteData = Context.HistoricalRelicTypes.Include(e => e.UserPosition).FirstOrDefault(x => x.Id == id);
                var deleteData = Context.Wards.FirstOrDefault(x => x.Id == id);
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

        public override Ward GetById(int id, params string[] includes)
        {
            return AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        }
    }
}
