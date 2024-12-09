using Common.Entity;
using Datas;
using Datas.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogService(DataContext dataContext) : AbstractService<LogData>(dataContext)
    {

        protected override IQueryable<LogData> AppendChildData(params string[] includes)
        {
            var result = Context.Logs.AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(LogData entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Logs.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(LogData entity)
        {
            var result = new MessageResult();
            try
            {
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public LogData GetById(string id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id.Equals(id));

        public override MessageResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override LogData GetById(int id, params string[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
