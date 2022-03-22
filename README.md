# 5.Monkey_Cache

Benytter [jamesmontemagno/monkey-cache](https://github.com/jamesmontemagno/monkey-cache) library. 

Tilføj NuGet pakken **MonkeyCache.SQLite** til alle Xamarin-projekterne.

Tilføj følgende i App.xaml.cs lige efter `InitializeComponent()`:

`MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";`

I Service-klassen laves forskellige test, som hvis de opfyldes vil returnere data fra Cachen:
- Mangler der en aktiv netværksforbindelse?
- Er tiden for *expired* ikke gået endnu?

Hvis ingen af disse test resulterer i at Cachen læses, bliver Cachen opdateret med friske data.
Her ses et eksempel fra `GetItemsAsync()`:

```csharp
public async Task<IEnumerable<Item>> GetItemsAsync()
{
    UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
    {
        Path = ApiConstants.ItemsEndpoint
    };

    string url = builder.Path;

    if (Connectivity.NetworkAccess == NetworkAccess.None)
    {
        return Barrel.Current.Get<IEnumerable<Item>>(key: url);
    }
    if (!Barrel.Current.IsExpired(key: url))
    {
        return Barrel.Current.Get<IEnumerable<Item>>(key: url);
    }

    var items = await _genericRepository.GetAsync<IEnumerable<Item>>(builder.ToString());
    //Saves the cache and pass it a timespan for expiration
    Barrel.Current.Add(key: url, data: items, expireIn: TimeSpan.FromSeconds(20));
    return items;
}
```

Demo: Sæt breakpoints i ItemService-klassen ved de to betingelser for læsning af Cachen, samt for ny-indlæsning.
Bemærk at Airplane mode kun virker på en fysisk device!

&nbsp;
