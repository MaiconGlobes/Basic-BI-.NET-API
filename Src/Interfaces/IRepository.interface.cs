using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Interfaces
{
   public interface IRepository
   {
      public Task<IEnumerable<UserModel>> GetUserAllAsync();

      public Task<int> CreateUser(UserModel AUser);
   }
}
