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
   public class UsuarioController : ControllerBase
   {
      private IServices FIServices { get; set; }

      public UsuarioController(IServices AiServices)
      {
         FIServices = AiServices;
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [Authorize]
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
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [AllowAnonymous]
      [Route("user/register")]
      public async Task<IActionResult> PostCreateRegisterAsync([FromBody] UserModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIServices.CreateRegisterAsync(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new CreatedResult(string.Empty, Json),
               (byte)GlobalEnum.eStatusProc.SemRegistros => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.RegistroDuplicado => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new ObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
