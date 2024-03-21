using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class AuthenticateService : IAuthenticate
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }

      public AuthenticateService(IMapper AIMapper, IRepository<UserModel> AiRepository, IHttpContextAccessor httpContextAccessor)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
      }

      /// <summary>
      /// Processa o login do usuário com base nos dados fornecidos no modelo especificado. Retorna um objeto JSON contendo o token de acesso em caso de sucesso ou uma mensagem de erro em caso de falha.
      /// </summary>
      /// <typeparam name="T">O tipo de modelo de dados usado para o login.</typeparam>
      /// <param name="AModel">O modelo de dados contendo as informações de login do usuário.</param>
      /// <returns>Um objeto contendo o status da operação e o token de acesso em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
      public async Task<(byte Status, object Json)> ProcessLoginAsync<T>(T AModel)
      {
         try
         {
            var userModelDto = AModel as UserModelDto;
            var user = this.FMapper.Map<UserModel>(userModelDto);

            user.Senha = SecurityService.New().EncryptPassword(userModelDto.Senha);

            var usersObject = await FIRepository.GetOneRegisterAsync(user);

            if (usersObject != null)
            {
               var newObject = new
               {
                  login    = userModelDto.Login,
                  email    = userModelDto.Email,
                  password = userModelDto.Senha,
               };

               var newToken = SecurityService.New().GenerateToken(newObject);

               var objReturn = new
               {
                  token = newToken
               };
               
               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(objReturn));
            }

            return UtilsClass.New().ProcessExceptionMessage(new Exception("Ooops! Acesso não autorizado."));
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
            var hours        = 24;
            var minutes      = 60;
            var days         = 30;

            if (user != null || person != null)
            {
               user.Senha         = SecurityService.New().EncryptPassword(userModelDto.Senha);
               user.Refresh_token = SecurityService.New().GenerateToken(userModelDto, (hours * minutes * days));
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
