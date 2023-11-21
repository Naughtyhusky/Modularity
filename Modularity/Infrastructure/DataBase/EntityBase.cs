using Infrastructure.Bus;
using Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    /// <summary>
    /// 业务实体基类
    /// </summary>
    public abstract class EntityBase
    {

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <param name="createUserName">创建者名称</param>
        /// <param name="createUserId">创建者id</param>
        /// <param name="timeAlignment">时间对齐 CreateTime和UpdateTime是否保持一致</param>
        protected EntityBase(long createUserId = default, string createUserName = "", bool timeAlignment = false)
        {
            CreateUserId = createUserId;
            CreateTime = DateTime.Now;
            CreateUserName = createUserName;

            UpdateUserId = default;
            UpdateUserName = string.Empty;
            UpdateTime = timeAlignment ? CreateTime : GetDefaultDateTime();
        }

        /// <summary>
        /// id
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; init; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public long CreateUserId { get; init; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreateUserName { get; init; }

        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public DateTime? UpdateTime { get; private set; }

        /// <summary>
        /// 最后一次更新数据的用户Id
        /// </summary>
        public long? UpdateUserId { get; private set; }

        /// <summary>
        /// 最后一次更新用户的名称
        /// </summary>
        public string? UpdateUserName { get; private set; }


        /// <summary>
        /// 领域事件
        /// </summary>
        private List<EventBase>? _domainEvents;


        int hashCode = default;

        /// <summary>
        /// 获取领域事件
        /// </summary>
        public IReadOnlyCollection<EventBase>? GetDomainEvents() => _domainEvents?.AsReadOnly();


        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(EventBase eventItem)
        {
            _domainEvents ??= [];
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// 移除领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(EventBase eventItem) => _domainEvents?.Remove(eventItem);


        /// <summary>
        /// 清空领域事件
        /// </summary>
        public void ClearDomainEvents() => _domainEvents?.Clear();

        /// <summary>
        /// 更改数据后记录更改信息
        /// </summary>
        /// <param name="updateUserId"></param>
        /// <param name="updateUserName"></param>
        protected virtual void UpdateRecord(long updateUserId = default, string updateUserName = "")
        {
            this.UpdateTime = DateTime.Now;
            this.UpdateUserName = updateUserName;
            this.UpdateUserId = updateUserId;
        }

        /// <summary>
        /// 获取一个系统的时间默认值
        /// </summary>
        /// <returns></returns>
        protected static DateTime GetDefaultDateTime()
        {
            return DateTimeHelper.GetDefaultTime();
        }


        /// <summary>
        /// 重写方法 相等运算
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            var compareTo = obj as EntityBase;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }
        /// <summary>
        /// 重写方法 实体比较 ==
        /// </summary>
        /// <param name="a">领域实体a</param>
        /// <param name="b">领域实体b</param>
        /// <returns></returns>
        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }
        /// <summary>
        /// 重写方法 实体比较 !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }
        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Id == default)
            {
                return base.GetHashCode();
            }

            if (hashCode != default)
            {
                hashCode = Id.GetHashCode() ^ 31;
            }

            return hashCode;
        }
        /// <summary>
        /// 输出领域对象的状态
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
