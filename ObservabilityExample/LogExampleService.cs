using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace ObservabilityExample
{
    public class LogExampleService : IHostedService
    {
        private readonly ILoggerFactory _loggerFactory;

        public LogExampleService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger("ServiceLogger");

            //Não é o ideal
            logger.LogError("Ocorreu um erro ao processar o contrato a29eeac3-e4a0-433e-9bb7-309271026bf4");

            // Log JSON detalhado
            logger.LogError("Ocorreu um erro ao processar o contrato {ContractId}. Exception: {ExceptionType} - {ExceptionMessage}", 
                "a29eeac3-e4a0-433e-9bb7-309271026bf4",
                "System.InvalidOperationException",
                "Nao foi possivel completar a operacao devido a dados invalidos.");

            var usuario = "Matheus Vidal";
            
            logger.LogInformation("Usuario {Usuario} realizou login com sucesso em {Timestamp}", usuario, DateTime.UtcNow.ToString("o"));
            
            logger.LogCritical("FATAL: Nao foi possivel encontrar o arquivo de configuracao.");
            
            logger.LogWarning("WARN: Verifique os dados de entrada.");

            logger.LogInformation("INFO: Processamento iniciado.");

            logger.LogDebug("DEBUG: Detalhes da execucao.");

#if DEBUG
            logger.LogDebug("[DEBUG] Ambiente de desenvolvimento - log extra de depuracao");
#endif
            
            logger.LogTrace("TRACE: Informacoes de rastreamento.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger("ServiceLogger");
            logger.LogInformation("LogFactory: Serviço finalizado");
            return Task.CompletedTask;
        }
    }
}
