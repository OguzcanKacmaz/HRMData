using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface ITitleRepository : IRepository<Title>
    {
        Task<string> FindNameAsync(string Id);
        Task<string> FindIdAsync(string Id);
    }
}
