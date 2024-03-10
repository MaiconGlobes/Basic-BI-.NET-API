using ASControllerAPI.Src.Utils;

namespace ASControllerAPI.Src.Middleware
{
   public class UserMiddleware
   {
      private readonly RequestDelegate _next;

      public UserMiddleware(RequestDelegate next)
      {
         this._next = next;
      }

      public async Task InvokeAsync(HttpContext context)
      {
         try
         {
            var isNext = UtilsClass.New().ValidatePathUserAll(context, this._next, "/user/all");

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
