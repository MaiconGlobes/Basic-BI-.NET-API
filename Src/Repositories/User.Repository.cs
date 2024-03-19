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

      public async Task<UserModel> GetOneRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.GetEntity<UserModel>()
                                     .Where(user => user.Email == AModel.Email && user.Senha == AModel.Senha)
                                     .SingleOrDefaultAsync();
      }

      public async Task<IEnumerable<UserModel>> GetAllRegisterAsync()
      {
         var users = await FRepositoryBase.GetEntity<UserModel>().Include(u => u.Person).ToListAsync();

         var result = await FRepositoryBase.GetEntity<UserModel>()
                          .Include(u => u.Person)
                          .Select(u => new UserModel
                          {
                             Id = u.Id,
                             Apelido = u.Apelido,
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

      public async Task<int> CreateRegisterAsync(UserModel AModel)
      {
         return await FRepositoryBase.InsertOneAsync(AModel);
      }
   }
}
