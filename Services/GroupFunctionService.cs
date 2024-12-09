using Common.Entity;
using Datas;
using Datas.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services
{
    public class GroupFunctionService
    {
        DataContext Context;
        public GroupFunctionService(DataContext dataContext)
        {
            Context = dataContext;
        }

        protected IQueryable<GroupFunction> AppendChildData(params string[] includes)
        {
            var result = Context.GroupFunctions.AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }
        #region GetData
        public IEnumerable<GroupFunction> GetAll(params string[] includes) => AppendChildData(includes).ToList();

        public virtual IEnumerable<GroupFunction> GetAll(Expression<Func<GroupFunction, bool>> predicate, params string[] includes) => AppendChildData(includes).Where(predicate);

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<GroupFunction, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Select(projector);

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<GroupFunction, bool>> predicate, Expression<Func<GroupFunction, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Where(predicate).Select(projector);

        public virtual GroupFunction GetFirstOrDefault(params string[] includes) => AppendChildData(includes).FirstOrDefault();

        public virtual GroupFunction GetFirstOrDefault(Expression<Func<GroupFunction, bool>> predicate, params string[] includes) => AppendChildData(includes).FirstOrDefault(predicate);

        public virtual B GetFirstOrDefault<B>(Expression<Func<GroupFunction, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Select(projector).FirstOrDefault();

        public virtual B GetFirstOrDefault<B>(Expression<Func<GroupFunction, bool>> predicate, Expression<Func<GroupFunction, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Where(predicate).Select(projector).FirstOrDefault();
        #endregion
        public MessageResult Add(GroupFunction entity)
        {
            var result = new MessageResult();
            try
            {
                Context.GroupFunctions.Add(entity);
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                Context.SaveChanges();
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public MessageResult Update(GroupFunction entity)
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

        public MessageResult Delete(int id)
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

        public GroupFunction GetById(int id, params string[] includes) => AppendChildData(includes).FirstOrDefault(x => x.Id == id);

    }
}
