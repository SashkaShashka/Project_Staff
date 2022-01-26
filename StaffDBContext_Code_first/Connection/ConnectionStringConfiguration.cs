using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using StaffDBContext_Code_first.Model;
using System.Linq;

namespace StaffDBContext_Code_first.Connection
{
    public class ConnectionStringConfiguration
    {
        public string ConnectionString { get; set; }

        public ConnectionStringConfiguration(string appsettingsPath = "appsettings.json",
            string connectionStringName = "DefaultConnection",
            string environmentVaiableName = "StaffDb_ConnectionString",
            string userSecretsSection = "StaffDb")
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appsettingsPath, optional: true)
                .AddUserSecrets<StaffContext>()
                .Build();

            string userId = "", password = "", server = "";
            config.Providers.Any(p => p.TryGet($"{userSecretsSection}:UserId", out userId));
            config.Providers.Any(p => p.TryGet($"{userSecretsSection}:Password", out password));
            config.Providers.Any(p => p.TryGet($"{userSecretsSection}:Server", out server));

            ConnectionString = string.Format(config.GetConnectionString(connectionStringName)
                ?? Environment.GetEnvironmentVariable(environmentVaiableName),
                server,
                userId,
                password
            );
        }
    }
}
