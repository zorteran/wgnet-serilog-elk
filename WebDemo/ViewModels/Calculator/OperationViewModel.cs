using System.ComponentModel;

namespace WebDemo.ViewModels.Calculator
{
    public class OperationViewModel
    {
        [DisplayName("First Number")]
        public int FirstNumber { get; set; }
        [DisplayName("Second Number")]
        public int SecondNumber { get; set; }
        [DisplayName("Result")]
        public double? Result { get; set; }
    }
}
