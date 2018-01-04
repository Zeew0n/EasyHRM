using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.Repository.Interfaces
{

    /// <summary>
    /// Interface for Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        /// <returns></returns>
        bool Contains(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns></returns>
        TEntity Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="tEntity">Specified a new object to create.</param>
        /// <returns></returns>
        TEntity Create(TEntity tEntity);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Get the list of objects or specific object according to the condition
        /// </summary>
        /// <param name="filter">filter condition</param>
        /// <param name="orderBy">orderby column</param>
        /// <param name="includeProperties">include reference entitites</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

       // IEnumerable<TEntity> GetWithPaging(out int totalPages, Expression<Func<TEntity, bool>> filter = null, int page = 0, int pageSize = 0, string orderBy = null, SortDirection listSortDirection = SortDirection.ASCENDING);

        IEnumerable<TEntity> GetOrderBy(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, bool>> orderBy = null,
            string includeProperties = "");
        /// <summary>
        /// Get the object by id, but it does not go all the way to db
        /// </summary>
        /// <param name="id">Id of the object</param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Get the list of object by passing query
        /// </summary>
        /// <param name="query">query string</param>
        /// <param name="parameters">extra params</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        /// <summary>
        /// Inserts an object - post operation
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Deletes an object acc. to Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// Delete an object passed
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Updates an object passed
        /// </summary>
        /// <param name="entityToUpdate"></param>
        int Update(TEntity entityToUpdate);

        /// <summary>
        /// Use this if we need to get rid of optimistic concurrency
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity oldEntity, TEntity entityToUpdate);

        /// <summary>
        /// Check if the object exists
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Boolean Exists(TEntity entity);

        ///// <summary>
        ///// Delete the object from database.
        ///// </summary>
        ///// <param name="t">Specified a existing object to delete.</param>
        //int Delete(TEntity tEntity);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        //  DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);

        int ExecWithStoreProcedure(string query, params object[] parameters);
        ///// <summary>
        ///// Update object changes and save to database.
        ///// </summary>
        ///// <param name="t">Specified the object to save.</param>
        ///// <returns></returns>
        //int Update(TEntity tEntity);

        IEnumerable<TEntity> ExecReadWithStoreProcedure(string query, params object[] parameters);

        List<TEntity> ExecuteSPwithParameterForList(string query, params object[] parameters);
    }
}
