using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Domain.Entities;
using Maruko.Core.Domain.Repositories;

namespace Maruko.FreeSql.Internal.Repos
{
    public interface IFreeSqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        IFreeSql GetAll();
    }
}
