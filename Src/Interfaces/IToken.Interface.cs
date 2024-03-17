namespace BaseCodeAPI.Src.Interfaces
{
   public interface IToken
   {
      public Task<(byte Status, object Json)> CreateNewToken<T>(T AModel);
   }
}
