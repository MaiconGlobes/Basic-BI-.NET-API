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

         public IList<UserModel> PopularParametros()
         {
            return new List<UserModel>
         {
            new()
            {
               Id = 1,
               Name = "Alberto Silva",
            },
         };
         }
      }
}
