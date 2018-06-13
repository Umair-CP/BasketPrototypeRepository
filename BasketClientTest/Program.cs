using System;
using BasketClientLibrary;

namespace BasketClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool returnStatus = false;

            Console.WriteLine("Program started for client C123");

            BasketClient basketClient = new BasketClient("C123");
            Console.WriteLine("Acquired new Basket ID: " + basketClient.BasketId);

            Console.WriteLine("Add item 1 with discount: ");
            returnStatus = basketClient.AddItem("111", "Car", "BMW", "Series 3", "Very cool", (float)5.50, 1, (float)2.50);
            Console.WriteLine("Return status for item 1: " + returnStatus);

            Console.WriteLine("Add item 2: ");
            returnStatus = basketClient.AddItem("112", "Bike", "Kawasaki", "Series 1", "Very cool", (float)3.50, 1, 0);
            Console.WriteLine("Return status for item 2: " + returnStatus);

            Console.WriteLine("Add item 3: ");
            returnStatus = basketClient.AddItem("113", "Boat", "Mercedes", "Series 2", "Very cool", (float)2.50, 1, 0);
            Console.WriteLine("Return status for item 3: " + returnStatus);

            Console.WriteLine("Retrieve basket after adding 3 items: ");
            string basketObject = basketClient.GetBasket();
            Console.WriteLine("Basket: " + basketObject);

            Console.WriteLine("Update quantity of item 2: ");
            returnStatus = basketClient.UpdateItem("112", 2);
            Console.WriteLine("Return status for item 2: " + returnStatus);

            Console.WriteLine("Retrieve basket after updating item 2: ");
            basketObject = basketClient.GetBasket();
            Console.WriteLine("Basket: " + basketObject);

            Console.WriteLine("Remove item 3: ");
            returnStatus = basketClient.RemoveItem("113");
            Console.WriteLine("Return status for item 3: " + returnStatus);

            Console.WriteLine("Retrieve basket after removing item 3: ");
            basketObject = basketClient.GetBasket();
            Console.WriteLine("Basket: " + basketObject);

            Console.WriteLine("Produce an order: ");
            string orderObject = basketClient.ProduceOrder("CUST123", "Jon Doe", "5th Street Somewhere", "07777777777", (float)20.00, (float)1.50);
            Console.WriteLine("Basket: " + orderObject);

            Console.WriteLine("Clear basket: ");
            returnStatus = basketClient.ClearBasket();
            Console.WriteLine("Return status for clear basket: " + returnStatus);

            Console.WriteLine("Retrieve basket clearing items: ");
            basketObject = basketClient.GetBasket();
            Console.WriteLine("Basket: " + basketObject);

            Console.WriteLine("Delete basket: ");
            returnStatus = basketClient.DeleteBasket();
            Console.WriteLine("Return status for delete basket: " + returnStatus);

            Console.WriteLine("Get basket after delete: ");
            basketObject = basketClient.GetBasket();
            Console.WriteLine("Basket: " + basketObject);

            Console.ReadLine();

        }
    }
}
