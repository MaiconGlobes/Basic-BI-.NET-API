using BaseCodeAPI.Src.Enums;

namespace BaseCodeAPI.Src.Utils
{
   public class ResponseUtils
   {
      private static ResponseUtils FInstancia { get; set; }
      private object FObjJSON { get; set; }

      public static ResponseUtils Instancia()
      {
         FInstancia ??= new ResponseUtils();
         return FInstancia;
      }

      internal virtual object RetornoOk<T>(T ADado)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADado
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoOk<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADados
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoCreated<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADados
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoCreated(Object ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADados
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoNotAcceptable<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Registro não localizado",
               codigo_status = GlobalEnum.eStatusProc.NaoLocalizado,
               mensagem = new
               {
                  descricao = "O registro que está tentando realizar a operação não se encontra no banco de dados.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoNotAcceptable(Object ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Registro não localizado",
               codigo_status = GlobalEnum.eStatusProc.SemRegistros,
               mensagem = new
               {
                  descricao = "O registro que está tentando realizar a operação não se encontra no banco de dados.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoNotFound<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Registro não localizado",
               codigo_status = GlobalEnum.eStatusProc.SemRegistros,
               mensagem = new
               {
                  descricao = "O registro que está tentando buscar não foi localizado no banco de dados.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoNotFound(Object ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Registro não localizado",
               codigo_status = GlobalEnum.eStatusProc.SemRegistros,
               mensagem = new
               {
                  descricao = "O registro que está tentando buscar não foi localizado no banco de dados.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoDuplicated<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Duplicidade de registro",
               codigo_status = GlobalEnum.eStatusProc.RegistroDuplicado,
               mensagem = new
               {
                  descricao = "O registro que está tentando inserir já se encontra no banco de dados.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoDuplicated(Object ADados)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Duplicidade de registro",
               codigo_status = GlobalEnum.eStatusProc.RegistroDuplicado,
               mensagem = new
               {
                  descricao = "Erro de duplicidade de registro.",
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoDuplicated(Exception AExcecao)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status = "Duplicidade de registro",
               codigo_status = GlobalEnum.eStatusProc.RegistroDuplicado,
               mensagem = new
               {
                  descricao = AExcecao?.Message
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoErrorProcess(Exception AExcecao)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status = "Erro de processamento",
               codigo_status = GlobalEnum.eStatusProc.ErroProcessamento,
               mensagem = new
               {
                  descricao = AExcecao?.Message
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoUnauthorized(Exception AExcecao)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status = "Não autorizado",
               codigo_status = GlobalEnum.eStatusProc.NaoAutorizado,
               mensagem = new
               {
                  descricao = AExcecao?.Message
               }
            }
         };

         return this.FObjJSON;
      }

      internal virtual object RetornoInternalErrorServer(Exception AExcecao)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status        = "Erro interno de servidor",
               codigo_status = GlobalEnum.eStatusProc.ErroServidor,
               mensagem = new
               {
                  descricao = AExcecao.Message,
               }
            }
         };

         return this.FObjJSON;
      }
   }
}
