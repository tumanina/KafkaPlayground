using KafkaPlayground.Listener2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configBuilder = new ConfigurationBuilder();

configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var config = configBuilder.AddEnvironmentVariables()
    .Build();
try
{
    var builder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<RunnableHost>();
            Startup.ConfigureServices(services, config);
        });

    builder
        .RunConsoleAsync()
        .GetAwaiter()
        .GetResult();
}
catch (Exception e)
{
    Environment.ExitCode = 1;
}
finally
{
}


