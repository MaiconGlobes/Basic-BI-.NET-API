namespace BaseCodeAPI.Src.Interfaces
{
   public interface IRepository<T>
   {
      public Task<T> GetOneRegisterAsync(T AModel);
      public Task<IEnumerable<T>> GetAllRegisterAsync();
      public Task<int> CreateRegisterAsync(T AModel);
   }
}
