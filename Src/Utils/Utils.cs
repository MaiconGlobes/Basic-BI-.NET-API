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

      internal string GetCustomPropertyName<T>(T AInstance, string APropertyName)
      {
         var property = typeof(T).GetProperty(APropertyName);
         var attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
         return attribute?.Name ?? APropertyName;
      }

      internal T ExtractModelFromObject<T>(string AName = null)
      {
         var instance = InstanceModel.LoadInstance();
         var property = instance?.GetType().GetProperty(AName ?? typeof(T).Name);

         return (property != null)
             ? (T)property.GetValue(instance)
             : (T)instance;//throw new ArgumentException($"A propriedade '{typeof(T).Name}' não foi encontrada no objeto requerido.");

      }

      internal (int? id, RequestDelegate isNext) ValidateProcessPath(HttpContext context, RequestDelegate next, string uri)
      {
         var path = context.Request.Path;

         if (!path.HasValue || !path.Value.Contains(uri))
         {
            return (null, next);
         }

         var segments = path.Value.Split('/');

         if (segments.Length < 3 || string.IsNullOrEmpty(segments[2]))
         {
            throw new Exception("#Generic Error: A URL não contém o formato esperado.");
         }

         if (!int.TryParse(segments[2], out int id))
         {
            throw new Exception("#Generic Error: O ID na URL não é um número válido.");
         }

         return (id, null);
      }

      internal int ConvertKgToGram(double AValueKg)
      {
         return (int)(AValueKg * 1000);
      }

      internal bool VerificarSeSomenteDigitos(string APeso)
      {
         Regex regex = new(@"^0*[0-9]{1,6}$");
         return regex.IsMatch(APeso);
      }
   }
}
