using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Interfaces
{
   public interface IRepository
   {
      public Task<IEnumerable<UserModel>> GetAllRegister();

      public Task<int> CreateRegister(UserModel AUser);
   }
}
