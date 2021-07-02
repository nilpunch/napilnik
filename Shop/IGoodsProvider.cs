namespace Code.napilnik.Shop
{
    public interface IGoodsProvider
    {
        bool HasGoods(Good good, Amount amount);
    }
}