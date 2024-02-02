using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using DevOps.Util.DotNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DevOps.Status
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // We can disable using key vault with this env var. See ..\Documentation\DevWithoutKeyVault.md
                    if (Environment.GetEnvironmentVariable("USE_KEYVAULT") == "0")
                    {
                        Console.WriteLine("Disabling Azure KeyVault");
                    }
                    else
                    {
                        config.AddAzureKeyVault(new Uri(DotNetConstants.KeyVaultEndPoint), new DefaultAzureCredential());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
