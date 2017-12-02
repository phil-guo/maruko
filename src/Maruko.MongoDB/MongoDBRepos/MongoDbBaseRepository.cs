using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Maruko.Domain.Entities;
using MongoDB.Driver;

namespace Maruko.MongoDB.MongoDBRepos
{
    /// <summary>
    /// mongodb 仓储
    /// </summary>
    public class MongoDbBaseRepository<TEntity,TKey>
        where TEntity : Entity<TKey>, IHasCreationTime, IHasModificationTime
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoDbBaseRepository(MongoDbContext mongoDatabase)
        {
            var mongoDatabase1 = mongoDatabase.GetDateBase();
            _collection = mongoDatabase1.GetCollection<TEntity>(typeof(TEntity).Name, new MongoCollectionSettings() { });
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void Insert(TEntity entity)
        {
            entity.CreateTime = DateTime.UtcNow;
            _collection.InsertOne(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        public async Task InsertManyAsync(List<TEntity> items)
        {
            items.ForEach(item =>
            {
                item.CreateTime = DateTime.UtcNow;
            });

            await _collection.InsertManyAsync(items);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> updateDefinition)
        {
            var result = await _collection.UpdateManyAsync<TEntity>(expression, updateDefinition);
            return result.ModifiedCount > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            entity.LastModificationTime = DateTime.UtcNow;
            return _collection.ReplaceOne(filter, entity).ModifiedCount == 1;
        }

        /// <summary>
        /// 根据主键获取
        /// </summary>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return await SigleOrDefault(filter);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        public async Task<TEntity> SigleOrDefault(FilterDefinition<TEntity> filter)
        {
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        public bool Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            return _collection.DeleteOne(filter).DeletedCount > 0;
        }
        /// <summary>
        /// 全部删除
        /// </summary>
        public async Task DeleteAllAsync()
        {
            await _collection.DeleteManyAsync(Builders<TEntity>.Filter.Empty);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        public IList<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        public async Task<IList<TEntity>> SearchAsync(FilterDefinition<TEntity> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// 根据条件查询（排序）
        /// </summary>
        public async Task<IList<TEntity>> SearchAsync(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort)
        {
            return await _collection.Find(filter).Sort(sort).ToListAsync();
        }
    }
}
