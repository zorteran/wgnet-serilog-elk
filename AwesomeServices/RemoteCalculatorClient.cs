using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeServices
{
    public class RemoteCalculatorClient : IRemoteCalculatorClient
    {
        private readonly HttpClient _httpClient;

        public RemoteCalculatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5001");
        }

        public async Task<double> Multi(int firstNumber, int secondNumber)
        {
            FormUrlEncodedContent content = CreateContent(firstNumber, secondNumber);

            var result = await _httpClient.PostAsync("/calculator/multi", content);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Oh no! Calculator Client falied!");
            }
            var resultString = await result.Content.ReadAsStringAsync();
            return Convert.ToDouble(resultString);
        }
        public async Task<double> Div(int firstNumber, int secondNumber)
        {
            FormUrlEncodedContent content = CreateContent(firstNumber, secondNumber);

            var result = await _httpClient.PostAsync("/calculator/div", content);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Oh no! Calculator Client falied!");
            }
            var resultString = await result.Content.ReadAsStringAsync();
            return Convert.ToDouble(resultString);
        }

        private static FormUrlEncodedContent CreateContent(int firstNumber, int secondNumber)
        {
            var values = new Dictionary<string, string>();
            values.Add("firstNumber", firstNumber.ToString());
            values.Add("secondNumber", secondNumber.ToString());
            var content = new FormUrlEncodedContent(values);
            return content;
        }
    }
}
