using AwesomeLogging;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeServices
{
    public class CalculatorService : ICalculatorService
    {
        private static int lag = 1;
        private readonly ILogger<CalculatorService> _logger;
        private readonly IRemoteCalculatorClient _remoteCalculatorClient;
        private readonly IAwesomeLogger _awesomeLogger;

        public CalculatorService(ILogger<CalculatorService> logger, IRemoteCalculatorClient remoteCalculatorClient, IAwesomeLogger awesomeLogger)
        {
            _logger = logger;
            _remoteCalculatorClient = remoteCalculatorClient;
            _awesomeLogger = awesomeLogger;
        }
        public async Task<double> Add(int firstNumber, int secondNumber)
        {
            _logger.BeginScope("Wow such service");
            _logger.LogInformation("Adding {firstNumber} and {secondNumber}", firstNumber, secondNumber);

            //var log = CreateAwesomeLog("Adding...");
            //log.AdditionalInfo.Add("input", new { firstNumber, secondNumber });
            //_awesomeLogger.WriteUsage(log);

            var result = firstNumber + secondNumber;

            _logger.LogDebug("Result: {result}", result);
            return result;
        }

        public async Task<double> Div(int firstNumber, int secondNumber)
        {
            _logger.LogInformation("Dividing...");
            _logger.LogDebug("Div {firstNumber} and {secondNumber}", firstNumber, secondNumber);

            //try
            //{
            var result = firstNumber / secondNumber;
            ////var result = (double)firstNumber / secondNumber;
            _logger.LogDebug("Result: {result}", result);
            return result;
            //}
            //catch (Exception e)
            //{
            //var log = CreateAwesomeErrorLog(e.Message, e);
            //_awesomeLogger.WriteError(log);
            //throw;
            //}
        }

        public async Task<double> Multi(int firstNumber, int secondNumber)
        {
            _logger.LogInformation("Multiplying...");
            _logger.LogDebug("Multi {firstNumber} and {secondNumber}", firstNumber, secondNumber);
            //var sw = new Stopwatch();
            //sw.Start();

            Thread.Sleep(lag * 1000);
            lag++;
            var result = (double)firstNumber * secondNumber;

            //sw.Stop();
            //var log = CreateAwesomeLog("Multi ended!");
            //log.ElapsedMilliseconds = sw.ElapsedMilliseconds;
            //_awesomeLogger.WritePerf(log);

            _logger.LogDebug("Result: {result}", result);
            return result;
        }

        public async Task<double> RemoteDiv(int firstNumber, int secondNumber)
        {
            _logger.LogInformation("Remote Div...");
            _logger.LogDebug("Multi {firstNumber} and {secondNumber}", firstNumber, secondNumber);
            try
            {
                var result = await _remoteCalculatorClient.Div(firstNumber, secondNumber);
                _logger.LogDebug("Result: {result}", result);
                return result;
            }
            catch (System.Exception e)
            {
                _logger.LogError("Nooooooooooooooooooo", e);
                throw e;
            }
        }

        public async Task<double> RemoteMulti(int firstNumber, int secondNumber)
        {
            _logger.LogInformation("Remote Multi...");
            _logger.LogDebug("Multi {firstNumber} and {secondNumber}", firstNumber, secondNumber);
            try
            {
                var result = await _remoteCalculatorClient.Multi(firstNumber, secondNumber);
                _logger.LogDebug("Result: {result}", result);
                return result;
            }
            catch (System.Exception e)
            {
                _logger.LogError("Nooooooooooooooooooo", e);
                throw e;
            }
        }

        public async Task<double> Sub(int firstNumber, int secondNumber)
        {
            _logger.LogInformation("Sub...");
            _logger.LogDebug("Sub {firstNumber} and {secondNumber}", firstNumber, secondNumber);

            var result = (double)firstNumber - secondNumber;

            _logger.LogDebug("Result: {result}", result);
            return result;
        }

        private AwesomeLog CreateAwesomeLog(string message)
        {
            return new AwesomeLog
            {
                Message = message,
                Location = nameof(CalculatorService),
                
            };
        }
        private AwesomeLog CreateAwesomeErrorLog(string message, Exception ex)
        {
            return new AwesomeLog
            {
                Message = message,
                Exception = ex,
                Location = nameof(CalculatorService),
            };
        }
    }
}
