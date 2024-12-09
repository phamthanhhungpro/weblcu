using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class QueryFilterProvider
    {
        public Func<IQueryable<T>, IQueryable<T>> CreateSort<T>(bool descending = false, params string[] sortExprs)
        {
            return source =>
            {
                if (sortExprs == null)
                {
                    return source;
                }

                var type = typeof(T);
                var parameter = Expression.Parameter(type, "p");
                var isFirst = true;
                MethodCallExpression resultExp = null;
                foreach (var sortExpr in sortExprs)
                {
                    var property = type.GetProperty(sortExpr);
                    if (property == null)
                    {
                        continue;
                    }
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);

                    if (isFirst)
                    {
                        resultExp = Expression.Call(typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
                                                    new[] { type, property.PropertyType }, source.Expression,
                                                    Expression.Quote(orderByExp));
                        isFirst = false;
                    }
                    else
                    {
                        resultExp = Expression.Call(typeof(Queryable), descending ? "ThenByDescending" : "ThenBy",
                                                    new[] { type, property.PropertyType }, resultExp,
                                                    Expression.Quote(orderByExp));
                    }
                }

                return resultExp == null ? source : source.Provider.CreateQuery<T>(resultExp);
            };
        }
    }
}
