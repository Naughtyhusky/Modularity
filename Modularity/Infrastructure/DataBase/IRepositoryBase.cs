using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T> where T : EntityBase
    {
        /// <summary>
        /// 返回DbSet
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        /// <summary>
        /// 返回无追踪实体的DbSet
        /// </summary>
        /// <returns></returns>
        IQueryable<T> NoTrackingQuery();

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entity"></param>
        void AddRange(IEnumerable<T> entity);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(params T[] entities);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 保存操作
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        void Delete(T entity);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(params T[] entities);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task DeleteAsync(params T[] entities);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task DeleteAsync(IEnumerable<T> entities);
      
    }
}
