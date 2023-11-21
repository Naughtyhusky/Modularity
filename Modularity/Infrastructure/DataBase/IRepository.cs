﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    /// <summary>
    /// 业务仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IRepositoryBase<T> where T : EntityBase
    {

    }
}
