using Common;
using Common.Entity;
using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class AbstractService<T, M>
    {
        private static readonly Lazy<QueryFilterProvider> _filterProviderInitializer = new Lazy<QueryFilterProvider>();

        public AbstractService(DataContext context)
        {
            Context = context;
        }

        protected DataContext Context
        {
            get; private set;
        }
        protected abstract IQueryable<T> AppendChildData(params string[] includes);
        public abstract T GetById(int id, params string[] includes);
        public abstract MessageResult Add(T entity);
        public abstract MessageResult Update(T entity);
        public abstract MessageResult Add(M entity);
        public abstract MessageResult Update(M entity);
        public abstract MessageResult Delete(int id);


        public virtual IEnumerable<T> GetAll(params string[] includes) => AppendChildData(includes).ToList();

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params string[] includes) => AppendChildData(includes).Where(predicate);

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Select(projector);

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<T, bool>> predicate, Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Where(predicate).Select(projector);

        public virtual T GetFirstOrDefault(params string[] includes) => AppendChildData(includes).FirstOrDefault();

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includes) => AppendChildData(includes).FirstOrDefault(predicate);

        public virtual B GetFirstOrDefault<B>(Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Select(projector).FirstOrDefault();

        public virtual B GetFirstOrDefault<B>(Expression<Func<T, bool>> predicate, Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Where(predicate).Select(projector).FirstOrDefault();

        protected Lazy<QueryFilterProvider> FilterProvider
        {
            get { return _filterProviderInitializer; }
        }
    }
    public abstract class AbstractService<T>
    {
        private static readonly Lazy<QueryFilterProvider> _filterProviderInitializer = new Lazy<QueryFilterProvider>();

        public AbstractService(DataContext context)
        {
            Context = context;
        }

        protected DataContext Context
        {
            get; private set;
        }
        protected abstract IQueryable<T> AppendChildData(params string[] includes);
        public abstract T GetById(int id, params string[] includes);
        public abstract MessageResult Add(T entity);
        public abstract MessageResult Update(T entity);
        public abstract MessageResult Delete(int id);


        public virtual IEnumerable<T> GetAll(params string[] includes)
        {
            return AppendChildData(includes).ToList();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            return AppendChildData(includes).Where(predicate);
        }

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<T, B>> projector, params string[] includes) where B : class
        {
            return AppendChildData(includes).Select(projector);
        }

        public virtual IEnumerable<B> GetAll<B>(Expression<Func<T, bool>> predicate, Expression<Func<T, B>> projector, params string[] includes) where B : class
        {
            return AppendChildData(includes).Where(predicate).Select(projector);
        }

        public virtual T GetFirstOrDefault(params string[] includes) => AppendChildData(includes).FirstOrDefault();

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includes) => AppendChildData(includes).FirstOrDefault(predicate);

        public virtual B GetFirstOrDefault<B>(Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Select(projector).FirstOrDefault();

        public virtual B GetFirstOrDefault<B>(Expression<Func<T, bool>> predicate, Expression<Func<T, B>> projector, params string[] includes) where B : class => AppendChildData(includes).Where(predicate).Select(projector).FirstOrDefault();

        protected Lazy<QueryFilterProvider> FilterProvider
        {
            get { return _filterProviderInitializer; }
        }
    }
}
