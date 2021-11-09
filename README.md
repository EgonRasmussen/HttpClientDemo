# HttpClient Demo

## 2.Refit_Demo

#### WebApi

Et simpelt WebApi, der benytter klassen `Item`.

**BaseUrl:** https:localhost:5001

```
GET: api/items      : liste af items
GET: api/items/3    : item med id=3
POST: api/item      : add af nyt item
PUT: api/item       : update af item
DELETE: api/item/3  : slet item=3
```

#### Client

Klienten består af et Console app. Repository benytter nu [Refit](https://github.com/reactiveui/refit) library.
Det betyder at vi blot skal skrive interfacet med de ønskede Repository metoder, samt parametre:

```csharp
public interface IBackendService
{
    [Get("/api/items")]
    Task<List<Item>> GetItems();

    [Get("/api/items/{id}")]
    Task<Item> GetItemById(string id);

    [Post("/api/items")]
    Task AddItem([Body] Item item);
}
```
Refit-library opretter automatisk en passende Repository-klasser, der matcher interfacet.

Når man ønsker at benytte klassen, er den eneste forskel at man skal oprette et objekt af `RestService` klassen og give dens .For-metode det ønskede interface med som parameter.
Resten er helt som med et manuelt oprettet repository:

```csharp
class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        // Her oprettes et objekt af IBackendService-typen
        var repos = RestService.For<IBackendService>("https://localhost:5001");

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
``` 

&nbsp;


