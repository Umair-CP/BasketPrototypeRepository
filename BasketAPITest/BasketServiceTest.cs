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

            //Check if null is returned if client is not recognised.
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

            //Check if API does not add new item if mandatory fields are missing
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

            //If item already exists, check the API does not add the item again as its quantity must be updated
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

            //Check API sets the item sub-total to 0 if the discount is > 0 and does not allow it to be negative
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

            //Check if the item is successfully added
            Assert.True(isUpdated);

            //Check the calculation is accurate after an item quantity is updated
            Assert.Equal((float)15.00, basketItemModel.SubTotal);
        }

        [Fact]
        public void UpdateQuantityWithZero()
        {
            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.00;
            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);
            bool isUpdated = basketService.UpdateBasketItemQuantity("C123", basketModel.Id, "111", 0);

            //Check if API does not update an item if quantity is 0, rather the item should be removed
            Assert.False(isUpdated);
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

            //Check if API does not remove an item that does not exist in basket
            Assert.False(isRemoved);
        }

    }
}
