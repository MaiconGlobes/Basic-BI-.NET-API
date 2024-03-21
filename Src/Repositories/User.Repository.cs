using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class UserRepository : IRepository<UserModel>
   {
      private RepositoryBase FRepositoryBase { get; set; }

      public UserRepository()
      {
         FRepositoryBase = new RepositoryBase();
      }

      /// <summary>
      /// Obtém um registro de usuário do banco de dados de forma assíncrona com base no email e senha fornecidos.
      /// </summary>
      /// <param name="AModel">O modelo de usuário contendo o email e senha para a consulta.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna o registro de usuário encontrado.</returns>
      public async Task<UserModel> GetOneRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.GetEntity<UserModel>()
                                     .Where(user => user.Login == AModel.Login.ToLower() && user.Email == AModel.Email && user.Senha == AModel.Senha)
                                     .SingleOrDefaultAsync();
      }

      /// <summary>
      /// Obtém todos os registros de usuários do banco de dados de forma assíncrona, incluindo informações relacionadas à pessoa associada a cada usuário.
      /// </summary>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna uma coleção de objetos UserModel contendo os dados dos usuários e suas respectivas informações de pessoa.</returns>
      public async Task<IEnumerable<UserModel>> GetAllRegisterAsync()
      {
         var users = await FRepositoryBase.GetEntity<UserModel>().Include(u => u.Person).ToListAsync();

         var result = await FRepositoryBase.GetEntity<UserModel>()
                          .Include(u => u.Person)
                          .Select(u => new UserModel
                          {
                             Id = u.Id,
                             Login = u.Login,
                             Email = u.Email,
                             Senha = u.Senha,
                             PessoaId = u.PessoaId,
                             Person = new PersonModel 
                             {
                                Nome = u.Person.Nome,
                                Cpf_cnpj = u.Person.Cpf_cnpj,
                                Telefone = u.Person.Telefone,
                                Addresses = u.Person.Addresses,
                             },
                          })
                          .ToListAsync();

         return result;
      }

      /// <summary>
      /// Cria um novo registro de usuário de forma assíncrona no banco de dados.
      /// </summary>
      /// <param name="AModel">O modelo de usuário a ser criado.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna o número de registros afetados.</returns>
      public async Task<int> CreateRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.InsertOneAsync(AModel);
      }
   }
}
