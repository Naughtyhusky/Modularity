using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 列表生成树形节点
        /// </summary>
        /// <typeparam name="T">集合对象的类型</typeparam>
        /// <typeparam name="K">父节点的类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="idSelector">主键ID</param>
        /// <param name="parentIdSelector">父节点</param>
        /// <param name="rootId">根节点</param>
        /// <param name="orderKeySelector">排序</param>
        /// <returns>列表生成树形节点</returns>
        public static IEnumerable<TreeModel<T>> GenerateTree<T, K, OrderKey>(
            this IEnumerable<T> collection,
            Func<T, K> idSelector,
            Func<T, K> parentIdSelector,
            Func<T, OrderKey> orderKeySelector,
            K rootId = default) where T : class where K : struct
        {
            foreach (var c in collection.Where(u =>
            {
                var selector = parentIdSelector(u);
                return rootId.Equals(selector);
            }).OrderBy(orderKeySelector))
            {
                yield return new TreeModel<T>
                {
                    Item = c,
                    Children = collection.GenerateTree(idSelector, parentIdSelector, orderKeySelector, idSelector(c))
                };
            }
        }
    }
}
