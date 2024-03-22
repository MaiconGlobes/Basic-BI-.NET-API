using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class AuthenticateController : ControllerBase
   {
      private IAuthenticate FIAuthenticate { get; set; }

      public AuthenticateController(IAuthenticate AiAuthenticate)
      {
         this.FIAuthenticate = AiAuthenticate;
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [AllowAnonymous]
      [Route("authenticate/login")]
      public async Task<IActionResult> GetLoginAsync([FromBody] UserModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIAuthenticate.ProcessLoginAsync(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new CreatedResult(string.Empty, Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().ReturnInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [AllowAnonymous]
      [Route("authenticate/register")]
      public async Task<IActionResult> PostCreateRegisterAsync([FromBody] UserModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIAuthenticate.CreateRegisterAsync(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new CreatedResult(string.Empty, Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.RegistroDuplicado => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
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
