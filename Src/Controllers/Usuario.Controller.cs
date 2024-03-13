using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaseCodeAPI.Src.Controllers
{
   [ApiController]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   [ApiExplorerSettings(IgnoreApi = true)]
   public class UsuarioController : ControllerBase
   {
      private UserService FUserService {  get; set; }
      

      public UsuarioController(IMapper AMapper)
      {
         FUserService = new UserService(AMapper);   
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
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
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModelDto))]
      [Route("user/create")]
      public async Task<IActionResult> PostCreateUser([FromBody] UserModelDto AModel)
      {
         try
         {
            var (Status, Json) = await this.FUserService.CreateUser(AModel);

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
            return new ObjectResult(ResponseUtils.Instancia().RetornoInternalErrorServer(ex)) { StatusCode = StatusCodes.Status500InternalServerError };
         }
      }
   }
}
