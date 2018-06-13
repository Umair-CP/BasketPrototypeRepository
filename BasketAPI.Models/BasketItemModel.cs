using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Models
{
    public class BasketItemModel
    {
        //Unique ID of item assigned by client so they can track item in their system
        public string Id { get; set; }
        //Name of item
        public string Name { get; set; }
        //Description of item 
        public string Description { get; set; }
        //Specification of item - e.g. size, colour, width, height, etc. 
        public string Specification { get; set; }
        //Comments added by user buying the item
        public string Comment { get; set; }
        //Price of single unit of item
        public float UnitPrice { get; set; }
        //Quantity of item
        public int Quantity { get; set; }
        //Percentage discount on item
        public float Discount { get; set; }
        //Calculated field based on unit price, quantity and discount
        public float SubTotal { get; set; }
    }
}
