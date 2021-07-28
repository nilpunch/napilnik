using System;

namespace Napilnik.napilnik.Refactoring
{
    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentForm paymentForm)
        {
            Console.WriteLine($"Вы оплатили с помощью {paymentForm.Name}");

            Console.WriteLine($"Проверка платежа через {paymentForm.Name}...");
            
            if (paymentForm.PaymentSuccessful)
                Console.WriteLine("Оплата прошла успешно!");
            else
                Console.WriteLine("Оплата не была произведена.");
        }
    }
}