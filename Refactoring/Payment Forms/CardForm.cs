using System;

namespace Napilnik.napilnik.Refactoring
{
    public class CardForm : IPaymentForm
    {
        public bool Available { get; }

        public bool PaymentSuccessful { get; private set; }
        
        public string Name { get; }
        
        public void Show()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }

        public void Pay()
        {
            PaymentSuccessful = true;
        }
    }
}