using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace ObservabilityExample
{
    public class SimpleService : IHostedService
    {
        private readonly ILoggerFactory _loggerFactory;

        public SimpleService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            // Log simples
            var logger = _loggerFactory.CreateLogger("ServiceLogger");

            logger.LogInformation("Ocorreu um erro ao processar o contrato a29eeac3-e4a0-433e-9bb7-309271026bf4");

            // Log JSON detalhado
            var errorLog = new {
                timestamp = "2025-09-29T15:54:00.000Z",
                level = "ERROR",
                message = "Ocorreu um erro ao processar o contrato",
                contractId = "a29eeac3-e4a0-433e-9bb7-309271026bf4",
                exception = new {
                    type = "System.InvalidOperationException",
                    message = "Não foi possível completar a operação devido a dados inválidos.",
                    stackTrace = "   at MyNamespace.MyService.ProcessContract(Guid contractId) in MyService.cs:line 42\n   at MyNamespace.MyController.HandleRequest(ContractRequest request) in MyController.cs:line 25"
                },
                source = "ObservabilityExample"
            };

            logger.LogError($"Log JSON: {JsonSerializer.Serialize(errorLog)}");

            // Exemplos de todos os níveis de log usando LoggerFactory
            logger.LogCritical("FATAL: Não foi possível encontrar o arquivo de configuração.");

            logger.LogError("ERROR: Ocorreu um erro ao processar o contrato.");

            logger.LogWarning("WARN: Atenção: Verifique os dados de entrada.");

            logger.LogInformation("INFO: Processamento iniciado.");

            logger.LogDebug("DEBUG: Detalhes da execução.");

#if DEBUG
            logger.LogDebug("[DEBUG] Ambiente de desenvolvimento - log extra de depuração");
#endif
            
            logger.LogTrace("TRACE: Informações de rastreamento.");

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
