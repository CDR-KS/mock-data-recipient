using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace CDR.DataRecipient.Repository.SQL.UnitTests
{
    public class SqlDataFixture
    {
        public IServiceProvider ServiceProvider { get; set; }

        public SqlDataFixture()
        {            
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var services = new ServiceCollection();

            var connectionStr = configuration.GetConnectionString("DefaultConnection");

            Log.Logger.Information($"Sqlite Db ConnectionString: {connectionStr}");

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddSingleton<ISqlDataAccess>(x => new SqlDataAccess(configuration));
            services.AddSingleton<IDataHoldersRepository>(x => new SqlDataHoldersRepository(configuration));
            services.AddSingleton<IConsentsRepository>(x => new SqlConsentsRepository(configuration));
            services.AddSingleton<IRegistrationsRepository>(x => new SqlRegistrationsRepository(configuration));

            this.ServiceProvider = services.BuildServiceProvider();

            var loggerFactory = this.ServiceProvider.GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("UnitTests");

            loggerFactory.AddSerilog();
        }
    }
}
