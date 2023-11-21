using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TreeModel<T>
    {
        /// <summary>
        /// 当前节点
        /// </summary>
        public T? Item { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IEnumerable<TreeModel<T>>? Children { get; set; }
    }
}
