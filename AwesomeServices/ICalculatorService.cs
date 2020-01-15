using System.Threading.Tasks;

namespace AwesomeServices
{
    public interface ICalculatorService
    {
        Task<double> Add(int firstNumber, int secondNumber);
        Task<double> Sub(int firstNumber, int secondNumber);
        Task<double> Multi(int firstNumber, int secondNumber);
        Task<double> RemoteMulti(int firstNumber, int secondNumber);
        Task<double> Div(int firstNumber, int secondNumber);
        Task<double> RemoteDiv(int firstNumber, int secondNumber);
    }
}