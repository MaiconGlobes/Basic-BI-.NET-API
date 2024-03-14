namespace BaseCodeAPI.Src.Interfaces
{
   public interface IServices
   {
      public Task<(byte Status, object Json)> GetAllRegister();
      public Task<(byte Status, object Json)> CreateRegister<T>(T AModel);
   }
}
