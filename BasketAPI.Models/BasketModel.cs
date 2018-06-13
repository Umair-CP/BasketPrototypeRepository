using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Models
{
    public class BasketModel
    {
        //ID of basket as a client can create multiple baskets
        public string Id { get; set; }
        //Client that created this basket
        public ClientModel Client { get; set; }
        //Items in the basket
        public List<BasketItemModel> BasketItems { get; set; }
        //Total price of all the items in the basket
        public float TotalBasketPrice { get; set; }
    }
}
