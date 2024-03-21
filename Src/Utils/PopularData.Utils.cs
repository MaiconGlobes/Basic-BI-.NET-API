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

      public PersonModel PopularPerson()
      {
         return new PersonModel
         {
            Id = 1,
            Nome = "Nome 1",
            Cpf_cnpj = "07640402948",
            Telefone = "19999999999" ,
         };
      }

      public UserModel PopularUsers()
      {
         return new UserModel()
         {
            Id = 1,
            Login = "Login",
            Email = "Email1@example.com",
            Senha = $"Senha criptografada {Guid.NewGuid()}",
            Refresh_token = Guid.NewGuid().ToString(),
            PessoaId = 1,
         };
      }

      public IList<AddressModel> PopularAddress()
      {
         return new List<AddressModel>
         {
            new()
            {
               Id = 1,
               Endereco = "Endereco 2" ,
               Numero = "1001",
               Bairro = "Bairro 2",
               Municipio = "Municipio 2",
               Uf = "SP",
               Cep = "13545215",
               PessoaId = 1
            },
            new()
            {
               Id = 2,
               Endereco = "Endereco 2" ,
               Numero = "1001",
               Bairro = "Bairro 2",
               Municipio = "Municipio 2",
               Uf = "SP",
               Cep = "13545215",
               PessoaId = 1
            },
            new()
            {
               Id = 3,
               Endereco = "Endereco 3" ,
               Numero = "1001",
               Bairro = "Bairro 3",
               Municipio = "Municipio 3",
               Uf = "SP",
               Cep = "13545215",
               PessoaId = 1
            },
         };
      }
   }
}
