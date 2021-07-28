using System;
using NUnit.Framework;

namespace Napilnik.napilnik.Refactoring
{
    public class RefactoringTests
    {
        [Test]
        public static void UseCase()
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var paymentForm = orderForm.ShowForm();
            
            paymentForm.Show();
            
            paymentForm.Pay();

            paymentHandler.ShowPaymentResult(paymentForm);
        }
    }
}