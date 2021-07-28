using System;

namespace Napilnik.napilnik.Refactoring
{
    public class NotIdentifiedForm : IPaymentForm
    {
        public bool Available => false;

        public bool PaymentSuccessful => false;

        public string Name => string.Empty;
            
        public void Show()
        {
            Console.WriteLine("Неизвестный способ платежа.");
        }

        public void Pay()
        {
        }
    }
}