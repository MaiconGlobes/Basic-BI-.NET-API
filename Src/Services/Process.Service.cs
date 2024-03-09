using ASControllerAPI.Src.Enums;
using ASControllerAPI.Src.Utils;

namespace ASControllerAPI.Src.Services
{
   public class ProcessService
   {
      public async Task<(byte Status, object Json)> ReturnProcessServices()
      {
         try
         {
            return await Task.Run(() =>
            {
               var objeto = new object(){};

               return ((byte)GeneralEnum.StatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(objeto));
            //return ((byte)GeneralEnum.StatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(process.Excecao));
            });
         }
         catch (Exception ex)
         {
            return ((byte)GeneralEnum.StatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
