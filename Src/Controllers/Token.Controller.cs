using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [AllowAnonymous]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class TokenController : ControllerBase
   {
      private TokenService FTokenService { get; set; }
      private IToken FIToken { get; set; }

      public TokenController(IToken AiToken)
      {
         FIToken = AiToken;
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenUserModelDto))]
      [AllowAnonymous]
      [Route("token/create-new-token")]
      public async Task<IActionResult> PostCreateNewToken([FromBody] TokenUserModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FIToken.CreateNewToken(AModel);

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new CreatedResult(string.Empty, Json),
               (byte)GlobalEnum.eStatusProc.NaoLocalizado => new OkObjectResult(Json),
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
