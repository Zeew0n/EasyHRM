using BeeHRM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace BeeHRM.Repository.Implementations
{ /// <summary>
  /// Generic Repository base class
  /// </summary>
  /// <typeparam name="TEntity">Object to be processed</typeparam>
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private members
        internal dbBeeHRMEntities context;
        private readonly DbSet<TEntity> _dbSet;
        private const bool ShareContext = false;

        #endregion

        #region Constructors

        public RepositoryBase()
        {
            context = new dbBeeHRMEntities();

        }
        public RepositoryBase(dbBeeHRMEntities context)
        {
            this.context = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Protected properties

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        #endregion

        #region Public methods

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            return query.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetOrderBy(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, bool>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return query.OrderBy(orderBy);
            }
            return query.AsEnumerable();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);

        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            context.SaveChanges();

        }

        public virtual int Update(TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }
            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();

        }
        public virtual void Update(TEntity oldEntity, TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }
            context.Entry(oldEntity).CurrentValues.SetValues(entityToUpdate);
            context.SaveChanges();
        }
        public virtual void DetachAndUpdate(DbContext ctxt, TEntity originalEntity, TEntity entityToUpdate)
        {
            var objectContext = ((IObjectContextAdapter)ctxt).ObjectContext;
            var objSet = objectContext.CreateObjectSet<TEntity>();
            var entityKey = objectContext.CreateEntityKey(objSet.EntitySet.Name, originalEntity);

            Object foundEntity;
            var exists = objectContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (!exists)
            {
                ctxt.Set<TEntity>().Attach(entityToUpdate);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                objectContext.Detach(foundEntity);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }

        }

        public virtual Boolean Exists(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public IEnumerable<TEntity> All()
        {
            return DbSet.AsEnumerable<TEntity>().ToList();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public object SortDirection { get; private set; }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public TEntity Create(TEntity tEntity)
        {


            try
            {
                var newEntry = DbSet.Add(tEntity);
                //if (ShareContext)
                context.SaveChanges();
                return newEntry;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (ShareContext)
            {
                return context.SaveChanges();
            }
            else
            {
                return 0;
            }


        }

        //public IEnumerable<TEntity> GetWithPaging(out int totalPages, Expression<Func<TEntity, bool>> filter = null,
        //    int page = 0, int pageSize = 0, string orderBy = null, SortDirection listSortDirection = SortDirection.ASCENDING)
        //{
        //    if (page == default(int))
        //    {
        //        page = 1;
        //    }

        //    if (pageSize == default(int))
        //    {
        //        pageSize = 10;
        //    }

        //    var query = DbSet.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (orderBy == null || typeof(TEntity).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
        //    {
        //        var defaultOrderBy = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.ToLower().Contains("id")).Name;
        //        query = (listSortDirection == SortDirection.ASCENDING) ? query.OrderBy(defaultOrderBy) : query.OrderByDescending(defaultOrderBy);
        //    }
        //    else
        //    {
        //        query = (listSortDirection == SortDirection.ASCENDING) ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        //    }

        //    totalPages = (int)Math.Ceiling((double)query.Count() / (double)pageSize);

        //    if (pageSize != default(int))
        //    {
        //        query = query.Skip(pageSize * (page - 1)).Take(pageSize);
        //    }

        //    return query.ToList();
        //}

        //to run stored procedre
        public int ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(query, parameters);

        }
        //to read from stored procedure
        public IEnumerable<TEntity> ExecReadWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(query, parameters);
        }

        public List<TEntity> ExecuteSP(string sql)//params object[] parameters
        {
            return context.Database.SqlQuery<TEntity>(sql).ToList();
        }

        public TEntity ExecuteSPwithParameterForSingleRow(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(sql, parameters).FirstOrDefault();
        }

        public List<TEntity> ExecuteSPwithParameterForList(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(sql, parameters).ToList();
        }

        //public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        //{
        //    return context.Database.SqlQuery<T>(sql, parameters);
        //}

        #endregion
    }
}
