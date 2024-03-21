using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class UserController : ControllerBase
   {
      private IServices FIServices { get; set; }

      public UserController(IServices AiServices)
      {
         this.FIServices = AiServices;
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [Authenticated()]
      [Route("user/all")]
      public async Task<IActionResult> GetAllRegisterAsync()
      {
         try
         {
            var (Status, Json) = await this.FIServices.GetAllRegistersAsync();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoAutorizado => new UnauthorizedObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
