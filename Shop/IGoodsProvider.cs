namespace Napilnik.Shop
{
    public interface IGoodsProvider
    {
        bool HasGoods(Good good, Amount amount);
    }
}