using System;
using System.IO;
using Finpe.Api.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry;

namespace Finpe.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (SentrySdk.Init(GetSentryOptions()))
            {
                UpdateDatabase();
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        private static SentryOptions GetSentryOptions()
        {
            IConfigurationRoot config = GetConfigValues();
            SentryOptions options = new SentryOptions()
            {
                Dsn = new Dsn(config.GetValue<string>("SentryKey")),
                Environment = config.GetValue<string>("SentryEnvironment")

            };
            return options;
        }

        private static void UpdateDatabase()
        {
            Console.WriteLine("updating database...");
            IConfigurationRoot config = GetConfigValues();

            string connectionString = config.GetValue<string>("ConnectionString") ?? "NOT FOUND...";
            var serviceProvider = CreateServices(connectionString);
            Console.WriteLine("Connection string: " + connectionString.Substring(0, Math.Min(90, connectionString.Length - 1)));

            using (var scope = serviceProvider.CreateScope())
            {
                RunMigrations(scope.ServiceProvider);
            }
            Console.WriteLine("Database update finished!");
        }

        private static IConfigurationRoot GetConfigValues()
        {
            return new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true)
                            .AddJsonFile("appsettings.Development.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
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

        private static void RunMigrations(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
