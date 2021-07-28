namespace Napilnik.Shop
{
    public struct Order
    {
        public Order(string paylink)
        {
            Paylink = paylink;
        }

        public string Paylink { get; }
    }
}