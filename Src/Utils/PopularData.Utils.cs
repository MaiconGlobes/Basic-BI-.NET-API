using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Utils
{
   internal class PopularDadosUtils
   {
      private static PopularDadosUtils FInstancia { get; set; }

      public static PopularDadosUtils Instancia()
      {
         FInstancia ??= new PopularDadosUtils();
         return FInstancia;
      }

      public IList<UserModel> PopularUser()
      {
         return new List<UserModel>
         {
            new()
            {
               Id = 1,
               Reference = new Guid("3FDDE6C3-4F54-4A27-958F-D72F785AA593"),
               Apelido = "Alberto Silva",
               Email = "usuario@usuario.com",
               Municipio = "Rio Claro",
               Uf = "SP",
               Idade = 23,
               Senha = "3FDDE6C3-4F54-4A27-958F-D72F785AA593",
            },
         };
      }
   }
}
