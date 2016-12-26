using System.Collections.Generic;

namespace ControlApp.Application.Interfaces
{
    public interface IAppServiceBase<TEntity>
        where TEntity : class
    {
        void Add(TEntity obj);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllActive();
    }
}
