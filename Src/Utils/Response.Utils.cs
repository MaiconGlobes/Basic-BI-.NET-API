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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de sucesso e os dados fornecidos.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADado">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de sucesso e os dados fornecidos.</returns>
      internal virtual object ReturnOk<T>(T ADado)
      {
         this.FObjJSON = new
         {
            retorno = new
            {
               status = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados = ADado
            }
         };

         return this.FObjJSON;
      }

      /// <summary>
      /// Retorna um objeto JSON contendo um status de sucesso e os dados fornecidos.
      /// </summary>
      /// <param name="ADados">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de sucesso e os dados fornecidos.</returns>
      internal virtual object ReturnOk(Object ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de sucesso e uma lista de dados fornecidos.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados na lista a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADados">A lista de dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de sucesso e a lista de dados fornecidos.</returns>
      internal virtual object ReturnOk<T>(List<T> ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de sucesso e uma lista de dados fornecidos para indicar que um recurso foi criado com sucesso.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados na lista a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADados">A lista de dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de sucesso e a lista de dados fornecidos.</returns>
      internal virtual object ReturnCreated<T>(List<T> ADados)
      {
         this.FObjJSON = new
         {
            Return = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADados
            }
         };

         return this.FObjJSON;
      }

      /// <summary>
      /// Retorna um objeto JSON contendo um status de sucesso e os dados fornecidos para indicar que um recurso foi criado com sucesso.
      /// </summary>
      /// <param name="ADados">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de sucesso e os dados fornecidos.</returns>
      internal virtual object ReturnCreated(Object ADados)
      {
         this.FObjJSON = new
         {
            Return = new
            {
               status        = "Sucesso",
               codigo_status = GlobalEnum.eStatusProc.Sucesso,
               dados         = ADados
            }
         };

         return this.FObjJSON;
      }

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Registro não localizado" e uma mensagem indicando que o registro não foi encontrado no banco de dados.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados na lista a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADados">A lista de dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Registro não localizado" e a mensagem correspondente.</returns>
      internal virtual object ReturnNotAcceptable<T>(List<T> ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Registro não localizado" e uma mensagem indicando que o registro não foi encontrado no banco de dados.
      /// </summary>
      /// <param name="ADados">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Registro não localizado" e a mensagem correspondente.</returns>
      internal virtual object ReturnNotAcceptable(Object ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Registro não localizado" e uma mensagem indicando que o registro não foi encontrado no banco de dados.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados na lista a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADados">A lista de dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Registro não localizado" e a mensagem correspondente.</returns>
      internal virtual object ReturnNotFound<T>(List<T> ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Registro não localizado" e uma mensagem indicando que o registro não foi encontrado no banco de dados.
      /// </summary>
      /// <param name="ADados">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Registro não localizado" e a mensagem correspondente.</returns>
      internal virtual object ReturnNotFound(Object ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Duplicidade de registro" e uma mensagem indicando que o registro que está sendo inserido já existe no banco de dados.
      /// </summary>
      /// <typeparam name="T">O tipo dos dados na lista a serem incluídos no objeto JSON.</typeparam>
      /// <param name="ADados">A lista de dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Duplicidade de registro" e a mensagem correspondente.</returns>
      internal virtual object ReturnDuplicated<T>(List<T> ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Duplicidade de registro" e uma mensagem genérica indicando um erro de duplicidade de registro.
      /// </summary>
      /// <param name="ADados">Os dados a serem incluídos no objeto JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Duplicidade de registro" e a mensagem genérica de erro.</returns>
      internal virtual object ReturnDuplicated(Object ADados)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Duplicidade de registro" e uma mensagem com a descrição da exceção especificada.
      /// </summary>
      /// <param name="AExcecao">A exceção que contém a mensagem a ser incluída na resposta JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Duplicidade de registro" e a mensagem da exceção.</returns>
      internal virtual object ReturnDuplicated(Exception AExcecao)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Erro de processamento" e uma mensagem com a descrição da exceção especificada.
      /// </summary>
      /// <param name="AExcecao">A exceção que contém a mensagem a ser incluída na resposta JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Erro de processamento" e a mensagem da exceção.</returns>
      internal virtual object ReturnErrorProcess(Exception AExcecao)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Não autorizado" e uma mensagem com a descrição da exceção especificada.
      /// </summary>
      /// <param name="AExcecao">A exceção que contém a mensagem a ser incluída na resposta JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Não autorizado" e a mensagem da exceção.</returns>
      internal virtual object ReturnUnauthorized(Exception AExcecao)
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

      /// <summary>
      /// Retorna um objeto JSON contendo um status de "Erro interno de servidor" e uma mensagem com a descrição da exceção especificada.
      /// </summary>
      /// <param name="AExcecao">A exceção que contém a mensagem a ser incluída na resposta JSON.</param>
      /// <returns>O objeto JSON contendo o status de "Erro interno de servidor" e a mensagem da exceção.</returns>
      internal virtual object ReturnInternalErrorServer(Exception AExcecao)
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
