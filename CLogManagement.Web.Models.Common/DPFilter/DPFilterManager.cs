using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using System.Linq.Dynamic.Core;

namespace CLogManagement.Web.Models.Common.DPFilter
{
    public class DPFilterManager
    {
        public DPFilterResult<T> GetDataFilter<T>(DPFilter filter, IQueryable<T> data)
        {
            var recordsTotal = 0;
            var recordsFiltered = 0;

            recordsTotal = data.Count();
            recordsFiltered = recordsTotal;
            if (filter != null)
            {
                if (filter.SearchFields != null)
                {
                    foreach (var item in filter.SearchFields)
                    {
                        var propName = item.Name;
                        var searchValue = item.Value;

                        if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(searchValue))
                            continue;

                        data = data.Where(BuildExpression<T>(propName, searchValue));
                    }
                }
                if (filter.OrderFields != null)
                {
                    List<string> orderExpressions = new List<string>();
                    foreach (var order in filter.OrderFields)
                    {
                        if (string.IsNullOrEmpty(order.Name))
                            continue;

                        if (order.Order == DPFilterSortOrder.ASC)
                            orderExpressions.Add(order.Name);
                        else
                            orderExpressions.Add(order.Name + " desc");
                    }
                    if (orderExpressions.Count > 0)
                        data = data.OrderBy(string.Join(", ", orderExpressions));
                }

                if (filter.Skip > 0)
                    data = data.Skip(filter.Skip);

                if (filter.Take > 0)
                    data = data.Take(filter.Take);

                recordsFiltered = data.Count();
            }
            return new DPFilterResult<T>()
            {
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data.ToList()
            };
        }

        private object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
        
        protected  Expression<Func<TEntity, bool>> BuildExpression<TEntity>(string propertyName, string pattern)
        {
            ParameterExpression parameters = Expression.Parameter(typeof(TEntity), "x");
            Expression property = Expression.PropertyOrField(parameters, propertyName);
            Expression ne = Expression.NotEqual(property, Expression.Constant(null));

            //Expression toString = Expression.Call(property, "ToString", null);
            Expression toLower = Expression.Call(property, "ToLower", null);
            Expression Contains = Expression.Call(toLower, "Contains", null, Expression.Constant(pattern));

            Expression and = Expression.AndAlso(ne, Contains);
            LambdaExpression lambda = Expression.Lambda(and, parameters);
            return (Expression<Func<TEntity, bool>>)lambda;
        }
    }
}