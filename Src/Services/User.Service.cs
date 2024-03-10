﻿using ASControllerAPI.Src.Enums;
using ASControllerAPI.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;

namespace ASControllerAPI.Src.Services
{
   public class UserService
   {
      private UserRepository FUserRepository { get; set; }

      public UserService()
      {
         FUserRepository = new();
      }

      public async Task<(byte Status, object Json)> GetUserAllAsync()
      {
         try
         {
            var usersObject = await FUserRepository.GetUserAllAsync();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(usersObject));

         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
