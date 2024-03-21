namespace BaseCodeAPI.Src.Interfaces
{
   public interface IAuthenticate
   {
      public Task<(byte Status, object Json)> ProcessLoginAsync<T>(T AModel);
      public Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel);
   }
}
