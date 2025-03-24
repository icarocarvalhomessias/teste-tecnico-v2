using Thunders.TechTest.ApiService.Common;

namespace Thunders.TechTest.ApiService.Data.Repositories;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
