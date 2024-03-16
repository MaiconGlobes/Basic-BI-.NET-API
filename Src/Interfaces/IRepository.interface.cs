namespace BaseCodeAPI.Src.Interfaces
{
   public interface IRepository<T>
   {
      public Task<IEnumerable<T>> GetAllRegisterAsync();

      public Task<int> CreateRegisterAsync(T AModel);
   }
}
