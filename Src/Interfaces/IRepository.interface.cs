using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Interfaces
{
   internal interface IRepository
    {
        DbSet<T> GetEntity<T>() where T : class;
        Task<T> GetOneAsync<T>() where T : class;
        Task<int> InsertOneAsync<T>(T AModel) where T : class;
        Task<int> InsertAllAsync<T>(List<T> AListModel) where T : class;
        Task<int> UpdateOneAsync<T>(T AModel) where T : class;
        Task<int> RemoveOneAsync<T>(T AModel) where T : class;
    }
}
