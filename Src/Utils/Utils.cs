using BaseCodeAPI.Src.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ASControllerAPI.Src.Utils
{
   internal class UtilsClass
   {
      internal static UtilsClass FInstancia { get; set; }

      internal static UtilsClass New()
      {
         FInstancia ??= new UtilsClass();
         return FInstancia;
      }

      internal RequestDelegate ValidatePathUserAll(HttpContext context, RequestDelegate next, string uri)
      {
         var path = context.Request.Path;

         if (!path.HasValue || !path.Value.Contains(uri))
         {
            return next;
         }

         var segments = path.Value.Split('/');

         if (segments.Length > 3)
         {
            throw new Exception("A URL não contém o formato esperado.");
         }

         return null;
      }

      //internal (int? id, RequestDelegate isNext) ValidatePathUserById(HttpContext context, RequestDelegate next, string uri)
      //{
      //   var path = context.Request.Path;

      //   if (!path.HasValue || !path.Value.Contains(uri))
      //   {
      //      return (null, next);
      //   }

      //   var segments = path.Value.Split('/');

      //   if (segments.Length > 4)
      //   {
      //      throw new Exception("A URL não contém o formato esperado.");
      //   }

      //   if (!int.TryParse(segments[2], out int id))
      //   {
      //      throw new Exception("O ID na URL não é um número válido.");
      //   }

      //   return (id, null);
      //}
   }
}
