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

      public IList<UserModel> PopularUser()
      {
         return new List<UserModel>
         {
            new() {
               Id = 1,
               Senha = "Senha 1",
               Apelido = "Apelido 1",
               Email = "Email1@example.com",
               Cpf_cnpj = "07640402948",
               //Addresses
               Telefone = "19999999999"
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
               Limite_credito = 1000.00,
               PessoaId = 1
            },
         };
      }

      public IList<CarrierModel> PopularCarrier()
      {
         return new List<CarrierModel>
         {
            new()
            {
               Id = 1,
               Cnpj = "14011580111022",
               PessoaId = 1
            },
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
               Pessoa_id = 1
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
               Pessoa_id = 1
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
               Pessoa_id = 1
            },
         };
      }
   }
}
