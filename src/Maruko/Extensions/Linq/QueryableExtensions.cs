using System;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;
using Maruko.Core.Domain.Specification;
using Maruko.Core.Enum;

namespace Maruko.Core.Extensions.Linq
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// linq 分页扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query is null");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }

        /// <summary>
        /// linq where 条件筛选扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition == true ? query.Where(predicate) : query;
        }


        public static Expression<Func<TEntity, bool>> ConditionToLambda<TEntity>(PageDto search)
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = item => true;

            search.DynamicFilters?.ForEach(filter =>
            {
                filter.Field = filter.Field.First().ToString().ToUpper() + filter.Field.Substring(1);

                var type = typeof(TEntity).GetProperty(filter.Field)?.PropertyType;
                if (type == null)
                    return;
                expression = expression.And(
                    CreateExpression<TEntity>(type, filter.Field, Convert.ChangeType(filter.Value, type), filter.Operate)
                    );
            });

            return expression;
        }

        public static Expression<Func<TEntity, bool>> CreateExpression<TEntity>(Type fieldType, string fieldName, object value,
            string operate)
            where TEntity : class
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            Expression lambdaBody = default;

            if (operate == Condition.Equal.ToString())
                lambdaBody = Expression.Equal(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );

            else if (operate == Condition.NotEqual.ToString())
                lambdaBody = Expression.NotEqual(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );

            else if (operate == Condition.Like.ToString())
            {
                lambdaBody = Expression.Call(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    fieldType.GetMethod("Contains", new Type[] { fieldType })
                    , Expression.Constant(value, fieldType));
            }
            else if (operate == Condition.GreaterThan.ToString())
            {
                lambdaBody = Expression.GreaterThan(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );
            }
            else if (operate == Condition.GreaterThanOrEqual.ToString())
            {
                lambdaBody = Expression.GreaterThanOrEqual(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );
            }
            else if (operate == Condition.LessThan.ToString())
            {
                lambdaBody = Expression.LessThan(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );
            }
            else if (operate == Condition.LessThanOrEqual.ToString())
            {
                lambdaBody = Expression.LessThanOrEqual(
                    Expression.PropertyOrField(lambdaParam, fieldName),
                    Expression.Constant(value, fieldType)
                );
            }

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
