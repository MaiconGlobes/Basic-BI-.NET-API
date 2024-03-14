using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class UserRepository : IRepository
   {
      private RepositoryBase FRepositoryBase { get; set; }


      public UserRepository()
      {
         FRepositoryBase = new();
      }

      public async Task<IEnumerable<UserModel>> GetUserAllAsync()
      {
         return await this.FRepositoryBase.GetEntity<UserModel>().ToListAsync();
      }

      public async Task<int> CreateUser(UserModel AUser)
      {
         return await this.FRepositoryBase.InsertOneAsync(AUser);
      }
   }
}
