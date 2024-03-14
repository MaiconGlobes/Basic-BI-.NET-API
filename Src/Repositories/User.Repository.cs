using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class UserRepository : IRepository<UserModel>
   {
      private RepositoryBase FRepositoryBase { get; set; }

      public UserRepository()
      {
         FRepositoryBase = new RepositoryBase();
      }

      public async Task<IEnumerable<UserModel>> GetAllRegisterAsync()
      {
         return await FRepositoryBase.GetEntity<UserModel>().ToListAsync();
      }

      public async Task<int> CreateRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.InsertOneAsync(AModel);
      }
   }
}
