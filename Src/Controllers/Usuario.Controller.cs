using ASControllerAPI.Src.Enums;
using ASControllerAPI.Src.Services;
using ASControllerAPI.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ASControllerAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class UsuarioController : ControllerBase
   {
      private UserService FUserService {  get; set; }

      public UsuarioController()
      {
         FUserService = new UserService();   
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
      [Route("user/all")]
      public async Task<IActionResult> GetUserAllAsync()
      {
         try
         {
            var (Status, Json) = await this.FUserService.GetUserAllAsync();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)){StatusCode = StatusCodes.Status500InternalServerError};
         }
      }
   }
}
