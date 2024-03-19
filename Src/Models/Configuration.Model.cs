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
