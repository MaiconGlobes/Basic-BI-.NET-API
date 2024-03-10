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
   public class MigrationController : ControllerBase
   {
      private MigrationService FMigrationService {  get; set; }

      public MigrationController()
      {
         FMigrationService = new MigrationService();   
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
      [Route("migrate")]
      public IActionResult GetUserAllAsync()
      {
         try
         {
            var (Status, Json) = this.FMigrationService.ApplyMigrate();

            return Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new NoContentResult(),
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
