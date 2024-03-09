using BaseCodeAPI.Src.Models.Entity;

namespace ASUpdateServices.Src.Utils
{
    internal class PopularDadosUtils
      {
         private static PopularDadosUtils FInstancia { get; set; }

         public static PopularDadosUtils Instancia()
         {
            FInstancia ??= new PopularDadosUtils();
            return FInstancia;
         }

         public IList<ParametroModel> PopularParametros()
         {
            return new List<ParametroModel>
         {
            new()
            {
               Id = 1,
               Telefone_suporte = "19999999999",
            },
         };
         }
      }
}
