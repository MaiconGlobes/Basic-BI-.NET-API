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
      private object FObjRetorno { get; set; }
      private UserService _userService {  get; set; }

      public UsuarioController()
      {
         _userService = new UserService();   
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
      [Route("user/all")]
      public IActionResult GetUserAllAsync()
      {
         try
         {
            var (Status, Json) = this._userService.GetUserAllAsync();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new System.NotImplementedException(),
               _ => throw new System.NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
