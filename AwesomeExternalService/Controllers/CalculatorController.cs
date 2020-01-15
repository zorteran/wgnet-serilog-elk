using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeExternalService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpPost("multi")]
        public string Multi([FromForm] int firstNumber, [FromForm] int secondNumber)
        {
            _logger.LogInformation("Multiplying...");
            _logger.LogDebug("Multiplication {firstNumber} and {secondNumber}", firstNumber, secondNumber);

            var result = (decimal)firstNumber * secondNumber;

            _logger.LogDebug("Result: {result}", result);
            return result.ToString("0.####");
        }


        [HttpPost("div")]
        public string Div([FromForm] int firstNumber, [FromForm] int secondNumber)
        {
            _logger.LogInformation("Dividing...");
            _logger.LogDebug("Division {firstNumber} and {secondNumber}", firstNumber, secondNumber);

            var result = (decimal)firstNumber / secondNumber;

            _logger.LogDebug("Result: {result}", result);
            return result.ToString("0.####");

        }
    }
}
