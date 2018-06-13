using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BasketClientLibrary
{
    public class BasketClient
    {
        public string ClientId { get; set; }
        public string BasketId { get; set; }

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

        public string ProduceOrder(string customerId, string customerName, string deliveryAddress,
                                    string contactNumber, float tax, float deliveryPrice)
        {
            UriBuilder builder = new UriBuilder("http://localhost:50000/api/order/ProduceOrder");
            builder.Query = "clientid=" + ClientId + "&basketid=" + BasketId + "&customertid=" + customerId +
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
