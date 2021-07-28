using System;

namespace Napilnik.napilnik.Refactoring
{
    public class QIWIForm : IPaymentForm
    {
        public bool Available => true;

        public bool PaymentSuccessful { get; private set; }

        public string Name => "QIWI";
            
        public void Show()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }

        public void Pay()
        {
            PaymentSuccessful = true;
        }
    }
}