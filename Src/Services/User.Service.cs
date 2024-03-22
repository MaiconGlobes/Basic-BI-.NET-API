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

            foreach (var user in usersObject)
            {
               user.Senha = null;
            };

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
   }
}
