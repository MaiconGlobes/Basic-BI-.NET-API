namespace BaseCodeAPI.Src.Enums
{
   public class GlobalEnum
   {
      private static GlobalEnum FInstancia { get; set; }

      public static GlobalEnum Instancia()
      {
         FInstancia ??= new GlobalEnum();
         return FInstancia;
      }


      public enum eStatusProc : byte
      {
         Sucesso           = 3,
         SemRegistros      = 20,
         RegistroDuplicado = 30,
         NaoLocalizado     = 32,
         ImpressoraOffLine = 33,
         NaoAutorizado     = 41,
         ErroProcessamento = 98,
         ErroServidor      = 99
      }
   }
}
