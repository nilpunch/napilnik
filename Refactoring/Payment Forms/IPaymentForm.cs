namespace Napilnik.napilnik.Refactoring
{
    public interface IPaymentForm
    {
        bool Available { get; }

        bool PaymentSuccessful { get; }
        
        string Name { get; }

        void Show();

        void Pay();
    }
}