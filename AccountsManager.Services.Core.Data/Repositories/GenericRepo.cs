using AccountsManager.Services.Core.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AccountsManager.Services.Core.Data.Repositories
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        #region Properties
        internal CoreDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepo(CoreDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        #endregion

        #region Methods
        public virtual IEnumerable<TEntity> Get(int pageNumber,
                                                int pageSize, 
                                                Expression<Func<TEntity, bool>> filter = null,
                                                Expression<Func<TEntity, string>> orderBy = null,
                                                string includeProperties = "")
        {
            int skip = (pageNumber - 1) * pageSize;
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                if (pageNumber == 0)
                {
                    return query.OrderBy(orderBy).ToList();
                }
                else
                {
                    return query.OrderBy(orderBy).Skip(skip).Take(pageSize).ToList();
                }
            }
            else
            {
                if (pageNumber == 0)
                {
                    return query.ToList();
                }
                else
                {
                    return query.OrderBy(x => 0).Skip(skip).Take(pageSize).ToList();
                }
            }
        }

        public virtual int GetTotalCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                return query.Where(filter).Count();
            }
            else
            {
                return query.Count();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity, Guid? userId)
        {
            if (userId != null)
            {
                if (entity.GetType().GetProperty("CreatedBy") != null)
                {
                    entity.GetType().GetProperty("CreatedBy").SetValue(entity, userId.Value);
                }
                if (entity.GetType().GetProperty("CreatedOn") != null)
                {
                    entity.GetType().GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow);
                }
            }
            dbSet.Add(entity);
        }

        public virtual void Insert(List<TEntity> entities, Guid? userId)
        {
            if (userId != null)
            {
                foreach (var entity in entities)
                {
                    if (entity.GetType().GetProperty("CreatedBy") != null)
                    {
                        entity.GetType().GetProperty("CreatedBy").SetValue(entity, userId.Value);
                    }
                    if (entity.GetType().GetProperty("CreatedOn") != null)
                    {
                        entity.GetType().GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow);
                    }
                }
            }
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate, Guid? userId)
        {
            if (userId != null)
            {
                if (entityToUpdate.GetType().GetProperty("ModifiedBy") != null)
                {
                    entityToUpdate.GetType().GetProperty("ModifiedBy").SetValue(entityToUpdate, userId.Value);
                }
                if (entityToUpdate.GetType().GetProperty("ModifiedOn") != null)
                {
                    entityToUpdate.GetType().GetProperty("ModifiedOn").SetValue(entityToUpdate, DateTime.UtcNow);
                }
            }
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(List<TEntity> entitiesToUpdate, Guid? userId)
        {
            foreach (var entityToUpdate in entitiesToUpdate)
            {
                if (userId != null)
                {
                    if (entityToUpdate.GetType().GetProperty("ModifiedBy") != null)
                    {
                        entityToUpdate.GetType().GetProperty("ModifiedBy").SetValue(entityToUpdate, userId.Value);
                    }
                    if (entityToUpdate.GetType().GetProperty("ModifiedOn") != null)
                    {
                        entityToUpdate.GetType().GetProperty("ModifiedOn").SetValue(entityToUpdate, DateTime.UtcNow);
                    }
                }
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified; 
            }
        }
        #endregion
    }
}