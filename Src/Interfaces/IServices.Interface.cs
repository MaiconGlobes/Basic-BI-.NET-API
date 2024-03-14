namespace BaseCodeAPI.Src.Interfaces
{
   public interface IServices
   {
      public Task<(byte Status, object Json)> GetAllRegisters();
      public Task<(byte Status, object Json)> CreateRegister<T>(T AModel);
   }
}
