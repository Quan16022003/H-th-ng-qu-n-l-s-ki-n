﻿using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Ultils;
using System.Linq.Expressions;
using Domain.Commons;

namespace Persistence.Repositories
{
    /// <summary>
    /// Generic Repository class for performing Database Entity Operations.
    /// </summary>
    /// <typeparam name="T">The Type of Entity to operate on</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected ReferenceCollection<T> References { get; }

        #region Readonlys

        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The Database Context</param>
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
            References = [];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity that was added</returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns><see cref="Task"/></returns>
        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes entities based on a condition.
        /// </summary>
        /// <param name="filter">The condition the entities must fulfil to be deleted</param>
        /// <returns><see cref="Task"/></returns>
        public Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var entities = _dbSet.Where(filter);

            _dbSet.RemoveRange(entities);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets a collection of all entities.
        /// </summary>
        /// <returns>A collection of all entities</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ExcuteAllInclude(References).ToListAsync();
        }

        /// <summary>
        /// Gets an entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve</param>
        /// <returns>The entity object if found, otherwise null</returns>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            T? result = await _dbSet
                .SingleOrDefaultAsync(e => e.Id == id);

            return result;
        }

        /// <summary>
        /// Gets a collection of entities based on the specified criteria.
        /// </summary>
        /// <param name="filter">The condition the entities must fulfil to be returned</param>
        /// <param name="orderBy">The function used to order the entities</param>
        /// <param name="top">The number of records to limit the results to</param>
        /// <param name="skip">The number of records to skip</param>
        /// <param name="includeProperties">Any other navigation properties to include when returning the collection</param>
        /// <returns>A collection of entities</returns>
        public virtual async Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? top = null,
            int? skip = null,
            params string[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Thực hiện xóa mềm một entity.
        /// </summary>
        /// <param name="entity">Entity cần xóa mềm</param>
        public async Task SoftDeleteAsync(T entity)
        {
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null)
            {
                property.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Thực hiện xóa mềm nhiều entities dựa trên điều kiện.
        /// </summary>
        /// <param name="filter">Điều kiện để chọn các entities cần xóa mềm</param>
        public async Task SoftDeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var entities = await _dbSet.Where(filter).ToListAsync();
            
            foreach (var entity in entities)
            {
                var property = entity.GetType().GetProperty("IsDeleted");
                if (property != null)
                {
                    property.SetValue(entity, true);
                    _dbSet.Update(entity);
                }
            }
        }

        #endregion
    }
}
