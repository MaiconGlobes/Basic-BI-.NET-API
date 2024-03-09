namespace ASControllerAPI.Src.Enums
{
   public class GeneralEnum
   {
      private static GeneralEnum FInstancia { get; set; }

      public static GeneralEnum Instancia()
      {
         FInstancia ??= new GeneralEnum();
         return FInstancia;
      }

      public enum StatusProc : byte
      {
         Sucesso           = 3,
         SemRegistros      = 20,
         RegistroDuplicado = 30,
         NaoLocalizado     = 32,
         ImpressoraOffLine = 33,
         ErroProcessamento = 98,
         ErroServidor      = 99
      }

      public enum eTipoProcesso : byte
      {
         Principal  = 1,
         SemBalanca = 2,
         Secundario = 3
      }

      public enum eValores : byte
      {
         Tara                 = 0,
         ValorQuilo           = 1,
         PesoLiquido          = 2,
         PesoBruto            = 3,
         TotalVenda           = 4,
         Sequencia            = 5,
         IdTipoPesagem        = 6,
         DescricaoTipoPesagem = 7,
         StatusImpressao      = 8
      }

      public enum eComandosImpressora
      {
         FonteNormal             = 0,
         FonteDuplaAltura        = 1,
         FonteDuplaLargura       = 2,
         FonteDuplaAlturaLargura = 3,
         FonteSmall              = 4
      }
   }
}
