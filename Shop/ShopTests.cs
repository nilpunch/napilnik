using System;
using NUnit.Framework;

namespace Napilnik.Shop
{
    public static class ShopTests
    {
        [Test]
        public static void UseCase()
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

//Вывод всех товаров на складе с их остатком
            foreach (var storedGood in warehouse)
            {
                Console.WriteLine($"{storedGood.Good.Name}: {storedGood.Amount.Value}");
            }

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            //cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

//Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().Paylink);
        }
    }
}