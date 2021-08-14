using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Core.Application.Servers;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Core.FreeSql.Internal.AppService
{
    public class CurdAppService<TEntity, TEntityDto> : CurdAppServiceBase<TEntity, TEntityDto, TEntityDto, TEntityDto>,
        ICurdAppService<TEntity, TEntityDto>
        where TEntity : FreeSqlEntity
        where TEntityDto : EntityDto
    {
        protected readonly IFreeSqlRepository<TEntity> Table;

        public CurdAppService(IObjectMapper objectMapper, IFreeSqlRepository<TEntity> repository) : base(objectMapper,
            repository)
        {
            Table = repository;
        }

        public virtual PagedResultDto PageSearch(PageDto search)
        {
            var query = Table.GetAll().Select<TEntity>();

            query = query.Where(ConditionToLambda(search));

            query = OrderFilter() != null
                ? query.OrderByDescending(OrderFilter())
                : query.OrderByDescending(item => item.Id);

            var result = query
                .Count(out var total)
                .Page(search.PageIndex, search.PageMax)
                .ToList();

            return new PagedResultDto(total, ConvertToEntityDTOs(result));
        }

        protected virtual void BeforeCreate(TEntityDto request)
        {
        }

        protected virtual void BeforeEdit(TEntityDto request)
        {
        }

        public virtual TEntityDto CreateOrEdit(TEntityDto request)
        {
            TEntity data = null;
            if (request.Id == 0)
            {
                BeforeCreate(request);
                request.CreateTime = DateTime.Now;
                data = Table.Insert(MapToEntity(request));
            }
            else
            {
                BeforeEdit(request);
                data = Table.FirstOrDefault(item => item.Id == request.Id);
                data = MapToEntity(request);
                data.CreateTime = DateTime.Now;
                data = Table.Update(data);
            }

            return data == null
                ? null
                : ObjectMapper.Map<TEntityDto>(data);
        }

        protected Expression<Func<TEntity, bool>> ConditionToLambda(PageDto search)
        {
            Expression<Func<TEntity, bool>> expression = item => true;

            search.DynamicFilters.ForEach(filter =>
            {
                filter.Field = filter.Field.First().ToString().ToUpper() + filter.Field.Substring(1);

                var type = typeof(TEntity).GetProperty(filter.Field)?.PropertyType;
                if (type == null)
                    return;
                expression = expression.And(CreateExpression(type, filter.Field, Convert.ChangeType(filter.Value, type),
                    filter.Operate));
            });

            return expression;
        }

        protected Expression<Func<TEntity, bool>> CreateExpression(Type fieldType, string fieldName, object value,
            string operate)
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

        protected virtual Expression<Func<TEntity, int>> OrderFilter()
        {
            return null;
        }

        protected virtual List<TEntityDto> ConvertToEntityDTOs(List<TEntity> entities)
        {
            return ObjectMapper.Map<List<TEntityDto>>(entities);
        }
    }
}