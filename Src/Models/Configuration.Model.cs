using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaseCodeAPI.Src.Models
{
   internal class ConfigurationModel
   {
      internal static ConfigurationModel FInstancia { get; set; }
      internal IConfigurationRoot FIConfigRoot { get; set; }

      internal static ConfigurationModel New()
      {
         FInstancia ??= new ConfigurationModel();
         return FInstancia;
      }

      internal ConfigurationModel()
      {
         FIConfigRoot = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("settingsconfig.json")
               .Build();
      }
   }
}
