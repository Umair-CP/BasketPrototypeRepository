using System;
using Xunit;
using BasketAPI.Services;
using BasketAPI.Models;

namespace BasketAPITest
{
    public class BasketServiceTest
    {
        [Fact]
        public void CreateNewBasket()
        {
            BasketService basketService = new BasketService();
            var returnObject = basketService.CreateNewBasket("C999");

            Assert.Null(returnObject);

        }

        [Fact]
        public void AddNewItemMissingMandatoryFields()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "";
            basketItemModel.Name = "";
            basketItemModel.Quantity = 0;
            basketItemModel.UnitPrice = -1;

            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);
            Assert.False(isAdded);
        }

        [Fact]
        public void AddNewItemExists()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.50;
            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);

            basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.50;
            bool isAddedAgain = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);

            Assert.False(isAddedAgain);
        }

        [Fact]
        public void AddNewItemCalculateSubTotal()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.55;
            basketItemModel.Discount = (float)130.00;

            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);
            Assert.Equal((float)0.00, basketItemModel.SubTotal);
        }

        [Fact]
        public void UpdateQuantity()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.00;
            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);
            bool isUpdated = basketService.UpdateBasketItemQuantity("C123", basketModel.Id, "111", 3);

            Assert.True(isUpdated);
            Assert.Equal((float)15.00, basketItemModel.SubTotal);
        }

        [Fact]
        public void RemoveItem()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.00;
            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);
            bool isRemoved = basketService.RemoveBasketItem("C123", basketModel.Id, "112");

            Assert.False(isRemoved);
        }

    }
}
