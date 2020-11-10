using System;

namespace ConsoleHttpClientDemo
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            SimpleRepository repos = new SimpleRepository();

            Item newItem = new Item { Text = "New Item", Description = "My New Item" };
            await repos.AddItem(newItem);


            var items = await repos.GetItems();
            foreach (Item item in items)
            {
                Console.WriteLine($"Text: {item.Text} - Description: {item.Description}");
            }

            Item singleItem = await repos.GetItemById(items[5].Id);
            Console.WriteLine($"\nSingle Item: {singleItem.Text}");

            Console.ReadLine();
        }
    }  
}
