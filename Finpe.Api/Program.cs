using System;
using System.IO;
using Finpe.Api.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finpe.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "migrate")
            {
                UpdateDatabase(args);
            }
            else
            {
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        private static void UpdateDatabase(string[] args)
        {
            Console.WriteLine("updating database...");
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            string connectionString = config.GetValue<string>("ConnectionString") ?? "NOT FOUND...";
            var serviceProvider = CreateServices(connectionString);
            Console.WriteLine("Connection string: " + connectionString.Substring(0, Math.Min(90, connectionString.Length - 1)));

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static IServiceProvider CreateServices(string connectionString) =>
            new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(InitialSchema).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
