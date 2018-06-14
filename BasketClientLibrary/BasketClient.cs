using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BasketClientLibrary
{

    /// <summary>
    /// This library will be used by clients in their applications to ineract with the basket web API
    /// </summary>
    public class BasketClient
    {
        public string ClientId { get; set; }
        public string BasketId { get; set; }


        /// <summary>
        /// The constructor creates a new basket and returns the ID of the basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        public BasketClient(string clientId)
        {
            ClientId = clientId;
            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/CreateNewBasket");
            builder.Query = "id=" + clientId;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            BasketId = JsonConvert.DeserializeObject<string>(content.Result);
        }


        /// <summary>
        /// This method adds a new item in to the basket
        /// </summary>
        /// <param name="itemId">Item ID</param>
        /// <param name="name">Item name</param>
        /// <param name="description">Description</param>
        /// <param name="specification">Specification</param>
        /// <param name="comment">User comments</param>
        /// <param name="unitPrice">Unit price of item</param>
        /// <param name="quantity">Quanity of item</param>
        /// <param name="discount">Discount rate</param>
        /// <returns>True/false based on if item was added</returns>
        public bool AddItem(string itemId, string name, string description, string specification,
                            string comment, float unitPrice, int quantity, float discount)
        {
            bool isAdded = false;
            
            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/AddBasketItem");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId + "&itemid=" + itemId +
                            "&name=" + name + "&description=" + description + "&specification=" + specification +
                            "&comment=" + comment + "&unitPrice=" + unitPrice + "&quantity=" + quantity +
                            "&discount=" + discount;


            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            isAdded = JsonConvert.DeserializeObject<bool>(content.Result);

            return isAdded;
        }


        /// <summary>
        /// Remove item from basket
        /// </summary>
        /// <param name="itemId">ID of item to be removed</param>
        /// <returns>True/false based on if item was removed</returns>
        public bool RemoveItem(string itemId)
        {
            bool isRemoved = false;

            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/RemoveBasketItem");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId + "&itemid=" + itemId;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            isRemoved = JsonConvert.DeserializeObject<bool>(content.Result);

            return isRemoved;
        }


        /// <summary>
        /// Update item quantity in basket
        /// </summary>
        /// <param name="itemId">Item ID</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>True/false based on if item quantity was updated</returns>
        public bool UpdateItem(string itemId, int quantity)
        {
            bool isUpdated = false;

            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/UpdateItemQuantity");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId + "&itemid=" + itemId +
                            "&quantity=" + quantity;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            isUpdated = JsonConvert.DeserializeObject<bool>(content.Result);

            return isUpdated;
        }


        /// <summary>
        /// Clear all basket items
        /// </summary>
        /// <returns>True/false based on if basket was cleared</returns>
        public bool ClearBasket()
        {
            bool isCleared = false;

            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/ClearBasket");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            isCleared = JsonConvert.DeserializeObject<bool>(content.Result);

            return isCleared;
        }


        /// <summary>
        /// Delete basket
        /// </summary>
        /// <returns>True/false based on if basket was deleted</returns>
        public bool DeleteBasket()
        {
            bool isDeleted = false;

            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/RemoveBasket");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            isDeleted = JsonConvert.DeserializeObject<bool>(content.Result);

            return isDeleted;
        }


        /// <summary>
        /// Get basket
        /// </summary>
        /// <returns>Basket model in JSON format</returns>
        public string GetBasket()
        {
            UriBuilder builder = new UriBuilder("http://localhost:50000/api/basket/GetBasket");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            string basketObject = content.Result;

            return basketObject;
        }


        /// <summary>
        /// Produce order
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="customerName">Customer Name</param>
        /// <param name="deliveryAddress">Delivery Address</param>
        /// <param name="contactNumber">Contact Number</param>
        /// <param name="tax">Tax rate</param>
        /// <param name="deliveryPrice">Delivery price</param>
        /// <returns>Order model in JSON format</returns>
        public string ProduceOrder(string customerId, string customerName, string deliveryAddress,
                                    string contactNumber, float tax, float deliveryPrice)
        {
            UriBuilder builder = new UriBuilder("http://localhost:50000/api/order/ProduceOrder");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId + "&customerid=" + customerId +
                            "&customername=" + customerName +
                            "&deliveryaddress=" + deliveryAddress + "&contactnumber=" + contactNumber +
                            "&tax=" + tax + "&deliveryprice=" + deliveryPrice;

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            var content = result.Content.ReadAsStringAsync();

            string orderObject = content.Result;

            return orderObject;
        }

    }
}
