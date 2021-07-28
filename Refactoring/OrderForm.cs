using System;

namespace Napilnik.napilnik.Refactoring
{
    public class OrderForm
    {
        public IPaymentForm ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");

            string formId = Console.ReadLine().ToLower();

            switch (formId)
            {
                case "qiwi": return new QIWIForm();
                case "webmoney": return new WebMoneyForm();
                case "card": return new CardForm();
                default: return new NotIdentifiedForm();
            }
        }
    }
}