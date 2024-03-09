using ASControllerAPI.Src.Utils;
using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ASControllerAPI.Src.Controllers
{
    [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class VerifyController : ControllerBase
   {
      private object FObjRetorno { get; set; }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultJSONModelDto))]
      [Route("ping")]
      public IActionResult GetPing()
      {
         try
         {
            ResultJSONModelDto verifyModel = new()
            {
               parametros = new ParametroModel()
               {
                  Id = 1,
                  Telefone_suporte = "19999999999"
               }
            };

            this.FObjRetorno = ResponseUtils.Instancia().RetornoOk(verifyModel);

            return new OkObjectResult(this.FObjRetorno);
         }
         catch (Exception ex)
         {
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
