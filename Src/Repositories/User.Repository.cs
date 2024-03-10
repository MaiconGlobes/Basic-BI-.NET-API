using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class UserRepository
   {
      private Repository FRepository { get; set; }

      public UserRepository()
      {
         FRepository = new();
      }

      public async Task<IEnumerable<UserModel>> GetUserAllAsync()
      {
         return await this.FRepository.GetEntity<UserModel>().ToListAsync();
      }

      public async Task<int> CreateUser(UserModel AUser)
      {
         return await this.FRepository.InsertOneAsync(AUser);
      }
   }
}
