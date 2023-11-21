using Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;

namespace Infrastructure.DataBase
{
    public class DiamondhuskyDbContext(DbContextOptions<DiamondhuskyDbContext> options) : DbContext(options)
    {

        /// <summary>
        /// 当前事务
        /// </summary>
        private IDbContextTransaction? _currentTransaction;

        /// <summary>
        /// 获取当前事务
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// 当前DbContext是否开启了事务
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisters = [];
            foreach (var module in GlobalConfiguration.Modules)
            {
                var list = module.Assembly?.DefinedTypes.Select(t => t.AsType());

                if (list != null && list.Any())
                {
                    typeToRegisters.AddRange(list);
                }                
            }

            RegisterEntities(modelBuilder, typeToRegisters);

            base.OnModelCreating(modelBuilder);

            RegisterCustomMappings(modelBuilder, typeToRegisters);
        }


        /// <summary>
        /// 注册各个模块的实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="typeToRegisters"></param>
        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(x => x.GetTypeInfo().IsSubclassOf(typeof(EntityBase)) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }


        /// <summary>
        /// 反射执行模块里面的ModelBuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="typeToRegisters"></param>
        private static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
            foreach (var builderType in customModelBuilderTypes)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType)!;
                    builder.Build(modelBuilder);
                }
            }
        }


        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolation"></param>
        /// <returns></returns>
        public async Task<IDbContextTransaction?> BeginTransactionAsync(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(isolation);

            return _currentTransaction;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CommitTransactionAsync(IDbContextTransaction? transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);
            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }
              
            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
