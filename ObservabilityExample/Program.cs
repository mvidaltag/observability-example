
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ObservabilityExample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			var host = Host.CreateDefaultBuilder(args)
				.ConfigureServices((_, services) =>
				{
					services.AddSingleton<ILoggerFactory>(provider => LoggerFactory.Create(builder =>
					{
						builder.AddConsole(options =>
						{
							//options.FormatterName = "json";
						});
						builder.SetMinimumLevel(LogLevel.Trace);
						builder.AddFile("logs/log.txt");
					}));
					services.AddHostedService<LogExampleService>();
				})
				.Build();

			host.Run();
		}
	}
}
