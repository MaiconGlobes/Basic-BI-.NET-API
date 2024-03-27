using BaseCodeAPI.Src.Enums;

namespace BaseCodeAPI.Src.Utils
{
   internal class UtilsClass
   {
      internal static UtilsClass FInstancia { get; set; }

      internal static UtilsClass New()
      {
         FInstancia ??= new UtilsClass();
         return FInstancia;
      }

      /// <summary>
      /// Retorna uma função que recebe uma exceção e retorna uma resposta não autorizada.
      /// </summary>
      /// <returns>Uma função que recebe uma exceção e retorna uma resposta não autorizada.</returns>
      private Func<Exception, object> GetUnauthorized()
      {
         return ex => ResponseUtils.Instancia().ReturnUnauthorized(new Exception(ex.Message));
      }

      /// <summary>
      /// Retorna uma função que recebe uma exceção e retorna uma resposta de erro por entrada duplicada.
      /// </summary>
      /// <returns>Uma função que recebe uma exceção e retorna uma resposta de erro por entrada duplicada.</returns>
      private Func<Exception, object> GetDuplicateEntryError()
      {
         return ex => {
            var indexForKey  = ex.InnerException.Message.IndexOf("for key");
            var errorMessage = ex.InnerException.Message[..indexForKey].Trim();
            errorMessage = errorMessage.Replace("Duplicate entry", "Registro já cadastrado para");

            var exceptionReturn = new Exception(errorMessage);

            return ResponseUtils.Instancia().ReturnDuplicated(exceptionReturn);
         };
      }

      /// <summary>
      /// Processa uma exceção e retorna um status e um objeto JSON correspondente ao erro.
      /// </summary>
      /// <param name="ex">A exceção a ser processada.</param>
      /// <returns>Uma tupla contendo o status do erro e o objeto JSON correspondente.</returns>
      internal (byte Status, object Json) ProcessExceptionMessage(Exception ex)
      {
         Dictionary<string, (byte, Func<Exception, object>)> errorMappings = new()
         {
            { "Token enviado é inválido", ((byte)GlobalEnum.eStatusProc.NaoAutorizado, GetUnauthorized())},
            { "Token inexistente no header", ((byte)GlobalEnum.eStatusProc.NaoAutorizado, GetUnauthorized())},
            { "Usuário não autorizado", ((byte)GlobalEnum.eStatusProc.NaoAutorizado, GetUnauthorized())},
            { "Duplicate entry", ((byte)GlobalEnum.eStatusProc.RegistroDuplicado, GetDuplicateEntryError())},
         };
     
         if ((ex != null) || (ex.InnerException != null))
         {
            string message = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);

            foreach (var kvp in errorMappings)
            {
               if (message.Contains(kvp.Key))
               {
                  var (status, func) = kvp.Value;
                  return (status, func(ex));
               }
            }
         }

         return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().ReturnErrorProcess(ex));
      }
   }
}
