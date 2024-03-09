using ASControllerAPI.Src.Utils;

namespace ASControllerAPI.Src.Middleware
{
   public class WeighingTypesMiddleware
   {
      private readonly RequestDelegate _next;

      public WeighingTypesMiddleware(RequestDelegate next)
      {
         this._next = next;
      }

      public async Task InvokeAsync(HttpContext context)
      {
         try
         {
            var (id, isNext) = UtilsClass.New().ValidateProcessPath(context, this._next, "/process/");

            if (isNext != null)
            {
               await this._next(context);
               return;
            }

            await this._next(context);
         }
         catch (Exception ex)
         {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
