using BaseCodeAPI.Src.Models.Entity;

namespace BaseCodeAPI.Src.Utils
{
   internal class PopularDataUtils
   {
      private static PopularDataUtils FInstancia { get; set; }

      public static PopularDataUtils Instancia()
      {
         FInstancia ??= new PopularDataUtils();
         return FInstancia;
      }

      public IList<PersonalModel> PopularPersonal()
      {
         return new List<PersonalModel>
         {
            new PersonalModel
            {
               Id = 1,
               Apelido = "Joao",
               Email = "joao@example.com",
               Telefone = "123456789",
               Endereco = "Rua A",
               Numero = "123",
               Bairro = "Centro",
               Municipio = "Sao Paulo",
               Uf = "SP",
               CEP = "12345678"
            },
         };
      }

      public IList<UserModel> PopularUser()
      {
         return new List<UserModel>
         {
            new()
            {
               Id = 1,
               Idade = 23,
               Senha = Guid.NewGuid().ToString(),
               Apelido = "Joao",
               Email = "joao@example.com",
               Telefone = "123456789",
               Endereco = "Rua A",
               Numero = "123",
               Bairro = "Centro",
               Municipio = "Sao Paulo",
               Uf = "SP",
               CEP = "12345678",
            },
         };
      }

      public IList<ClientModel> PopularClient()
      {
         return new List<ClientModel>
         {
            new()
            {
               Id = 1,
               Cpf_cnpj = "12345678901",
               Limite_credito = 1000.00,
               Apelido = "Joao",
               Email = "joao@example.com",
               Telefone = "123456789",
               Endereco = "Rua A",
               Numero = "123",
               Bairro = "Centro",
               Municipio = "Sao Paulo",
               Uf = "SP",
               CEP = "12345678",
            },
         };
      }

   }
}
