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
    public class FunctionService(DataContext dataContext) : AbstractService<Function>(dataContext)
    {

        protected override IQueryable<Function> AppendChildData(params string[] includes)
        {
            var result = Context.Functions.AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public override MessageResult Add(Function entity)
        {
            var result = new MessageResult();
            try
            {
                Context.Functions.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(Function entity)
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

        public override Function GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

        public override MessageResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
