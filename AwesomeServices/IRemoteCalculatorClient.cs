using System.Threading.Tasks;

namespace AwesomeServices
{
    public interface IRemoteCalculatorClient
    {
        Task<double> Multi(int firstNumber, int secondNumber);
        Task<double> Div(int firstNumber, int secondNumber);
    }
}