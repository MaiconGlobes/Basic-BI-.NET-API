namespace BaseCodeAPI.Src.Interfaces
{
   public interface IServices
   {
      public Task<(byte Status, object Json)> GetAllRegistersAsync();
   }
}
