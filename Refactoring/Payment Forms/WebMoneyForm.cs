using System;

namespace Napilnik.napilnik.Refactoring
{
    public class WebMoneyForm : IPaymentForm
    {
        public bool Available => true;

        public bool PaymentSuccessful { get; private set; }

        public string Name => "WebMoney";
            
        public void Show()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }
        
        public void Pay()
        {
            PaymentSuccessful = true;
        }
    }
}