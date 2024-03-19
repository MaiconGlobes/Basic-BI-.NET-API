using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class UserService : IServices
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }
      private string FToken { get; set; }

      public UserService(IMapper AIMapper, IRepository<UserModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
         this.FToken = (string)httpContextAccessor?.HttpContext?.Items["Token"]!;
      }

      /// <summary>
      /// Obtém todos os registros de usuários do banco de dados de forma assíncrona e retorna um status e um objeto JSON correspondente aos registros.
      /// </summary>
      /// <param name="httpContextAccessor">O acessador de contexto HTTP para obter o token de autorização.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna uma tupla contendo o status da operação e o objeto JSON correspondente aos registros de usuários.</returns>
      public async Task<(byte Status, object Json)> GetAllRegistersAsync()
      {
         try
         {
            var usersObject = await FIRepository.GetAllRegisterAsync();

            var objReturn = new
            {
               token = (string.IsNullOrEmpty(this.FToken) ? null : this.FToken),
               usuario = usersObject
            };

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      /// <summary>
      /// Cria um novo registro de usuário de forma assíncrona no banco de dados com base nos dados do modelo fornecido e retorna um status e um objeto JSON correspondente ao resultado da operação.
      /// </summary>
      /// <typeparam name="T">O tipo do modelo.</typeparam>
      /// <param name="AModel">O modelo de dados a ser utilizado para criar o registro de usuário.</param>
      /// <returns>Uma tarefa que representa a operação assíncrona e retorna uma tupla contendo o status da operação e o objeto JSON correspondente ao novo registro de usuário.</returns>
      public async Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         try
         {
            var userModelDto = AModel as UserModelDto;
            var user         = this.FMapper.Map<UserModel>(userModelDto);
            var person       = this.FMapper.Map<PersonModel>(userModelDto.Pessoa);

            if (user != null || person != null)
            {
               user.Senha         = SecurityService.New().EncryptPassword(userModelDto.Senha);
               user.Refresh_token = SecurityService.New().GenerateToken(userModelDto, 10080);
               user.Person        = person!;

               int rowsAffect = await FIRepository.CreateRegisterAsync(user);

               if (rowsAffect > 0)
               {
                  userModelDto.PessoaId = person.Id;
                  userModelDto.Token    = SecurityService.New().GenerateToken(userModelDto, 1);
                  userModelDto.Senha    = null;

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(userModelDto));
               }
            }

            throw new Exception("Houve um erro ao salvar o usuário.");
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
