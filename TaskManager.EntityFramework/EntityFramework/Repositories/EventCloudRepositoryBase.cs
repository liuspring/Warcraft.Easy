using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace TaskManager.EntityFramework.Repositories
{
    public abstract class TaskManagerRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TaskManagerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TaskManagerRepositoryBase(IDbContextProvider<TaskManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class TaskManagerRepositoryBase<TEntity> : TaskManagerRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TaskManagerRepositoryBase(IDbContextProvider<TaskManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
