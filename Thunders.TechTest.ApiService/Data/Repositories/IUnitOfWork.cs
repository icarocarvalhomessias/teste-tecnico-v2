namespace Thunders.TechTest.ApiService.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
