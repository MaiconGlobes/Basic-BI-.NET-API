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

      internal RequestDelegate StepValidation(HttpContext context, RequestDelegate next, string uri)
      {
         var path = context.Request.Path;

         if (path.HasValue || path.Value.Contains(uri))
         {
            return next;
         }

         //var segments = path.Value.Split('/');

         //if (segments.Length > 3)
         //{
         //   throw new Exception("A URL não contém o formato esperado.");
         //}

         return null;
      }

      private Func<Exception, object> GetUnauthorized()
      {
         return ex => ResponseUtils.Instancia().RetornoUnauthorized(new Exception(ex.Message));
      }

      private Func<Exception, object> GetDuplicateEntryError()
      {
         return ex => {
            int indexForKey = ex.InnerException.Message.IndexOf("for key");
            string errorMessage = ex.InnerException.Message[..indexForKey].Trim();
            errorMessage = errorMessage.Replace("Duplicate entry", "Registro já cadastrado para");

            var exceptionReturn = new Exception(errorMessage);

            return ResponseUtils.Instancia().RetornoDuplicated(exceptionReturn);
         };
      }

      internal (byte Status, object Json) ProcessExceptionMessage(Exception ex)
      {
         Dictionary<string, (byte, Func<Exception, object>)> errorMappings = new()
         {
            { "Token enviado é inválido", ((byte)GlobalEnum.eStatusProc.NaoAutorizado, GetUnauthorized())},
            { "Token inexistente no header", ((byte)GlobalEnum.eStatusProc.NaoAutorizado, GetUnauthorized())},
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

         return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
      }
   }
}
